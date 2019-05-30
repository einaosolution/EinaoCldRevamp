import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppStatusDsComponent } from './app-status-ds.component';

describe('AppStatusDsComponent', () => {
  let component: AppStatusDsComponent;
  let fixture: ComponentFixture<AppStatusDsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppStatusDsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppStatusDsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
