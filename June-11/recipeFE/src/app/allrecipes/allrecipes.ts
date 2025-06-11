// import { Component, OnInit } from '@angular/core';
// import { Recipe } from "../recipe/recipe";
// import { RecipeModel } from '../Models/RecipeModel';
// import { RecipeService } from '../Services/RecipeService';

// @Component({
//   selector: 'app-allrecipes',
//   imports: [Recipe],
//   templateUrl: './allrecipes.html',
//   styleUrl: './allrecipes.css'
// })
// export class Allrecipes implements OnInit {

//   recipes:RecipeModel[] | undefined=undefined;

//   constructor(private RecipeService:RecipeService){

//   }

//   ngOnInit(): void {
//     this.RecipeService.getAllRecipes().subscribe(
//       {
//       next:(data:any)=>{
//         this.recipes=data.recipes as RecipeModel[];
//       },
//       error:(e)=>{console.log(e)},
//       complete:()=>{console.log("completed fetching")}

//       }
//     )
//   }
// }

import { Component, OnInit, signal, effect } from '@angular/core';
import { Recipe } from "../recipe/recipe";
import { RecipeModel } from '../Models/RecipeModel';
import { RecipeService } from '../Services/RecipeService';

@Component({
  selector: 'app-allrecipes',
  standalone: true,
  imports: [Recipe],
  templateUrl: './allrecipes.html',
  styleUrls: ['./allrecipes.css'] 
})
export class Allrecipes implements OnInit {

  recipes = signal<RecipeModel[]>([]);

  constructor(private recipeService: RecipeService) {}

  ngOnInit(): void {
    this.recipeService.getAllRecipes().subscribe({
      next: (data: any) => {
        this.recipes.set(data.recipes as RecipeModel[]);
      },
      error: (e) => {
        console.error(e);
      },
      complete: () => {
        console.log("completed fetching");
      }
    });

    
  }
}

