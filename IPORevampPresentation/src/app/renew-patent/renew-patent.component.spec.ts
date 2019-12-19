import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RenewPatentComponent } from './renew-patent.component';

describe('RenewPatentComponent', () => {
  let component: RenewPatentComponent;
  let fixture: ComponentFixture<RenewPatentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RenewPatentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RenewPatentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
