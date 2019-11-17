import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentReconductSearchComponent } from './patent-reconduct-search.component';

describe('PatentReconductSearchComponent', () => {
  let component: PatentReconductSearchComponent;
  let fixture: ComponentFixture<PatentReconductSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentReconductSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentReconductSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
