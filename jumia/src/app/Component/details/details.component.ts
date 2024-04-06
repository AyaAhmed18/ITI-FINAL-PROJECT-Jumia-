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
export class DetailsComponent implements OnInit {
  currentProdId: number = 0;
  product: ProductDto | null = null;

  constructor(
    private activatedRoute: ActivatedRoute,
    private apiProductsService: ApiProductsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(paramMap => {
      this.currentProdId = Number(paramMap.get('id'));
      this.getProductById(this.currentProdId);
    });
  }

  getProductById(id: number): void {
    this.apiProductsService.getProductById(id).subscribe({
      next: (res: ProductDto) => {
        this.product = res;
        console.log("detalse")
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
}