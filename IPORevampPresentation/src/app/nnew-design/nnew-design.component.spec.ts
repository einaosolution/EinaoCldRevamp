import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NnewDesignComponent } from './nnew-design.component';

describe('NnewDesignComponent', () => {
  let component: NnewDesignComponent;
  let fixture: ComponentFixture<NnewDesignComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NnewDesignComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NnewDesignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
