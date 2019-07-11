import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReConductSearchComponent } from './re-conduct-search.component';

describe('ReConductSearchComponent', () => {
  let component: ReConductSearchComponent;
  let fixture: ComponentFixture<ReConductSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReConductSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReConductSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
