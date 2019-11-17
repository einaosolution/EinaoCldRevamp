import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPatent2Component } from './new-patent2.component';

describe('NewPatent2Component', () => {
  let component: NewPatent2Component;
  let fixture: ComponentFixture<NewPatent2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewPatent2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPatent2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
