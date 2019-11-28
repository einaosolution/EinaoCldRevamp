import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrademarkPreviewComponent } from './trademark-preview.component';

describe('TrademarkPreviewComponent', () => {
  let component: TrademarkPreviewComponent;
  let fixture: ComponentFixture<TrademarkPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrademarkPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrademarkPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
