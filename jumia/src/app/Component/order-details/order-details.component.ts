import { Component, OnInit } from '@angular/core';
import { IOrder } from '../../Models/i-order';
import { APIOrderServiceService } from '../../Services/apiorder-service.service';
import { CommonModule } from '@angular/common';
import {  RouterLink, RouterOutlet } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [CommonModule,RouterLink,RouterOutlet ,TranslateModule],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent implements OnInit{
  Orders:IOrder[]=[] as IOrder[]
  OrdersNumbers:number=0
  closedOrders:number=0
  openOrders:number=0
  clientId=Number(localStorage.getItem('userId'))
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
  constructor(private _OrderService:APIOrderServiceService ,private  translate: TranslateService ){}
 ngOnInit(): void {
  this._OrderService.GetUserOrders(this.clientId).subscribe(Orders => {
    if(Orders.length==0){
      this.OrdersNumbers=0
      console.log(this.OrdersNumbers);
      
    }
    else{
      this.OrdersNumbers=Orders.length 
      this.Orders = Orders;
      for(let i=0;i<Orders.length;i++){
        if(Orders[i].status==4 || Orders[i].status==5 ){
          this.closedOrders+=1;
        }else{
          this.openOrders+=1;
        }
      }
      console.log(this.Orders.length);
    }
    
  });
  this.translate.onLangChange.subscribe((Event)=>{
    this.isArabic = Event.lang === 'ar'
  })
 }
 changeLanguage(lang: string) {
  if (lang == 'en') {
    localStorage.setItem('lang', 'en')
  }
  else {
    localStorage.setItem('lang', 'ar')
  }

  window.location.reload();

}
 getStatusColor(status: string): string {
  switch (status) {
    case 'Pending':
      return 'orange'; 
    case 'Shipped':
      return 'green'; 
    case 'Delivered':
      return 'blue'; 
    case 'Cancelled':
      return 'red';
      case 'Returned':
      return 'red'; 
    default:
      return ' rgba(253, 130, 29, 0.842)'; 
  }}

}
