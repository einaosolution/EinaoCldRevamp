import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignuserlistingComponent } from './designuserlisting.component';

describe('DesignuserlistingComponent', () => {
  let component: DesignuserlistingComponent;
  let fixture: ComponentFixture<DesignuserlistingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesignuserlistingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignuserlistingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
