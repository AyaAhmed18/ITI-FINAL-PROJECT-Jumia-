import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterServiceService {
  private apiUrl = 'http://localhost:5094/api/Product';
  constructor(private _httpClient: HttpClient) { }

  filterByDiscountRange(minDisc: number): Observable<any> {

    let allpro= this._httpClient.get<any>(`${this.apiUrl}/FilterByDiscountRange?MinDisc=${minDisc}`);
    console.log("minDisc")
    console.log(minDisc)
    console.log("allpro")
    console.log(allpro)
    return allpro;

   
  }



}
