import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { TestMakerFreeApiService } from '../providers/api.service';

@Component({
  selector: 'quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {
  constructor(
    private api: TestMakerFreeApiService,
    private router: Router
  ) { }

  /**
   * Css class for this list of quizzes.
   */
  @Input()
  public class: string;

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
   * URL for calling api.
   */
  public quizUrl: string = 'api/quiz/';

  /**
   * On Init.
   */
  public ngOnInit(): void {
    switch (this.class) {
      case 'latest':
      default:
        this.title = 'Latest Quizzes';
        this.quizUrl += 'Latest/';
        break;
      case 'byTitle':
        this.title = 'Quizzes by Title';
        this.quizUrl += 'ByTitle/';
        break;
      case 'random':
        this.title = 'Random Quizzes';
        this.quizUrl += 'Random/';
        break;
    }
    this.getQuizzes();
  }

  /**
   * Logic to execute when a quiz is selected.
   * @param quiz 
   */
  public onSelect(quiz: Quiz): void {
    this.selectedQuiz = quiz;
    console.log('Quiz ID#: ' + this.selectedQuiz.Id + ' has been selected.');
    this.router.navigate(['quiz', this.selectedQuiz.Id]);
  }

  /**
   * Get quizzes.
   */
  private getQuizzes(): void {
    this.api.get<Quiz[]>(this.quizUrl)
      .subscribe(res => {
        this.quizzes = res;
      });
  }
}
