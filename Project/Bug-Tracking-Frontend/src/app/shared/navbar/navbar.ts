import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { authService } from '../../core/Services/authService';
import { RouterLink } from '@angular/router';
import { NotificationService } from '../../core/Services/notificationService';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule,RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar implements OnInit {

  isLoggedIn:Boolean=false;
  role:any;
  bellClassName:string="bi bi-bell";
    notifications: string[] = [];
    constructor(public authService:authService,public notificationService:NotificationService)
    {
      this.isLoggedIn=authService.isLoggedIn();
      this.role=authService.role();
      console.log(`role from nav:${this.role}`)
    }
  ngOnInit(): void {
    this.notificationService.notifications$.subscribe(n => {
      this.notifications = n;
      this.bellClassName = n.length ? 'bi bi-bell-fill text-danger' : 'bi bi-bell';
      console.log(this.notifications)
    });
  }
  handleLogout()
  {
    this.authService.logoutAPI().subscribe({
      next:(res)=>{
        console.log(res);
        if(res.status==200)
        {
          console.log("logged out and deleting cookie")
           this.authService.logout();

        }
        else
        {
          console.log(res.status)
        }
        
      },
      error:(err)=>{
        console.log(err)
      },
      complete:()=>{
        console.log("finished logout")
      }
    })
  }

  handleBell()
  {
    if(this.bellClassName=="bi bi-bell-fill")
    {
      this.bellClassName="bi bi-bell";
    }
    else{
      this.bellClassName='bi bi-bell-fill';

    }
  }
  dismissNotification(index: number): void {
  this.notifications.splice(index, 1);
  this.notificationService.updateNotifications(this.notifications);
}

}
