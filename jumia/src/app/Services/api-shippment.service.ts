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
    return this._httpClient.post<any>(`${this.apiUrl}/Shippment`, { shippment });
}

AddOrder(order:IOrder): Observable<any> {
 return this._httpClient.post<any>(`${this.apiUrl}/Order`, { order });

}

UpdateClientAddress(shippment:IShippment): Observable<any> {
  return this._httpClient.post<any>(`${this.apiUrl}/Shippment`, { shippment });
}
 //   const userDetails = {
  //     FirstNameEn: shippment.FirstNameEn,
  //     LastName: shippment.LastName,
  //     PhoneNumber: shippment.PhoneNumber,
  //     Address: shippment.Address,
  //     AdressInformation: shippment.AdressInformation,
  //     City: shippment.City,
  //     CityAr: shippment.CityAr,
  //     Region: shippment.Region,
  //     RegionAr: shippment.RegionAr,
  //     UserIdentityId: shippment.UserIdentityId,
  //     Cost: shippment.Cost,

  // };

}
