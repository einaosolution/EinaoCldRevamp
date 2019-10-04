import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentuserlistingComponent } from './patentuserlisting.component';

describe('PatentuserlistingComponent', () => {
  let component: PatentuserlistingComponent;
  let fixture: ComponentFixture<PatentuserlistingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentuserlistingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentuserlistingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
