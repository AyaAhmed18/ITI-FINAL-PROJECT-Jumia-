import { SafeUrl } from "@angular/platform-browser";

export interface ProductDto {
    id:number,
    name:string,
    nameAr:string,
    nongDescription:string,
    shortDescription:string,
    stockQuantity:number,
    realPrice:number,
    discount:number,
    images:SafeUrl,
    subCategoryId:number,
    brandId:number,
    addedToCart?: boolean,
    cartQuantity:number
    
}