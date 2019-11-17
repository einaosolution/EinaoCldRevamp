import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPatentComponent } from './new-patent.component';

describe('NewPatentComponent', () => {
  let component: NewPatentComponent;
  let fixture: ComponentFixture<NewPatentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewPatentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPatentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
