import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignReceiveAppealComponent } from './design-receive-appeal.component';

describe('DesignReceiveAppealComponent', () => {
  let component: DesignReceiveAppealComponent;
  let fixture: ComponentFixture<DesignReceiveAppealComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignReceiveAppealComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignReceiveAppealComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
