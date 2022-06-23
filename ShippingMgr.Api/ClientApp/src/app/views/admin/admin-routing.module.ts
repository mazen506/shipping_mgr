import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { JwtInterceptor } from '@auth0/angular-jwt';
import { AuthGuard } from 'src/app/helpers/auth-guard';
import { CurrenciesComponent } from './currencies/currencies.component';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderComponent } from './order/order.component';

const routes: Routes = [
    { path: '', component: CurrenciesComponent },
    { path: 'currencies', component: CurrenciesComponent, canActivate: [AuthGuard]  },
    { path: 'order', component: OrderComponent},
    { path: 'orders', component: OrderListComponent},
  ];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    providers: [
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }]
})
export class AdminRoutingModule {}