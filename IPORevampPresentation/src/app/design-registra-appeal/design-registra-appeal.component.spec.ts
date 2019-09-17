import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignRegistraAppealComponent } from './design-registra-appeal.component';

describe('DesignRegistraAppealComponent', () => {
  let component: DesignRegistraAppealComponent;
  let fixture: ComponentFixture<DesignRegistraAppealComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignRegistraAppealComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignRegistraAppealComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
