import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignAppealRefusalComponent } from './design-appeal-refusal.component';

describe('DesignAppealRefusalComponent', () => {
  let component: DesignAppealRefusalComponent;
  let fixture: ComponentFixture<DesignAppealRefusalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignAppealRefusalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignAppealRefusalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
