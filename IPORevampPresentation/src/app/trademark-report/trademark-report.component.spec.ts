import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrademarkReportComponent } from './trademark-report.component';

describe('TrademarkReportComponent', () => {
  let component: TrademarkReportComponent;
  let fixture: ComponentFixture<TrademarkReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrademarkReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrademarkReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
