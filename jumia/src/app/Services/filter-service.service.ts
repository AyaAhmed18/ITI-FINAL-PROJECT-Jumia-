import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterServiceService {
  private apiUrl = 'http://localhost:64866/api/Product/FilterByAllWithPagination?';
  constructor(private _httpClient: HttpClient) { }
  private ChangingPages = new BehaviorSubject<any>(null);
  setValue(value: any) {
    this.ChangingPages.next(value);
  }

  getValue() {
    return this.ChangingPages.asObservable();
  }
  filterByAll(minDisc?: number,minPrice?: number, maxPrice?: number,BrandList? : string,pageNumber?: number ,pageSize? : number): Observable<any> {

    this.apiUrl = 'http://localhost:64866/api/Product/FilterByAllWithPagination?';
    if(BrandList!=undefined)
    {
      this.apiUrl+=`BrandList=${BrandList}`;
    }
    if(minPrice!=undefined)
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?')this.apiUrl+=`MinPrice=${minPrice}`;
      else this.apiUrl+=`&MinPrice=${minPrice}`
    }
    if(maxPrice!=undefined)
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?')this.apiUrl+=`MaxPrice=${maxPrice}`;
      else this.apiUrl+=`&MaxPrice=${maxPrice}`;
    }
    if(minDisc!=undefined)
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?') this.apiUrl+=`MinDisc=${minDisc}`;
      else this.apiUrl+=`&MinDisc=${minDisc}`
    }
    if(BrandList!=undefined)
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?') this.apiUrl+=`BrandList=${BrandList}`;
      else this.apiUrl+=`&BrandList=${BrandList}`
    }
    if(pageNumber!=undefined)
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?') this.apiUrl+=`pageNumber=${pageNumber}`;
      else this.apiUrl+=`&pageNumber=${pageNumber}`
    }
    else
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?') this.apiUrl+=`pageNumber=1`;
      else this.apiUrl+=`&pageNumber=1`
    }
    if(pageSize!=undefined)
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?') this.apiUrl+=`pageSize=${pageSize}`;
      else this.apiUrl+=`&pageSize=${pageSize}`
    }
    else
    {
      if(this.apiUrl[this.apiUrl.length-1]=='?') this.apiUrl+=`pageSize=15`;
      else this.apiUrl+=`&pageSize=15`
    }
    console.log(this.apiUrl);
    let allpro= this._httpClient.get<any>(`${this.apiUrl}`);
    console.log("minDisc")
    console.log(minDisc)
    console.log("minPrice")
    console.log(minPrice)
    console.log("maxPrice")
    console.log(maxPrice)
    console.log("BrandList ")
    console.log(BrandList)
    console.log("allpro")
    console.log(allpro);
    return allpro;


  }
  // filterByPriceRange(minPrice: number, maxPrice: number): Observable<any> {
  //   let allpro= this._httpClient.get<any>(`${this.apiUrl}/FilterByPriceRange?MinPrice=${minPrice}&MaxPrice=${maxPrice}`);
  //   console.log(minPrice)
  //   console.log(maxPrice)


  //   return allpro;
  // }



}
