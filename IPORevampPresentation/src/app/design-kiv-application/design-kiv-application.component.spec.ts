import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignKivApplicationComponent } from './design-kiv-application.component';

describe('DesignKivApplicationComponent', () => {
  let component: DesignKivApplicationComponent;
  let fixture: ComponentFixture<DesignKivApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignKivApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignKivApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
