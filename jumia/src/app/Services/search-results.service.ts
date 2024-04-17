import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProductDto } from '../ViewModels/product-dto';

@Injectable({
  providedIn: 'root'
})
export class SearchResultsService {
  private searchResultsSubject: BehaviorSubject<ProductDto[]> = new BehaviorSubject<ProductDto[]>([]);

  constructor() { }

  setSearchResults(results: ProductDto[]): void {
    console.log(results)
    this.searchResultsSubject.next(results);
    console.log("set");
    console.log(this.searchResultsSubject);
    
  }

  getSearchResults(): Observable<any[]> {
    console.log("get");
    return this.searchResultsSubject.asObservable();


  }
}
