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
}
