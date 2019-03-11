import { Component, OnInit } from '@angular/core';
import { TestMakerAlertService } from './alert.service';
import { TestMakerAlert } from './alert';


@Component({
  selector: 'tm-alert',
  templateUrl: 'alert.component.html',
  styleUrls: ['alert.component.css']
})
export class TestMakerAlertComponent implements OnInit {
  constructor (
    private alertService: TestMakerAlertService
  ) { }

  /**
   * Collection of active alerts.
   */
  public alerts: TestMakerAlert[] = []

  /**
   * On init.
   */
  public ngOnInit(): void {
    this.alertService.change.subscribe((alert: TestMakerAlert) => {
      this.add(alert);
    });
  }

  /**
   * Add an alert to the collection.
   * 
   * @param alert
   */
  private add(alert: TestMakerAlert): void{
    this.alerts.push(alert);
  }

  /**
   * Remove an alert from the collection.
   * 
   * @param alert 
   */
  public remove(alert: TestMakerAlert): void {
    let index: number = this.alerts.indexOf(alert);

    if (index !== -1) {
      this.alerts.splice(index, 1);
    }
  }
}