import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalMergerAndAssignment2Component } from './gen-recordal-merger-and-assignment2.component';

describe('GenRecordalMergerAndAssignment2Component', () => {
  let component: GenRecordalMergerAndAssignment2Component;
  let fixture: ComponentFixture<GenRecordalMergerAndAssignment2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalMergerAndAssignment2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalMergerAndAssignment2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
