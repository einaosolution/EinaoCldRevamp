import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeOfCounterOppositionComponent } from './notice-of-counter-opposition.component';

describe('NoticeOfCounterOppositionComponent', () => {
  let component: NoticeOfCounterOppositionComponent;
  let fixture: ComponentFixture<NoticeOfCounterOppositionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticeOfCounterOppositionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticeOfCounterOppositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
