import { Component, HostBinding } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'WebUI';
  @HostBinding('class') className = '';

  isDarkMode = false;

  constructor() {}

  toggleDarkMode() {
    const darkClassName = 'darkMode';
    this.isDarkMode = !this.isDarkMode;
    let mainContainer = document.getElementsByClassName(
      'mat-app-background'
    )[0] as HTMLElement;

    if (this.isDarkMode) {
      mainContainer!.classList.add(darkClassName);
    } else {
      mainContainer!.classList.remove(darkClassName);
    }
  }
}
