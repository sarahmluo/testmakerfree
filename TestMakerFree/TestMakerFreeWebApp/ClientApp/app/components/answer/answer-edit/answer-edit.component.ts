import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { TestMakerFreeApiService } from '../../providers/api.service';

@Component({
  selector: 'answer-edit',
  templateUrl: 'answer-edit.component.html',
  styleUrls: ['./answer-edit.component.css']
})
export class AnswerEditComponent implements OnInit {
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
  public answer: Answer;

  /**
   * Flag to determine edit mode.
   */
  public editMode: boolean;

  /**
   * API url.
   */
  private url: string = "api/answer";

  /**
   * On Init.
   */
  public ngOnInit(): void {
    this.answer = <Answer>{};

    const id: number = +this.activatedRoute.snapshot.params['id'];

    // check if we're in edit mode or not
    this.editMode = (this.activatedRoute.snapshot.url[1].path === 'edit');

    if (this.editMode) {
      // fetch the question from the server
      this.api.get<Answer>(this.url + '/' + id)
        .subscribe(res => {
          this.answer = res;
          this.title = 'Edit - ' + this.answer.Text;
        })
    } else {
      this.answer.QuestionId = id;
      this.title = 'Create a New Answer';
    }
  }

  /**
   * Logic to execute when form is submitted.
   */
  public onSubmit(answer: Answer): void {
    if (this.editMode) {
      this.api.put<Answer>(this.url, answer)
        .subscribe(res => {
          const retAnswer: Answer = res;
          console.log('Answer ' + retAnswer.Id + ' has been updated');
          this.router.navigate(['quiz/edit', retAnswer.QuestionId]);
      })
    } else {
      this.api.post<Answer>(this.url, answer)
        .subscribe(res => {
          const newAnswer: Answer = res;
          console.log('Answer ' + newAnswer.Id + ' has been created');
          this.router.navigate(['quiz.edit', newAnswer.QuestionId]);
        })
    }
  }

  /**
   * Navigate Back.
   */
  public onBack(): void {
    this.router.navigate(['question/edit', this.answer.QuestionId]);
  }

}