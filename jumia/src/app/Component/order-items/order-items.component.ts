import { Component, OnInit } from '@angular/core';
import { APIOrderServiceService } from '../../Services/apiorder-service.service';
import { IOrderItems } from '../../Models/iorder-items';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-items',
  standalone: true,
  imports: [],
  templateUrl: './order-items.component.html',
  styleUrl: './order-items.component.css'
})
export class OrderItemsComponent implements OnInit{
  OrderItems:IOrderItems[]=[] as IOrderItems[]
  constructor(private _OrderService:APIOrderServiceService,private route: ActivatedRoute){}
  ordId:number=0
  ItemsNumber:number=0
  ngOnInit(): void {
    this.ordId = Number(this.route.snapshot.paramMap.get('ordId'));
    console.log('Order ID:', this.ordId);
    this._OrderService.GetOrderItems(this.ordId).subscribe(OrderItems => {
     this.OrderItems=OrderItems
     this.ItemsNumber=OrderItems.length
    });
   }

   cancelItem(){

   }
   ReturnItem(){
    
   }

}
