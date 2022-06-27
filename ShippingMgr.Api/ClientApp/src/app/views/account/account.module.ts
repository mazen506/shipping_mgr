import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from '../../views/account/register/register.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { RouterModule } from '@angular/router';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { NgJwtInterceptor } from 'src/app/helpers/jwt-interceptor';
import { AccountBaseComponent } from './account-base/account-base.component';
import { IonicModule } from '@ionic/angular';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

@NgModule({
  declarations: [LoginComponent,
                 RegisterComponent,
                 ConfirmEmailComponent, 
                 ForgotPasswordComponent, 
                 ResetPasswordComponent, AccountBaseComponent],
  imports: [ CommonModule,
             AccountRoutingModule,
             FormsModule,
             IonicModule,
             RouterModule,
             ReactiveFormsModule,
             TranslateModule.forChild({
              loader: {
                provide: TranslateLoader,
                useFactory: createTranslateLoader,
                deps: [HttpClient]
              }
            }),
            ],
  providers: [
      { provide: HTTP_INTERCEPTORS, useClass: NgJwtInterceptor, multi: true }
    ],
  bootstrap: [],
})
export class AccountModule {}
