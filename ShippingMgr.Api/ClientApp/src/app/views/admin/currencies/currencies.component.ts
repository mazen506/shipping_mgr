import { Component, OnInit } from '@angular/core';
import { Currency } from 'src/app/models/currency';
import { AuthService } from 'src/app/services/auth.service';
import { CurrencyService } from 'src/app/services/currency.service';

@Component({
  selector: 'app-currencies',
  templateUrl: './currencies.component.html',
  styleUrls: ['./currencies.component.scss'],
})
export class CurrenciesComponent implements OnInit {

  currencies: Currency[];
  constructor(private currencyService: CurrencyService,private authService: AuthService ) { }

  ngOnInit() {
    this.getCurrencies();
  }

  getCurrencies(){
    this.currencyService.getList().subscribe(currencies => this.currencies = currencies);
  }

  isLoggedIn(){
      return this.authService.isLoggedIn();
  }

  logOut(){
      this.authService.logOut();
  }



}
