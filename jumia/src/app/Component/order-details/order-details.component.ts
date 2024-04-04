import { Component, OnInit } from '@angular/core';
import { IOrder } from '../../Models/i-order';
import { APIOrderServiceService } from '../../Services/apiorder-service.service';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent implements OnInit{
  Orders:IOrder[]=[] as IOrder[]
  OrdersNumbers:number=0
  clientId=Number(localStorage.getItem('userId'))
  constructor(private _OrderService:APIOrderServiceService){}
 ngOnInit(): void {
  this._OrderService.GetUserOrders(this.clientId).subscribe(Orders => {
    if(Orders.length=0){
      this.OrdersNumbers=0
    }
    else{
      this.OrdersNumbers=Orders.length
      this.Orders = Orders;
    }
    
  });
 }


}
