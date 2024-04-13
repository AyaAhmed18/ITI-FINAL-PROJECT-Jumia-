import { Component, ElementRef, ViewChild } from '@angular/core';
import { ProductComponent } from "../product/product.component";
import { FilterServiceService } from '../../Services/filter-service.service';
import { CommonModule} from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrandServiceService } from '../../Services/brand-service.service';
import { IBrandDto } from '../../ViewModels/ibrand-dto';
import { ProductDto } from '../../ViewModels/product-dto';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { NgxSliderModule, Options } from '@angular-slider/ngx-slider';

@Component({
    selector: 'app-filter',
    standalone: true,
    templateUrl: './filter.component.html',
    styleUrl: './filter.component.css',
    imports: [ProductComponent,FormsModule,CommonModule,TranslateModule , NgxSliderModule]
})
export class FilterComponent {
    minDiscount: number=0;
    ListPrand:string='';
    products: any[]=[];
    
    AllBrands: any = [];
    selectedBrands: number[] = [];
    selectedBrandsStr : string ='';
    totalPages: number = 0;
    pageNumber: number = 1;
    pageNumbers: number[]=[];
    AllProd:number=0;
    pageSize:number = 10;
   
    //@ViewChild('filterComponent') filterComponent: FilterComponent | undefined; // Replace with child component type

    currentCategoryId: number = 0;
  currentSubCategoryId: number = 0;
  isArabic: boolean = false;

  minPrice: number = 0;
  maxPrice: number = 1000000;
  value=44;
  highvalue=1000;
  options:Options={
    step:10,
    floor:0,
    ceil:10000
  }


  
    constructor(private _filterService: FilterServiceService,
      private _brandService : BrandServiceService
      ,private _router : Router, private _activeRouter: ActivatedRoute,
      private _sanitizer:DomSanitizer,
      private  translate: TranslateService 
      
      ) { }

    ngOnInit(): void {
      
      this.translate.onLangChange.subscribe((Event)=>{
        this.isArabic = Event.lang === 'ar'
      })

      console.log("Starting Fillter")
      this.GetBrands();
      //this.filterProducts();


      console.log(this.minDiscount)
    const currentRoute = this._router.url;
    console.log(currentRoute)
    if (currentRoute.includes('GetSubCategory')) {
      //this.products = []
      this._activeRouter.paramMap.subscribe(parammap=>
        {
          this.currentSubCategoryId =Number(parammap.get('id'));
          console.log(this.currentSubCategoryId);
          this.getProductBySubCategoryId(this.currentSubCategoryId);
        })
      console.log("SubCategory")
      console.log();
      
    }
    else if (currentRoute.includes('GetCategory')) {
      //this.products =
      this._activeRouter.paramMap.subscribe(parammap=>
        {
          this.currentCategoryId =Number(parammap.get('id'));
          console.log(this.currentCategoryId);
          this.getProductByCategoryId(this.currentCategoryId);
        })
      console.log("Category")

    }
    else
    {
      console.log("Filter is working")
      this.filterProducts();
    }
    }

    GetBrands()
    {
      this._brandService.getAllBrands()
      .subscribe({ next: (data) => {
        this.AllBrands = data;
        console.log("allBrands")
        console.log( data)
      }
      });
    }
    getProductByCategoryId(id:number)
    {
      this._filterService.getProductByCatId(id).subscribe(
      {
        next:(data: any)=>{
          this.products=data.entities
          console.log(data);
          console.log("ProductList")
          console.log(this.products);
          this.sanitizeImages();
        }
      })
    }
    getProductBySubCategoryId(id:number)
    {
      this._filterService.getProductBySubCatId(id).subscribe(
      {
        next:(data: any)=>{
          this.products=data.entities
          console.log(data);
          console.log("ProductList12")
          console.log(this.products);
          console.log(this.products[1].images[0])
          this.sanitizeImages();
        }
        
      })
    }
    filterProducts(): void {
      this.selectedBrandsStr = this.selectedBrands.join(',');
      console.log("selected Brands");
      console.log(this.selectedBrands)
      console.log(this.selectedBrandsStr)

      this._filterService.filterByAll(this.minDiscount, this.minPrice, this.maxPrice  , this.selectedBrandsStr, this.pageNumber,this.pageSize)
      .subscribe(data => {
        this.products = data.entities;
      this.AllProd = data.count;

      this.totalPages=Math.ceil( this.AllProd / this.pageSize)
      this.pageNumbers = Array.from({ length: this.totalPages }, (_, index) => index + 1);
        console.log("BehaviorSubject")
        console.log(this._filterService.getValue());

        console.log("selected Brands");
        console.log(this.selectedBrands)
        console.log(this.selectedBrandsStr)


        this.products = data.entities;
        console.log(data.count);

        console.log(this.products[1].images[0])

        console.log("filter--")
        console.log( this.products)
        this.sanitizeImages();
      });




    }


