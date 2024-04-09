import { Injectable } from '@angular/core';
import { ProductDto } from '../ViewModels/product-dto';
import { BehaviorSubject } from 'rxjs';
import { CartService } from './cart.service';
@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private wishlistItems: ProductDto[] = [];
  private wishlistSubject = new BehaviorSubject<ProductDto[]>([]);

  constructor(private _cartService: CartService) {
    this.initializeCartFromSession();
  }

  initializeCartFromSession() {
    const wishlistItemsFromSession = sessionStorage.getItem('wishlistItems');
    //const existingProduct = sessionStorage.getItem('cartItems');
    if (wishlistItemsFromSession) {
      this.wishlistItems = JSON.parse(wishlistItemsFromSession);
      this.wishlistSubject.next([...this.wishlistItems]);
    }
  }

  updateSessionStorage() {
    sessionStorage.setItem('wishlistItems', JSON.stringify(this.wishlistItems));
  }

  addProductToWishlist(product: ProductDto) {
    const existingProduct = sessionStorage.getItem('cartItems');
    if (!existingProduct) {
    this.wishlistItems.push(product);
    this.wishlistSubject.next([...this.wishlistItems]);
    this.updateSessionStorage();}
  }
  

  removeProductFromWishlist(productToRemove: ProductDto) {
    const index = this.wishlistItems.findIndex(item => item.id === productToRemove.id);
    if (index !== -1) {
      // Remove product from wishlist
      this.wishlistItems.splice(index, 1);
      this.wishlistSubject.next([...this.wishlistItems]);
      this.updateSessionStorage();

     }}
  getWishlist() {
    return this.wishlistSubject.asObservable();}
  getWishlistItemsFromSession(): any[] {
    const wishlistItemsString = sessionStorage.getItem('wishlistItems');
    return wishlistItemsString ? JSON.parse(wishlistItemsString) : [];
  }

  saveWishlistItemsToSession(wishlistItems: any[]) {
    sessionStorage.setItem('wishlistItems', JSON.stringify(wishlistItems));
  }
}