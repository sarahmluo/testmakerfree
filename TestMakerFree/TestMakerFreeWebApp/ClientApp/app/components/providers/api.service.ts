import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { TestMakerAlertService } from '../alert/alert.service';


@Injectable()
export class TestMakerFreeApiService {
  constructor(
    private http: HttpClient,
    private alertService: TestMakerAlertService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
   }

   /**
    * URL for api calls.
    */
   private baseUrl: string;

  /**
   * URL for calling quiz api.
   */
  public quizUrl: string = 'api/quiz/';

   /**
    * Make api call via HTTP get.
    */
   public get<T>(url: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}${url}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
   }

   /**
    * Make api call via HTTP post.
    * @param url 
    */
   public post<T>(url: string, data: T): Observable<T> {
     return this.http.post<T>(`${this.baseUrl}${url}`, data)
     .pipe(
       catchError(error => this.handleError(error))
     );
   }

   /**
    * Make api call via HTTP post.
    * @param url 
    */
   public put<T>(url: string, data: T): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}${url}`, data)
    .pipe(
      catchError(error => this.handleError(error))
    );
  }

  /**
   * Fetch quiz corresponding to provided id.
   * 
   * @param id 
   */
  public getQuiz(id: number): Observable<Quiz> {
    return this.http.get<Quiz>(this.baseUrl + this.quizUrl + id)
      .pipe(
        catchError(error => Observable.throw(error))
      );
  }

  /**
   * Update provided quiz.
   * 
   * @param quiz 
   */
  public putQuiz(quiz: Quiz): Observable<Quiz> {
    return this.http.put<Quiz>(this.baseUrl + this.quizUrl, quiz)
      .pipe(
        catchError(error => Observable.throw(error))
      );
  }

  /**
   * Create New quiz.
   * 
   * @param quiz 
   */
  public postQuiz(quiz: Quiz): Observable<Quiz> {
    return this.http.post<Quiz>(this.baseUrl + this.quizUrl, quiz)
      .pipe(
        catchError(error => Observable.throw(error))
      );
  }

  /**
   * Delete a quiz.
   * 
   * @param id 
   */
  public deleteQuiz(id: number): Observable<any> {
    return this.http.delete(this.baseUrl + this.quizUrl + id)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Delete generic entity.
   * @param url
   * @param id 
   */
  public delete(url: string, id: number): Observable<any> {
    return this.http.delete(this.baseUrl + url + id)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Add error to alerts.
   * 
   * @param err 
   */
  private handleError(err: any): Observable<any> {
    let message: string = err.message || err.error.error;
    this.alertService.error(message);

    return Observable.throw(message);
  }
}
