import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewrenewalComponent } from './viewrenewal.component';

describe('ViewrenewalComponent', () => {
  let component: ViewrenewalComponent;
  let fixture: ComponentFixture<ViewrenewalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewrenewalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewrenewalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
