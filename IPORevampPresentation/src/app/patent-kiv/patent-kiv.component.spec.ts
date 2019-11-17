import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentKivComponent } from './patent-kiv.component';

describe('PatentKivComponent', () => {
  let component: PatentKivComponent;
  let fixture: ComponentFixture<PatentKivComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentKivComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentKivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
