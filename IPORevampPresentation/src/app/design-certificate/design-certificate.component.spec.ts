import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignCertificateComponent } from './design-certificate.component';

describe('DesignCertificateComponent', () => {
  let component: DesignCertificateComponent;
  let fixture: ComponentFixture<DesignCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
