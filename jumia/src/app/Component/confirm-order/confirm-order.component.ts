import { Component, ElementRef, ViewChild } from '@angular/core';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { APIOrderServiceService } from '../../Services/apiorder-service.service';
import { IOrder } from '../../Models/i-order';
import { IOrderItems } from '../../Models/iorder-items';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ApiShippmentService } from '../../Services/api-shippment.service';
import { IShippment } from '../../Models/ishippment';
import { ApiProductsService } from '../../Services/api-products.service';

@Component({
  selector: 'app-confirm-order',
  standalone: true,
  imports: [CommonModule ,TranslateModule,RouterLink],
  templateUrl: './confirm-order.component.html',
  styleUrl: './confirm-order.component.css'
})
export class ConfirmOrderComponent {
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  payment:boolean=false
  clientId=localStorage.getItem('userId')
  order:IOrder={} as IOrder
  orderItem:IOrderItems={} as IOrderItems
  shippment:IShippment={} as IShippment
  totalamount:number = 50
  showPayment: boolean = false;
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
  userId:number=0
  prd!:ProductDto
 // shippment:IShippment={} as IShippment
  @ViewChild('paymentRef', { static: true }) paymentRef!: ElementRef;

  constructor(private _cartService:CartService
    ,private router:Router,private _orderService:APIOrderServiceService,
    private  translate: TranslateService,
    private _productService:ApiProductsService
    ,private _ShippmentService:ApiShippmentService 
   
    
  ){
    
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
    });

    window.paypal.Buttons({
      createOrder: (data: any, actions: any) => {

        return actions.order.create({
          purchase_units: [
            {
              amount: {
                value: this.TotalCartPrice.toString(),
                currency: 'USD'
              }

            }


          ]

        })
      },
      onApprove:(data:any, actions:any)=>{
        return actions.order.capture().then((details:any)=>{
          console.log(details);
          if(details.status === 'COMPLETED'){
            alert("Transaction Successfll ")
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
                  this.orderItem.totalPrice=element.realPrice*element.cartQuantity
                 // element.stockQuantity-=element.cartQuantity;
                  this._orderService.AddOrderItems(this.orderItem).subscribe({
                    next: (res) => {
                      console.log(res);
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
                      
                      if(res.isSuccess){
                      this._cartService.removeProduct(element)
                    }}
                  })
                 
                },
                this.router.navigate(['/OrderDetails'])
              );
                }
              }})
          }
          
        })
      },

      onError:(error:any)=>{
        console.log(error);
        alert(error)
        
      }


    }).render(this.paymentRef.nativeElement)
    
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
  ConfirmOrder(){
   

  }
  SetOrderData(){
    
    if(this.cartNumber!=null && this.TotalCartPrice!=null &&this.clientId!=null ){
      this.order.totalAmount= this.cartNumber
      this.order.customerId=parseInt(this.clientId);
      this.order.totalOrderPrice=this.TotalCartPrice
    }
    this.order.paymentStatus=1
    this.order.status=0
    //this.order.createdDate= new Date();
    this.order.discount=10
    this.order.cancelOrder=false;
   
  } 

  
}
