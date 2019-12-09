import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptancedesignReprintletterComponent } from './acceptancedesign-reprintletter.component';

describe('AcceptancedesignReprintletterComponent', () => {
  let component: AcceptancedesignReprintletterComponent;
  let fixture: ComponentFixture<AcceptancedesignReprintletterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcceptancedesignReprintletterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptancedesignReprintletterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
