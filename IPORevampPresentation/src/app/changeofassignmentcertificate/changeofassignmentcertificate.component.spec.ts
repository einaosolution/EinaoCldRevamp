import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeofassignmentcertificateComponent } from './changeofassignmentcertificate.component';

describe('ChangeofassignmentcertificateComponent', () => {
  let component: ChangeofassignmentcertificateComponent;
  let fixture: ComponentFixture<ChangeofassignmentcertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeofassignmentcertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeofassignmentcertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
