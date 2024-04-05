import { SafeUrl } from "@angular/platform-browser"

export interface IOrderItems {
    id:number
    orderId:number
    productId:number
    productName:string
    productQuantity:number
    totalPrice:number
    discount:number
    Images:SafeUrl
    productDiscription:string
}
