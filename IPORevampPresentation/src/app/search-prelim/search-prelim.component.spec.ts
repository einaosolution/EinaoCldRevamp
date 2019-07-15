import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchPrelimComponent } from './search-prelim.component';

describe('SearchPrelimComponent', () => {
  let component: SearchPrelimComponent;
  let fixture: ComponentFixture<SearchPrelimComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchPrelimComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchPrelimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
