import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayRenewalDesignComponent } from './pay-renewal-design.component';

describe('PayRenewalDesignComponent', () => {
  let component: PayRenewalDesignComponent;
  let fixture: ComponentFixture<PayRenewalDesignComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayRenewalDesignComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayRenewalDesignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
