import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalChangeOfName2Component } from './gen-recordal-change-of-name2.component';

describe('GenRecordalChangeOfName2Component', () => {
  let component: GenRecordalChangeOfName2Component;
  let fixture: ComponentFixture<GenRecordalChangeOfName2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalChangeOfName2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalChangeOfName2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
