import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AboutComponent } from './components/about/about.component';
import { TestMakerAlertComponent } from './components/alert/alert.component';
import { TestMakerAlertService } from './components/alert/alert.service';
import { AnswerEditComponent } from './components/answer/answer-edit/answer-edit.component';
import { AnswerListComponent } from './components/answer/answer-list.component';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { TestMakerFreeApiService } from './components/providers/api.service';
import { QuestionEditComponent } from './components/question/question-edit/question-edit.component';
import { QuestionListComponent } from './components/question/question-list.component';
import { QuizListComponent } from './components/quiz-list/quiz-list.component';
import { QuizEditComponent } from './components/quiz/quiz-edit/quiz-edit.component';
import { QuizSearchComponent } from './components/quiz/quiz-search/quiz-search.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { ResultEditComponent } from './components/result/result-edit/result-edit.component';
import { ResultListComponent } from './components/result/result-list.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AboutComponent,
        LoginComponent,
        QuizComponent,
        QuizEditComponent,
        QuizListComponent,
        QuizSearchComponent,
        QuestionListComponent,
        QuestionEditComponent,
        AnswerListComponent,
        AnswerEditComponent,
        ResultEditComponent,
        ResultListComponent,
        TestMakerAlertComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'quiz/create', component: QuizEditComponent },
            { path: 'quiz/edit/:id', component: QuizEditComponent },
            { path: 'quiz/:id', component: QuizComponent},
            { path: 'question/create/:id', component: QuestionEditComponent},
            { path: 'question/edit/:id', component: QuestionEditComponent},
            { path: 'answer/create/:id', component: AnswerEditComponent },
            { path: 'answer/edit/:id', component: AnswerEditComponent },
            { path: 'result/create/:id', component: ResultEditComponent },
            { path: 'result/edit/:id', component: ResultEditComponent },
            { path: 'about', component: AboutComponent },
            { path: 'login', component: LoginComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        TestMakerFreeApiService,
        TestMakerAlertService
    ]
})
export class AppModuleShared {
}
