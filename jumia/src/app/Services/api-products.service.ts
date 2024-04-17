import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProductDto } from '../ViewModels/product-dto';
import { environment } from '../../environment/environment';


@Injectable({
  providedIn: 'root'
})
export class ApiProductsService {
  httpHeader={}
  private apiUrl = environment.apiUrl;
  //private apiUrl = 'http://localhost:64866/api/Product';
//http://localhost:5094/api/Product bahaa
  //localhost:64866/api/Product
  constructor(private _httpClient:HttpClient) {
    this.httpHeader={headers:new HttpHeaders(
      {"Content-Type":"application/json","Accept-Language":"ar-EG"}
      //{"Content-Type":"application/json"}
    )
    }
  }

  // getAllProducts(): Observable<any> {
  //   let allpro= this._httpClient.get<any>(this.apiUrl);
  //   return allpro;
  // }

  getAllProducts(pageSize: number, pageNumber: number): Observable<any> {
    const params = new HttpParams()
        .set('pageSize', pageSize.toString())
        .set('pageNumber', pageNumber.toString());
    return this._httpClient.get<any>(`${this.apiUrl}/Product`, { params});
}

getAllProductsWithOrderAascWithPagination(pageSize: number, pageNumber: number): Observable<any> {
  const params = new HttpParams()
      .set('pageSize', pageSize.toString())
      .set('pageNumber', pageNumber.toString());
  return this._httpClient.get<any>(`${this.apiUrl}/Product/GetOrderedAscByPagination`, { params});
}
  getAllProductsWithOrderAasc(): Observable<any> {


    let allpro= this._httpClient.get<any>(`${this.apiUrl}/Product/GetOrderedAsc`);
    return allpro;
  }
  getAllProductsWithOrderDascWithPagination(pageSize: number, pageNumber: number): Observable<any> {
    const params = new HttpParams()
        .set('pageSize', pageSize.toString())
        .set('pageNumber', pageNumber.toString());
    return this._httpClient.get<any>(`${this.apiUrl}/Product/GetOrderedDscByPagination`, { params});
  }
  getAllProductsWithOrderDasc(): Observable<any> {


    let allpro= this._httpClient.get<any>(`${this.apiUrl}/Product/GetOrderedDsc`);
    return allpro;
  }
  getAllProductsWithNewestArrivalsWithPagination(pageSize: number, pageNumber: number): Observable<any> {
    const params = new HttpParams()
        .set('pageSize', pageSize.toString())
        .set('pageNumber', pageNumber.toString());
    return this._httpClient.get<any>(`${this.apiUrl}/Product/GetNewestArrivalByPagination`, { params});
  }
  getAllProductsWithNewestArrivals(): Observable<any> {


    let allpro= this._httpClient.get<any>(`${this.apiUrl}/Product/GetNewestArrivals`);
    return allpro;
  }

  getProductById(id: number): Observable<ProductDto> {
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/Product/${id}`);
  }

  SearchByNameOrDesc(nameOrdesc:string): Observable<ProductDto>{
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/Product/SearchByName?PartialName=${nameOrdesc}`);
  }

  getProductByCatId(id: number): Observable<ProductDto> {
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/Product/GetbyCategoryId?CatId=${id}&pageSize=10&pageNumber=1`);
  }
  getProductBySubCatId(id: number): Observable<ProductDto> {
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/Product/GetbySubCategoryId?SubCatId=${id}&pageSize=10&pageNumber=1`);
  }

  FilterByDiscountRangeToSlider(): Observable<ProductDto> {
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/Product/FilterByDiscountRangeToSlider?MinDisc=30&pageSize=20&pageNumber=1`);
  }

  GetNewestArrivalsToSlider(): Observable<ProductDto> {
    return this._httpClient.get<ProductDto>(`${this.apiUrl}/Product/GetNewestArrivalsToSlider?pageSize=20&pageNumber=1`);
  }
  UpdateProductQuantity(prd:ProductDto): Observable<any> {
    let id=prd.id
     return this._httpClient.put<ProductDto>(`${this.apiUrl}/Product/${id}`,  prd );////
    
    }
}
