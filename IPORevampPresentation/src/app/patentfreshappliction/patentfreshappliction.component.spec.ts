import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentfreshapplictionComponent } from './patentfreshappliction.component';

describe('PatentfreshapplictionComponent', () => {
  let component: PatentfreshapplictionComponent;
  let fixture: ComponentFixture<PatentfreshapplictionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentfreshapplictionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentfreshapplictionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
