import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewChangeOfNameComponent } from './view-change-of-name.component';

describe('ViewChangeOfNameComponent', () => {
  let component: ViewChangeOfNameComponent;
  let fixture: ComponentFixture<ViewChangeOfNameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewChangeOfNameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewChangeOfNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
