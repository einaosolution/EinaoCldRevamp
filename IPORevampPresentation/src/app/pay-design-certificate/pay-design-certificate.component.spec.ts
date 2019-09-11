import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PayDesignCertificateComponent } from './pay-design-certificate.component';

describe('PayDesignCertificateComponent', () => {
  let component: PayDesignCertificateComponent;
  let fixture: ComponentFixture<PayDesignCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayDesignCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayDesignCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
