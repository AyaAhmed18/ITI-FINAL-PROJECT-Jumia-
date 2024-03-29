import { Injectable } from '@angular/core';
import { ProductDto } from '../ViewModels/product-dto';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems: ProductDto[] = [];
  private cartSubject = new BehaviorSubject<ProductDto[]>([]);
  constructor() { }
  getCart() {
    return this.cartSubject.asObservable();
  }

  addToCart(product: ProductDto) {
    this.cartItems.push(product);
    this.cartSubject.next([...this.cartItems]);
  }

}
