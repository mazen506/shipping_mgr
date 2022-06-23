import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup,FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { customValidators } from '../../../providers/customValidators';
import { AlertService } from 'src/app/services/alert.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})

export class RegisterComponent implements OnInit {
  registerForm:FormGroup;
  isSuccess: boolean;
  isSubmitted = false;
  isProcessing=false;
  errCode;
  constructor(private auth:AuthService, 
              private router: Router,
              private alertService: AlertService,
              private translateService: TranslateService,
              public fb: FormBuilder ) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      email: ['mazen506@hotmail.com', Validators.email],
      first_name: ['Mazen', Validators.required],
      last_name: ['Mustafa', Validators.required],
      password: ['Admin@1234', Validators.required],
      confirm_password: ['Admin@1234', Validators.required]
    },{
        validators: customValidators.mustMatch('password', 'confirm_password')
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  public register(){
    this.isSubmitted = true;
    if (this.registerForm.valid){
      this.isProcessing = true;
      this.auth.register({"email": this.registerForm.controls["email"].value,
                          "firstName": this.registerForm.controls["first_name"].value,
                          "lastName": this.registerForm.controls["last_name"].value,
                          "password" : this.registerForm.controls["password"].value,
                          "locale": "en",
                          "role": 2
                        })
      .subscribe(
        (response) => {
        this.isSuccess = true;
        this.translateService.get('Account.RegistrationSuccess', 
                                  {email: this.registerForm.controls["email"].value})
                              .subscribe(response=> {
                                  this.alertService.success(response);
                              });
      }, (err) => {
        this.isSuccess = false;
        this.errCode = err.error;
        if (this.errCode == null)
          this.errCode = "Unexptected";
        this.translateService.get('Errors.' + this.errCode)
                              .subscribe(message=> {
                                  this.alertService.error(message);
                              })
        }).add(
            ()=> {
              this.isProcessing = false;
            }
      );
    }
  }
}
