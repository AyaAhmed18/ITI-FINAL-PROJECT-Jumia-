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
import { ReloadService } from './Services/reload.service';

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
export class AppComponent implements OnInit{

  lang:any
  
  constructor(private translate:TranslateService,private reloadService: ReloadService){
   this.lang =  localStorage.getItem('lang')
    translate.use(this.lang);
    

  }
  
  ngOnInit() {
    // Check if the reload count is less than 2
    if (this.reloadService.getReloadCount() < 2) {
      setTimeout(() => {
      //  window.location.reload();
        // Increment the reload count after reloading
        this.reloadService.incrementReloadCount();
        // Reset reload count if it's now 2
        if (this.reloadService.getReloadCount() >= 2) {
          this.reloadService.resetReloadCount();
        }
      }, 2000); // Adjust the interval as needed
    }
  }
  }

  
  


 
 

