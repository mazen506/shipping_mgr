export class OrderItem {
    id:number;
    orderId: number;
    itemId: number;
    name:string;
    qty: number;
    price: number;
    amount: number;
    note: string;
    constructor(init?: Partial<OrderItem>){
        Object.assign(this, init);
    }

    fill(data?: Partial<OrderItem>){
        Object.assign(this, data);
    }
}