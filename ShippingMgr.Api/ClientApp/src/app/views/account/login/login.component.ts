import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup,FormBuilder, Validators,FormControl } from '@angular/forms';
import { AlertService } from 'src/app/services/alert.service';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm:FormGroup;
  invalidLogin: boolean;
  errMessage;
  apiUrl;
  constructor(private auth:AuthService, 
              private alertService:AlertService,
              private route:ActivatedRoute,
              private router: Router,
              private translateService: TranslateService,
              public fb: FormBuilder ) { }

  ngOnInit() {
    this.apiUrl = environment.apiUrl;
    this.loginForm = this.fb.group({
      email: ['mazen506@hotmail.com', Validators.email],
      password: ['Admin@1234', Validators.required],
    });

    this.route.queryParams.subscribe(params=> {
      if (params.email)
        this.loginForm.controls["email"].setValue(params.email);
    });
  }

  public login(){
    if (this.loginForm.valid){
      this.auth.login(this.loginForm.value)
      .subscribe(response => {
        this.invalidLogin = false;
        this.router.navigate(["/currencies"]);
      }, err => {

        this.errMessage = err.name;
        console.log(err);
        if (this.errMessage == null)
          this.errMessage ="Unexpected";
        this.translateService.get('Errors.' + this.errMessage, 
                                  {email: this.loginForm.controls["email"].value})
                             .subscribe((response:string) => {
                                  this.alertService.error(response);
                                });
        this.invalidLogin = true;
      });
    }
  }
}
