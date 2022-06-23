import { NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';
import { RouteReuseStrategy, RouterModule } from '@angular/router';
 import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClient, HttpClientModule , HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuard } from './helpers/auth-guard';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { AlertComponent } from './views/shared/alert/alert.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgJwtInterceptor } from './helpers/jwt-interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [AppComponent, AlertComponent],
  entryComponents: [
      
  ],
  imports: [BrowserModule,
            FormsModule, 
            ReactiveFormsModule,
            CommonModule,
            RouterModule,
            BrowserAnimationsModule,
            ToastrModule.forRoot(),
            IonicModule.forRoot(), 
            TranslateModule.forRoot({ 
                loader: { 
                provide: TranslateLoader, 
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
              }
            }),
            AppRoutingModule,  
            JwtModule.forRoot({
            config: {
              tokenGetter: tokenGetter,
              allowedDomains: ["localhost:5068"],
              disallowedRoutes: []
                  }
            }),
            HttpClientModule,
            BrowserAnimationsModule,
            NgbModule
           ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: NgJwtInterceptor, multi: true },
              { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
              JwtHelperService, 
              AuthGuard,
              Title],
  bootstrap: [AppComponent],
})
export class AppModule {}
