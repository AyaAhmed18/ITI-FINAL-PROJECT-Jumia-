import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigiationBarComponent } from './Component/navigiation-bar/navigiation-bar.component';
import { FooterComponent } from './Component/footer/footer.component';
import { ProductComponent } from './Component/product/product.component';
import { HomeComponent } from './Component/home/home.component';
import { FilterComponent } from './Component/filter/filter.component';
import { OrdersComponent } from './Component/orders/orders.component';
import { MyAccountComponent } from './Component/my-account/my-account.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { APICategoryService } from './Services/apicategory.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavigiationBarComponent ,
    FooterComponent ,
     ProductComponent ,
      HomeComponent , FilterComponent ,
       OrdersComponent ,
        MyAccountComponent,HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
 
})
export class AppComponent implements OnInit{
  products:any=[]
  constructor(private apiCategory:APICategoryService){

  }
  // fetchProducts(){
  //   this.httpClient.get('http://localhost:64866/api/Category/4').subscribe((data:any)=>{
  //     this.products=data;
  //   console.log(data);
  //   }) 
  // }
  ngOnInit(): void {
    this.apiCategory.fetchCategories;
  }
  title = 'jumia';
  //httpClient=inject(HttpClient)
}
