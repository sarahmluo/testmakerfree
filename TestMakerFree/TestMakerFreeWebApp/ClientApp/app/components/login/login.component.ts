import { Component } from '@angular/core';

@Component({
  selector: 'login',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  constructor() { }

  /**
   * Page title.
   */
  public title: string = 'Login';
}