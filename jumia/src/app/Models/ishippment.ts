import { DecimalPipe } from "@angular/common";

export interface IShippment {
    id:number;
    FirstName :string;
    LastName:string;
    PhoneNumber :string
    Address:string;
    AdressInformation :string;
    Region :string;
    City:string;
    Cost :DecimalPipe
    RegionAr :string
    CityAr :string;
    CustomerId :number
}
