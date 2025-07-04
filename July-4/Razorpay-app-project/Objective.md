Objective
Develop an Angular web application to simulate and test UPI payments using Razorpay’s Test UPI Details.

You will:
Build a clean, responsive form.
Integrate Razorpay Checkout in test mode.
Write unit tests using Jasmine and Karma.
Dockerize and run the application.
 
Assignment Details
 
Application Features
Angular Frontend
Create a new Angular project (Angular 16+ recommended).
Implement a Payment Form with the following fields:
Amount (required, must be >0)
Customer Name (required)
Email (required, valid email)
Contact Number (required, 10 digits)
Razorpay Integration
On form submission:
Call your backend endpoint to create a test payment order (or hardcode a dummy order ID if you don’t want to build a backend).
Use Razorpay Checkout script to launch the payment modal.
Pre-fill UPI IDs like success@razorpay for simulating successful payment.
Payment Result Display
After payment attempt:
Show Success Message with payment ID if completed.
Show Failure or Cancelled Message if not completed.
 
Testing
Write unit tests using Jasmine + Karma:
Test form validation logic:
Required fields.
Invalid inputs.
Test component rendering.
Mock the Razorpay Checkout script:
Ensure your payment initiation method is called.
 
Dockerization
Dockerize the Angular app:
Create a Dockerfile to:
Build the Angular app using ng build --prod.
Serve the built files via Nginx.
Provide Docker commands in README.md:
Build image.
Run container.
Access the app in the browser.
 
Deliverables
Full Angular source code (clean structure).
Unit test reports or screenshots (showing all tests passing).
Dockerfile and run instructions.
Screenshots or video demo of:
Payment initiation.
Success flow.
Failure/cancel flow.


Bonus (Optional)
⭐ Add payment history (store locally in a service).

⭐ Add basic styling with Angular Material.

⭐ Show a spinner/loading indicator during payment processing.
Test UPI ID Details to Test Payment Flow
Use test UPI IDs to test domestic (Indian) one-time payments.
 