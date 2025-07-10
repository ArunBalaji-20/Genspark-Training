import { Injectable } from '@angular/core';

@Injectable()
export class PaymentService {
  payWithRazorpay(options: any) {
    
    const rzp = new (window as any).Razorpay(options);
    rzp.open();
    rzp.on('payment.failed', (response: any) => {
      alert('Payment failed. Please try again.');
    });

    
  }
}