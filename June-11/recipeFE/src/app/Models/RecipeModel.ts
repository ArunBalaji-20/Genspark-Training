
export class RecipeModel{

    constructor(public id:number=0,public name:string="",public image:string="",public cuisine:string="",public cookTimeMinutes:number=0,public ingredients:string[]=[])
    {

    }
}

// Recipe Name
// Cuisine
// Cooking Time (in minutes)
// Ingredients (as comma-separated text)