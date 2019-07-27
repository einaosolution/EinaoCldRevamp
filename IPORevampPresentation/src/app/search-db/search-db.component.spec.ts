import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchDbComponent } from './search-db.component';

describe('SearchDbComponent', () => {
  let component: SearchDbComponent;
  let fixture: ComponentFixture<SearchDbComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchDbComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchDbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
