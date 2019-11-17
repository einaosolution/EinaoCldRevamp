import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RenewDesignComponentComponent } from './renew-design-component.component';

describe('RenewDesignComponentComponent', () => {
  let component: RenewDesignComponentComponent;
  let fixture: ComponentFixture<RenewDesignComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RenewDesignComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RenewDesignComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
