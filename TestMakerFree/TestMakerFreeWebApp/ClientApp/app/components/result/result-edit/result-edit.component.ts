import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { TestMakerFreeApiService } from '../../providers/api.service';

@Component({
  selector: 'result-edit',
  templateUrl: 'result-edit.component.html',
  styleUrls: ['./result-edit.component.css']
})
export class ResultEditComponent implements OnInit {
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
   * Result to edit.
   */
  public result: Result;

  /**
   * Flag to determine edit mode.
   */
  public editMode: boolean;

  /**
   * API url.
   */
  private url: string = "api/result/";

  /**
   * On Init.
   */
  public ngOnInit(): void {
    this.result = <Result>{};

    const id: number = +this.activatedRoute.snapshot.params['id'];

    // check if we're in edit mode or not
    this.editMode = (this.activatedRoute.snapshot.url[1].path === 'edit');

    if (this.editMode) {
      // fetch the question from the server
      this.api.get<Result>(this.url + id)
        .subscribe(res => {
          this.result = res;
          this.title = 'Edit - ' + this.result.Text;
        })
    } else {
      this.result.QuizId = id;
      this.title = 'Create a New Result';
    }
  }

  /**
   * Logic to execute when form is submitted.
   */
  public onSubmit(result: Result): void {
    if (this.editMode) {
      this.api.put<Result>(this.url, result)
        .subscribe(res => {
          const retResult: Result = res;
          console.log('Result ' + retResult.Id + ' has been updated');
          this.router.navigate(['quiz/edit', retResult.QuizId]);
      })
    } else {
      this.api.post<Result>(this.url, result)
        .subscribe(res => {
          const newResult: Result = res;
          console.log('Result ' + newResult.Id + ' has been created');
          this.router.navigate(['quiz.edit', newResult.QuizId]);
        })
    }
  }

  /**
   * Navigate Back.
   */
  public onBack(): void {
    this.router.navigate(['quiz/edit', this.result.QuizId]);
  }

}