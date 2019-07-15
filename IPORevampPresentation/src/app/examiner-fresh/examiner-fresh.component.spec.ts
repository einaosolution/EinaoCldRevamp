import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminerFreshComponent } from './examiner-fresh.component';

describe('ExaminerFreshComponent', () => {
  let component: ExaminerFreshComponent;
  let fixture: ComponentFixture<ExaminerFreshComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminerFreshComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminerFreshComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
