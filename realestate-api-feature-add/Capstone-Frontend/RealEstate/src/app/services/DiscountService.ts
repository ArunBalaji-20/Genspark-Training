import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Buyer, UpdateBuyerDto } from '../models/buyer.model';

@Injectable({ providedIn: 'root' })
export class DiscountService {
  private baseUrl = `${environment.apiUrl}/DiscountCode`;

  constructor(private http: HttpClient) {}

  getCodes(query: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Get`, { params: query });
  }

  CreateCode(query:any): Observable<any>{

    return this.http.post<any>(`${this.baseUrl}/Add`,query)

  }

 
  checkCode(code: string): Observable<any> {
  return this.http.get<any>(`${this.baseUrl}/Check`, {
    params: { code }
  });
}

  DeleteCode(id:string):Observable<any>{
    return this.http.delete<any>(`${this.baseUrl}/Delete`,{
      params:{id}
    });
}

PatchExpiryStatus(id:string):Observable<any>{
    return this.http.patch<any>(`${this.baseUrl}/Expire?id=${id}`,{})
     ;
}
  
}
