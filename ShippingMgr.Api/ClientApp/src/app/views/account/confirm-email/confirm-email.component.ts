import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss'],
})

export class ConfirmEmailComponent implements OnInit {

  isSend;
  isProcessing;
  isSuccess;
  userEmail;
  errMessage;

  constructor(private auth: AuthService,
              private alertService: AlertService,
              private route:ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
      
      
      this.route.queryParams
      .subscribe(params => {
        this.isProcessing = true;
        if (params.code == null) //Resend
        {
          this.userEmail = params.email;
          this.isSend = true;
          this.alertService.info("Sending email confirmation link,, Please wait .... ", {processing: true});
          this.auth.sendEmailConfirmationLink(params.email)
          .subscribe(response => {
              this.isProcessing = false;
              this.isSuccess = true;
              this.alertService.success("Confirmation link sent successfully!! Please click on the link sent to activate your account..");
          }, err => {
              console.log (err);
              this.alertService.error(err.error);
              this.isProcessing = false;
              this.isSuccess = false;
          });
        } else { //Confirm
          this.isSend = false;
          this.alertService.info("Please wait while confirming your email address...", {processing: true});
          this.auth.confirmEmail(params.email, encodeURIComponent(params.code))
          .subscribe(response => {
              this.isProcessing = false;
              this.isSuccess = true;
              this.alertService.success("Account activated, You can <a href='/account/login?email=" + 
              params.email + "'>Login Here</a>!");
          }, err => {
              console.log (err);
              this.errMessage = err.error;
              if (this.errMessage == 'InvalidActivationLink')
                 this.errMessage = "Invalid or expired token! <a href='/account/confirmEmail?email=" + 
                  this.userEmail + "'>Send Again</a>";
              this.isProcessing = false;
              this.isSuccess = false;
          });
        }
      });
  }

  ResendConfirmationLink() {
        window.location.reload();    //if id is same then just reload the page
  }

}
