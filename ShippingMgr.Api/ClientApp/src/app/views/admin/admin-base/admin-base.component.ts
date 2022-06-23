import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../shared/base/base.component';

@Component({
  selector: 'app-admin-base',
  templateUrl: './admin-base.component.html',
  styleUrls: ['./admin-base.component.scss'],
})
export class AdminBaseComponent extends BaseComponent implements OnInit {

  constructor(public router: Router,
              public route: ActivatedRoute) {
        super(router, route);
   }

  ngOnInit() {}

}
