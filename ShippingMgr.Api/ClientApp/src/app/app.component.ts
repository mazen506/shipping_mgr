import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor(private router: Router,  
              private activatedRoute: ActivatedRoute, 
              private translateService: TranslateService,
              public titleService: Title) {
                this.initializeApp();
              }

  initializeApp(){
        this.translateService.setDefaultLang("ar");
  }              

  ngOnInit() {  
    this.router.events.pipe(  
        filter(event => event instanceof NavigationEnd),  
      ).subscribe(() => {  
        const rt = this.getChild(this.activatedRoute);  
        rt.data.subscribe(data => {  
          console.log(data);
          this.titleService.setTitle(data.title)});  
      });  
  }  
  
  getChild(activatedRoute: ActivatedRoute) {  
    if (activatedRoute.firstChild) {  
      return this.getChild(activatedRoute.firstChild);  
    } else {  
      return activatedRoute;  
    }  
  }

}
