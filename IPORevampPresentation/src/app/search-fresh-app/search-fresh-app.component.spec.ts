import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchFreshAppComponent } from './search-fresh-app.component';

describe('SearchFreshAppComponent', () => {
  let component: SearchFreshAppComponent;
  let fixture: ComponentFixture<SearchFreshAppComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchFreshAppComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchFreshAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
