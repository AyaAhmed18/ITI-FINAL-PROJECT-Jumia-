import { CommonModule, NgFor } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';
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
  //Client=localStorage.getItem('userName')
  clientId=Number(localStorage.getItem('userId'))
  selectedRegionCities: string[] = [];
  regions: string[] = []
  cartItems: ProductDto[] = [];
  cartNumber:number=0
  TotalCartPrice=0
  id:number=0
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
  citiesByRegionAr: { [key: string]: string[] } = {
    'Sohag': [
      'مدينة سوهاج',
      'أخميم',
      'جرجا',
      'طهطا',
      'دار السلام',
      'ساقل قسم قنا',
      'درو',
      'جهينة',
      'جرجا',
      'المراغة',
      'تما',
      'المنشاة',
      'الويلي',
      'العويس',
      'الحوارتة',
      'دار السلام قسم سوهاج',
      'الغنيم',
      'القوصية',
      'البلايزة',
      'أخميم قسم سوهاج',
      'مديرية سوهاج',
      'مديرية أخميم',
      'مديرية جرجا',
      'مديرية المراغة',
      'مديرية طهطا',
      'مديرية دار السلام',
      'مديرية ساقل قسم قنا',
      'مديرية درو',
      'مديرية جهينة',
      'مديرية تما'
    ],
    'القاهرة': [
      'مدينة نصر',
      'المعادي',
      'الزمالك',
      'مصر الجديدة',
      'المهندسين',
      'الدقي',
      'جاردن سيتي',
      'القاهرة الجديدة',
      'وسط البلد',
      'مدينة الشيخ زايد',
      'مدينة 6 أكتوبر',
      'التجمع الخامس',
      'شبرا',
      'العجوزة',
      'المقطم',
      'حلوان',
      'السيدة زينب',
      'العباسية',
      'عين شمس',
      'روض الفرج'
    ],
    'الإسكندرية': [
      'المنتزة',
      'ميامي',
      'سيدي جابر',
      'الإبراهيمية',
      'المنشية',
      'الرمل',
      'بولكلي',
      'الجمرك',
      'اللبان',
      'كفر عبده',
      'كرموز',
      'مندرة',
      'سان ستيفانو',
      'شط العرب',
      'سيدي بشر',
      'ستانلي',
      'فيكتوريا',
      'زيزينيا',
      'العوايد',
      'الورديان'
    ]
  };
  regionAr = [
    'القاهرة',
    'الإسكندرية',
    'الجيزة',
    'شبرا الخيمة',
    'بورسعيد',
    'السويس',
    'الأقصر',
    'أسيوط',
    'الإسماعيلية',
    'الفيوم',
    'الزقازيق',
    'أسوان',
    'دمنهور',
    'المحلة الكبرى',
    'بني سويف',
    'قنا',
    'سوهاج',
    'المنيا',
    'الغردقة',
    'شبين الكوم',
  ];
  
  constructor(private _ShippmentService:ApiShippmentService,
    private _OrderServie:APIOrderServiceService,
    private router:Router,
    private _cartService:CartService,
    private route: ActivatedRoute
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
    this._ShippmentService.Getshippment(this.clientId).subscribe(shippment => {
     // this.shippment=shippment.entity
      console.log(shippment);
      if(shippment!=null){
       this.shippment=shippment
       this.onRegionChange()
       this.onCityChange()
       console.log(shippment);
       this.shippment.city=shippment.city
       console.log(shippment.city);
      }
   });
    this._cartService.getCart().subscribe(cartItems => {
      this.cartItems = cartItems;
     this.TotalCartPrice= this._cartService.calculateTotalCartPrice();
      this.cartNumber=this._cartService.calculateTotalCartNumber();
    });
    this.id = Number(this.route.snapshot.paramMap.get('id'))
   
  
  }
  AddAddress(clientAddress:IShippment){
    if(this.clientId!=null){
      clientAddress.userIdentityId=this.clientId
            clientAddress.cost=100
    }
   
    this._ShippmentService.AddClientAddress(clientAddress).subscribe({
      next: (res) => {
        if (res.isSuccess && res.entity != null) {
          alert("Added Successfully");
          this.router.navigate(['/Delivary']);
        } else if (!res.isSuccess && res.entity != null) {
          alert("Your Information already Saved")
          //this.router.navigate(['/Delivary']);
          this._ShippmentService.UpdateClientAddress(clientAddress).subscribe({
            next: (updateRes) => {
              this.shippment = updateRes.entity;
              if (updateRes.isSuccess) {
               
                alert("Your Information Saved successfully")
                this.router.navigate(['/Delivary']);
              }

            },
            error: (updateErr) => {
              console.log(updateErr);
            }
          });
        }
      },
      error: (err) => {
        console.log(err);
      }
    });
   
  } 

  SetOrderData(){
    
    if(this.cartNumber!=null && this.TotalCartPrice!=null &&this.clientId!=null ){
      this.order.totalAmount= this.cartNumber
     // this.order.TotalOrderPrice=this.TotalCartPrice
      this.order.customerId=this.clientId;
    }
   // this.order.createdDate= new Date().toDateString();
    this.order.paymentStatus=0
    this.order.status=0
    this.order.cancelOrder=false;
    //this.order.discount=5;  
   
  } 
  onRegionChange() {
    this.selectedRegionCities = this.citiesByRegion[this.shippment.region] || [];
    const index = this.regions.indexOf(this.shippment.region);
    if (index !== -1) {
      this.shippment.regionAr = this.regionAr[index];
    }
    const indexAr = this.regionAr.indexOf(this.shippment.regionAr);
    if (indexAr !== -1) {
      this.shippment.region = this.regions[indexAr];
    }
  }
  onCityChange() {
    let selectedCity: string = this.shippment.city;
    let region: string = Object.keys(this.citiesByRegion).find(region => this.citiesByRegion[region].includes(selectedCity))!;

    if (region) {
      let index: number = this.citiesByRegion[region].indexOf(selectedCity);
      this.shippment.cityAr = this.citiesByRegionAr[region][index];
    }

    let selectedCityAr: string = this.shippment.cityAr;
    let regionAr: string = Object.keys(this.citiesByRegionAr).find(region => this.citiesByRegionAr[region].includes(selectedCityAr))!;

    if (regionAr) {
      let index: number = this.citiesByRegionAr[regionAr].indexOf(selectedCityAr);
      this.shippment.city = this.citiesByRegion[regionAr][index];
    }
  
}
}
