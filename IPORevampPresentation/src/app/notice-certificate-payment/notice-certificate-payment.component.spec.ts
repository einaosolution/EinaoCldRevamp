import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeCertificatePaymentComponent } from './notice-certificate-payment.component';

describe('NoticeCertificatePaymentComponent', () => {
  let component: NoticeCertificatePaymentComponent;
  let fixture: ComponentFixture<NoticeCertificatePaymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticeCertificatePaymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticeCertificatePaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
