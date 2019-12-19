import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeofaddresscertificateComponent } from './changeofaddresscertificate.component';

describe('ChangeofaddresscertificateComponent', () => {
  let component: ChangeofaddresscertificateComponent;
  let fixture: ComponentFixture<ChangeofaddresscertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeofaddresscertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeofaddresscertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
