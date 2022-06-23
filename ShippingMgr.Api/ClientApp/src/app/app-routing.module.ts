import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { CurrenciesComponent } from './views/admin/currencies/currencies.component';

const routes: Routes = [
  { path: 'admin', loadChildren: () => import('./views/admin/admin.module').then(m => m.AdminModule)
  },
  {
    path: 'account', loadChildren: () => import('./views/account/account.module').then(m => m.AccountModule)
  }
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload', preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
