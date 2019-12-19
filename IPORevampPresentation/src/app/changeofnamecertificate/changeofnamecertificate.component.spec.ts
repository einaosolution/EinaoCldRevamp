import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeofnamecertificateComponent } from './changeofnamecertificate.component';

describe('ChangeofnamecertificateComponent', () => {
  let component: ChangeofnamecertificateComponent;
  let fixture: ComponentFixture<ChangeofnamecertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeofnamecertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeofnamecertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
