import { Routes } from '@angular/router';
import { HomeComponent } from './Component/home/home.component';
import { ProductComponent } from './Component/product/product.component';
import { SignInComponent } from './Component/sign-in/sign-in.component';
import { OrdersComponent } from './Component/orders/orders.component';
import { DelivaryComponent } from './Component/delivary/delivary.component';
import { MyAccountComponent } from './Component/my-account/my-account.component';
import { CartComponent } from './Component/cart/cart.component';

export const routes: Routes = [
    {path:'',redirectTo:'/Home',pathMatch:'full'},
    {path:'Home',component:HomeComponent},
    {path:'Product',component:ProductComponent},
    {path:'SignIn',component:SignInComponent},
    {path:'Order',component:OrdersComponent},
    {path:'Delivary',component:DelivaryComponent},
    {path:'MyAccount',component:MyAccountComponent},
    {path:'Cart',component:CartComponent},

];
