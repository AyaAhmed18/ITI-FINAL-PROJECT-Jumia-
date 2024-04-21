import { Component, OnInit } from '@angular/core';
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ApiLoginService } from '../../Services/api-login.service';
import { SliderComponent } from '@angular-slider/ngx-slider';
import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-discount-slider',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule,TranslateModule],
  templateUrl: './discount-slider.component.html',
  styleUrl: './discount-slider.component.css'
})
export class DiscountSliderComponent implements OnInit {


  productDesc: ProductDto[]|null = null;
  discPro:any[]=[];

  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
 
constructor(private _ApiProductsService: ApiProductsService
  ,private _router : Router,
  private  translate: TranslateService,
  private _sanitizer:DomSanitizer,
)
{}

ngOnInit(): void {
  

  this.FilterByDiscountRangeToSlider();
}


  FilterByDiscountRangeToSlider() {
    this._ApiProductsService.FilterByDiscountRangeToSlider().subscribe(
      {
        next: (data) => {
          this.discPro=data.entities;
          console.log("GetProductByDiscountRange");
          console.log(data);
          this.sanitizeImages2();
        },
        error: (error) => {
          console.error('Error fetching product data:', error);
        }
      }
    );
  }
  
  isArabicLanguage(): boolean {
    return this.translate.currentLang === 'ar';
  }

  
navigateToDetails(productId: number): void {
  this._router.navigateByUrl(`/Detalse/${productId}`);
}
sanitizeImages2() {
  this.productDesc?.forEach(product => {
    console.log(product.images);
    product.images[0] = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images[0]);
    console.log("Images");
    console.log(product.images);

  });

}


// sanitizeImages() {
//   this.discPro.forEach(product => {
//     this._ApiProductsService.getProductById(product.id).subscribe({
//       next:  (res: ProductDto) => {
//         console.log(res.images);
//         product.images=res.images
//         product.images = res.images.map(image => 
//           this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + image) as SafeUrl
//         );
       
//         }})
        
        
        
//   });
// }

}
