import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchKivComponent } from './search-kiv.component';

describe('SearchKivComponent', () => {
  let component: SearchKivComponent;
  let fixture: ComponentFixture<SearchKivComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchKivComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchKivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
