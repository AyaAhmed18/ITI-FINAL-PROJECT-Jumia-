import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { ApiSpecficationsService } from '../../Services/api-specfications.service';
import { ISpecfications } from '../../Models/ispecfications';
import { SafeUrl, DomSanitizer } from "@angular/platform-browser";
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'], 
})
export class DetailsComponent implements OnInit {
  currentProdId: number = 0;
  product!: ProductDto;
  sizeSpecs: string[] = [];
  AllSpecs: ISpecfications[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private apiProductsService: ApiProductsService,
    private router: Router,
    private _specsServive: ApiSpecficationsService,
    private _sanitizer: DomSanitizer
  ) { }

  ngOnInit(): void {
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
}