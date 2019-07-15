import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiveAppealComponent } from './receive-appeal.component';

describe('ReceiveAppealComponent', () => {
  let component: ReceiveAppealComponent;
  let fixture: ComponentFixture<ReceiveAppealComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReceiveAppealComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceiveAppealComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
