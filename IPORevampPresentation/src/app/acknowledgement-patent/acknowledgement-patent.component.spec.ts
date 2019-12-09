import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcknowledgementPatentComponent } from './acknowledgement-patent.component';

describe('AcknowledgementPatentComponent', () => {
  let component: AcknowledgementPatentComponent;
  let fixture: ComponentFixture<AcknowledgementPatentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcknowledgementPatentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcknowledgementPatentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
