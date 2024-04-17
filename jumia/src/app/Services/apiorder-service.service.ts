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
    return this._httpClient.post<any>(`${this.apiUrl}/Order`,  order );////
   
   }

  UpdateOrder(order:IOrder): Observable<any> {
   let id=order.id
   console.log(id);
   
    return this._httpClient.put<IOrder>(`${this.apiUrl}/Order/${id}`,  order );////
   
   }

   AddOrderItems(orderItem:IOrderItems): Observable<any> {
    return this._httpClient.post<any>(`${this.apiUrl}/OrderItems`,  orderItem );////
   
   }
//Get
   GetUserOrders(UserId:number):Observable<IOrder[]>{
    return this._httpClient.get<IOrder[]>(`${this.apiUrl}/Order/${UserId}`)
  }
  GetOrder(Id:number):Observable<IOrder>{
    return this._httpClient.get<IOrder>(`${this.apiUrl}/Order/user/${Id}`)
  }

  GetOrderItems(OrdId:number):Observable<IOrderItems[]>{
    return this._httpClient.get<IOrderItems[]>(`${this.apiUrl}/OrderItems/${OrdId}`)
    
  }
}
