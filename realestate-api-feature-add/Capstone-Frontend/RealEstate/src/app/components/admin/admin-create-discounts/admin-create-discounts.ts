import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DiscountService } from '../../../services/DiscountService';
import { futureDateValidator } from '../../../CustomValidators/futureDateValidator';

@Component({
  selector: 'app-admin-create-discounts',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './admin-create-discounts.html',
  styleUrl: './admin-create-discounts.css'
})
export class AdminCreateDiscounts {
  DiscountForm:FormGroup;


 isSubmitting=false;
  errorMessage: string | null = null;
  successMessage: string | null = null;
  selectedFiles:File[]=[];
  previewUrls: string[] = [];
  fileInputRef: HTMLInputElement | null = null;
  

  constructor(private fb:FormBuilder,public router:Router,private discountService:DiscountService)
  {
     this.DiscountForm=this.fb.group({
       Code: ['', [Validators.required, Validators.minLength(5)]],
       Remaining:['',[Validators.required]],
       DiscountPercentage:['',[Validators.required]],
       ExpiryDate:['',[Validators.required,futureDateValidator]]
     })
   
  }
  
  submit()
  {
    if(this.DiscountForm.invalid)
    {
      console.log("form invalid");
      this.errorMessage="Please Fill All Required Details"
      return
    }

    const formValue = this.DiscountForm.value ;
    console.log('formvalue:', formValue); 

     if (formValue.ExpiryDate) {
    formValue.ExpiryDate = new Date(formValue.ExpiryDate + 'T00:00:00Z');
  }

    console.log('Adjusted form value:', formValue);

    this.isSubmitting=true;
    this.discountService.CreateCode(formValue).subscribe({
      next:(res)=>{
        console.log(res);
        this.errorMessage="";
        this.successMessage="Code Created"
      },
      error:(err)=>{
        console.log(err);
        this.isSubmitting = false;
        if(err.error=="Code already exist")
        {
          this.errorMessage=err.error;
          
        }
        else{
        this.errorMessage="Error Occured.."

        }
      },
      complete:()=>{
        console.log("api completed")
        this.isSubmitting = false;
      }

    })
  }

   get f() {
    return this.DiscountForm.controls;
  }
}
