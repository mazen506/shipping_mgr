import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { VendorRoutingModule } from './vendor-routing.module';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [],
  imports: [ CommonModule,
             RouterModule,
             IonicModule,
             VendorRoutingModule ],
  bootstrap: [],
})
export class VendorModule {}
