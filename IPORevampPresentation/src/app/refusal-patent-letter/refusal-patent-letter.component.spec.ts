import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefusalPatentLetterComponent } from './refusal-patent-letter.component';

describe('RefusalPatentLetterComponent', () => {
  let component: RefusalPatentLetterComponent;
  let fixture: ComponentFixture<RefusalPatentLetterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefusalPatentLetterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefusalPatentLetterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
