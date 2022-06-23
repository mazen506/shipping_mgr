import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../shared/base/base.component';

@Component({
  selector: 'app-account-base',
  templateUrl: './account-base.component.html',
  styleUrls: ['./account-base.component.scss'],
})
export class AccountBaseComponent extends BaseComponent implements OnInit {

  constructor(public router: Router,
              public route: ActivatedRoute) { 
                  super(router, route);
              }

  ngOnInit() {}

}
