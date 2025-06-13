import { Component, Input, SimpleChanges } from '@angular/core';
import { WeatherModel } from '../Models/WeatherModel';

@Component({
  selector: 'app-weather-card-component',
  imports: [],
  templateUrl: './weather-card-component.html',
  styleUrl: './weather-card-component.css'
})
export class WeatherCardComponent {

  @Input() weatherData: WeatherModel[] = [];
  

  constructor(){
    console.log(this.weatherData)
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes['weatherData']) {
      console.log('weatherData:', this.weatherData);
    }
  }
}

