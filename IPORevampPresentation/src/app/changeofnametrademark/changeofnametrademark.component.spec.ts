import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeofnametrademarkComponent } from './changeofnametrademark.component';

describe('ChangeofnametrademarkComponent', () => {
  let component: ChangeofnametrademarkComponent;
  let fixture: ComponentFixture<ChangeofnametrademarkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeofnametrademarkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeofnametrademarkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
