import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchtreatedComponent } from './searchtreated.component';

describe('SearchtreatedComponent', () => {
  let component: SearchtreatedComponent;
  let fixture: ComponentFixture<SearchtreatedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchtreatedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchtreatedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
