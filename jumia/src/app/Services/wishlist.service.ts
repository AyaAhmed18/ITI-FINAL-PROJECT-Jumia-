import { Injectable } from '@angular/core';
import { ProductDto } from '../ViewModels/product-dto';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private wishlistItems: ProductDto[] = [];
  private wishlistSubject = new BehaviorSubject<ProductDto[]>([]);

  constructor() {
    this.initializeCartFromSession();
    // // Retrieve wishlist from session storage if available
    // const cartItemsFromSession = sessionStorage.getItem('cartItems');
    
    // const storedWishlist = sessionStorage.getItem('wishlist');
    // if (storedWishlist) {
    //   this.wishlistItems = JSON.parse(storedWishlist);
    //   this.wishlistSubject.next([...this.wishlistItems]);
    // }
  }
//test

initializeCartFromSession() {
  const cartItemsFromSession = sessionStorage.getItem('cartItems');
  if (cartItemsFromSession) {
    this.wishlistItems = JSON.parse(cartItemsFromSession);
    this.wishlistSubject.next([...this.wishlistItems]);
  }
}

updateSessionStorage() {
  sessionStorage.setItem('wishlistItems', JSON.stringify(this.wishlistItems));
}

addProductToWishlist(product: ProductDto) {
  this.wishlistItems.push(product);
 // this.calculateTotalCartInfo();
  this.wishlistSubject.next([...this.wishlistItems]);
  this.updateSessionStorage();
}

removeProductFromWishlist(productToRemove: ProductDto) {
  const index = this.wishlistItems.indexOf(productToRemove);
    this.wishlistItems.splice(index, 1);
    this.wishlistSubject.next([...this.wishlistItems]);
    this.updateSessionStorage();
}



//test


  getWishlist() {
    return this.wishlistSubject.asObservable();
  }
  
  // addProductToWishlist(productToAdd: ProductDto) {
  //   let wishlistItems = this.getWishlistItemsFromSession();
  //   wishlistItems.push(productToAdd);
  //   this.saveWishlistItemsToSession(wishlistItems);
  // }

  // Method to remove a product from the wishlist
  // removeProductFromWishlist(productToRemove: any) {
  //   let wishlistItems = this.getWishlistItemsFromSession();
  //   const index = wishlistItems.indexOf(productToRemove);
  //   if (index !== -1) {
  //     wishlistItems.splice(index, 1);
  //     this.saveWishlistItemsToSession(wishlistItems);
  //   }
  // }

  // Method to get wishlist items from session storage
  getWishlistItemsFromSession(): any[] {
    const wishlistItemsString = sessionStorage.getItem('wishlistItems');
    return wishlistItemsString ? JSON.parse(wishlistItemsString) : [];
  }

  // Method to save wishlist items to session storage
  saveWishlistItemsToSession(wishlistItems: any[]) {
    sessionStorage.setItem('wishlistItems', JSON.stringify(wishlistItems));
  }
}
  


