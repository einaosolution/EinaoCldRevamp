import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignApplicationListingComponent } from './design-application-listing.component';

describe('DesignApplicationListingComponent', () => {
  let component: DesignApplicationListingComponent;
  let fixture: ComponentFixture<DesignApplicationListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignApplicationListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignApplicationListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
