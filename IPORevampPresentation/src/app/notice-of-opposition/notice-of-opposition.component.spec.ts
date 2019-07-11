import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeOfOppositionComponent } from './notice-of-opposition.component';

describe('NoticeOfOppositionComponent', () => {
  let component: NoticeOfOppositionComponent;
  let fixture: ComponentFixture<NoticeOfOppositionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticeOfOppositionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticeOfOppositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
