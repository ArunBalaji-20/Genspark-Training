import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WeatherDashboardComponent } from "./weather-dashboard-component/weather-dashboard-component";
import { CitySearchComponent } from "./city-search-component/city-search-component";
import { WeatherCardComponent } from "./weather-card-component/weather-card-component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, WeatherDashboardComponent, CitySearchComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'weatherApp';
}
