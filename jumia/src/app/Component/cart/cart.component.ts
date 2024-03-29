import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit{
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  PriceAfterDiscount:number=0
  constructor(private _cartService: CartService, private router: Router) { }
  //priceAftrDiscount
  priceAftrDiscount(pro:ProductDto){
   this.PriceAfterDiscount = pro.realPrice-(pro.realPrice*pro.discount/100)
  }
  ngOnInit(): void {
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
  }
  startShopping(){
    this.router.navigate(['/Home']);
  }
  removeProduct(productToRemove: any) {
    this._cartService.removeProduct(productToRemove)
  }
 
  decreaseQuantity(item:ProductDto){
    if (item.cartQuantity > 1) {
      item.cartQuantity--;
    this.TotalCartPrice= this._cartService.calculateTotalCartPrice()
    this.cartNumber= this._cartService.calculateTotalCartNumber()
    }
  }
  increaseQuantity(item:ProductDto){
    item.cartQuantity++;
    this.TotalCartPrice= this._cartService.calculateTotalCartPrice()
    this.cartNumber= this._cartService.calculateTotalCartNumber()
  }
  cartNumbers(){
    this.cartNumber = this.cartItems.reduce((total, item) => total + (item.cartQuantity), 0);
  }
}
