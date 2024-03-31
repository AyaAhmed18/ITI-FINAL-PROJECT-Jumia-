import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductDto } from '../ViewModels/product-dto';


@Injectable({
  providedIn: 'root'
})
export class ApiProductsService {
 
  private apiUrl = 'http://localhost:5094/api/Product';
  constructor(private _httpClient:HttpClient) { }

  // getAllProducts(): Observable<any> {
  //   let allpro= this._httpClient.get<any>(this.apiUrl);
  //   return allpro;
  // }
  getAllProducts(pageSize: number, pageNumber: number): Observable<any> {
    const params = new HttpParams()
        .set('pageSize', pageSize.toString())
        .set('pageNumber', pageNumber.toString());
    return this._httpClient.get<any>(this.apiUrl, { params });
}


  getAllProductsWithOrderAasc(): Observable<any> {
    

    let allpro= this._httpClient.get<any>(`${this.apiUrl}/GetOrderedAsc`);
    return allpro;
  }

  getAllProductsWithOrderDasc(): Observable<any> {
   

    let allpro= this._httpClient.get<any>(`${this.apiUrl}/GetOrderedDsc`);
    return allpro;
  }
  getAllProductsWithNewestArrivals(): Observable<any> {
   

    let allpro= this._httpClient.get<any>(`${this.apiUrl}/GetNewestArrivals`);
    return allpro;
  }

  getProductById(id: number): Observable<ProductDto> {
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/${id}`);
  }
  
  SearchByNameOrDesc(nameOrdesc:string): Observable<ProductDto>{
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/SearchByName?PartialName=${nameOrdesc}`);
  }

}
