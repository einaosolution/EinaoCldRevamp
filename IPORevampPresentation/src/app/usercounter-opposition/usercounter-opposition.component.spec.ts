import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsercounterOppositionComponent } from './usercounter-opposition.component';

describe('UsercounterOppositionComponent', () => {
  let component: UsercounterOppositionComponent;
  let fixture: ComponentFixture<UsercounterOppositionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsercounterOppositionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsercounterOppositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
