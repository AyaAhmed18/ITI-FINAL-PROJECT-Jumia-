import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit, inject } from '@angular/core';
import { IOrder } from '../Models/i-order';
import { environment } from '../../environment/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class APIOrderServiceService {
  Orders:any=[]
  private apiUrl = environment.apiUrl;
  constructor(private _httpClient:HttpClient ) { }
  
  AddOrder(order:IOrder): Observable<any> {
    return this._httpClient.post<any>(`${this.apiUrl}/Order`, { order });
   
   }

   GetUserOrders(UserId:number):Observable<IOrder[]>{

    return this._httpClient.get<IOrder[]>(`${this.apiUrl}/Order/${UserId}`)
  
  }
}
