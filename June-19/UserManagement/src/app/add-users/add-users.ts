import { Component } from '@angular/core';
import { UserModel } from '../Models/UserModel';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../Services/UserService';
import { passwordValidator } from '../Misc/passwordValidator';
import { passwordMatchValidator } from '../Misc/passwordMatchValidatior';
import { RoleValidator } from '../Misc/RoleValidator';

@Component({
  selector: 'app-add-users',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './add-users.html',
  styleUrl: './add-users.css'
})
export class AddUsers {

  user:UserModel= new UserModel();
  AddUsersForm: FormGroup;

  constructor(private userService:UserService)
  {
    // console.log("from constructor")
   this.AddUsersForm = new FormGroup(
  {
    email: new FormControl(null, Validators.email),
    pass: new FormControl(null, [Validators.required, passwordValidator()]),
    confirmPass: new FormControl(null, [Validators.required]),
    role: new FormControl(null, [Validators.required,RoleValidator(['admin', 'superadmin'])])
  },
  { validators: passwordMatchValidator }
);

  }

public get email() : any {
  return this.AddUsersForm.get("email")
}
public get pass() : any {
  return this.AddUsersForm.get("pass")
}

public get confirmPass() : any {
  return this.AddUsersForm.get("confirmPass")
}

public get role() : any {
  return this.AddUsersForm.get("role")
}

  handleFormsSubmission()
  {
    this.user = this.AddUsersForm.value;
    console.log(this.user);

    this.userService.addUser(this.user);
    this.AddUsersForm.reset();
    // console.log(this.user)

  }
}
