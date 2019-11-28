import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultdashboarddesigntemplateComponent } from './defaultdashboarddesigntemplate.component';

describe('DefaultdashboarddesigntemplateComponent', () => {
  let component: DefaultdashboarddesigntemplateComponent;
  let fixture: ComponentFixture<DefaultdashboarddesigntemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefaultdashboarddesigntemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultdashboarddesigntemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
