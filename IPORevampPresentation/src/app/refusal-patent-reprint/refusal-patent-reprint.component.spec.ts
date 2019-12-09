import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefusalPatentReprintComponent } from './refusal-patent-reprint.component';

describe('RefusalPatentReprintComponent', () => {
  let component: RefusalPatentReprintComponent;
  let fixture: ComponentFixture<RefusalPatentReprintComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefusalPatentReprintComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefusalPatentReprintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
