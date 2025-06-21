import { Component } from '@angular/core';
import { authService } from '../../core/Services/authService';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginModel } from '../../core/Models/LoginModel';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  user:LoginModel= new LoginModel();
  LoginForm: FormGroup;

  constructor(private authService:authService,private router:Router)
  {
   this.LoginForm = new FormGroup(
  {
    email: new FormControl(null, Validators.email),
    password: new FormControl(null, [Validators.required]),
  },
);

// passwordValidator()

  }

public get email() : any {
  return this.LoginForm.get("email")
}
public get password() : any {
  return this.LoginForm.get("password")
}



statusMessage: string = '';
role:any;

handleFormsSubmission() {
  this.user = this.LoginForm.value;
  console.log(this.user)
  if (this.LoginForm.invalid) {
    this.statusMessage = "Invalid Inputs";
    return;
  }

  this.authService.loginAPI(this.user.email, this.user.password).subscribe({
    next: (res) => {
      if (res.status === 200 && res.body?.accessToken && res.body?.refreshToken) {
        this.authService.setAuthToken(res.body.accessToken,res.body.refreshToken);
        this.statusMessage = "Logged in";
        this.role=this.authService.role();
        console.log(`role from login.ts:${this.role}`)
        this.LoginForm.reset();

         if (this.role === 'Admin') {
            this.router.navigate(['/admin/dashboard']);
          } else if (this.role === 'Tester') {
            this.router.navigate(['/tester/report']);
          } else if (this.role === 'Dev') {
            this.router.navigate(['/developer/myBugs']);
          } else {
            this.router.navigate(['/']);
          }
      }
    },
    error: (err) => {
      console.log(err)
      if (err.status === 400 || err.status==401) {
        this.statusMessage = "Invalid Credentials";
      } else {
        this.statusMessage = "An error occurred";
      }
    },
    complete:()=>{
      console.log("login api completed")
    }
  });
}

// ...existing code...

// handleFormsSubmission() {
//   this.user = this.LoginForm.value;
//   if (this.LoginForm.invalid) {
//     this.statusMessage = "Form is invalid";
//     return;
//   }

//   this.aurhService.loginAPI(this.user.email, this.user.password)
//     .then(() => {
//       this.statusMessage = "Logged in";
//       this.LoginForm.reset();
//     })
//     .catch((err) => {
//       if (err.status === 400 || err.status==401) {
//         this.statusMessage = "Invalid credentials";
//       } else {
//         this.statusMessage = "An error occurred";
//       }
//     });
// }
}
