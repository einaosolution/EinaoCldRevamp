import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignAppealComponent } from './assign-appeal.component';

describe('AssignAppealComponent', () => {
  let component: AssignAppealComponent;
  let fixture: ComponentFixture<AssignAppealComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssignAppealComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignAppealComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
