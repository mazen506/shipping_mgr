import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/models/customer';
import { Item } from 'src/app/models/item';
import { ItemService } from 'src/app/services/item.service';
import { CustomerService } from 'src/app/services/customer.service';
import { FormBuilder, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { customValidators } from 'src/app/providers/customValidators';
import { OrderService } from 'src/app/services/order.service';
import { Order } from 'src/app/models/order';
import {formatDate, Location } from '@angular/common';
import { MatDialog, MatDialogConfig, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { OrderItemComponent } from '../order-item/order-item.component';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { HttpResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { ToasterPosition } from 'src/app/models/toastr';
import { AdminBaseComponent } from '../admin-base/admin-base.component';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent extends AdminBaseComponent implements OnInit {
  order:Order;
  frm:FormGroup;
  customers:Customer[];
  items: Item[];
  isSubmitted;
  constructor(
              private location: Location,
              private customerService: CustomerService,
              private itemService:ItemService,
              private orderService: OrderService,
              private dialog: MatDialog,
              private toaster: ToastrService,
              public router: Router,
              public route: ActivatedRoute,
              private fb: FormBuilder
              ) {
                  super(router, route);
                  this.order = this.orderService.order;
               }

  ngOnInit() {

      
      this.frm = this.fb.group({
          customerId: [2 , customValidators.notEqual(0)],
          date: [formatDate(Date.now(),"yyyy-MM-ddT00:00:00", 'en-US'), Validators.required],
          note: ['this is a test']
      });

      forkJoin({
      customers: this.customerService.list(),
      items: this.itemService.list()}).subscribe(({customers, items}) =>
        {
          this.customers = customers;
          this.items = items;
          //fill order
          this.route.queryParams.subscribe(params => {
              if (params.id)//Edit
                {
                     this.orderService.getById(params.id).subscribe(res=> {
                          this.order.fill(res);
                          this.frm.setValue({ customerId: this.order.customerId,
                                              date: this.order.date,
                                              note: this.order.note   });
                      });
                }
          });
        }
      );
  }

  get f(){
    return this.frm.controls;
  }

  editItem(index){
      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      dialogConfig.disableClose = true;
      dialogConfig.width = "90%";
      dialogConfig.position = {
        top: '50vh',
        left: '50vw'
      };
      dialogConfig.panelClass = 'makeItMiddle'; //Class Name that can be defined in styles.css as follows:
      dialogConfig.data = { index };
      this.dialog.open(OrderItemComponent, dialogConfig);
  }


  test(){
      console.log('testing');
      this.orderService.order.note ="Tesint" + Date.now();
  }

  getItem(id){
      return this.items.find(x => x.id == id);
  }

  submit(){
      this.isSubmitted = true;
      if (this.frm.valid){
         this.order.fill(this.frm.value);
          if (this.order.id == null) //New
          {        this.orderService.create().subscribe(
                         (res: HttpResponse<any>)=> {
                          this.order.fill(res.body);
                          this.location.replaceState("/admin/order?id=" + this.order.id);
                          this.toaster.success("Successfully created!!","Al-Mansoor", {positionClass: ToasterPosition.bottomRight});
                      }, err=> { 
                          this.toaster.error("Error: " + JSON.stringify(err), "Al-Mansoor", {positionClass: ToasterPosition.bottomRight});
                       });
          } else //Update
          {
                    this.orderService.update().subscribe(res=> {
                          console.log('Updated Successfully!!');
                    }, err=> {
                          console.log(err);
                    });
          }
      }
  }

  delete(){
      if (this.order.id != null){
          this.orderService.delete(this.order.id).subscribe(res=> {
              console.log("Deleted!!");
          }, err=> {
              console.log(err);
          });
      }
  }


  delItem(index){
      this.order.items.splice(index,1);
  }

 

}
