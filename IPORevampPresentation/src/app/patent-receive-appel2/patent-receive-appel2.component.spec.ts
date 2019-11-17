import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentReceiveAppel2Component } from './patent-receive-appel2.component';

describe('PatentReceiveAppel2Component', () => {
  let component: PatentReceiveAppel2Component;
  let fixture: ComponentFixture<PatentReceiveAppel2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentReceiveAppel2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentReceiveAppel2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
