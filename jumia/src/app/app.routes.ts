import { Routes } from '@angular/router';
import { HomeComponent } from './Component/home/home.component';
import { ProductComponent } from './Component/product/product.component';
import { SignInComponent } from './Component/sign-in/sign-in.component';
import { OrdersComponent } from './Component/orders/orders.component';
import { DelivaryComponent } from './Component/delivary/delivary.component';
import { MyAccountComponent } from './Component/my-account/my-account.component';
import { CartComponent } from './Component/cart/cart.component';
import { authGuard } from './guards/auth.guard';
import { ShippmentComponent } from './Component/shippment/shippment.component';
import { PaymentComponent } from './Component/payment/payment.component';
import { RegistrationComponent } from './Component/registration/registration.component';
import { FilterComponent } from './Component/filter/filter.component';
import { OrderDetailsComponent } from './Component/order-details/order-details.component';
import { OrderItemsComponent } from './Component/order-items/order-items.component';
export const routes: Routes = [
    {path:'',redirectTo:'/Home',pathMatch:'full'},
    {path:'Home',component:HomeComponent},
    {path:'Product',component:FilterComponent},
    {path:'SignIn',component:SignInComponent},
    {path:'Registeration',component:RegistrationComponent},
    {path:'Order',component:OrdersComponent , canActivate:[authGuard]},  //
    {path:'Delivary',component:DelivaryComponent},
    {path:'MyAccount',component:MyAccountComponent , canActivate:[authGuard]},  //
    {path:'Cart',component:CartComponent},
    {path:'shippment',component:ShippmentComponent,canActivate:[authGuard]},
    {path:'Payment',component:PaymentComponent,canActivate:[authGuard]},
    {path:'OrderDetails',component:OrderDetailsComponent,canActivate:[authGuard]},
    {path:'OrderItems/:ordId',component:OrderItemsComponent,canActivate:[authGuard]},
   // {path:'CartProduct',component:CartwithProductComponent},
   // {path:'test',component:TestComponent},


];
