import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignPaidCertificateComponentComponent } from './design-paid-certificate-component.component';

describe('DesignPaidCertificateComponentComponent', () => {
  let component: DesignPaidCertificateComponentComponent;
  let fixture: ComponentFixture<DesignPaidCertificateComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignPaidCertificateComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignPaidCertificateComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
