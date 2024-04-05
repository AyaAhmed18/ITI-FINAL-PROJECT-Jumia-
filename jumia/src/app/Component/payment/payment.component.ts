import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  payment:boolean=false

  totalamount:number = 50

  @ViewChild('paymentRef', { static: true }) paymentRef!: ElementRef;

  constructor(private _cartService:CartService,private router:Router){
    
  }
  ngOnInit(): void {
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });

    window.paypal.Buttons({

    

      createOrder: (data: any, actions: any) => {

        return actions.order.create({
          purchase_units: [
            {
              amount: {
                value: this.totalamount.toString(),
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
          }
          
        })
      },

      onError:(error:any)=>{
        console.log(error);
        alert(error)
        
      }


    }).render(this.paymentRef.nativeElement)
    
  }


  ConfirmPayment(){
    this.payment=true
  }
}
