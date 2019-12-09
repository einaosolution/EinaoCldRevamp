import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcknowledgementDesignComponent } from './acknowledgement-design.component';

describe('AcknowledgementDesignComponent', () => {
  let component: AcknowledgementDesignComponent;
  let fixture: ComponentFixture<AcknowledgementDesignComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcknowledgementDesignComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcknowledgementDesignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
