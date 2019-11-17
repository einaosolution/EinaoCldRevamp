import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeofmergerComponent } from './noticeofmerger.component';

describe('NoticeofmergerComponent', () => {
  let component: NoticeofmergerComponent;
  let fixture: ComponentFixture<NoticeofmergerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticeofmergerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticeofmergerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
