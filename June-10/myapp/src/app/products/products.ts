import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-products',
  imports: [CommonModule,FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {

  cartCount:number;
  CartClassName:string="bi bi-cart";

  constructor()
  {
    this.cartCount=0;
  }

  AddToCart()
  {
    this.cartCount+=1;
    this.toggleCartClass()
  }

  toggleCartClass()
  {
    if(this.cartCount==0){
      this.CartClassName="bi bi-cart";
    }
    else{
      this.CartClassName="bi bi-cart-fill";
    }
  }
}
