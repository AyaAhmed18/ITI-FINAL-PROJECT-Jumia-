import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { IShippment } from '../Models/ishippment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiShippmentService {
  private apiUrl = environment.apiUrl;
  shippmentAddress!:IShippment
  constructor(private _httpClient:HttpClient) { }
  AddClientAddress(shippment:IShippment): Observable<any> {
    return this._httpClient.post<any>(`${this.apiUrl}/Shippment/Post`, { shippment });
}

UpdateClientAddress(shippment:IShippment): Observable<any> {
  return this._httpClient.post<any>(`${this.apiUrl}/Shippment/Put`, { shippment });
}
}
