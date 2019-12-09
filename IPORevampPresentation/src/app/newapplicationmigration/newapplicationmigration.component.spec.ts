import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewapplicationmigrationComponent } from './newapplicationmigration.component';

describe('NewapplicationmigrationComponent', () => {
  let component: NewapplicationmigrationComponent;
  let fixture: ComponentFixture<NewapplicationmigrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewapplicationmigrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewapplicationmigrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
