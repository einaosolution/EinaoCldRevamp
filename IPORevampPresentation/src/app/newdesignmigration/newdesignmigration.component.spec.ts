import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewdesignmigrationComponent } from './newdesignmigration.component';

describe('NewdesignmigrationComponent', () => {
  let component: NewdesignmigrationComponent;
  let fixture: ComponentFixture<NewdesignmigrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewdesignmigrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewdesignmigrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
