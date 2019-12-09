import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptanceLetterReprintComponent } from './acceptance-letter-reprint.component';

describe('AcceptanceLetterReprintComponent', () => {
  let component: AcceptanceLetterReprintComponent;
  let fixture: ComponentFixture<AcceptanceLetterReprintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcceptanceLetterReprintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptanceLetterReprintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
