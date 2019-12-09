import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignmigrateapplicationComponent } from './designmigrateapplication.component';

describe('DesignmigrateapplicationComponent', () => {
  let component: DesignmigrateapplicationComponent;
  let fixture: ComponentFixture<DesignmigrateapplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignmigrateapplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignmigrateapplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
