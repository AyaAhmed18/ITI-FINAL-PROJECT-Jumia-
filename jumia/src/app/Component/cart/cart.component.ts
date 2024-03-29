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
  constructor(private _cartService: CartService, private router: Router) { }
  ngOnInit(): void {
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
      this.calculateTotalCartPrice();
    });
  }
  startShopping(){
    this.router.navigate(['/Home']);
  }
  removeProduct(productToRemove: any) {
    const index = this.cartItems.indexOf(productToRemove); 
    if (index !== -1) {
      this.cartItems.splice(index, 1); 
      this.calculateTotalCartPrice();
      this.cartNumbers()
    }
  }
  calculateTotalCartPrice() {
    this.TotalCartPrice = this.cartItems.reduce((total, item) => total + (item.realPrice* item.cartQuantity), 0);
  }
  decreaseQuantity(item:ProductDto){
    if (item.cartQuantity > 1) {
      item.cartQuantity--;
      this.calculateTotalCartPrice();
      this.cartNumbers()
    }
  }
  increaseQuantity(item:ProductDto){
    item.cartQuantity++;
    this.calculateTotalCartPrice();
    this.cartNumbers()
  }
  cartNumbers(){
    this.cartNumber = this.cartItems.reduce((total, item) => total + (item.cartQuantity), 0);
  }
}
