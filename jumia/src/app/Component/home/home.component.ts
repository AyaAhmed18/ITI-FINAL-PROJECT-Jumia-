import { CommonModule, IMAGE_CONFIG } from '@angular/common';
import { Component,OnInit  } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ApiProductsService } from '../../Services/api-products.service';
import { CategoryserviceService } from '../../Services/categoryservice.service';
import { SubcategoryserviceService } from '../../Services/subcategoryservice.service';
import { SliderComponent } from '../slider/slider.component';
// const imgesUrl=require("../../../assets/ProductImg/ApplianceEN.png");
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink,RouterOutlet,SliderComponent,CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',

  providers: [
    {
      provide: IMAGE_CONFIG,
      useValue: {
        disableImageSizeWarning: true,
        disableImageLazyLoadWarning: true
      }
    }
  ],
})
export class HomeComponent {
  allCategories : any[]=[];
  SubCategories : any[]=[];
  allProducts : any[]=[];
  SelectedCategoryId : number=0;
constructor(private _categoryService :CategoryserviceService
  ,private _subCategoryService :SubcategoryserviceService
  ,private _apiProductsService: ApiProductsService
  ,private _router : Router)
{

}
ngOnInit(): void {
this.GetCategories();
}
GetCategories()
    {
      this._categoryService.getAllCategory()
      .subscribe({ next: (data) => {
        this.allCategories = data;
        console.log("allCategories")
        console.log(data)
      }
      });
    }
GetSubCategories(categoryId: number)
{
  this._subCategoryService.getSubCategoryByCategoryId(categoryId)
  .subscribe({ next: (data) => {
    this.SubCategories = data;
    console.log("SubCategories")
    console.log(data)
  }
  });
}
GetProductsByCatId(categoryId: number):void
{
  this._router.navigateByUrl(`/GetCategory/${categoryId}`);
}
GetProductsBySubCatId(subcategoryId: number):void
{
  this._router.navigateByUrl(`/GetSubCategory/${subcategoryId}`);
}

}
