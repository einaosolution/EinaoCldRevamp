import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MergerTrademarkComponent } from './merger-trademark.component';

describe('MergerTrademarkComponent', () => {
  let component: MergerTrademarkComponent;
  let fixture: ComponentFixture<MergerTrademarkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MergerTrademarkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MergerTrademarkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
