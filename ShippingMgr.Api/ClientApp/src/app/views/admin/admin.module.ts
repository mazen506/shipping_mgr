import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy, RouterModule } from '@angular/router';
import { CommonModule, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { AdminRoutingModule } from './admin-routing.module';
import { CurrenciesComponent } from './currencies/currencies.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgNetInterceptor } from 'src/app/helpers/ngnet-interceptor';
import { AuthService } from 'src/app/services/auth.service';
import { NgJwtInterceptor } from 'src/app/helpers/jwt-interceptor';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderComponent } from './order/order.component';
import { OrderItemComponent } from './order-item/order-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {  MatDialogModule } from '@angular/material/dialog';
import { AdminBaseComponent } from './admin-base/admin-base.component';



@NgModule({
  declarations: [CurrenciesComponent, OrderListComponent, OrderComponent, 
                 OrderItemComponent, AdminBaseComponent],

  imports: [ CommonModule,
             RouterModule,
             AdminRoutingModule,
             FormsModule,
             IonicModule,
             ReactiveFormsModule,
             MatDialogModule
            ],
  entryComponents: [
  //            OrderItemComponent
          ],
  providers: [
    Location,
    { provide: LocationStrategy, useClass: PathLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: NgJwtInterceptor, multi: true }],
  bootstrap: [],
})
export class AdminModule {}
