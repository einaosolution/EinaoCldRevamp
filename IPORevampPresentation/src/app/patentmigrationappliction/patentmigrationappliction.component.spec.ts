import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatentmigrationapplictionComponent } from './patentmigrationappliction.component';

describe('PatentmigrationapplictionComponent', () => {
  let component: PatentmigrationapplictionComponent;
  let fixture: ComponentFixture<PatentmigrationapplictionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatentmigrationapplictionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatentmigrationapplictionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
