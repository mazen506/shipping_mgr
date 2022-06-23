import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/models/customer';
import { Order } from 'src/app/models/order';
import { CustomerService } from 'src/app/services/customer.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss'],
})
export class OrderListComponent implements OnInit {

  orders:Order[];
  customers:Customer[];
  constructor(private orderService:OrderService,
              private customerService:CustomerService
            ) { }

  ngOnInit() {

    this.customerService.list().subscribe(
      res=> {
        this.customers = res;
        this.orderService.list().subscribe(
              res=> this.orders = res
        );
      }
    );
      

      
  }

  getCustomer(id){
      var customer = this.customers.find(x=> x.id == id);
      if (customer)
        return customer.name;
      return '';
  }

}
