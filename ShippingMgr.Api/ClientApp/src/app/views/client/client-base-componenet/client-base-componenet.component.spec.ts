import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { ClientBaseComponenetComponent } from './client-base-componenet.component';

describe('ClientBaseComponenetComponent', () => {
  let component: ClientBaseComponenetComponent;
  let fixture: ComponentFixture<ClientBaseComponenetComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientBaseComponenetComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(ClientBaseComponenetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
