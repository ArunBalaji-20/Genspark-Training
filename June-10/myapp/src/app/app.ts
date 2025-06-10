// import { Component } from '@angular/core';
// import { RouterOutlet } from '@angular/router';

// @Component({
//   selector: 'app-root',
//   imports: [RouterOutlet],
//   templateUrl: './app.html',
//   styleUrl: './app.css'
// })
// export class App {
//   protected title = 'myapp';
// }

import { Component } from '@angular/core';
import { First } from "./first/first";
import { CustomerDetails } from "./customer-details/customer-details";
import { Products } from "./products/products";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [First, CustomerDetails, Products]
})
export class App {
  protected title = 'myApp';
}
