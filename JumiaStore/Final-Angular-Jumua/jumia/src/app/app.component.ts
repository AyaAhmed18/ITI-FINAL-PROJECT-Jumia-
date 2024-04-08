import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigiationBarComponent } from './Component/navigiation-bar/navigiation-bar.component';
import { FooterComponent } from './Component/footer/footer.component';
import { ProductComponent } from './Component/product/product.component';
import { HomeComponent } from './Component/home/home.component';
import { FilterComponent } from './Component/filter/filter.component';
import { OrdersComponent } from './Component/orders/orders.component';
import { MyAccountComponent } from './Component/my-account/my-account.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavigiationBarComponent ,
    FooterComponent ,
     ProductComponent ,
      HomeComponent , FilterComponent ,
       OrdersComponent ,
        MyAccountComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
 
})
export class AppComponent {
  title = 'jumia';
}
