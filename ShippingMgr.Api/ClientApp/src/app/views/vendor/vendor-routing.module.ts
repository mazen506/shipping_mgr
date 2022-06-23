import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/helpers/auth-guard';

const routes: Routes = [
  
  ];
@NgModule({
    imports: [RouterModule.forChild(routes)]
})
export class VendorRoutingModule {}