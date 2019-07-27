import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RenewTrademarkComponent } from './renew-trademark.component';

describe('RenewTrademarkComponent', () => {
  let component: RenewTrademarkComponent;
  let fixture: ComponentFixture<RenewTrademarkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RenewTrademarkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RenewTrademarkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
