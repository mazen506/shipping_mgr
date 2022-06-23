import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
        { path: 'login', component: LoginComponent, data:  {title: 'Account.Login'} },
        { path: 'register', component: RegisterComponent, data: {title: 'Account.Register'} },
        { path: 'confirmEmail', component: ConfirmEmailComponent, data: {title: 'Account.EmailConfirmation'} },
        { path: 'forgotPassword', component: ForgotPasswordComponent, data: { title: 'Account.ForgotPassword'}},
        { path: 'resetPassword', component: ResetPasswordComponent, data: { title: 'Account.ResetPassword'}}

  ];
@NgModule({
    imports: [RouterModule.forChild(routes)]
})
export class AccountRoutingModule {}