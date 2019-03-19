import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { TestMakerFreeApiService } from '../../providers/api.service';

@Component({
  selector: 'question-edit',
  templateUrl: 'question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})
export class QuestionEditComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private api: TestMakerFreeApiService
  ) { }

  /**
   * Display title.
   */
  public title: string;

  /**
   * Question to edit.
   */
  public question: Question;

  /**
   * Flag to determine edit mode.
   */
  public editMode: boolean;

  /**
   * API url.
   */
  private url: string = "api/question";

  /**
   * On Init.
   */
  public ngOnInit(): void {
    this.question = <Question>{};

    const id: number = +this.activatedRoute.snapshot.params['id'];

    // check if we're in edit mode or not
    this.editMode = (this.activatedRoute.snapshot.url[1].path === 'edit');

    if (this.editMode) {
      // fetch the question from the server
      this.api.get<Question>('api/question' + id)
        .subscribe(res => {
          this.question = res;
          this.title = 'Edit - ' + this.question.Text;
        })
    } else {
      this.question.QuizId = id;
      this.title = 'Create a New Question';
    }
  }

  /**
   * Logic to execute when form is submitted.
   */
  public onSubmit(question: Question): void {
    if (this.editMode) {
      this.api.put<Question>(this.url, question)
        .subscribe(res => {
          const retQuestion: Question = res;
          console.log('Question ' + retQuestion.Id + ' has been updated');
          this.router.navigate(['quiz/edit', retQuestion.QuizId]);
      })
    } else {
      this.api.post<Question>(this.url, question)
        .subscribe(res => {
          const newQuestion: Question = res;
          console.log('Question ' + newQuestion.Id + ' has been created');
          this.router.navigate(['quiz.edit', newQuestion.QuizId]);
        })
    }
  }

  /**
   * Navigate Back.
   */
  public onBack(): void {
    this.router.navigate(['quiz/edit', this.question.QuizId]);
  }

}