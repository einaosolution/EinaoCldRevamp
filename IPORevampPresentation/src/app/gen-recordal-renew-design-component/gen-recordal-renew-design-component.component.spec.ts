import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalRenewDesignComponentComponent } from './gen-recordal-renew-design-component.component';

describe('GenRecordalRenewDesignComponentComponent', () => {
  let component: GenRecordalRenewDesignComponentComponent;
  let fixture: ComponentFixture<GenRecordalRenewDesignComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalRenewDesignComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalRenewDesignComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
