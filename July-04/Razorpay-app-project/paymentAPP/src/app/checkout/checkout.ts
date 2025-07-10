// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-checkout',
//   imports: [],
//   templateUrl: './checkout.html',
//   styleUrl: './checkout.css'
// })
// export class Checkout {

// }
import { CommonModule } from '@angular/common';
import { Component, NgZone } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PaymentService } from '../Services/paymentService';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.html',
  styleUrls: ['./checkout.css'],
  imports:[ReactiveFormsModule,FormsModule,CommonModule]
})
export class Checkout {
  checkoutForm: FormGroup;
  statusmessage:string='';
  errormessage:string='';

  constructor(private fb: FormBuilder,private paymentService:PaymentService,private ngzone:NgZone) {
    this.checkoutForm = this.fb.group({
      amount: ['', [Validators.required, Validators.min(1)]],
      customerName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      contactNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]]
    });
  }

  onSubmit() {
    if (this.checkoutForm.invalid) {
      this.checkoutForm.markAllAsTouched();
      return;
    }

    const form = this.checkoutForm.value;
    const dummyOrderId='order_12345'

    const options: any = {
      key: '', //add key here
      amount: form.amount * 100, 
      currency: 'INR',
      name: form.customerName,
      description: 'Test Transaction',
      // order_id: dummyOrderId,
      handler: (response: any) => {
        console.log(response)
        alert('Payment successful! Payment ID: ' + response.razorpay_payment_id);  
        this.ngzone.run(() => {
        if (response.razorpay_payment_id) {
          // this.statusmessage = `Payment success :${response.razorpay_payment_id}`;
          this.statusmessage = 'Payment success: ' + response.razorpay_payment_id;

          this.errormessage = '';
          // this.checkoutForm.reset();
        } else {
          this.errormessage = 'Payment failure';
          this.statusmessage = '';
        }
      });
      },
        modal: {
    ondismiss: () => {
      this.ngzone.run(() => {
        this.errormessage = 'Payment cancelled by user';
        this.statusmessage = '';
      });
    }
  },
      prefill: {
        name: form.customerName,
        email: form.email,
        contact: form.contactNumber
      
      },
      theme: {
        color: '#3399cc'
      }
    };

    this.paymentService.payWithRazorpay(options);
  }
}

// paymentid=pay_QosHzZIujOKxcH