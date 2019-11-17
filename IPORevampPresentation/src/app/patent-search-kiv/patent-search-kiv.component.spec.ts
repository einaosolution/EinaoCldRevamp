import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentSearchKivComponent } from './patent-search-kiv.component';

describe('PatentSearchKivComponent', () => {
  let component: PatentSearchKivComponent;
  let fixture: ComponentFixture<PatentSearchKivComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentSearchKivComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentSearchKivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
