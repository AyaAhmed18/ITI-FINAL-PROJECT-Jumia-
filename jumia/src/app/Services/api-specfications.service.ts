import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISpecfications } from '../Models/ispecfications';

@Injectable({
  providedIn: 'root'
})
export class ApiSpecficationsService {
  private apiUrl = environment.apiUrl;
  constructor(private _httpClient:HttpClient ) { }

  GetProductSpecs(prdId:number):Observable<ISpecfications[]>{
    return this._httpClient.get<ISpecfications[]>(`${this.apiUrl}/Specfication/${prdId}`)
  }
}
