import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { IShippment } from '../Models/ishippment';
import { Observable } from 'rxjs';
import { IOrder } from '../Models/i-order';

@Injectable({
  providedIn: 'root'
})
export class ApiShippmentService {
  private apiUrl = environment.apiUrl;
  //private url="http://localhost:64866/api/Order"
  constructor(private _httpClient:HttpClient) { }
 
  AddClientAddress(shippment:IShippment): Observable<any> {
    return this._httpClient.post<any>(`${this.apiUrl}/Shippment`,  shippment );
}



UpdateClientAddress(shippment:IShippment): Observable<any> {
  console.log(shippment)
  let id=shippment.id
  return this._httpClient.put<any>(`${this.apiUrl}/Shippment/${id}`,  shippment );
}
 
Getshippment(UserId:number):Observable<any>{
  return this._httpClient.get<any>(`${this.apiUrl}/Shippment/${UserId}`)
}
}
