import { Injectable } from '@angular/core';
import { ProductDto } from '../ViewModels/product-dto';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems: ProductDto[] = [] as ProductDto[];
  private cartSubject = new BehaviorSubject<ProductDto[]>([]);
   cartNumber: number = 0;
   totalCartPrice: number = 0;
  
  constructor() { this.initializeCartFromSession(); }

  initializeCartFromSession() {
    const cartItemsFromSession = sessionStorage.getItem('cartItems');
    if (cartItemsFromSession) {
      this.cartItems = JSON.parse(cartItemsFromSession);
     this.calculateTotalCartPrice();
     this.calculateTotalCartNumber();
      this.cartSubject.next([...this.cartItems]);
    }
  }

  updateSessionStorage() {
    sessionStorage.setItem('cartItems', JSON.stringify(this.cartItems));
    sessionStorage.setItem('cartNumber', this.cartNumber.toString());
    sessionStorage.setItem('totalCartPrice', this.totalCartPrice.toString());
  }

  getCart() {
    return this.cartSubject.asObservable();
  }

  addToCart(product: ProductDto) {
    console.log(product.id);
    console.log(this.cartItems);
    const existingProduct = this.cartItems.find(item => item.id === product.id);
    if (!existingProduct) {
      product.addedToCart = true;
      sessionStorage.setItem(`addedToCart${product.id}`, product.addedToCart.toString());
      this.cartItems.push(product);
      this.calculateTotalCartPrice();
      this.calculateTotalCartNumber();
      this.cartSubject.next([...this.cartItems]);
      this.updateSessionStorage();
     
    }
    else{
      product.addedToCart = true;
    }
   
  }

  removeProduct(productToRemove: ProductDto) {
    const index = this.cartItems.indexOf(productToRemove);
    if (index !== -1) {
      this.cartItems.splice(index, 1);
      productToRemove.stockQuantity+=productToRemove.cartQuantity
      this.calculateTotalCartInfo();
      productToRemove.addedToCart=false
      sessionStorage.removeItem(`addedToCart${productToRemove.id}`);
      this.cartSubject.next([...this.cartItems]);
      this.updateSessionStorage();
    }
  }
  calculateTotalCartPrice():number{
    this.totalCartPrice = this.cartItems.reduce((total, item) => total + (item.realPrice * item.cartQuantity), 0);
    this.updateSessionStorage();
    return  this.totalCartPrice
  }
  calculateTotalCartNumber():number{
    this.cartNumber = this.cartItems.reduce((total, item) => total + item.cartQuantity, 0);
    this.updateSessionStorage();
    return  this.cartNumber
  }
  calculateTotalCartInfo() {
     this.cartNumber = this.cartItems.reduce((total, item) => total + item.cartQuantity, 0);
 }

}
