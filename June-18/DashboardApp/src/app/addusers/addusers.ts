import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserAddModel } from '../Models/UserAddModel';
import { UserService } from '../Services/UserService';

@Component({
  selector: 'app-addusers',
  imports: [FormsModule],
  templateUrl: './addusers.html',
  styleUrl: './addusers.css'
})
export class Addusers {

  user:UserAddModel=new UserAddModel();
  isAdded:Boolean=false;

  constructor(private userService:UserService)
  {}

handleSubmisson() {
 this.userService.AddUsers(this.user).subscribe({
  next: (data: any) => {
    this.isAdded = true;
    console.log("handlesubmission called");
  },
  error: (error: any) => {
    console.log("error,User not added");
  }
  });
}

}
