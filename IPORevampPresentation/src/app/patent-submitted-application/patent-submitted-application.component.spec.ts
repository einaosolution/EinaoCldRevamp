import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentSubmittedApplicationComponent } from './patent-submitted-application.component';

describe('PatentSubmittedApplicationComponent', () => {
  let component: PatentSubmittedApplicationComponent;
  let fixture: ComponentFixture<PatentSubmittedApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentSubmittedApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentSubmittedApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
