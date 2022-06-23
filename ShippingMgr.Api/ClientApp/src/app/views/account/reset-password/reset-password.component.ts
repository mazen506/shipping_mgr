import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { customValidators } from 'src/app/providers/customValidators';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {

  formResetPwd:FormGroup;
  isProcessing=false;
  isSubmitted=false;
  isSuccess=false;
  errCode;

  constructor(private authService: AuthService,
              private alertService:AlertService,
              private route:ActivatedRoute,
              private translateService: TranslateService,
              private router:Router,
              public fb: FormBuilder) { }

  ngOnInit() {
      this.formResetPwd = this.fb.group({
        password: ['Test@1234', Validators.required],
        confirm_password: ['Test@1234', Validators.required]
      },{
          validators: customValidators.mustMatch('password', 'confirm_password')
        }
      );
  }

  get f() {
    return this.formResetPwd.controls;
  }

  submit(){
      this.isSubmitted= true;
      if (this.formResetPwd.valid){
        this.route.queryParams.subscribe(params => {
            this.isProcessing = true;
            this.authService.resetPassword({ email: params.email,
                      code: encodeURIComponent(params.code),
                      password: this.formResetPwd.controls["password"].value })
                      .subscribe(response =>
                      {
                          this.isSuccess = true;
                          this.translateService.get("ResetPasswordSuccess")
                                                .subscribe(message => {
                                                    this.alertService.success(message);
                                                });
                      }, err => {
                          this.isSuccess = false;
                          this.errCode = err.error;
                          if (this.errCode == null)
                              this.errCode = "Unexptected";
                          this.translateService.get("Errors." + this.errCode)
                                                .subscribe(message => {
                                                  this.alertService.error(message);
                                                });
                      }).add(()=> {
                          this.isProcessing = false;
                      });
        });
      }
  }
}
