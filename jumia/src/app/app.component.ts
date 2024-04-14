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
import { ApiLoginService } from './Services/api-login.service';

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
  IsUserLogged: boolean = false
  loggedInUsername: string;
  isArabic: boolean = false;
  constructor(private translate:TranslateService,
    private reloadService: ReloadService,private _apiLoginService: ApiLoginService){
      this.loggedInUsername = this._apiLoginService.getLoggedInUsername();
   this.lang =  localStorage.getItem('lang')
  
    translate.use(this.lang);
    

  }
  
  ngOnInit() { 
    this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
      localStorage.setItem("isArabic",this.isArabic.toString())
    })
   
    //localStorage.removeItem("isArabic")
  }
 reload(){
  if (this.reloadService.getReloadCount() < 2) {
    setTimeout(() => {
      window.location.reload();
      // Increment the reload count after reloading
      this.reloadService.incrementReloadCount();
      // Reset reload count if it's now 2
      if (this.reloadService.getReloadCount() >= 2) {
        this.reloadService.resetReloadCount();
      }
    }, 2000); // Adjust the interval as needed
  }
 }
 changeLanguage(lang: string) {
  if (lang == 'en') {
    localStorage.setItem('lang', 'en')
  }
  else {
    localStorage.setItem('lang', 'ar')
  }
     
  window.location.reload();

}
  }

  
  


 
 

