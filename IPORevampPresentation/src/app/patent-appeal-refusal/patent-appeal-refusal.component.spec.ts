import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentAppealRefusalComponent } from './patent-appeal-refusal.component';

describe('PatentAppealRefusalComponent', () => {
  let component: PatentAppealRefusalComponent;
  let fixture: ComponentFixture<PatentAppealRefusalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentAppealRefusalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentAppealRefusalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
