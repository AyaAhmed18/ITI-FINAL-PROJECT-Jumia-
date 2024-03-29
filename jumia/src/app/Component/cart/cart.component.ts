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
  constructor(private _cartService: CartService, private router: Router) { }
  ngOnInit(): void {
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
    });
  }
  startShopping(){
    this.router.navigate(['/Home']);
  }
  ChangePrice(prod:ProductDto){
    this.cartItems.push(prod)
  }
  removeProduct(productToRemove: any) {
    const index = this.cartItems.indexOf(productToRemove); 
    if (index !== -1) {
      this.cartItems.splice(index, 1); 
    }
  }
}
