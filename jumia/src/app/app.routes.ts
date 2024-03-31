import { Routes } from '@angular/router';
import { HomeComponent } from './Component/home/home.component';
import { ProductComponent } from './Component/product/product.component';
import { SignInComponent } from './Component/sign-in/sign-in.component';
import { OrdersComponent } from './Component/orders/orders.component';
import { DelivaryComponent } from './Component/delivary/delivary.component';
import { MyAccountComponent } from './Component/my-account/my-account.component';
import { CartComponent } from './Component/cart/cart.component';
import { authGuard } from './guards/auth.guard';
import { RegistrationComponent } from './Component/registration/registration.component';
export const routes: Routes = [
    {path:'',redirectTo:'/Home',pathMatch:'full'},
    {path:'Home',component:HomeComponent},
    {path:'Registration',component:RegistrationComponent},
    {path:'Product',component:ProductComponent},
    {path:'SignIn',component:SignInComponent},
    {path:'Order',component:OrdersComponent , canActivate:[authGuard]},  //
    {path:'Delivary',component:DelivaryComponent},
    {path:'MyAccount',component:MyAccountComponent , canActivate:[authGuard]},  //
    {path:'Cart',component:CartComponent},
   // {path:'CartProduct',component:CartwithProductComponent},
   // {path:'test',component:TestComponent},


];
