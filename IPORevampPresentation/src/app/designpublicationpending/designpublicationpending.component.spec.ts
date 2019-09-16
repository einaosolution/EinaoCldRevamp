import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignpublicationpendingComponent } from './designpublicationpending.component';

describe('DesignpublicationpendingComponent', () => {
  let component: DesignpublicationpendingComponent;
  let fixture: ComponentFixture<DesignpublicationpendingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignpublicationpendingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignpublicationpendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
