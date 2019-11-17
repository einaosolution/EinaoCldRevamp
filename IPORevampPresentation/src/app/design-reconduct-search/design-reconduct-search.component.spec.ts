import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignReconductSearchComponent } from './design-reconduct-search.component';

describe('DesignReconductSearchComponent', () => {
  let component: DesignReconductSearchComponent;
  let fixture: ComponentFixture<DesignReconductSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignReconductSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignReconductSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
