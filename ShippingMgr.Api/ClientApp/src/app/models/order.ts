import { Customer } from "./customer";
import { OrderItem } from "./orderItem";

export class Order{
    id: number;
    customerId: number;
    customer: Customer;
    date:Date;
    note: string;
    createdAt: Date;
    updatedAt:Date;
    items: OrderItem[] = [];
    constructor(init?: Partial<Order>){
        this.items = [];
        Object.assign(this, init);
    }

    fill(init?:Partial<Order>){
        Object.assign(this, init);
        if (this.items == null)
            this.items = [];
    }
}