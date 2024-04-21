import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IOrderItems } from '../../Models/iorder-items';
import { IOrder } from '../../Models/i-order';
import { APIOrderServiceService } from '../../Services/apiorder-service.service';
import { ApiShippmentService } from '../../Services/api-shippment.service';
import { IShippment } from '../../Models/ishippment';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ApiProductsService } from '../../Services/api-products.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink ,TranslateModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  payment:boolean=false
  selectedPayment!: string;
  clientId=localStorage.getItem('userId')
  order:IOrder={} as IOrder
  orderItem:IOrderItems={} as IOrderItems
  userId:number=0
  shippment:IShippment={} as IShippment
  totalamount:number = 50
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';

  @ViewChild('paymentRef', { static: true }) paymentRef!: ElementRef;

  constructor(private _cartService:CartService,private router:Router
    ,private _orderService:APIOrderServiceService,
    private _ShippmentService:ApiShippmentService,private  translate: TranslateService,
  private _productService:ApiProductsService ){
    
  }
  ngOnInit(): void {
    this.userId=Number(this.clientId)
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
    this._ShippmentService.Getshippment(this.userId).subscribe(shipping => {
      this.shippment=shipping
      console.log(shipping.firstNameEn)
     });
     this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
    })
  }
  
 
  //
  ConfirmPayment(){
   // this.payment=true
    console.log('Selected Payment Method:', this.selectedPayment);
    if(this.selectedPayment=="Paypal"){
      this.router.navigate(['/confirmOrder']);
    }
    else{
      this.payment=true
    }
  }

  SetOrderData(){
    
    if(this.cartNumber!=null && this.TotalCartPrice!=null &&this.clientId!=null ){
      this.order.totalAmount= this.cartNumber
      this.order.customerId=parseInt(this.clientId);
      this.order.totalOrderPrice=this.TotalCartPrice
    }
    this.order.paymentStatus=2
    this.order.status=0
    this.order.cancelOrder=false;
   // this.order.createdDate= new Date().toDateString();
    this.order.discount=10
   
  } 

  confirmOrder(){
    this.SetOrderData()
    this._orderService.AddOrder(this.order).subscribe({
      next: (res) => {
        console.log(res);
        
        if(res.isSuccess){
          alert("your Order Saved Successfully")
          this.cartItems.forEach(element => {
          this.orderItem.productQuantity=element.cartQuantity
          this.orderItem.orderId=res.entity.id
          this.orderItem.discount=element.discount
          this.orderItem.productId=element.id
          console.log(element.id);
          console.log(res.entity.id);
          console.log(element.stockQuantity);
          
          this.orderItem.totalPrice=element.realPrice*element.cartQuantity
         // element.stockQuantity-=element.cartQuantity;
          this._orderService.AddOrderItems(this.orderItem).subscribe({
            next: (res) => {
              console.log("iteeeems");

               //Update product quantity

               this._productService.getProductById(element.id).subscribe({
                next:  (res: ProductDto) => {
                  res.stockQuantity-=element.cartQuantity
                  this._productService.UpdateProductQuantity(res).subscribe({
                   next:(res:ProductDto)=>{
                    console.log(res.stockQuantity);
                   }
                   })
                 
                  }}) 
                   //end Update product quantity
              this._cartService.removeProduct(element)
           //   this._productService.UpdateProductQuantity(element).subscribe({next:(res)=>{alert("Quantity updated")}})
              console.log(res);
              if(res.isSuccess){
                this._productService.UpdateProductQuantity(element).subscribe({next:(res)=>{alert("Quantity updated")}})
             
              this._cartService.removeProduct(element)
             // alert("your cart is empty")
            }}
          })
         
        },
        this.router.navigate(['/OrderDetails'])
      );
        }
      }})
  }
}
