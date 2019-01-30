import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { tap } from 'rxjs/operators';


@Injectable()
export class TestMakerFreeApiService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
   }

   /**
    * URL for api calls.
    */
   private baseUrl: string;

   /**
    * Make api call via HTTP get.
    */
   public get<T>(url: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}${url}`)
      .pipe(
        tap(res => res, error => console.error(error))
      );
   }
}