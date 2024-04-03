import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FilterComponent } from "../filter/filter.component";
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { SearchResultsService } from '../../Services/search-results.service';
import { CartService } from '../../Services/cart.service';
import { WishlistService } from '../../Services/wishlist.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product',
  standalone: true,
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
  imports: [FilterComponent,CommonModule,FormsModule]
})
export class ProductComponent implements  OnInit{
  @Input() AllProducts:ProductDto[]=[];
  @Input() products: any[]=[];
  cartItems: ProductDto[] = [];
  productList: ProductDto[] = [];
// AllProducts:ProductDto[]=[];
//  products: any;
searchResults: any[] = [];
pageSize:number = 10;
AllProd:number=0;
totalPages: number = 0;
pageNumber: number = 1;
pageNumbers: number[]=[];
AllProductsProducts : any[]=[];
//  @Input() product?: ProductDto;
  cartTotalPrice:number=0
  @Output() addToCartClicked = new EventEmitter<ProductDto>();
  @Output() addTowashlistClicked = new EventEmitter<ProductDto>();
  addedToCart = false;
  addedTowashlist= false;
  constructor(private _ApiProductsService :ApiProductsService ,
       private _sanitizer:DomSanitizer,
      private _cartService:CartService,
      private _wishlist :WishlistService,
      private _searchResultsService: SearchResultsService)
   {

   }
   ngOnInit(): void {
    //this.Sershresult();
    this.getAllProducts();
    }

  //start Add to Cart
  AddToCart(prod:ProductDto){
      if(prod.stockQuantity>0){
        prod.cartQuantity = 1;
         this.cartTotalPrice+=prod.realPrice
         this._cartService.addToCart(prod);
         this.addToCartClicked.emit(prod);
         prod.addedToCart = true;
         this._wishlist.removeProductFromWishlist(prod)
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
  // ngOnInit(): void {
  //     this._ApiProductsService.getAllProducts().subscribe({
  //         next:(data)=>{
  //       this.AllProducts=data
  //       console.log(data);

  //       this.AllProducts.forEach(Product => {

  //         Product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + Product.images);
  //       });
  //       },
  //       error:(err)=>{

  //       console.log(err)
  //       }

  //       })
  // }
  Sershresult() {
    this._searchResultsService.getSearchResults().subscribe({
        next: (data) => {
            this.AllProducts = data;
            console.log(this.AllProducts );
            this.sanitizeImages();

        },
        error: (err) => {
            console.log(err);
        }
    });
}
getAllProducts() {
  this._ApiProductsService.getAllProducts(this.pageSize, this.pageNumber).subscribe({
      next: (data) => {
          this.products = data;
          this.AllProd = data.count;



          this.totalPages=Math.ceil( this.AllProd / this.pageSize)
          this.pageNumbers = Array.from({ length: this.totalPages }, (_, index) => index + 1);
          console.log("all");
          console.log( this.products);

          this.sanitizeImages();
      },
      error: (err) => {
          console.log(err);
      }
  });
}
nextPage(): void {
if (this.pageNumber < this.totalPages) {
  console.log( this.pageNumber);

  this.pageNumber++;
  console.log( this.pageNumber);


console.log();
  this.getAllProducts();
}
}

prevPage(): void {
if (this.pageNumber > 1) {
  this.pageNumber--;
  this.getAllProducts();
}
}
goToPage(page: number): void {
if (page >= 1 && page <= this.totalPages) {
  this.pageNumber = page;
  this.getAllProducts();
}
}
  loadAllProductsOrderedAsc() {
    this._ApiProductsService.getAllProductsWithOrderAasc().subscribe({
      next: (data) => {
        this.products = data;
        this.AllProd=data.length;
        this.sanitizeImages();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  loadAllProductsOrderedDsc() {
    this._ApiProductsService.getAllProductsWithOrderDasc().subscribe({
      next: (data) => {
        this.products = data;
        this.sanitizeImages();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  loadAllProductsNewestArrivals() {
    this._ApiProductsService.getAllProductsWithNewestArrivals().subscribe({
      next: (data) => {
        this.products = data;
        this.sanitizeImages();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  sanitizeImages() {
    this.AllProducts.forEach(product => {
      product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images);
    });
  }

  onSortChange(event: any) {
    const selectedSortOption = event.target.value;
    switch (selectedSortOption) {
      case 'allProducts':
        this.getAllProducts();
        break;
      case 'PriceLowToHigh':
        this.loadAllProductsOrderedAsc();
        break;
      case 'PriceHighToLow':
        this.loadAllProductsOrderedDsc();
        break;
      case 'NewestArrivals':
        this.loadAllProductsNewestArrivals();
        break;
      default:
        this.getAllProducts();
        break;
    }
  }


  searchProducts(searchTerm: string): void {
    this._ApiProductsService.SearchByNameOrDesc(searchTerm).subscribe(
      (data: any) => {
        this.searchResults = data;
      },
      (error: any) => {
        console.error('Error fetching search results:', error);
      }
    );
    }

// ngOnInit(): void {
//     this._ApiProductsService.getAllProductsWithOrderAasc().subscribe({
//         next:(data)=>{
//       this.AllProducts=data

//       this.AllProducts.forEach(Product => {

//         Product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + Product.images);
//      });

//       },
//       error:(err)=>{

//       console.log(err)
//       }

//       })
// }

}
