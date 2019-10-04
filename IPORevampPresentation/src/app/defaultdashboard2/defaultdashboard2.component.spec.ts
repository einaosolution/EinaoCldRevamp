import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Defaultdashboard2Component } from './defaultdashboard2.component';

describe('Defaultdashboard2Component', () => {
  let component: Defaultdashboard2Component;
  let fixture: ComponentFixture<Defaultdashboard2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Defaultdashboard2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Defaultdashboard2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
