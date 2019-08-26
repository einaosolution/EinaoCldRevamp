import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticePatentCertificatePaymentComponent } from './notice-patent-certificate-payment.component';

describe('NoticePatentCertificatePaymentComponent', () => {
  let component: NoticePatentCertificatePaymentComponent;
  let fixture: ComponentFixture<NoticePatentCertificatePaymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticePatentCertificatePaymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticePatentCertificatePaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
