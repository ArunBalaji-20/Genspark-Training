import { Component, Input } from '@angular/core';
import { PropertyListingResponseDto } from '../../../models/property-listing.model';
import { NgbActiveModal, NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PurchaseService } from '../../../services/purchase.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DiscountService } from '../../../services/DiscountService';

@Component({
  selector: 'app-checkout-modal',
  imports: [NgbModule,FormsModule,CommonModule],
  templateUrl: './checkout-modal.html',
  styleUrl: './checkout-modal.css'
})
export class CheckoutModal {
  @Input() property!: PropertyListingResponseDto;
  isLoading = false;
  paymentMethod = 'bank_transfer';
  agreedToTerms = false;
   discountValid: boolean | null = null;
     discountCode: string = '';
  discountDetails: any = null;
 finalPrice:number=0;
  constructor(
    public activeModal: NgbActiveModal,
    private purchaseService: PurchaseService,
    private router: Router,
    private discountService:DiscountService
  ) {
    
  }




checkDiscountCode(code:string ) {
  if (!this.discountCode?.trim()) {
    this.discountValid = null;
    this.discountDetails = null;
    return;
  }

  this.discountService.checkCode(code).subscribe({
    next: (res) => {
      this.discountValid = true;
      this.discountDetails = res; // Save details like discount percentage etc.
      console.log('Code valid:', res);
      this.SetFinalPrice(code);
    },
    error: (err) => {
      this.discountValid = false;
      this.discountDetails = null;
      console.warn('Invalid code:', err);
    }
  });
}

SetFinalPrice(code:string)
{
  this.discountCode=code;
  this.purchaseService.getFinalPrice(this.property.id,code).subscribe({
    next:(res)=>{
      console.log(res)
      this.finalPrice=res;
      console.log(this.finalPrice)
    },
    error:(err)=>{
      console.log(err)
    },
    complete:()=>{
      console.log('completed setting finalprice')
    }
  })
}

  confirmPurchasev2(): void { //new function with discount feature
    if (!this.agreedToTerms) {
      alert('Please agree to the terms and conditions');
      return;
    }

    this.isLoading = true;
    this.purchaseService.buyPropertyV2(this.property.id,this.discountCode).subscribe({
      next: (res) => {
        console.log(res)
        this.activeModal.close('success');
        this.router.navigate(['/buyer/my-purchases']);
      },
      error: (err) => {
        this.isLoading = false;
        alert(err.error?.error || 'Failed to complete purchase');
      }
    });
  }

  
  confirmPurchase(): void {
    if (!this.agreedToTerms) {
      alert('Please agree to the terms and conditions');
      return;
    }

    this.isLoading = true;
    this.purchaseService.buyProperty(this.property.id).subscribe({
      next: () => {
        this.activeModal.close('success');
        this.router.navigate(['/buyer/my-purchases']);
      },
      error: (err) => {
        this.isLoading = false;
        alert(err.error?.error || 'Failed to complete purchase');
      }
    });
  }

  formatPrice(price: number): string {
    if (price >= 10000000) {
      return `₹${(price / 10000000).toFixed(1)} Cr`;
    } else if (price >= 100000) {
      return `₹${(price / 100000).toFixed(1)} L`;
    }
    return `₹${price.toLocaleString('en-IN')}`;
  }
}
