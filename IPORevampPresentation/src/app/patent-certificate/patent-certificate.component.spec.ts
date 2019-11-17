import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentCertificateComponent } from './patent-certificate.component';

describe('PatentCertificateComponent', () => {
  let component: PatentCertificateComponent;
  let fixture: ComponentFixture<PatentCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
