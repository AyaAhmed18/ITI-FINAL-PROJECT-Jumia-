import { SafeUrl } from "@angular/platform-browser"

export interface IOrderItems {
    id:number
    orderId:number
    productId:number
    productName:string
    productNameAr:string
    productQuantity:number
    totalPrice:number
    discount:number
    images:SafeUrl
    productDiscription:string
}
