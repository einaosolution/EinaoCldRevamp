import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserOppositionComponent } from './user-opposition.component';

describe('UserOppositionComponent', () => {
  let component: UserOppositionComponent;
  let fixture: ComponentFixture<UserOppositionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserOppositionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserOppositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
