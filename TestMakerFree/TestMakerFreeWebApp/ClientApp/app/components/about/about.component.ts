import { Component } from "@angular/core";

@Component({
  selector: 'about',
  templateUrl: 'about.component.html'
})
export class AboutComponent {
  constructor () { }

  /**
   * Page title.
   */
  public title: string = 'About';
}