import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptancepatentLetterComponent } from './acceptancepatent-letter.component';

describe('AcceptancepatentLetterComponent', () => {
  let component: AcceptancepatentLetterComponent;
  let fixture: ComponentFixture<AcceptancepatentLetterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcceptancepatentLetterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptancepatentLetterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
