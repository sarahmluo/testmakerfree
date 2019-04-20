import { Component, Input } from '@angular/core';

@Component({
  selector: 'quiz-search',
  templateUrl: './quiz-search.component.html',
  styleUrls: ['./quiz-search.component.css'],
})
export class QuizSearchComponent {
  constructor() { }

  /**
   * Additional form css class.
   */
  @Input()
  public class: string;

  /**
   * Search Placeholder text.
   */
  @Input()
  public placeholder: string;
}