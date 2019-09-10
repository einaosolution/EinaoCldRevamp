import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignRegistraCertificateComponent } from './design-registra-certificate.component';

describe('DesignRegistraCertificateComponent', () => {
  let component: DesignRegistraCertificateComponent;
  let fixture: ComponentFixture<DesignRegistraCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignRegistraCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignRegistraCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
