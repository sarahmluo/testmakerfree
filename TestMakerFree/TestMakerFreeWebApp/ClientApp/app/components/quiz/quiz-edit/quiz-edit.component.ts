import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { TestMakerFreeApiService } from '../../providers/api.service';

@Component({
    selector: 'quiz-edit',
    templateUrl: './quiz-edit.component.html',
    styleUrls: ['./quiz-edit.component.css']
})
export class QuizEditComponent implements OnInit {
    constructor (
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private api: TestMakerFreeApiService
    ) { }

    /**
     * Page title.
     */
    public title: string;

    /**
     * True when editing, false when creating.
     */
    public editMode: boolean;

    /**
     * Quiz object.
     */
    public quiz: Quiz;

    /**
     * On Init.
     */
    public ngOnInit(): void {
        this.quiz = <Quiz>{};
        let id: number = +this.activatedRoute.snapshot.params['id'];
        if (id) {
            this.editMode = true;
            // fetch the quiz from the server
            this.api.getQuiz(id).subscribe(res => {
                this.quiz = res;
                this.title = "Edit - " + this.quiz.Title;
            });
        } else {
            this.editMode = false;
            this.title = "Create New Quiz";
        }
    }

    /**
     * Logic executed when form is submitted.
     * 
     * @param quiz 
     */
    public onSubmit(quiz: Quiz): void {
        if (this.editMode) {
            this.api.putQuiz(quiz).subscribe(res => {
                let updatedQuiz: Quiz = res;
                console.log("Quiz " + updatedQuiz.Id + " has been updated");
                this.router.navigate(["home"]);
            });
        } else {
            this.api.postQuiz(quiz).subscribe(res => {
                let newQuiz: Quiz = res;
                console.log("Quiz " + newQuiz.Id + "  has been created");
                this.router.navigate(["home"]);
            });
        }
    }

    /**
     * Navigate back to home.
     */
    public onBack(): void {
        this.router.navigate(["home"]);
    }
}
