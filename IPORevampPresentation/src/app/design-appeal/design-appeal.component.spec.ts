import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignAppealComponent } from './design-appeal.component';

describe('DesignAppealComponent', () => {
  let component: DesignAppealComponent;
  let fixture: ComponentFixture<DesignAppealComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignAppealComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignAppealComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
