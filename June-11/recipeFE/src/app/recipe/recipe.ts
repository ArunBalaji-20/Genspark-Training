import { Component, inject, Input, input } from '@angular/core';
import { RecipeModel } from '../Models/RecipeModel';
import { RecipeService } from '../Services/RecipeService';

@Component({
  selector: 'app-recipe',
  imports: [],
  templateUrl: './recipe.html',
  styleUrl: './recipe.css'
})
export class Recipe {
  @Input() recipe:RecipeModel|null = new RecipeModel();
  private recipeService=inject(RecipeService);

  constructor()
  {

  }

}


