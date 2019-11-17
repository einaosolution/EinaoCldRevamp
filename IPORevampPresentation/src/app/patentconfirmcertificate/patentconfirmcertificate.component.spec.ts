import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentconfirmcertificateComponent } from './patentconfirmcertificate.component';

describe('PatentconfirmcertificateComponent', () => {
  let component: PatentconfirmcertificateComponent;
  let fixture: ComponentFixture<PatentconfirmcertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentconfirmcertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentconfirmcertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
