import { Component, Input } from '@angular/core';
import { ProductModel } from '../Models/ProductModel';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {

    @Input() productinp!: ProductModel;
    @Input() searchTerm:string="";

    constructor(private router:Router,private route: ActivatedRoute){
      
    }

    handleProductClick( id:number=0)
    {
      console.log(`id is ${id}`)
      this.router.navigateByUrl("/home/product/"+id)
      // this.router.navigate(['/home/product', id]);
      console.log("redirected to product/n")
    }

}
