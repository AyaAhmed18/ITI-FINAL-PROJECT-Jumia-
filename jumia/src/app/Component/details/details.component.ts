import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { ApiSpecficationsService } from '../../Services/api-specfications.service';
import { ISpecfications } from '../../Models/ispecfications';
import { SafeUrl, DomSanitizer } from "@angular/platform-browser";
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { CartService } from '../../Services/cart.service';
import { WishlistService } from '../../Services/wishlist.service';
import { BrandServiceService } from '../../Services/brand-service.service';


@Component({
  selector: 'app-details',
  standalone: true,
  imports: [CommonModule ,TranslateModule],
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'], 
})
export class DetailsComponent implements OnInit {
  currentProdId: number = 0;
  product!: ProductDto;
  sizeSpecs: string[] = [];
  AllSpecs: ISpecfications[] = [];
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
  showAlert1: boolean = false;
  cartTotalPrice:number=0
  @Output() addToCartClicked = new EventEmitter<ProductDto>();
  constructor(
    private activatedRoute: ActivatedRoute,
    private apiProductsService: ApiProductsService,
    private router: Router,
    private _specsServive: ApiSpecficationsService,
    private _sanitizer: DomSanitizer,
    private  translate: TranslateService,
    private _cartService:CartService,
    private _wishlist :WishlistService,
    private _brand:BrandServiceService
  ) { }

  ngOnInit(): void {
    this.brand();
    this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
    })

    this.activatedRoute.paramMap.subscribe(paramMap => {
      this.currentProdId = Number(paramMap.get('id'));
      this.getProductById(this.currentProdId);
    });
    
    this._specsServive.GetProductSpecs(this.currentProdId).subscribe(specs => {
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

   getProductById(id: number):void {
    this.apiProductsService.getProductById(id).subscribe({
      next:  (res: ProductDto) => {
        this.product = res;
       // this.product.images = 
        //this.product.images.map(url => this._sanitizer.bypassSecurityTrustUrl(url));
        this.product.images = this.product.images.map(imageData =>
          this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + imageData));
   
        console.log("details")
        console.log(this.product);
        
       
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  

  navigateToDetails(productId: number): void {
    this.router.navigateByUrl(`/Detalse/${productId}`);
  }

  sanitizeImages() {
     this.product.images[0] = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + this.product.images);
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
  isArabicLanguage(): boolean {
    return this.translate.currentLang === 'ar';
  }
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
  closeAlert(){
    this.showAlert1 = false;
    //this.showAlert2 = false;
  }
  //localization
  brand() {
    this._brand.getAllBrands().subscribe((brands: any[]) => { 
        const brand = brands.find((brand: any) => brand.id === this.product.brandId); 
       const brandName = brand.name;
       this.product.brandName=brandName
       console.log(this.product.brandId);
       
            console.log("Brand Name:", brandName);
       return brandName     
       
    });
}
}