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
import { ActivatedRoute, Router } from '@angular/router';
import { FilterServiceService } from '../../Services/filter-service.service';
import { ApiSpecficationsService } from '../../Services/api-specfications.service';
import { ISpecfications } from '../../Models/ispecfications';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-product',
  standalone: true,
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
  imports: [FilterComponent,CommonModule,FormsModule ,TranslateModule]
})
export class ProductComponent implements  OnInit{
  @Input() AllProducts:ProductDto[]=[];
  @Input() products: any[]=[];
  AllProductsProducts: any[]=[];
  cartItems: ProductDto[] = [];
  productList: ProductDto[]|null = null;
// AllProducts:ProductDto[]=[];
//  products: any;
searchResults: any[] = [];
pageSize:number = 10;
AllProd:number=0;
totalPages: number = 0;
pageNumber: number = 1;
pageNumbers: number[]=[];
currentCategoryId :number = 0;
sizeSpecs: string[] = [];
AllSpecs: ISpecfications[] = [];
showAlert1: boolean = false;
showAlert2: boolean = false;
//  @Input() product?: ProductDto;
  cartTotalPrice:number=0
  @Output() addToCartClicked = new EventEmitter<ProductDto>();
  @Output() addTowashlistClicked = new EventEmitter<ProductDto>();
  @Input() triggerFunction: boolean = false;
  WhichSorting: Number = 0;
  //addedToCart = false;
  addedTowashlist= false;
  isArabic: boolean = false;
  constructor(private _ApiProductsService :ApiProductsService ,
       private _sanitizer:DomSanitizer,
      private _cartService:CartService,
      private _wishlist :WishlistService,
      private _searchResultsService: SearchResultsService,
      private _activeRouter: ActivatedRoute,
      private _route: Router,
      private _filterServices : FilterServiceService,
    private _specsServive:ApiSpecficationsService,
    private  translate: TranslateService)

   {

   }

   ngOnInit(): void {
    this.isArabicLanguage();
    // this._activeRouter.paramMap.subscribe(parammap=>
    //   {
    //     this.currentCategoryId =Number(parammap.get('id'));
    //     this.getProductByCategoryId(this.currentCategoryId);
    //   })
    // //
    // this.getAllProducts();

    this.Sershresult();
    const currentRoute = this._route.url;
    // if (currentRoute.includes('GetSubCategory')) {
    //   this.products = []
    //   console.log("SubCategory")
    // }
    // else if (currentRoute.includes('GetCategory')) {
    //   //this.products =
    //   this.products.length = 0
    //   console.log("Category")

    // }
    // else
    // {
    //   this.products = this.products;
    //   console.log("filter")
    // }
    this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
    })

    console.log("onInit");
    console.log(this.products);
    //this.pageNumbers = this.products[1]
   /// this.GetSpecs()
    }

    getProductByCategoryId(id:number)
    {
      this._ApiProductsService.getProductByCatId(id).subscribe(
      {
        next:(data: any)=>{
          this.productList=data.entities
          console.log(data);
          console.log("ProductList")
          console.log(this.productList);
        }
      })
    }
  //start Add to Cart
  AddToCart(prod:ProductDto){
    if(prod.stockQuantity>0){
      prod.cartQuantity = 1;
       this.cartTotalPrice+=prod.realPrice
       this._cartService.addToCart(prod);
       this.addToCartClicked.emit(prod);
       this._wishlist.removeProductFromWishlist(prod)
       this.showAlert1 = true;
    }
}

