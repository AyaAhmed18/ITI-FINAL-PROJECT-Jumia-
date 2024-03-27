import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class APICategoryService{
  Category:any=[]
  private apiUrl = environment.apiUrl;
  constructor(private httpClient:HttpClient) {}
  // addCategory(prd:IProduct):Observable<IProduct>{
  //   return  this.httpClient.post<IProduct>(`http://localhost:3000/products` , JSON.stringify(prd))
  //   }
  //  getAllProducts():Observable<IProduct[]>{
  //   return this.httpClient.get<IProduct[]>("http://localhost:3000/products")
  //  }
  //  getProductById(Id:number):Observable<IProduct>{
  //    return this.httpClient.get<IProduct>(`http://localhost:3000/products/${Id}`)
  //  }
  //  getProductByCatId(catId:number):Observable<IProduct[]>{
  //    return this.httpClient.get<IProduct[]>(`http://localhost:3000/products?catId=${catId}`)
  //  }
   fetchCategories(){
    // this.httpClient.get(`${this.apiUrl}/Category`);
    this.httpClient.get(`${this.apiUrl}/Category/1`).subscribe((data:any)=>{
      this.Category=data;
    console.log(data);
    }) 
  }
  // ngOnInit(): void {
  //   this.fetchCategories();
  // }
}
