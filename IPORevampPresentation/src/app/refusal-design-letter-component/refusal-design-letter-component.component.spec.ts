import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefusalDesignLetterComponentComponent } from './refusal-design-letter-component.component';

describe('RefusalDesignLetterComponentComponent', () => {
  let component: RefusalDesignLetterComponentComponent;
  let fixture: ComponentFixture<RefusalDesignLetterComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefusalDesignLetterComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefusalDesignLetterComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