//Sprcifications
GetSpecs(pro:ProductDto){
    this._specsServive.GetProductSpecs(pro.id).subscribe(specs => {
      if (specs != null) {
        console.log(specs);
        specs.forEach(spec => {
          this.AllSpecs.push(spec);
          if (spec.specificationName === 'Size') {
            console.log(spec.value);
            if (spec.value.includes(',')) {
              this.sizeSpecs = spec.value.split(',');
            } else {
              this.sizeSpecs = [spec.value];
            }
          }
          console.log(this.sizeSpecs);
        });
      }
    });

}
  // end Add to Cart

    //Addtowashlist
    addToWishlist(product: ProductDto) {
      if (this.isInWishlist(product)) {
          this._wishlist.removeProductFromWishlist(product);
      } else {
          this._wishlist.addProductToWishlist(product);
          this.showAlert2 = true;
      }
      product.addedTowashlist = !this.isInWishlist(product); // Toggle the addedTowashlist property


    }


    isInWishlist(product: ProductDto): boolean {
      return !!product.addedTowashlist;
  }  // ngOnInit(): void {
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
          console.log(data[0].name)
          console.log(data[0][0].name)
          console.log(data[0].name)
          console.log(data)
          console.log("in product ts")
            this.products = data[0];
            console.log(this.products );
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
          this.WhichSorting = 1;
        //  this.sanitizeImages();
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
if(this.WhichSorting == 2)this.loadAllProductsOrderedAsc();
else if(this.WhichSorting == 3)this.loadAllProductsOrderedDsc();
else if(this.WhichSorting == 4)this.loadAllProductsNewestArrivals();
else this.getAllProducts();
  //this.getAllProducts();
}
}

prevPage(): void {
if (this.pageNumber > 1) {
  this.pageNumber--;
  this._filterServices.setValue(this.pageNumber);
  if(this.WhichSorting == 2)this.loadAllProductsOrderedAsc();
  else if(this.WhichSorting == 3)this.loadAllProductsOrderedDsc();
  else if(this.WhichSorting == 4)this.loadAllProductsNewestArrivals();
  else this.getAllProducts();
}
}
goToPage(page: number): void {
if (page >= 1 && page <= this.totalPages) {
  this.pageNumber = page;
  this._filterServices.setValue(this.pageNumber);
  if(this.WhichSorting == 2)this.loadAllProductsOrderedAsc();
  else if(this.WhichSorting == 3)this.loadAllProductsOrderedDsc();
  else if(this.WhichSorting == 4)this.loadAllProductsNewestArrivals();
  else this.getAllProducts();
  //this.getAllProducts();
}
}
  loadAllProductsOrderedAsc() {
    this._ApiProductsService.getAllProductsWithOrderAascWithPagination(this.pageSize, this.pageNumber).subscribe({
      next: (data) => {
        this.AllProd=data.length;
        this.products = data;
        this.AllProd = data.count;

        this.totalPages=Math.ceil( this.AllProd / this.pageSize)
        this.pageNumbers = Array.from({ length: this.totalPages }, (_, index) => index + 1);
          console.log("all");
          console.log( this.products);
          this.WhichSorting  = 2;
        //this.sanitizeImages();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  loadAllProductsOrderedDsc() {
    this._ApiProductsService.getAllProductsWithOrderDascWithPagination(this.pageSize, this.pageNumber).subscribe({
      next: (data) => {
        this.AllProd=data.length;
        this.products = data;
        this.AllProd = data.count;

        this.totalPages=Math.ceil( this.AllProd / this.pageSize)
        this.pageNumbers = Array.from({ length: this.totalPages }, (_, index) => index + 1);
          console.log("all");
          console.log( this.products);
          this.WhichSorting  = 3;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  loadAllProductsNewestArrivals() {
    this._ApiProductsService.getAllProductsWithNewestArrivalsWithPagination(this.pageSize, this.pageNumber).subscribe({
      next: (data) => {
        this.AllProd=data.length;
        this.products = data;
        this.AllProd = data.count;

        this.totalPages=Math.ceil( this.AllProd / this.pageSize)
        this.pageNumbers = Array.from({ length: this.totalPages }, (_, index) => index + 1);
          console.log("all");
          console.log( this.products);
          this.WhichSorting  = 4;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  sanitizeImages() {
    this.products.forEach(product => {
      console.log(product.images);
      product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images[0]);
      console.log(product.images);
      console.log(product.images);

    });
  }
//   sanitizeImages() {
//     this.products.images[0]
//     = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + this.product.images);
//  }

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
        this.products = data;
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
navigateToDetails(productId: number): void {
  this._route.navigateByUrl(`/Detalse/${productId}`);
}
//localization
changeLanguage(lang: string) {
  if (lang == 'en') {
    localStorage.setItem('lang', 'en')
  }
  else {
    localStorage.setItem('lang', 'ar')
  }

  window.location.reload();

}

isArabicLanguage(): boolean {
  return this.translate.currentLang === 'ar';
}
closeAlert(){
  this.showAlert1 = false;
  this.showAlert2 = false;
}
}
