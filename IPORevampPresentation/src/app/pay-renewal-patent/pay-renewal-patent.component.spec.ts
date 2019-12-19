import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayRenewalPatentComponent } from './pay-renewal-patent.component';

describe('PayRenewalPatentComponent', () => {
  let component: PayRenewalPatentComponent;
  let fixture: ComponentFixture<PayRenewalPatentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayRenewalPatentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayRenewalPatentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
