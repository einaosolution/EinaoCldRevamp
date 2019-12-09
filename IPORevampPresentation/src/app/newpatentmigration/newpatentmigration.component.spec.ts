import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewpatentmigrationComponent } from './newpatentmigration.component';

describe('NewpatentmigrationComponent', () => {
  let component: NewpatentmigrationComponent;
  let fixture: ComponentFixture<NewpatentmigrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewpatentmigrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewpatentmigrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
