import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationBatch2Component } from './publication-batch2.component';

describe('PublicationBatch2Component', () => {
  let component: PublicationBatch2Component;
  let fixture: ComponentFixture<PublicationBatch2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PublicationBatch2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicationBatch2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
