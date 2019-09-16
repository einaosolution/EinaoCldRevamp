import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationDetail2Component } from './publication-detail2.component';

describe('PublicationDetail2Component', () => {
  let component: PublicationDetail2Component;
  let fixture: ComponentFixture<PublicationDetail2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PublicationDetail2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicationDetail2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
