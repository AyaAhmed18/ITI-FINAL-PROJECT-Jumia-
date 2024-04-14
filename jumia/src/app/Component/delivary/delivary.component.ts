import { Component, OnInit, Sanitizer } from '@angular/core';
import {  Router, RouterLink } from '@angular/router';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { ApiShippmentService } from '../../Services/api-shippment.service';
import { IShippment } from '../../Models/ishippment';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ApiProductsService } from '../../Services/api-products.service';

@Component({
  selector: 'app-delivary',
  standalone: true,
  imports: [RouterLink ,TranslateModule],
  templateUrl: './delivary.component.html',
  styleUrl: './delivary.component.css'
})
export class DelivaryComponent implements OnInit {
  proItems: ProductDto = {} as ProductDto;
  cartItems: any[]=[];
  cartNumber:number=0
  TotalCartPrice=0
  clientId=localStorage.getItem('userId')
  userId:number=0
  shippment:IShippment={} as IShippment
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
constructor(private router:Router, private _cartService:CartService,
  private  _ShippmentService:ApiShippmentService ,private  translate: TranslateService,
private _sanitizer:DomSanitizer,
private _apiProductService:ApiProductsService){

}
  ngOnInit(): void {
    this.userId=Number(this.clientId)
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
    this.sanitizeImages();
    this._ShippmentService.Getshippment(this.userId).subscribe(shipping => {
      this.shippment=shipping
      console.log(shipping.firstNameEn)
     });
    
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
  ConfirmDelivary(){
    this.router.navigate(['/Payment']);
  }
  isArabicLanguage(): boolean {
    return this.translate.currentLang === 'ar'; 
  }
  sanitizeImages() {
    this.cartItems.forEach(product => {
      this._apiProductService.getProductById(product.id).subscribe({
        next:  (res: ProductDto) => {
          console.log(res.images);
          product.images=res.images
          product.images = res.images.map(image => 
            this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + image) as SafeUrl
          );
         
          }})
          
          
          
    });
  }

  increaseDate(days: number): { dayName: string, dayNumber: number, monthName: string } {
    let date = new Date();
    date.setDate(date.getDate() + days);
  
  
    const dayName = new Intl.DateTimeFormat('en-US', { weekday: 'long' }).format(date);
    const dayNumber = date.getDate();
    const monthName = new Intl.DateTimeFormat('en-US', { month: 'long' }).format(date);
  
    return { dayName, dayNumber, monthName };
  }
}
