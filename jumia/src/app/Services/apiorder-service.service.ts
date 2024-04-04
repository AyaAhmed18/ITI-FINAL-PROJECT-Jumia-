import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit, inject } from '@angular/core';
import { IOrder } from '../Models/i-order';
import { environment } from '../../environment/environment';
import { Observable } from 'rxjs';
import { IOrderItems } from '../Models/iorder-items';

@Injectable({
  providedIn: 'root'
})
export class APIOrderServiceService {
  Orders:any=[]
  private apiUrl = environment.apiUrl;
  constructor(private _httpClient:HttpClient ) { }
  
  AddOrder(order:IOrder): Observable<any> {
    return this._httpClient.post<any>(`${this.apiUrl}/Order`, { order });////
   
   }

   GetUserOrders(UserId:number):Observable<IOrder[]>{
    return this._httpClient.get<IOrder[]>(`${this.apiUrl}/Order/${UserId}`)
  }

  GetOrderItems(OrdId:number):Observable<IOrderItems[]>{
    return this._httpClient.get<IOrderItems[]>(`${this.apiUrl}/OrderItems/${OrdId}`)
  }
}
