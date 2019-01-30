import { Component, OnInit } from '@angular/core';

import { TestMakerFreeApiService } from '../providers/api.service';

@Component({
  selector: 'quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {
  constructor(
    private api: TestMakerFreeApiService
  ) { }

  /**
   * List title.
   */
  public title: string;

  /**
   * Currently selcted quiz.
   */
  public selectedQuiz: Quiz;

  /**
   * Array of quiz objects.
   */
  public quizzes: Quiz[];

  /**
   * URL for retrieving the latest quizzes.
   */
  public getLatestUrl: string = 'api/quiz/Latest/';

  /**
   * On Init.
   */
  public ngOnInit(): void {
    this.title = 'Latest Quizzes';
    // get the latest quizzes
    this.getLatest();
  }

  /**
   * Logic to execute when a quiz is selected.
   * @param quiz 
   */
  public onSelect(quiz: Quiz): void {
    this.selectedQuiz = quiz;
    console.log('Quiz ID#: ' + this.selectedQuiz.Id + ' has been selected.');
  }

  /**
   * Get the latest quizzes.
   */
  private getLatest(): void {
    this.api.get<Quiz[]>(this.getLatestUrl)
      .subscribe(res => {
        this.quizzes = res;
      });
  }
}
