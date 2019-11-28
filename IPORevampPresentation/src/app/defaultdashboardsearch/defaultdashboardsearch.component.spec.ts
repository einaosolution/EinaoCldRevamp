import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultdashboardsearchComponent } from './defaultdashboardsearch.component';

describe('DefaultdashboardsearchComponent', () => {
  let component: DefaultdashboardsearchComponent;
  let fixture: ComponentFixture<DefaultdashboardsearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefaultdashboardsearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultdashboardsearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
