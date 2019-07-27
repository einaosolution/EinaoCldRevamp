import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateIssuedCertificateComponent } from './generate-issued-certificate.component';

describe('GenerateIssuedCertificateComponent', () => {
  let component: GenerateIssuedCertificateComponent;
  let fixture: ComponentFixture<GenerateIssuedCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenerateIssuedCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenerateIssuedCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
