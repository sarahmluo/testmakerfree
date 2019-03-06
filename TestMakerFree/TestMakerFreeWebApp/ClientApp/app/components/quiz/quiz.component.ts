import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { TestMakerFreeApiService } from '../providers/api.service';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit{
  constructor (
    private activedRoute: ActivatedRoute,
    private api: TestMakerFreeApiService,
    private router: Router
  ) { }

  /**
   * Quiz object.
   */
  public quiz: Quiz;

  /** 
   * On Init.
   */
  public ngOnInit(): void {
    // use of snapshot avoids having to subscribe to the observable
    // plus sign returns numeric version of the variable
    let id: number = +this.activedRoute.snapshot.params["id"];
    console.log(id);
    if (id) {
      this.api.getQuiz(id).subscribe(res => this.quiz = res);
    } else {
      // redirect back to home
      console.log("Invalid id: routing back to home...");
      this.router.navigate(['home']);
    }
  }
}
