import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrademarkuserlistingComponent } from './trademarkuserlisting.component';

describe('TrademarkuserlistingComponent', () => {
  let component: TrademarkuserlistingComponent;
  let fixture: ComponentFixture<TrademarkuserlistingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrademarkuserlistingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrademarkuserlistingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
