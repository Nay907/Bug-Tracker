import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BugCommentComponent } from './bug-comment.component';

describe('BugCommentComponent', () => {
  let component: BugCommentComponent;
  let fixture: ComponentFixture<BugCommentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BugCommentComponent]
    });
    fixture = TestBed.createComponent(BugCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
