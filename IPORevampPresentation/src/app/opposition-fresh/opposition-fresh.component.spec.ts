import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OppositionFreshComponent } from './opposition-fresh.component';

describe('OppositionFreshComponent', () => {
  let component: OppositionFreshComponent;
  let fixture: ComponentFixture<OppositionFreshComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OppositionFreshComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OppositionFreshComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
