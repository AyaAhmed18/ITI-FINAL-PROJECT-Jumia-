import { CommonModule, NgFor } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { IShippment } from '../../Models/ishippment';
import { IOrder } from '../../Models/i-order';
import { ApiShippmentService } from '../../Services/api-shippment.service';
import { APIOrderServiceService } from '../../Services/apiorder-service.service';
import { IUserLogin } from '../../Models/iuser-login';
import { ProductDto } from '../../ViewModels/product-dto';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-shippment',
  standalone: true,
  imports: [RouterLink,RouterOutlet,HttpClientModule,FormsModule,CommonModule,NgFor],
  templateUrl: './shippment.component.html',
  styleUrl: './shippment.component.css'
})
export class ShippmentComponent implements OnInit{
  shippment:IShippment={} as IShippment
  order:IOrder={} as IOrder
  Client=localStorage.getItem('userName')
  clientId=localStorage.getItem('userId')
  selectedRegionCities: string[] = [];
  regions: string[] = []
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  citiesByRegion: { [key: string]: string[] } = {
    'Sohag': ['Sohag City', 'Akhmim', 'Girga', 'Tahta', 'Dar El Salam', 'Saqil Qism Qena', 'Daraw', 'Juhaynah', 'Gerga', 'El Maragha', 'Tama', 'Al Monshah', 'Al Waily', 'Alawais', 'Al Hawarta', 'Dar El Salam Qism Sohag', 'Al Ghanayim', 'El Kossia', 'El Balayaza', 'Akhmim Qism Sohag', 'Sohag District', 'Akhmim District', 'Gerga District', 'El Maragha District', 'Tahta District', 'Dar El Salam District', 'Saqil Qism Qena District', 'Daraw District', 'Juhaynah District', 'Tama District'],
    'Cairo': [
      'Nasr City',
      'Maadi',
      'Zamalek',
      'Heliopolis',
      'Mohandessin',
      'Dokki',
      'Garden City',
      'New Cairo',
      'Downtown Cairo',
      'Sheikh Zayed City',
      '6th of October City',
      'Tagamo\' Al Khames',
      'Shubra',
      'Agouza',
      'Al-Mokattam',
      'Helwan',
      'Sayeda Zeinab',
      'Abbasiya',
      'Ain Shams',
      'Rod El Farag'
    ],
    'Alexandria': [
      'Al-Montazah',
      'Miami','Sidi Gaber','Al-Ibrahimia',
      'El-Mansheya', 'El-Raml', 'Bolkly','El-Gomrok','El-Labban','Kafr Abdo',  'Karmouz',
      'Mandara',
      'San Stefano',
      'Schutz',
      'Sidi Beshr',
      'Stanley',
      'Victoria','Zizinia', 'El-Awayed', 'Al-Wardian'
    ]
  };
  
  constructor(private _ShippmentService:ApiShippmentService,
    private _OrderServie:APIOrderServiceService,
    private router:Router,
    private _cartService:CartService
    ){
    //this.client.userName!=this.Client
    this.regions= [
      'Cairo',
      'Alexandria',
      'Giza',
      'Shubra El-Kheima',
      'Port Said','Suez','Luxor', 'Asyut','Ismailia','Faiyum','Zagazig','Aswan', 'Damanhur',
      'El-Mahalla El-Kubra','Beni Suef','Qena','Sohag','Minya','Hurghada', 'Shibin El Kom',
    ];
  }
  ngOnInit(): void {
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
     // this.clientId=
    });
    this.SetOrderData()
  }
  debugger: any
  AddAddress(clientAddress:IShippment){
    if(this.clientId!=null){
      clientAddress.userIdentityId=parseInt(this.clientId)
      clientAddress.cityAr="سوهاج"
      clientAddress.regionAr="سوهاج"
      clientAddress.cost=100
    }
    this._ShippmentService.AddClientAddress(clientAddress).subscribe({next:(res)=>{
      console.log(res.Entity);
      
      if (res.IsSuccess && res.Entity!=null) {
       alert("Added Successfully")
      } else if(!res.IsSuccess && res.Entity!=null) {
        //this.router.navigate(['/shippment']);
        this.shippment=res.Entity
        alert("update Address")
      }
     
    },
    error:(err)=>{
      console.log(err);
      
    }});
   
  } 

  SetOrderData(){
    
    if(this.cartNumber!=null && this.TotalCartPrice!=null &&this.clientId!=null ){
      this.order.totalAmount= this.cartNumber
     // this.order.TotalOrderPrice=this.TotalCartPrice
      this.order.customerId=parseInt(this.clientId);
    }
   // this.order.createdDate= new Date().toDateString();
    this.order.paymentStatus=0
    this.order.status=0
    this.order.cancelOrder=false;
    //this.order.discount=5;  
   
  }

 
  
  
  onRegionChange() {
    this.selectedRegionCities = this.citiesByRegion[this.shippment.region] || [];
   
  }
}
