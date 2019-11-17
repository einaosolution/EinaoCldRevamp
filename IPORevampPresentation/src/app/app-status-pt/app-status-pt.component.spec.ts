import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppStatusPtComponent } from './app-status-pt.component';

describe('AppStatusPtComponent', () => {
  let component: AppStatusPtComponent;
  let fixture: ComponentFixture<AppStatusPtComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppStatusPtComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppStatusPtComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
