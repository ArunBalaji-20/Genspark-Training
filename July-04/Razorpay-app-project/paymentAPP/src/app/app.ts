import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Checkout } from "./checkout/checkout";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Checkout],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'paymentAPP';
}
