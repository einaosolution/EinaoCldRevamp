import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayCertificateComponent } from './pay-certificate.component';

describe('PayCertificateComponent', () => {
  let component: PayCertificateComponent;
  let fixture: ComponentFixture<PayCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
