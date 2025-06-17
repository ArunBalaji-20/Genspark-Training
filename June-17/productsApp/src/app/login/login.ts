import { Component } from '@angular/core';
import { UserLoginModel } from '../Models/UserLoginModel';
import { Router } from '@angular/router';
import { UserService } from '../Services/UserService';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  user:UserLoginModel=new UserLoginModel();

  constructor(private userService:UserService,private route:Router)
  {}
  handleLogin(){
  this.userService.validateUserLogin(this.user);
  this.route.navigateByUrl("/home");
}


}
