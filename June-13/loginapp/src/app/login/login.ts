import { Component, inject } from '@angular/core';
import { AuthService } from '../Services/AuthService';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {
  email = '';
  password = '';
  error = '';

  private authService = inject(AuthService);
  constructor() {}

  login() {
    const user = this.authService.validateUser(this.email, this.password);
    if (user) {
      // Serialize and store the user in session storage
      // sessionStorage.setItem('loggedInUser', JSON.stringify(user));
      localStorage.setItem('LoggedInUser',JSON.stringify(user))
      console.log("Logged In...")
    } else {
      this.error = 'Invalid credentials!';
      console.log("Invalid creds")
    }
  }
}


