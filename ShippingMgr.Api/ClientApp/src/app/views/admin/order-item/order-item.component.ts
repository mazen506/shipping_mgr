import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Item } from 'src/app/models/item';
import { ItemService } from 'src/app/services/item.service';
import {  MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { customValidators } from 'src/app/providers/customValidators';
import { OrderService } from 'src/app/services/order.service';
import { Order } from 'src/app/models/order';
import { OrderItem } from 'src/app/models/orderItem';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.scss'],
})
export class OrderItemComponent implements OnInit {

  order:Order;
  items:Item[];
  frm:FormGroup;
  isSubmitted:boolean;
  constructor(private itemService: ItemService,
              private orderService: OrderService,
              @Inject(MAT_DIALOG_DATA) public data,
              public dialogRef: MatDialogRef<OrderItemComponent>,
              private fb: FormBuilder) {
                    this.order = this.orderService.order;
              }

  ngOnInit() {
    this.frm = this.fb.group({
        itemId: [1, customValidators.notEqual(0)],
        qty:[12, Validators.min(1)],
        price: [100, Validators.required],
        amount: [1200, Validators.required],
        note: ['jeans']
    })
      this.itemService.list().subscribe(
          res=> {
              this.items = res;
                  if (this.data.index != null)
                    this.frm.setValue(this.order.items[this.data.index]);
              });
  }

  close(){
            this.dialogRef.close();
  }

  submit(){
      this.isSubmitted = true;
      console.log(this.frm.valid);
      if (this.frm.valid){
          if (this.data.index != null) //Update
          {  console.log(this.orderService.order.items[this.data.index]);
            this.orderService.order.items[this.data.index] = this.frm.value;
          }
          else //New
          {
            this.orderService.order.items.push(this.frm.value);
          }
          this.close();
      }
  }


}


