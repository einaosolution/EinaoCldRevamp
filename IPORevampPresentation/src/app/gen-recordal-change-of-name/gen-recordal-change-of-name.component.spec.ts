import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenRecordalChangeOfNameComponent } from './gen-recordal-change-of-name.component';

describe('GenRecordalChangeOfNameComponent', () => {
  let component: GenRecordalChangeOfNameComponent;
  let fixture: ComponentFixture<GenRecordalChangeOfNameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenRecordalChangeOfNameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenRecordalChangeOfNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
