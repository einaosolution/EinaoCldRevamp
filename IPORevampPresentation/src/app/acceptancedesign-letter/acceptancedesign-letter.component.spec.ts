import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptancedesignLetterComponent } from './acceptancedesign-letter.component';

describe('AcceptancedesignLetterComponent', () => {
  let component: AcceptancedesignLetterComponent;
  let fixture: ComponentFixture<AcceptancedesignLetterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcceptancedesignLetterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptancedesignLetterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
