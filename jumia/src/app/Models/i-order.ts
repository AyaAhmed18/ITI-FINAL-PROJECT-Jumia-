export interface IOrder {
    id:number;
    totalAmount:number;
    totalPrice:number;
    createdDate:string;
    discount:number;
    status:number;
    cancelOrder:boolean;
    customerId:number;
    paymentStatus:number
}
