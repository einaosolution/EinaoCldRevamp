import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticeofchangeofnameComponent } from './noticeofchangeofname.component';

describe('NoticeofchangeofnameComponent', () => {
  let component: NoticeofchangeofnameComponent;
  let fixture: ComponentFixture<NoticeofchangeofnameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticeofchangeofnameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticeofchangeofnameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
