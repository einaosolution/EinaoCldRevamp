import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BackEndUserProfileComponent } from './back-end-user-profile.component';

describe('BackEndUserProfileComponent', () => {
  let component: BackEndUserProfileComponent;
  let fixture: ComponentFixture<BackEndUserProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BackEndUserProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BackEndUserProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
