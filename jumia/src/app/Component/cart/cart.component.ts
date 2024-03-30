import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { CommonModule } from '@angular/common';
import { WishlistService } from '../../Services/wishlist.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit{
  cartItems: ProductDto[] = [];
  wishlistItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  PriceAfterDiscount:number=0
  constructor(private _cartService: CartService, private router: Router,private _wishlist : WishlistService) { }
  //priceAftrDiscount
 
  priceAftrDiscount(pro:ProductDto){
   this.PriceAfterDiscount = pro.realPrice-(pro.realPrice*pro.discount/100)
  }
  
  ngOnInit(): void {
    this._wishlist.getWishlist().subscribe(Items=>{
      this.wishlistItems =Items
      console.log(Items);});
    console.log(this.wishlistItems);
    
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
    //
   
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

  removeProductFromWishlist(productToRemove: any) {
    this._wishlist.removeProductFromWishlist(productToRemove);
  }
  
    //start Add to Cart 
     AddToCart(prod:ProductDto){
      if(prod.stockQuantity>0){
        prod.cartQuantity = 1;
         this._wishlist.removeProductFromWishlist(prod);
         prod.addedTowashlist = true;}
}
   //Addtowashlist
   addToWishlist(product: ProductDto) {
    if (this.isInWishlist(product)) {
        this._wishlist.removeProductFromWishlist(product);
    } else {
        this._wishlist.addProductToWishlist(product);
    }
    product.addedTowashlist = !this.isInWishlist(product); // Toggle the addedTowashlist property
}


  isInWishlist(product: ProductDto): boolean {
    return !!product.addedTowashlist; 
}

}
