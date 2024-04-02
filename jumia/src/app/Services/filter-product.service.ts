import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { filter, Observable } from 'rxjs';
import { environment } from '../../environment/environment.prod';
import { ProductDto } from '../ViewModels/product-dto';

@Injectable({
  providedIn: 'root'
})
export class FilterProductService {
private apiUrl = `${environment.apiUrl}`; 

  constructor(private _httpClient:HttpClient) {}

  filterByDiscountRange(minDisc :number):Observable<ProductDto>{
    let result = this._httpClient.get<ProductDto>(`${this.apiUrl}/FilterByDiscountRange/${minDisc}`); 
    return result;
  }
}
