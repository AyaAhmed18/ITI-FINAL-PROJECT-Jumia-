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
  private url="http://localhost:64866/api/Order"
  constructor(private _httpClient:HttpClient) { }

  AddClientAddress(shippment:IShippment): Observable<any> {
    return this._httpClient.post<any>(`${this.apiUrl}/Shippment`, { shippment });
}

AddOrder(order:IOrder): Observable<any> {
 return this._httpClient.post<any>(this.url, { order });

}

UpdateClientAddress(shippment:IShippment): Observable<any> {
  return this._httpClient.post<any>(`${this.apiUrl}/Shippment`, { shippment });
}


}
