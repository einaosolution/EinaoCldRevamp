import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminerDesignFreshComponent } from './examiner-design-fresh.component';

describe('ExaminerDesignFreshComponent', () => {
  let component: ExaminerDesignFreshComponent;
  let fixture: ComponentFixture<ExaminerDesignFreshComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminerDesignFreshComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminerDesignFreshComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
