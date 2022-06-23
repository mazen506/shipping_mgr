import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { environment } from 'src/environments/environment';

@Injectable()

export class NgJwtInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {} 

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {

        // add auth header with jwt if user is logged in and request is to the api url
        const user = this.authService.userValue;
        const isApiUrl = req.url.startsWith(environment.apiUrl);
         
        if (this.authService.isLoggedIn() && isApiUrl) {
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${user.token}`
                }
            });
        }
        return next.handle(req);
    }
}