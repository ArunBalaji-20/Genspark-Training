import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";


@Injectable()
export class WeatherService{
     private http=inject(HttpClient);
    
    getWeatherData(city:string):Observable<any[]>
    {
        return this.http.get<any[]>(`https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=fb63e95f46e3bef4a1a00dd64cfc41d5`)
    }
}


