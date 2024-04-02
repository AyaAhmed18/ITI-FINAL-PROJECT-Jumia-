import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  payment:boolean=false
  constructor(private _cartService:CartService,private router:Router){
    
  }
  ngOnInit(): void {
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
  }


  ConfirmPayment(){
    this.payment=true
  }
}
