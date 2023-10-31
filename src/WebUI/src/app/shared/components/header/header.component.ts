import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  toggleControl = new FormControl(false);
  @Output() isDarkMode = new EventEmitter<boolean>();

  constructor() {}

  ngOnInit(): void {
    this.toggleControl.valueChanges.subscribe((darkMode) => {
      this.isDarkMode.emit(darkMode ?? false);
    });
  }
}
