import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminerTreatedComponent } from './examiner-treated.component';

describe('ExaminerTreatedComponent', () => {
  let component: ExaminerTreatedComponent;
  let fixture: ComponentFixture<ExaminerTreatedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminerTreatedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminerTreatedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
