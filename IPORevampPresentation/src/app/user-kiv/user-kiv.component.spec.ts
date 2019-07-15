import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserKivComponent } from './user-kiv.component';

describe('UserKivComponent', () => {
  let component: UserKivComponent;
  let fixture: ComponentFixture<UserKivComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserKivComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserKivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
