import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit, inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class APIOrderServiceService implements OnInit {
  Orders:any=[]
  constructor() { }
  fetchOrders(){
    this.httpClient.get('http://localhost:64866/api/Order').subscribe((data:any)=>{
      this.Orders=data;
    console.log(data);
    }) 
  }
  ngOnInit(): void {
    this.fetchOrders();
  }
  httpClient=inject(HttpClient)
}
