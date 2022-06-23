import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { customValidators } from 'src/app/providers/customValidators';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent implements OnInit {

  form:FormGroup;
  isSuccess = false;
  isProcessing = false;
  isSubmitted = false;
  errCode;
  constructor(private authService: AuthService,
              private alertService: AlertService,
              private translateService: TranslateService,
              public fb: FormBuilder) { }

  ngOnInit() {
    this.form = this.fb.group({
      email: ['mazen506@gmail.com', Validators.email]
    })
  }

  get f(){
    return this.form.controls;
  }

  public submit(){
    this.isSubmitted= true;
    console.log(this.form.valid);

    if (this.form.valid){
      this.isProcessing = true;
      this.authService.sendResetPasswordLink(this.form.controls["email"].value)
      .subscribe(response =>
        {
            this.isSuccess = true;
            this.translateService.get('ResetLinkSuccess')
                                  .subscribe(message=> {
                                    this.alertService.success(message);
                                  });
            
        },
        err => {
            this.isSuccess = false;
            this.errCode = err.error;
            if (this.errCode ==  null)
                this.errCode = "Unexpected";
            this.translateService.get('errors.' + this.errCode)
                                  .subscribe(message=> {
                                      this.alertService.error(message);                            
                                  });
        }).add(()=> {
            this.isProcessing = false;
        });
    }
  }
}
