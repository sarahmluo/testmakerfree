import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AboutComponent } from './components/about/about.component';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { TestMakerFreeApiService } from './components/providers/api.service';
import { QuizListComponent } from './components/quiz-list/quiz-list.component';
import { QuizEditComponent } from './components/quiz/quiz-edit/quiz-edit.component';
import { QuizComponent } from './components/quiz/quiz.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AboutComponent,
        LoginComponent,
        QuizComponent,
        QuizEditComponent,
        QuizListComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'quiz/create', component: QuizEditComponent },
            { path: 'quiz/:id', component: QuizComponent},
            { path: 'about', component: AboutComponent },
            { path: 'login', component: LoginComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        TestMakerFreeApiService
    ]
})
export class AppModuleShared {
}
