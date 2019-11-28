import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultdashboardtrademarktemplateComponent } from './defaultdashboardtrademarktemplate.component';

describe('DefaultdashboardtrademarktemplateComponent', () => {
  let component: DefaultdashboardtrademarktemplateComponent;
  let fixture: ComponentFixture<DefaultdashboardtrademarktemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefaultdashboardtrademarktemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultdashboardtrademarktemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
