import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultdashboardpatenttemplateComponent } from './defaultdashboardpatenttemplate.component';

describe('DefaultdashboardpatenttemplateComponent', () => {
  let component: DefaultdashboardpatenttemplateComponent;
  let fixture: ComponentFixture<DefaultdashboardpatenttemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefaultdashboardpatenttemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultdashboardpatenttemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
