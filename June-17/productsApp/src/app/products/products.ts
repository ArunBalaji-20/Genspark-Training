import { Component, HostListener, OnInit } from '@angular/core';
import { ProductModel } from '../Models/ProductModel';
import { ProductService } from '../Services/ProductService';
import { Product } from "../product/product";
import { debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-products',
  imports: [Product,FormsModule,RouterOutlet],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products implements OnInit {

  products:ProductModel[]=[];

  searchString:string="";
  searchSubject = new Subject<string>();

  loading:Boolean=false;
  total:number=0;
  skip:number=0;
  limit:number=10;

  constructor(private productService:ProductService,public route:ActivatedRoute )
  {

  }

 
  handleSearchProducts(){
    console.log(this.searchString)
    this.searchSubject.next(this.searchString);
  }

  ngOnInit(): void {

    // this.productService.getAllProducts().subscribe({
    //   next: (data: any) => {
    //     this.products = data.products as ProductModel[];
    //   },
    //   error: () => {},
    //   complete: () => {}
    // });


    

      this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      tap(()=>this.loading=true),
      switchMap(query=>this.productService.getProductSearchResult(query)),
       tap(()=>this.loading=false)).subscribe({
        next:(data:any)=>{this.products = data.products as ProductModel[];
          this.total = data.total;
          // console.log(this.total)
        }
      });
  }



@HostListener('window:scroll',[])
  onScroll():void
  {

    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight-100;
    if(scrollPosition>=threshold && this.products?.length<this.total)
    {
      // console.log(scrollPosition);
      // console.log(threshold)
      
      this.loadMore();
    }
  }
  loadMore(){
    this.loading = true;
    this.skip += this.limit;
    this.productService.getProductSearchResult(this.searchString,this.limit,this.skip)
        .subscribe({
          next:(data:any)=>{
            // [...this.products,...data.products]
            this.products = [...this.products, ...data.products];
            this.skip=this.products.length;
            this.loading = false;

          }
        })
  }
}




