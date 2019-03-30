import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';

import { TestMakerFreeApiService } from '../providers/api.service';

@Component({
  selector: 'result-list',
  templateUrl: './result-list.component.html',
  styleUrls: ['./result-list.component.css']
})
export class ResultListComponent implements OnChanges {
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
   * List of results for the provided quiz.
   */
  public results: Result[];

  /**
   * Display title.
   */
  public title: string;

  /**
   * On Changes.
   * @param changes 
   */
  public ngOnChanges(changes: SimpleChanges): void {
    if (changes.quiz) {
      // only perform the task if the value has been changed.
      if (!changes.quiz.isFirstChange()) {
        this.loadData();
      }
    }
  }

  /**
   * Navigate to result create component.
   */
  public onCreate(): void {
    this.router.navigate(['/result/create', this.quiz.Id]);
  }

  /**
   * Navigate to result edit component.
   */
  public onEdit(result: Result): void {
    this.router.navigate(['/result/edit', result.Id]);
  }

  /**
   * Delete a result.
   * @param result
   */
  public onDelete(result: Result): void {
    if (confirm('Do you really want to delete this result?')) {
      this.api.delete('api/result/', result.Id)
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
    this.api.get<Question[]>('api/result/All/' + this.quiz.Id)
      .subscribe(res => this.results = res);
  }
}