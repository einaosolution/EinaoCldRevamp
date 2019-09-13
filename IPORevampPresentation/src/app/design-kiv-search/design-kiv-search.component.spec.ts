import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignKivSearchComponent } from './design-kiv-search.component';

describe('DesignKivSearchComponent', () => {
  let component: DesignKivSearchComponent;
  let fixture: ComponentFixture<DesignKivSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignKivSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignKivSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
