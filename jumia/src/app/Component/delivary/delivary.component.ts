import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-delivary',
  standalone: true,
  imports: [],
  templateUrl: './delivary.component.html',
  styleUrl: './delivary.component.css'
})
export class DelivaryComponent implements OnInit {
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
constructor(private router:Router, private _cartService:CartService ){

}
  ngOnInit(): void {
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
  }

  ConfirmDelivary(){
    this.router.navigate(['/Payment']);
  }
}
