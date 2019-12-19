import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalRenew2Component } from './gen-recordal-renew2.component';

describe('GenRecordalRenew2Component', () => {
  let component: GenRecordalRenew2Component;
  let fixture: ComponentFixture<GenRecordalRenew2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalRenew2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalRenew2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
