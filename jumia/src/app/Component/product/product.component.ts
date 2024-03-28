import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FilterComponent } from "../filter/filter.component";
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
    selector: 'app-product',
    standalone: true,
    templateUrl: './product.component.html',
    styleUrl: './product.component.css',
    imports: [FilterComponent]
})
export class ProductComponent implements OnInit{
   AllProducts:ProductDto[]=[];
    products: any;
    
   
    constructor(private _ApiProductsService :ApiProductsService , private _sanitizer:DomSanitizer) { }


    ngOnInit(): void {
        this._ApiProductsService.getAllProducts().subscribe({
            next:(data)=>{
          this.AllProducts=data
          
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
