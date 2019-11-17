import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentRegistraCertificateComponent } from './patent-registra-certificate.component';

describe('PatentRegistraCertificateComponent', () => {
  let component: PatentRegistraCertificateComponent;
  let fixture: ComponentFixture<PatentRegistraCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentRegistraCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentRegistraCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
