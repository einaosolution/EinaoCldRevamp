import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppStatusTmComponent } from './app-status-tm.component';

describe('AppStatusTmComponent', () => {
  let component: AppStatusTmComponent;
  let fixture: ComponentFixture<AppStatusTmComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppStatusTmComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppStatusTmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
