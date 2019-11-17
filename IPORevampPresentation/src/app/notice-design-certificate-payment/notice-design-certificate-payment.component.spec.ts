import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeDesignCertificatePaymentComponent } from './notice-design-certificate-payment.component';

describe('NoticeDesignCertificatePaymentComponent', () => {
  let component: NoticeDesignCertificatePaymentComponent;
  let fixture: ComponentFixture<NoticeDesignCertificatePaymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticeDesignCertificatePaymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticeDesignCertificatePaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
