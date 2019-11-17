import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalRenewComponent } from './gen-recordal-renew.component';

describe('GenRecordalRenewComponent', () => {
  let component: GenRecordalRenewComponent;
  let fixture: ComponentFixture<GenRecordalRenewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalRenewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalRenewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
