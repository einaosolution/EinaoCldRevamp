import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefuseApplicationComponent } from './refuse-application.component';

describe('RefuseApplicationComponent', () => {
  let component: RefuseApplicationComponent;
  let fixture: ComponentFixture<RefuseApplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefuseApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefuseApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
