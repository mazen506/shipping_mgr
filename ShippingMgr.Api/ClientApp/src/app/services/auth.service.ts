import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { NgNetInterceptor } from '../helpers/ngnet-interceptor';
import { resetPassword } from '../models/resetPassword';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  
  constructor(private http: NgNetInterceptor,
              private router: Router,
              private jwtHelper: JwtHelperService) { 
                this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
                this.user = this.userSubject.asObservable();
              }

  public get userValue(): User {
      return this.userSubject.value;
  }              


  login(model) {
      return this.http.post("api/auth/login", model).pipe(
        map(user => {
          localStorage.setItem('user', JSON.stringify(user));
          this.userSubject.next(user);
          return user;
        })
      );
      
  }

  register(model): Observable<any>{
    return this.http.post("api/auth/register",model);
  }

  confirmEmail(email:string, code:string): Observable<any>{
    return this.http.post("api/auth/confirmEmail?email=" + email + "&code=" + code , null);
  }

  sendEmailConfirmationLink(email:string): Observable<any>{
    return this.http.get("api/auth/sendEmailConfirmationLink?email=" + email);
  }

  sendResetPasswordLink(email:string): Observable<any>{
    return this.http.post("api/auth/sendResetPasswordLink?email=" + email, null);
  }

  resetPassword(model: resetPassword): Observable<any>{
    return this.http.post("api/auth/ResetPassword", model);
  }

  isLoggedIn() {
    const user = this.userSubject.value;
    if (user && !this.jwtHelper.isTokenExpired(user.token)) {
      return true;
    }
    else {
      return false;
    }
  }

  public logOut = () => {
      localStorage.removeItem("user");
      this.userSubject.next(null);
      this.router.navigate(['/account/login']);
  }
  
}
