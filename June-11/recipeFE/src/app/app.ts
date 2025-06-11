import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Allrecipes } from "./allrecipes/allrecipes";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Allrecipes],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'recipeFE';
}
