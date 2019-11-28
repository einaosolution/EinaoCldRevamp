import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultdashboardadmintemplateComponent } from './defaultdashboardadmintemplate.component';

describe('DefaultdashboardadmintemplateComponent', () => {
  let component: DefaultdashboardadmintemplateComponent;
  let fixture: ComponentFixture<DefaultdashboardadmintemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefaultdashboardadmintemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultdashboardadmintemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
