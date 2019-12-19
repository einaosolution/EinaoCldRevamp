import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GgenRecordalRenewDesign2Component } from './ggen-recordal-renew-design2.component';

describe('GgenRecordalRenewDesign2Component', () => {
  let component: GgenRecordalRenewDesign2Component;
  let fixture: ComponentFixture<GgenRecordalRenewDesign2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GgenRecordalRenewDesign2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GgenRecordalRenewDesign2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
