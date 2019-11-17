import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayRenewalComponent } from './pay-renewal.component';

describe('PayRenewalComponent', () => {
  let component: PayRenewalComponent;
  let fixture: ComponentFixture<PayRenewalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayRenewalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayRenewalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
