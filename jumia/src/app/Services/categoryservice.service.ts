import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICategory } from '../Models/icategory';


@Injectable({
  providedIn: 'root'
})
export class CategoryserviceService {

  private apiUrl = environment.apiUrl

  constructor(private  _HttpClient:HttpClient) { }




getAllCategory():Observable<ICategory[]>{

  return this._HttpClient.get<ICategory[]>(`${this.apiUrl}/Category`)

}

getCategoryById(id:number):Observable<ICategory>{

  return this._HttpClient.get<ICategory>(`${this.apiUrl}/Category/${id}`)
}







}
