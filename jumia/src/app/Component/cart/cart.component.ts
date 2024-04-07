import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { CommonModule } from '@angular/common';
import { WishlistService } from '../../Services/wishlist.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule,TranslateModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit{
  cartItems: ProductDto[] = [];
  wishlistItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  showAlert1: boolean = false;
  showAlert2: boolean = false;
  PriceAfterDiscount:number=0
  isArabic: boolean = false;
  constructor(private _cartService: CartService, private router: Router,private _wishlist : WishlistService ,private  translate: TranslateService) { }
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
    this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
    })
   
  }
  changeLanguage(lang: string) {
    if (lang == 'en') {
      localStorage.setItem('lang', 'en')
    }
    else {
      localStorage.setItem('lang', 'ar')
    }

    window.location.reload();

  }
  startShopping(){
    this.router.navigate(['/Home']);
  }
  removeProduct(productToRemove: any) {
    this._cartService.removeProduct(productToRemove)
  }
 
  decreaseQuantity(item:ProductDto){
    this.showAlert2 = false;
    if (item.cartQuantity > 1) {
      item.cartQuantity--;
      item.stockQuantity++;
    this.TotalCartPrice= this._cartService.calculateTotalCartPrice()
    this.cartNumber= this._cartService.calculateTotalCartNumber()
    this.showAlert2 = true;
    
    }
  }
  increaseQuantity(item:ProductDto){
    if (item.stockQuantity > 1) {
    item.cartQuantity++;
    item.stockQuantity--;
    this.TotalCartPrice= this._cartService.calculateTotalCartPrice()
    this.cartNumber= this._cartService.calculateTotalCartNumber()
    this.showAlert1 = true;
    }
  }
  cartNumbers(){
    this.cartNumber = this.cartItems.reduce((total, item) => total + (item.cartQuantity), 0);
  }

  removeProductFromWishlist(productToRemove: any) {
    this._wishlist.removeProductFromWishlist(productToRemove);
  }
  
   
   //start Add to Cart 
   AddToCart(prod:ProductDto){
    this.showAlert1 = false;
    console.log(this.showAlert1);
    
    if(prod.stockQuantity>0){
      prod.cartQuantity = 1;
      prod.stockQuantity--;
       this._wishlist.removeProductFromWishlist(prod);
       this._cartService.addToCart(prod);
       this.showAlert1 = true;
       console.log(this.showAlert1);
      }
}

closeAlert(){
  this.showAlert1 = false;
  this.showAlert2 = false;
}
}
