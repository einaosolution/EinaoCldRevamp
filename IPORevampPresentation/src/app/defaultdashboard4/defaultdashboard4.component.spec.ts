import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Defaultdashboard4Component } from './defaultdashboard4.component';

describe('Defaultdashboard4Component', () => {
  let component: Defaultdashboard4Component;
  let fixture: ComponentFixture<Defaultdashboard4Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Defaultdashboard4Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Defaultdashboard4Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
