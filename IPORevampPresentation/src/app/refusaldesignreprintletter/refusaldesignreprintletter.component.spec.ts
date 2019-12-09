import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefusaldesignreprintletterComponent } from './refusaldesignreprintletter.component';

describe('RefusaldesignreprintletterComponent', () => {
  let component: RefusaldesignreprintletterComponent;
  let fixture: ComponentFixture<RefusaldesignreprintletterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefusaldesignreprintletterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefusaldesignreprintletterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
