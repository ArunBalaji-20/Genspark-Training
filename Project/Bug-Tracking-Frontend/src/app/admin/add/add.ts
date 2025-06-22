import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { empManageService } from '../../core/Services/empManageService';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add',
  imports: [FormsModule,ReactiveFormsModule,CommonModule],
  templateUrl: './add.html',
  styleUrl: './add.css'
})
export class Add {
   employeeForm: FormGroup;
  successMessage = false;
  errorMessage = false;
  validationErrors: string = '';


  constructor( private empservice: empManageService) {
    this.employeeForm = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      email:new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null,[ Validators.required]),
      role: new FormControl(null,[ Validators.required])
    });
  }

  handleEmployeeSubmission() {
    if (this.employeeForm.invalid) {
      console.log('Invalid Form');
      return;
    }

    const employeeData = this.employeeForm.value;

    this.empservice.addEmployeeAPI(employeeData).subscribe({
      next: (res) => {
        console.log('Employee added', res);
        this.successMessage = true;
        this.errorMessage = false;
        this.employeeForm.reset();
      },
      error: (err) => {
        console.error('Error adding employee', err);
        this.successMessage = false;
        this.errorMessage = true;

        if (err.status === 400 && err.error ) {
        this.validationErrors = err.error;  // <<-- Set the Name validation error
      } else {
        this.validationErrors = 'An unexpected error occurred!';
      }
      }
    });
  }
}
