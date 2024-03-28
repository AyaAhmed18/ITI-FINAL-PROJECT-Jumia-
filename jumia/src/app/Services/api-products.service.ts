import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductDto } from '../ViewModels/product-dto';


@Injectable({
  providedIn: 'root'
})
export class ApiProductsService {
 
  private apiUrl = 'http://localhost:5094/api/Product';
  constructor(private _httpClient:HttpClient) { }

  getAllProducts(): Observable<any> {
    let allpro= this._httpClient.get<any>(this.apiUrl);
    return allpro;
  }

  getProductById(id: number): Observable<ProductDto> {
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/${id}`);
  }


}
// add-ProductDtos-And-apiProductService-By-Bahaa