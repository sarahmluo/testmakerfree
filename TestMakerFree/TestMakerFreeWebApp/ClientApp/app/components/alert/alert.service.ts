import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

import { TestMakerAlert, TestMakertAlertType } from './alert';


@Injectable()
export class TestMakerAlertService {
  constructor () { 
    this.change = this.emitter.asObservable();
  }
  
  public emitter: Subject<TestMakerAlert> = new Subject<TestMakerAlert>();
  public change: Observable<TestMakerAlert>;

  private cssTypes = {
    [TestMakertAlertType.Error]: 'alert-error'
  };

  /**
   * Add error alert.
   * 
   * @param message 
   */
  public error(message: string): void {
    this.add({
      text: message,
      type: TestMakertAlertType.Error,
    });
  }

  /**
   * Add an alert.
   * 
   * @param alert 
   */
  private add(alert: TestMakerAlert): void {

    alert = Object.assign({
      css: this.cssTypes[alert.type]
    }, alert);

    // Tell the subscriber there's a new alert
    this.emitter.next(alert);
  }
}