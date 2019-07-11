import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationBatchComponent } from './publication-batch.component';

describe('PublicationBatchComponent', () => {
  let component: PublicationBatchComponent;
  let fixture: ComponentFixture<PublicationBatchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PublicationBatchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicationBatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
