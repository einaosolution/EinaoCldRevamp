import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeofchangeofaddressComponent } from './noticeofchangeofaddress.component';

describe('NoticeofchangeofaddressComponent', () => {
  let component: NoticeofchangeofaddressComponent;
  let fixture: ComponentFixture<NoticeofchangeofaddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticeofchangeofaddressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticeofchangeofaddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
