import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalMergerAndAssignmentComponent } from './gen-recordal-merger-and-assignment.component';

describe('GenRecordalMergerAndAssignmentComponent', () => {
  let component: GenRecordalMergerAndAssignmentComponent;
  let fixture: ComponentFixture<GenRecordalMergerAndAssignmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalMergerAndAssignmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalMergerAndAssignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
