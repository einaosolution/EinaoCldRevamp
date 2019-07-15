import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PremSearchComponent } from './prem-search.component';

describe('PremSearchComponent', () => {
  let component: PremSearchComponent;
  let fixture: ComponentFixture<PremSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PremSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PremSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
