import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BackEndUserComponent } from './back-end-user.component';

describe('BackEndUserComponent', () => {
  let component: BackEndUserComponent;
  let fixture: ComponentFixture<BackEndUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BackEndUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BackEndUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
