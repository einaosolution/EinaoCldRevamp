import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignAppeal2Component } from './assign-appeal2.component';

describe('AssignAppeal2Component', () => {
  let component: AssignAppeal2Component;
  let fixture: ComponentFixture<AssignAppeal2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssignAppeal2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignAppeal2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
