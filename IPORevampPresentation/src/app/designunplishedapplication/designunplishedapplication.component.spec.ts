import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignunplishedapplicationComponent } from './designunplishedapplication.component';

describe('DesignunplishedapplicationComponent', () => {
  let component: DesignunplishedapplicationComponent;
  let fixture: ComponentFixture<DesignunplishedapplicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignunplishedapplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignunplishedapplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
