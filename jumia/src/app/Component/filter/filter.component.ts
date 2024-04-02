import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormsModule } from '@angular/forms';
import { FilterProductService } from '../../Services/filter-product.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { ProductComponent } from '../product/product.component';

@Component({
  selector: 'app-filter',
  standalone: true,
  imports: [FormsModule,CommonModule,ProductComponent],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterComponent implements  OnInit {
  @Output() filterSelected  = new EventEmitter<string>();

  // onFilterChange(value: string) {
  //   this.filterSelected.emit(value);
  // }
  minDiscount : number = 0;
  products : ProductDto[] = [];
  constructor(private _filterService:FilterProductService){}
  ngOnInit():void
  {
    this.filterProducts();

  }
  filterProducts():void
  {
    this._filterService.filterByDiscountRange(this.minDiscount).subscribe(
      (data)=>
    {
      this.products = data as ProductDto[]; 
    });
  }

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
