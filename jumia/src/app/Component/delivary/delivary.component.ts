import { Component, OnInit, Sanitizer } from '@angular/core';
import {  Router, RouterLink } from '@angular/router';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { ApiShippmentService } from '../../Services/api-shippment.service';
import { IShippment } from '../../Models/ishippment';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-delivary',
  standalone: true,
  imports: [RouterLink ,TranslateModule],
  templateUrl: './delivary.component.html',
  styleUrl: './delivary.component.css'
})
export class DelivaryComponent implements OnInit {
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  clientId=localStorage.getItem('userId')
  userId:number=0
  shippment:IShippment={} as IShippment
  isArabic: boolean = false;
constructor(private router:Router, private _cartService:CartService,
  private  _ShippmentService:ApiShippmentService ,private  translate: TranslateService,
private _sanitizer:DomSanitizer){

}
  ngOnInit(): void {
    this.userId=Number(this.clientId)
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
      this.sanitizeImages();
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
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
      console.log(product.images);
      product.images[0] = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images[0]);
      console.log(product.images);
      console.log(product.images);

    });}

}
