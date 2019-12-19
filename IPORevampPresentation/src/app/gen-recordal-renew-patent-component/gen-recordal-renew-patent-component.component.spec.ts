import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalRenewPatentComponentComponent } from './gen-recordal-renew-patent-component.component';

describe('GenRecordalRenewPatentComponentComponent', () => {
  let component: GenRecordalRenewPatentComponentComponent;
  let fixture: ComponentFixture<GenRecordalRenewPatentComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalRenewPatentComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalRenewPatentComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
