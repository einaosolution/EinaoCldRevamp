import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayPatentCertificateComponent } from './pay-patent-certificate.component';

describe('PayPatentCertificateComponent', () => {
  let component: PayPatentCertificateComponent;
  let fixture: ComponentFixture<PayPatentCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayPatentCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayPatentCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
