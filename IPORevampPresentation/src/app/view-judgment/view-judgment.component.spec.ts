import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewJudgmentComponent } from './view-judgment.component';

describe('ViewJudgmentComponent', () => {
  let component: ViewJudgmentComponent;
  let fixture: ComponentFixture<ViewJudgmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewJudgmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewJudgmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
