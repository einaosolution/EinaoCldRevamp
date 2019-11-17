import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Patentregistracertificate2Component } from './patentregistracertificate2.component';

describe('Patentregistracertificate2Component', () => {
  let component: Patentregistracertificate2Component;
  let fixture: ComponentFixture<Patentregistracertificate2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Patentregistracertificate2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Patentregistracertificate2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
