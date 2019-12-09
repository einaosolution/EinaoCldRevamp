import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefuseApplicationReprintComponent } from './refuse-application-reprint.component';

describe('RefuseApplicationReprintComponent', () => {
  let component: RefuseApplicationReprintComponent;
  let fixture: ComponentFixture<RefuseApplicationReprintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefuseApplicationReprintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefuseApplicationReprintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
