import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent implements OnInit{
  currenntProdId:number=0;
  product:ProductDto|null=null;

 
    constructor(
      private _Activatedroute:ActivatedRoute , 
      private _ApiProductsService:ApiProductsService,
     
      private  router:Router
      ){}
      ngOnInit(): void {
        this._Activatedroute.paramMap.subscribe(paramMap => {
          this.currenntProdId = Number(paramMap.get('id'));
          this.getProductById(this.currenntProdId);
        });
      }
  
      getProductById(id: number): void {
        this._ApiProductsService.getProductById(id).subscribe({
          next: (res) => {
            this.product = res;
            console.log(this.product);
          },
          error: (err) => {
            console.log(err);
          }
        });
      }
      navigateToDetails(productId: number): void {
        this.router.navigateByUrl(`/details/${productId}`);
      }
  }
  