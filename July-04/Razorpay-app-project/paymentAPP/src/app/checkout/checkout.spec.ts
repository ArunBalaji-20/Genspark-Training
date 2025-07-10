import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Checkout } from './checkout';
import { PaymentService } from '../Services/paymentService';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

describe('Checkout', () => {
  let component: Checkout;
  let fixture: ComponentFixture<Checkout>;
  let paymentServiceSpy: jasmine.SpyObj<PaymentService>;

  beforeEach(async () => {
    paymentServiceSpy = jasmine.createSpyObj('PaymentService', ['payWithRazorpay']);

    await TestBed.configureTestingModule({
      imports: [Checkout],
      providers: [
        { provide: PaymentService, useValue: paymentServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Checkout);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call paymentService with correct options if form is valid', () => {
    component.checkoutForm.setValue({
      amount: 100,
      customerName: 'Test User',
      email: 'test@example.com',
      contactNumber: '1234567890'
    });

    component.onSubmit();

    expect(paymentServiceSpy.payWithRazorpay).toHaveBeenCalled();
    const options = paymentServiceSpy.payWithRazorpay.calls.mostRecent().args[0];
    expect(options.amount).toBe(10000); 
    expect(options.prefill.name).toBe('Test User');
    expect(options.prefill.email).toBe('test@example.com');
    expect(options.prefill.contact).toBe('1234567890');
  });

  it('should set statusmessage on successful payment', () => {
    component.checkoutForm.setValue({
      amount: 100,
      customerName: 'Test User',
      email: 'test@example.com',
      contactNumber: '1234567890'
    });

    component.onSubmit();

    const options = paymentServiceSpy.payWithRazorpay.calls.mostRecent().args[0];
    const dummyPaymentResponse = { razorpay_payment_id: 'pay_test' };

    options.handler(dummyPaymentResponse);

    expect(component.statusmessage).toBe('Payment success: pay_test');
    
    expect(component.errormessage).toBe('');
  });

  it('should not call paymentService if form is invalid', () => {
  component.checkoutForm.setValue({
    amount: null,
    customerName: '',
    email: 'invalid-email',
    contactNumber: ''
  });

  component.onSubmit();

  expect(paymentServiceSpy.payWithRazorpay).not.toHaveBeenCalled();
  expect(component.errormessage).toBe('');
});

});
