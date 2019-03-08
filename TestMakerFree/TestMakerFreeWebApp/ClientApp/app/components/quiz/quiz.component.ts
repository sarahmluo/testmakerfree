import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { TestMakerFreeApiService } from '../providers/api.service';

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

  /**
   * Logic executed when Edit button is clicked.
   */
  public onEdit(): void {
    this.router.navigate(["quiz/edit", this.quiz.Id]);
  }

  /**
   * Logic executed when delete button is clicked.
   */
  public onDelete(): void {
    if (confirm("Do you really want to delete this quiz?")) {
      this.api.deleteQuiz(this.quiz.Id).subscribe(res => {
        console.log("Quiz " + this.quiz.Id + " has been deleted.");
        this.router.navigate(["home"]);
      })
    }
  }
}
