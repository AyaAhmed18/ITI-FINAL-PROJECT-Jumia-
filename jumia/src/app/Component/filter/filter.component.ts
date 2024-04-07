import { Component } from '@angular/core';
import { ProductComponent } from "../product/product.component";
import { FilterServiceService } from '../../Services/filter-service.service';
import { CommonModule} from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrandServiceService } from '../../Services/brand-service.service';
import { IBrandDto } from '../../ViewModels/ibrand-dto';
import { ProductDto } from '../../ViewModels/product-dto';

@Component({
    selector: 'app-filter',
    standalone: true,
    templateUrl: './filter.component.html',
    styleUrl: './filter.component.css',
    imports: [ProductComponent,FormsModule,CommonModule]
})
export class FilterComponent {
    minDiscount: number=0;
    ListPrand:string='';
    products: any[]=[];
    minPrice: number = 0;
    maxPrice: number = 1000000;
    AllBrands: any = [];
    selectedBrands: number[] = [];
    selectedBrandsStr : string ='';
    totalPages: number = 0;
    pageNumber: number = 1;
    pageNumbers: number[]=[];
    AllProd:number=0;
    pageSize:number = 10;

    constructor(private _filterService: FilterServiceService,private _brandService : BrandServiceService) { }

    ngOnInit(): void {
        console.log(this.minDiscount)
      this.filterProducts();
      this.GetBrands();
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





}


// const rangeInput = document.querySelectorAll<HTMLInputElement>(".range-input input"),
//     priceInput = document.querySelectorAll<HTMLInputElement>(".price-input input"),
//     range = document.querySelector<HTMLElement>(".slider .progress");
// let priceGap = 1000;

// priceInput.forEach((input) => {
//     input.addEventListener("input", (e) => {
//         let minPrice = parseInt(priceInput[0].value),
//             maxPrice = parseInt(priceInput[1].value);

//         if (maxPrice - minPrice >= priceGap && maxPrice <= parseInt(rangeInput[1].getAttribute("max") || "0")) {
//             if (e.target?.classList.contains("input-min")) {
//                 (rangeInput[0] as HTMLInputElement).value = minPrice.toString();
//                 if (range)
//                     range.style.left = (minPrice / parseInt((rangeInput[0] as HTMLInputElement).max) * 100) + "%";
//             } else {
//                 (rangeInput[1] as HTMLInputElement).value = maxPrice.toString();
//                 if (range)
//                     range.style.right = (100 - (maxPrice / parseInt((rangeInput[1] as HTMLInputElement).max) || 0) * 100) + "%";
//             }
//         }
//     });
// });

// rangeInput.forEach((input) => {
//     input.addEventListener("input", (e) => {
//         let minVal = parseInt((rangeInput[0] as HTMLInputElement).value),
//             maxVal = parseInt((rangeInput[1] as HTMLInputElement).value);

//         if (maxVal - minVal < priceGap) {
//             if (e.target?.classList.contains("range-min")) {
//                 (rangeInput[0] as HTMLInputElement).value = (maxVal - priceGap).toString();
//             } else {
//                 (rangeInput[1] as HTMLInputElement).value = (minVal + priceGap).toString();
//             }
//         } else {
//             (priceInput[0] as HTMLInputElement).value = minVal.toString();
//             (priceInput[1] as HTMLInputElement).value = maxVal.toString();
//             if (range) {
//                 range.style.left = ((minVal / parseInt((rangeInput[0] as HTMLInputElement).max) || 0) * 100) + "%";
//                 range.style.right = (100 - (maxVal / parseInt((rangeInput[1] as HTMLInputElement).max) || 0) * 100) + "%";
//             }
//         }
//     });
// });
