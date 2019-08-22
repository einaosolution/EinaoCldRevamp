import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminerPatentFreshComponent } from './examiner-patent-fresh.component';

describe('ExaminerPatentFreshComponent', () => {
  let component: ExaminerPatentFreshComponent;
  let fixture: ComponentFixture<ExaminerPatentFreshComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminerPatentFreshComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminerPatentFreshComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