    AddtoSelected($event: any,arg1: any) {
      console.log($event.target.checked)
      console.log(this.selectedBrands);
      console.log(arg1);

      if($event.target.checked)
      {
        let brId = arg1.brandID;
        console.log(brId);
        this.selectedBrands.push(arg1.brandID);

        console.log(this.selectedBrandsStr);
      }

      }
      nextPage(): void {
        if (this.pageNumber < this.totalPages) {
          console.log( this.pageNumber);

          this.pageNumber++;
          console.log( this.pageNumber);


        console.log();
          //this.getAllProducts();
          this.filterProducts();
        }
        }

        prevPage(): void {
        if (this.pageNumber > 1) {
          this.pageNumber--;
          //this._filterServices.setValue(this.pageNumber);
          //this.getAllProducts();
          this.filterProducts();

        }
        }
        goToPage(page: number): void {
        if (page >= 1 && page <= this.totalPages) {
          this.pageNumber = page;
          //this._filterServices.setValue(this.pageNumber);
          //this.getAllProducts();
          this.filterProducts();

        }
        }
 // this._filterService.filterByDiscountRange(this.minDiscount)
      //   .subscribe(data => {
      //     this.products = data.entities;
      //     console.log("filter")
      //     console.log( this.products)
      //   });
      // this._filterService.filterByPriceRange(this.minPrice, this.maxPrice)
      //   .subscribe(data => {
      //     this.products = data.entities;
      //     console.log("filter")
      //     console.log( this.products)
      //   });

      sanitizeImages() {
        this.products.forEach(product => {
          console.log(product.images);
          product.images[0] = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images[0]);
          console.log(product.images);
          console.log(product.images);

        });
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

clearSelection() {
  this.minDiscount = 0; 
 
  this.filterProducts();
}


// ngAfterViewInit(): void {
//   const rangeInput = this.range.nativeElement.querySelectorAll("input"),
//         priceInput = [this.minPriceInput.nativeElement, this.maxPriceInput.nativeElement],
//         range = this.range.nativeElement.querySelector(".progress");

//   priceInput.forEach((input: HTMLInputElement) => {
//     input.addEventListener("input", (e) => {
//       if (e.target) {
//         let minPrice = parseInt(priceInput[0].value),
//             maxPrice = parseInt(priceInput[1].value);

//         if (maxPrice - minPrice >= this.priceGap && maxPrice <= parseInt(rangeInput[1].max)) {
//           if ((e.target as HTMLInputElement).className === "input-min") {
//             rangeInput[0].value = minPrice.toString();
//             range.style.left = (minPrice / parseInt(rangeInput[0].max)) * 100 + "%";
//           } else {
//             rangeInput[1].value = maxPrice.toString();
//             range.style.right = (100 - (maxPrice / parseInt(rangeInput[1].max)) * 100) + "%";
//           }
//         }
//       }
//     });
//   });

//   rangeInput.forEach((input: HTMLInputElement) => {
//     input.addEventListener("input", (e) => {
//       if (e.target) {
//         let minVal = parseInt(rangeInput[0].value),
//             maxVal = parseInt(rangeInput[1].value);

//         if (maxVal - minVal < this.priceGap) {
//           if ((e.target as HTMLInputElement).className === "range-min") {
//             rangeInput[0].value = (maxVal - this.priceGap).toString();
//           } else {
//             rangeInput[1].value = (minVal + this.priceGap).toString();
//           }
//         } else {
//           priceInput[0].value = minVal.toString();
//           priceInput[1].value = maxVal.toString();
//           range.style.left = (minVal / parseInt(rangeInput[0].max)) * 100 + "%";
//           range.style.right = (100 - (maxVal / parseInt(rangeInput[1].max)) * 100) + "%";
//         }
//       }
//     });
//   });
// }
}