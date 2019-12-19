import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeofaddresstrademarkComponent } from './changeofaddresstrademark.component';

describe('ChangeofaddresstrademarkComponent', () => {
  let component: ChangeofaddresstrademarkComponent;
  let fixture: ComponentFixture<ChangeofaddresstrademarkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeofaddresstrademarkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeofaddresstrademarkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
