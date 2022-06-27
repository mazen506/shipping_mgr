import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { SharedRoutingModule } from './shared-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AlertComponent } from './alert/alert.component';
import { BaseComponent } from './base/base.component';

@NgModule({
  declarations: [ AlertComponent, BaseComponent ],
  imports: [ CommonModule,
             SharedRoutingModule,
             FormsModule,
             IonicModule,
             RouterModule,
             ReactiveFormsModule,
            ],
  bootstrap: [],
})
export class SharedModule {}
