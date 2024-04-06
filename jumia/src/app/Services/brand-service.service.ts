import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient  } from '@angular/common/http';
import { IBrandDto } from '../ViewModels/ibrand-dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BrandServiceService {
  private apiUrl = 'http://localhost:5094/api/Brand';
 

  constructor(private  _HttpClient:HttpClient) { }


  getAllBrands():Observable<any>{

    return this._HttpClient.get<any>(`${this.apiUrl}`)

  }
 
}
