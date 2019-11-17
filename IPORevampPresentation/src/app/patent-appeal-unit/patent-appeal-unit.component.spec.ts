import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentAppealUnitComponent } from './patent-appeal-unit.component';

describe('PatentAppealUnitComponent', () => {
  let component: PatentAppealUnitComponent;
  let fixture: ComponentFixture<PatentAppealUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentAppealUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentAppealUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
