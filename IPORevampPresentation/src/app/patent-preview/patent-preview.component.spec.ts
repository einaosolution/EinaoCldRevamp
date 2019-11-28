import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentPreviewComponent } from './patent-preview.component';

describe('PatentPreviewComponent', () => {
  let component: PatentPreviewComponent;
  let fixture: ComponentFixture<PatentPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
