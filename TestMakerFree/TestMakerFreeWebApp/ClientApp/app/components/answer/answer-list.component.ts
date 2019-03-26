import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';

import { TestMakerFreeApiService } from '../providers/api.service';

@Component({
  selector: 'answer-list',
  templateUrl: './answer-list.component.html',
  styleUrls: ['./answer-list.component.css']
})
export class AnswerListComponent implements OnChanges {
  constructor (
    private api: TestMakerFreeApiService,
    private router: Router
  ) { }

  /**
   * Parent question object.
   */
  @Input()
  public question: Question;

  /**
   * List of questions for the provided quiz.
   */
  public answers: Answer[] = [];

  /**
   * Quiz title.
   */
  public title: string;

  /**
   * On Changes.
   * @param changes 
   */
  public ngOnChanges(changes: SimpleChanges): void {
    if (changes.question) {
      // only perform the task if the value has been changed.
      if (!changes.question.isFirstChange()) {
        this.loadData();
      }
    }
  }

  /**
   * Navigate to answer create component.
   */
  public onCreate(): void {
    this.router.navigate(['/answer/create/', this.question.Id]);
  }

  /**
   * Navigate to answer edit component.
   */
  public onEdit(answer: Answer): void {
    this.router.navigate(['/answer/edit/', answer.Id]);
  }

  /**
   * Delete ann answer
   * @param question
   */
  public onDelete(answer: Answer): void {
    if (confirm('Do you really want to delete this answer?')) {
      this.api.delete('api/answer/', answer.Id)
        .subscribe(res => { 
          console.log(res);
          this.loadData();
        });
    }
  }

  /**
   * Get all answers associated to a question.
   */
  private loadData(): void {
    this.api.get<Answer[]>('api/Answer/All/' + this.question.Id)
      .subscribe(res => this.answers = res);
  }
}