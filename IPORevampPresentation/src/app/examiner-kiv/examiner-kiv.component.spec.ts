import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminerKivComponent } from './examiner-kiv.component';

describe('ExaminerKivComponent', () => {
  let component: ExaminerKivComponent;
  let fixture: ComponentFixture<ExaminerKivComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminerKivComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminerKivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
