import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentPaidCertificaeComponent } from './patent-paid-certificae.component';

describe('PatentPaidCertificaeComponent', () => {
  let component: PatentPaidCertificaeComponent;
  let fixture: ComponentFixture<PatentPaidCertificaeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentPaidCertificaeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentPaidCertificaeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
