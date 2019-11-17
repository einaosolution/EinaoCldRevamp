import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentUserApplicationComponent } from './patent-user-application.component';

describe('PatentUserApplicationComponent', () => {
  let component: PatentUserApplicationComponent;
  let fixture: ComponentFixture<PatentUserApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentUserApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentUserApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
