export interface IOrder {
    id:number;
    totalAmount:number;
    orderDate:string;
    createdDate:string
    discount:number;
    status:number;
    cancelOrder:boolean;
    customerId:number;
    paymentStatus:number;
    totalOrderPrice:number
}
