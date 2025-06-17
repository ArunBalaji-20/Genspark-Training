import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductModel } from '../Models/ProductModel';
import { ProductService } from '../Services/ProductService';

@Component({
  selector: 'app-single-product',
  imports: [],
  templateUrl: './single-product.html',
  styleUrl: './single-product.css'
})
export class SingleProduct {

  productId: number = 0;
  product: ProductModel | null = null;
  router = inject(ActivatedRoute)

  constructor(private productService: ProductService)
  {
    this.productId = this.router.snapshot.params["n"] as number;
    console.log(this.productId);

    this.productService.getSingleProduct(this.productId).subscribe((result: ProductModel) => {
      this.product = result;
    });

  }

  
}
