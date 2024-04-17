import { Component, OnInit } from '@angular/core';
import { APIOrderServiceService } from '../../Services/apiorder-service.service';
import { IOrderItems } from '../../Models/iorder-items';
import { ActivatedRoute, Router } from '@angular/router';
import { IOrder } from '../../Models/i-order';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { DomSanitizer } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ApiShippmentService } from '../../Services/api-shippment.service';
import { IShippment } from '../../Models/ishippment';

@Component({
  selector: 'app-order-items',
  standalone: true,
  imports: [TranslateModule,CommonModule],
  templateUrl: './order-items.component.html',
  styleUrl: './order-items.component.css'
})
export class OrderItemsComponent implements OnInit{
  OrderItems:IOrderItems[]=[] as IOrderItems[]
  constructor(private _OrderService:APIOrderServiceService,private route: ActivatedRoute,
    private router:Router ,private  translate: TranslateService,
    private _sanitizer:DomSanitizer,
    private _shippment:ApiShippmentService
  ){}
  ordId:number=0
  ItemsNumber:number=0
  showAlert1: boolean = false;
  showAlert2: boolean = false;
  clientId=localStorage.getItem('userId')
  userId:number=0
  isArabic: boolean = localStorage.getItem('isArabic') === 'true';
 shippment:IShippment={} as IShippment
  order: IOrder  = {} as IOrder;
  ngOnInit(): void {
    this.userId=Number(this.clientId)
    this.ordId = Number(this.route.snapshot.paramMap.get('ordId'));
    console.log('Order ID:', this.ordId);
    this._OrderService.GetOrder(this.ordId).subscribe(order => {
      this.order = order;
      console.log(this.order);
    });
    this._shippment.Getshippment(this.userId).subscribe(shipping => {
      this.shippment=shipping
      console.log(shipping.firstNameEn)
     });
    this._OrderService.GetOrderItems(this.ordId).subscribe(OrderItems => {
     this.OrderItems=OrderItems
     this.ItemsNumber=OrderItems.length
     this.sanitizeImages()
    });

    this.translate.onLangChange.subscribe((Event)=>{
      this.isArabic = Event.lang === 'ar'
    })
   }
   
   isArabicLanguage(): boolean {
    return this.translate.currentLang === 'ar'; 
  }
  sanitizeImages() {
    this.OrderItems.forEach(product => {
      console.log(product.images);
      product.images = this._sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + product.images);
      console.log(product.images);
      console.log(product.images);

    });
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
   
   cancelOrder(){
    console.log(this.order.status);
    if(this.order.status>=2){
      this.showAlert1 = true;
     // this.router.navigate(['/OrderDetails']);
    }
    else{
      this.order.status=4
      this._OrderService.UpdateOrder(this.order).subscribe(order => {
        this.order=order
        
        });
        this.showAlert2 = true;
        this.router.navigate(['/OrderDetails']);
       
    }
    
   }
  
   closeAlert() {
    this.showAlert1 = false;
    this.showAlert2 = false;
  }
  getPaymentStatusWord(): string {
    switch (this.order.paymentStatus) {
      case 0:
        return "Pending";
      case 1:
        return "PayPall";
      case 2:
        return "Mobile Money";
      case 3:
        return "Cash";
      default:
        return "UNConfirmed";
    }
  }
increaseDate(days: number): { dayName: string, dayNumber: number, monthName: string } {
  let date = new Date(this.order.createdDate);
  date.setDate(date.getDate() + days);


  const dayName = new Intl.DateTimeFormat('en-US', { weekday: 'long' }).format(date);
  const dayNumber = date.getDate();
  const monthName = new Intl.DateTimeFormat('en-US', { month: 'long' }).format(date);

  return { dayName, dayNumber, monthName };
}
  
}
