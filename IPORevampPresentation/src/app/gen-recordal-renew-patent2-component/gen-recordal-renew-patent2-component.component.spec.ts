import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalRenewPatent2ComponentComponent } from './gen-recordal-renew-patent2-component.component';

describe('GenRecordalRenewPatent2ComponentComponent', () => {
  let component: GenRecordalRenewPatent2ComponentComponent;
  let fixture: ComponentFixture<GenRecordalRenewPatent2ComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalRenewPatent2ComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalRenewPatent2ComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
