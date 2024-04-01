import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { ISubCategory } from '../models/isub-category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubcategoryserviceService {

  private apiUrl = environment.apiUrl

  constructor(private  _HttpClient:HttpClient) { }


  getAllSubCategory():Observable<ISubCategory[]>{

    return this._HttpClient.get<ISubCategory[]>(`${this.apiUrl}/SubCategory`)
    
  }


   getSubCategoryById(id:number):Observable<ISubCategory>{

    return this._HttpClient.get<ISubCategory>(`${this.apiUrl}/SubCategory/${id}`)

   }





}
