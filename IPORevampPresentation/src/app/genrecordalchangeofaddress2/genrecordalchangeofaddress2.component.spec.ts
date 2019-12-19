import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Genrecordalchangeofaddress2Component } from './genrecordalchangeofaddress2.component';

describe('Genrecordalchangeofaddress2Component', () => {
  let component: Genrecordalchangeofaddress2Component;
  let fixture: ComponentFixture<Genrecordalchangeofaddress2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Genrecordalchangeofaddress2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Genrecordalchangeofaddress2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
