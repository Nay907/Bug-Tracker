import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Bugs } from 'src/app/shared/models/Bugs';

@Component({
  selector: 'app-bug-comment',
  templateUrl: './bug-comment.component.html',
  styleUrls: ['./bug-comment.component.scss']
})
export class BugCommentComponent {
  @Input() bug: Bugs | null = null;
  @Output() close = new EventEmitter<void>();

 closeModal(): void {
    this.close.emit();
 }

}
