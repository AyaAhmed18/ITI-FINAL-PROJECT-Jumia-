import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { ISubCategory } from '../Models/isub-category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubcategoryserviceService {

  private apiUrl = `http://localhost:64866/api/SubCategory`

  constructor(private  _HttpClient:HttpClient) { }


  getAllSubCategory():Observable<ISubCategory[]>{

    return this._HttpClient.get<ISubCategory[]>(`http://localhost:64866/api/SubCategory`)

  }


   getSubCategoryById(id:number):Observable<ISubCategory>{

    return this._HttpClient.get<ISubCategory>(`${this.apiUrl}/SubCategory/${id}`)

   }
   getSubCategoryByCategoryId(catId:number):Observable<ISubCategory[]>
   {
      return this._HttpClient.get<ISubCategory[]>(`${this.apiUrl}/GetByCatId?CatId=${catId}`)

   }





}
