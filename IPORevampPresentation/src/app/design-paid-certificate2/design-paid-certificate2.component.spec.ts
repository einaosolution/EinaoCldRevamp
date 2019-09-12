import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignPaidCertificate2Component } from './design-paid-certificate2.component';

describe('DesignPaidCertificate2Component', () => {
  let component: DesignPaidCertificate2Component;
  let fixture: ComponentFixture<DesignPaidCertificate2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignPaidCertificate2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignPaidCertificate2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
