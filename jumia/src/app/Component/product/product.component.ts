import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FilterComponent } from "../filter/filter.component";
import { ApiProductsService } from '../../Services/api-products.service';
import { ProductDto } from '../../ViewModels/product-dto';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';


@Component({
    selector: 'app-product',
    standalone: true,
    templateUrl: './product.component.html',
    styleUrl: './product.component.css',
    imports: [FilterComponent,CommonModule]
})
export class ProductComponent implements OnInit{
   AllProducts:ProductDto[]=[];
    products: any;
    searchResults: any[] = [];
    pageSize:number = 15; 
  totalPages: number = 0; 
  pageNumber: number = 1; 
  pageNumbers: number[]=[];
 // Method to calculate pages array



    constructor(private _ApiProductsService :ApiProductsService , private _sanitizer:DomSanitizer) { }
    
    
   
    ngOnInit(): void {
        this.getAllProducts();
      }

      getAllProducts() {
        this._ApiProductsService.getAllProducts(this.pageSize, this.pageNumber).subscribe({
            next: (data) => {
                this.AllProducts = data;
                console.log( );
                
                this.totalPages=Math.ceil(data.length / this.pageSize)
                this.pageNumbers = Array.from({ length: this.totalPages }, (_, index) => index + 1);
                console.log(this.totalPages);
                
                this.sanitizeImages();
            },
            error: (err) => {
                console.log(err);
            }
        });
    }
    nextPage(): void {
      if (this.pageNumber < this.totalPages) {
        this.pageNumber++;
        this.getAllProducts();
      }
    }
  
    prevPage(): void {
      if (this.pageNumber > 1) {
        this.pageNumber--;
        this.getAllProducts();
      }
    }
  
    goToPage(page: number): void {
      if (page >= 1 && page <= this.totalPages) {
        this.pageNumber = page;
        this.getAllProducts();
      }
    }


      // getAllProducts() {
      //   this._ApiProductsService.getAllProducts().subscribe({
      //     next: (data) => {
      //       this.AllProducts = data;
      //       this.sanitizeImages();
      //     },
      //     error: (err) => {
      //       console.log(err);
      //     }
      //   });
      // }
      loadAllProductsOrderedAsc() {
        this._ApiProductsService.getAllProductsWithOrderAasc().subscribe({
          next: (data) => {
            this.AllProducts = data;
            this.sanitizeImages();
          },
          error: (err) => {
            console.log(err);
          }
        });
      }
    
      loadAllProductsOrderedDsc() {
        this._ApiProductsService.getAllProductsWithOrderDasc().subscribe({
          next: (data) => {
            this.AllProducts = data;
            this.sanitizeImages();
          },
          error: (err) => {
            console.log(err);
          }
        });
      }
    
      loadAllProductsNewestArrivals() {
        this._ApiProductsService.getAllProductsWithNewestArrivals().subscribe({
          next: (data) => {
            this.AllProducts = data;
            this.sanitizeImages();
          },
          error: (err) => {
            console.log(err);
          }
        });
      }
    
      sanitizeImages() {
        this.AllProducts.forEach(product => {
          product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images);
        });
      }
    
      onSortChange(event: any) {
        const selectedSortOption = event.target.value;
        switch (selectedSortOption) {
          case 'allProducts':
            this.getAllProducts();
            break;
          case 'PriceLowToHigh':
            this.loadAllProductsOrderedAsc();
            break;
          case 'PriceHighToLow':
            this.loadAllProductsOrderedDsc();
            break;
          case 'NewestArrivals':
            this.loadAllProductsNewestArrivals();
            break;
          default:
            this.getAllProducts();
            break;
        }
      }


      searchProducts(searchTerm: string): void {
        this._ApiProductsService.SearchByNameOrDesc(searchTerm).subscribe(
          (data: any) => {
            this.searchResults = data;
          },
          (error: any) => {
            console.error('Error fetching search results:', error);
          }
        );
        }

    // ngOnInit(): void {
    //     this._ApiProductsService.getAllProductsWithOrderAasc().subscribe({
    //         next:(data)=>{
    //       this.AllProducts=data
          
    //       this.AllProducts.forEach(Product => {
            
    //         Product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + Product.images);
    //      });

    //       },
    //       error:(err)=>{
          
    //       console.log(err)
    //       }
          
    //       })
    // } 
  
}
