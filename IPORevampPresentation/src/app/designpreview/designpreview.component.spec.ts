import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignpreviewComponent } from './designpreview.component';

describe('DesignpreviewComponent', () => {
  let component: DesignpreviewComponent;
  let fixture: ComponentFixture<DesignpreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignpreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignpreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
