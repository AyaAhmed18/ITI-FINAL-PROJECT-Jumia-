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
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavigiationBarComponent ,
    FooterComponent ,
     ProductComponent ,
      HomeComponent , FilterComponent ,
       OrdersComponent ,
        MyAccountComponent,HttpClientModule, TranslateModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
 
})
export class AppComponent {

  lang:any
  
  constructor(private translate:TranslateService){
   this.lang =  localStorage.getItem('lang')
    translate.use(this.lang);

  }


  
  // changeLangeue(lang: string) {
  //   this.translate.use(lang); 
  // }


 
 
}
