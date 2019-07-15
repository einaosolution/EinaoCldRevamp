import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefusalLetterComponent } from './refusal-letter.component';

describe('RefusalLetterComponent', () => {
  let component: RefusalLetterComponent;
  let fixture: ComponentFixture<RefusalLetterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefusalLetterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefusalLetterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
