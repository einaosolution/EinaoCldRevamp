import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentReceiveAppealComponent } from './patent-receive-appeal.component';

describe('PatentReceiveAppealComponent', () => {
  let component: PatentReceiveAppealComponent;
  let fixture: ComponentFixture<PatentReceiveAppealComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentReceiveAppealComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentReceiveAppealComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
