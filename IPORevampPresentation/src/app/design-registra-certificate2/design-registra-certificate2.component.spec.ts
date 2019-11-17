import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignRegistraCertificate2Component } from './design-registra-certificate2.component';

describe('DesignRegistraCertificate2Component', () => {
  let component: DesignRegistraCertificate2Component;
  let fixture: ComponentFixture<DesignRegistraCertificate2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignRegistraCertificate2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignRegistraCertificate2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
