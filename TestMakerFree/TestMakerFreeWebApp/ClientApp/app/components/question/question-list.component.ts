import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';

import { TestMakerFreeApiService } from '../providers/api.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'question-list',
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent implements OnChanges {
  constructor (
    private api: TestMakerFreeApiService,
    private router: Router
  ) { }

  /**
   * Parent quiz object.
   */
  @Input()
  public quiz: Quiz;

  /**
   * List of questions for the provided quiz.
   */
  public questions: Question[] = [];

  /**
   * Quiz title.
   */
  public title: string;

  /**
   * On Changes.
   * @param changes 
   */
  public ngOnChanges(changes: SimpleChanges): void {
    if (changes.quiz && changes.quiz.currentValue) {
      // only perform the task if the value has been changed.
      if (!changes.quiz.isFirstChange()) {
        this.loadData();
      ;
      }
    }
  }

  /**
   * Navigate to question create component.
   */
  public onCreate(): void {
    this.router.navigate(['/question/create', this.quiz.Id]);
  }

  /**
   * Navigate to question edit component.
   */
  public onEdit(question: Question): void {
    this.router.navigate(['/question/edit', question.Id]);
  }

  /**
   * Delete a question.
   * @param question
   */
  public onDelete(question: Question): void {
    if (confirm('Do you really want to delete this question?')) {
      this.api.delete('api/question', question.Id)
        .subscribe(res => { 
          console.log(res);
          this.loadData();
        });
    }
  }

  /**
   * Get all questions associated to a quiz.
   */
  private loadData(): void {
    this.api.get<Question[]>('api/question/All/' + this.quiz.Id)
      .subscribe(res => this.questions = res);
  }
}