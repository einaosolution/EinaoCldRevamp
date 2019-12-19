import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Defaultdashboard5Component } from './defaultdashboard5.component';

describe('Defaultdashboard5Component', () => {
  let component: Defaultdashboard5Component;
  let fixture: ComponentFixture<Defaultdashboard5Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Defaultdashboard5Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Defaultdashboard5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
