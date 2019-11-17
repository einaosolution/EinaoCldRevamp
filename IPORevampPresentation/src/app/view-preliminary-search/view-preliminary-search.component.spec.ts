import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPreliminarySearchComponent } from './view-preliminary-search.component';

describe('ViewPreliminarySearchComponent', () => {
  let component: ViewPreliminarySearchComponent;
  let fixture: ComponentFixture<ViewPreliminarySearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewPreliminarySearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPreliminarySearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
