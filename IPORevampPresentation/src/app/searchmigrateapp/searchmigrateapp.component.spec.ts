import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchmigrateappComponent } from './searchmigrateapp.component';

describe('SearchmigrateappComponent', () => {
  let component: SearchmigrateappComponent;
  let fixture: ComponentFixture<SearchmigrateappComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchmigrateappComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchmigrateappComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
