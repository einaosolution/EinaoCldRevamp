import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemittaComponent } from './remitta.component';

describe('RemittaComponent', () => {
  let component: RemittaComponent;
  let fixture: ComponentFixture<RemittaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemittaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemittaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
