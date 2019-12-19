import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalChangeOfAddressComponent } from './gen-recordal-change-of-address.component';

describe('GenRecordalChangeOfAddressComponent', () => {
  let component: GenRecordalChangeOfAddressComponent;
  let fixture: ComponentFixture<GenRecordalChangeOfAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalChangeOfAddressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalChangeOfAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
