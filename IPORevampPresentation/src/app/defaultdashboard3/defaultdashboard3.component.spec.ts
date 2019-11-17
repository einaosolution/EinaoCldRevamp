import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Defaultdashboard3Component } from './defaultdashboard3.component';

describe('Defaultdashboard3Component', () => {
  let component: Defaultdashboard3Component;
  let fixture: ComponentFixture<Defaultdashboard3Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Defaultdashboard3Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Defaultdashboard3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
