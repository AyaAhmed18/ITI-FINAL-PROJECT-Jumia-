import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FilterComponent } from "../filter/filter.component";
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../Services/cart.service';
import { WishlistService } from '../../Services/wishlist.service';


@Component({
    selector: 'app-product',
    standalone: true,
    templateUrl: './product.component.html',
    styleUrl: './product.component.css',
    imports: [FilterComponent,CommonModule,FormsModule]
})
export class ProductComponent implements  OnInit{
    @Input() AllProducts:ProductDto[]=[];
    products: any;
    cartItems: ProductDto[] = [];
  //  @Input() product?: ProductDto;
    cartTotalPrice:number=0
    @Output() addToCartClicked = new EventEmitter<ProductDto>();
    @Output() addTowashlistClicked = new EventEmitter<ProductDto>();
    addedToCart:boolean= false;
    addedTowashlist: boolean = false;
    constructor(private _ApiProductsService :ApiProductsService ,
         private _sanitizer:DomSanitizer,
        private _cartService:CartService,
        private _wishlist :WishlistService)
     { 
        
     }

    //start Add to Cart 
    AddToCart(prod:ProductDto){
        if(prod.stockQuantity>0){
          prod.cartQuantity = 1;
           this.cartTotalPrice+=prod.realPrice
           this._cartService.addToCart(prod);
           this.addToCartClicked.emit(prod);
           prod.addedToCart = true;
           this._wishlist.removeProductFromWishlist(prod);
        }
    }

    // end Add to Cart

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
    
    ngOnInit(): void {
        this._ApiProductsService.getAllProducts().subscribe({
            next:(data)=>{
          this.AllProducts=data
          console.log(data);
          
          this.AllProducts.forEach(Product => {
            
            Product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + Product.images);
          });
          },
          error:(err)=>{
          
          console.log(err)
          }
          
          })
    } 
}