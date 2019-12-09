import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptancepatentreprintLetterComponent } from './acceptancepatentreprint-letter.component';

describe('AcceptancepatentreprintLetterComponent', () => {
  let component: AcceptancepatentreprintLetterComponent;
  let fixture: ComponentFixture<AcceptancepatentreprintLetterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcceptancepatentreprintLetterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptancepatentreprintLetterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
