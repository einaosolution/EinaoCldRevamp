import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignFreshApplicationComponent } from './design-fresh-application.component';

describe('DesignFreshApplicationComponent', () => {
  let component: DesignFreshApplicationComponent;
  let fixture: ComponentFixture<DesignFreshApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignFreshApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignFreshApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
