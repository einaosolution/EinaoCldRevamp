import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadJudgementComponent } from './upload-judgement.component';

describe('UploadJudgementComponent', () => {
  let component: UploadJudgementComponent;
  let fixture: ComponentFixture<UploadJudgementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadJudgementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadJudgementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
