import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { authService } from '../../core/Services/authService';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule,RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {

  isLoggedIn:Boolean=false;
  role:any;
  bellClassName:string="bi bi-bell";
    constructor(public authService:authService)
    {
      this.isLoggedIn=authService.isLoggedIn();
      this.role=authService.role();
      console.log(`role from nav:${this.role}`)
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
}
