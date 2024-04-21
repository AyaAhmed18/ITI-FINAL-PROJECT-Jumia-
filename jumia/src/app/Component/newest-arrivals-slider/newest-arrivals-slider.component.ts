import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-newest-arrivals-slider',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule,TranslateModule],
  templateUrl: './newest-arrivals-slider.component.html',
  styleUrl: './newest-arrivals-slider.component.css'
})
export class NewestArrivalsSliderComponent {
  productNews: ProductDto[]|null = null;
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
 
constructor(private _ApiProductsService: ApiProductsService
  ,private _router : Router,
  private  translate: TranslateService,
  private _sanitizer:DomSanitizer,
)
{}

ngOnInit(): void {
  
  this.GetNewestArrivalsToSlider();


}



GetNewestArrivalsToSlider() {
  this._ApiProductsService.GetNewestArrivalsToSlider().subscribe(
    {
      next: (data) => {
        this.productNews=data.entities;
        console.log("GetNewestArrivalsToSlider");
        console.log(data);
        this.sanitizeImages3()
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


sanitizeImages3() {
  this.productNews?.forEach(product => {
    console.log(product.images);
    product.images[0] = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images[0]);
    console.log("Images");
    console.log(product.images);

  });

}

}
