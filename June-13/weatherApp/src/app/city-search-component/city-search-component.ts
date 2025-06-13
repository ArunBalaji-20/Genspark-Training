import { Component, OnInit, signal } from '@angular/core';
import { WeatherService } from '../Services/WeatherService';
import { WeatherModel } from '../Models/WeatherModel';
import { FormsModule } from '@angular/forms';
import { WeatherCardComponent } from "../weather-card-component/weather-card-component";

@Component({
  selector: 'app-city-search-component',
  imports: [FormsModule, WeatherCardComponent],
  templateUrl: './city-search-component.html',
  styleUrl: './city-search-component.css'
})
export class CitySearchComponent  {

  weatherData = signal<WeatherModel[]>([]);
  cityname:string="";
  constructor(private weatherService:WeatherService)
  {

  }

  // search() {
  //   this.weatherService.getWeatherData(this.cityname).subscribe({
  //     next: (data: any) => {
  //     // If data is a single object, wrap it in an array
  //     if (Array.isArray(data)) {
  //       this.weatherData.set(data as WeatherModel[]);
  //     } else if (data) {
  //       this.weatherData.set([data] as WeatherModel[]);
  //     } else {
  //       this.weatherData.set([]);
  //     }
  //   },
  //     error:(e)=>{
  //       console.error(e);
  //     },
  //     complete:()=>{
  //       console.log("completed fetching");
  //     }
  //   });'
  search() {
  this.weatherService.getWeatherData(this.cityname).subscribe({
    next: (data: any) => {
      if (Array.isArray(data)) {
        this.weatherData.set(data.map(WeatherModel.fromApiResponse));
      } else if (data) {
        this.weatherData.set([WeatherModel.fromApiResponse(data)]);
      } else {
        this.weatherData.set([]);
      }
    },
    error: (e) => {
      console.error(e);
      this.weatherData.set([]);
    },
    complete: () => {
      console.log("completed fetching");
    }
  });


  }

}




