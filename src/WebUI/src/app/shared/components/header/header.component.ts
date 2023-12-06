import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  toggleControl = new FormControl(false);
  isVisible = true;
  @Output() isDarkMode = new EventEmitter<boolean>();

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.router.events.subscribe((val) => {
      this.isVisible = this.router.url === '/keep';
    });
    this.toggleControl.valueChanges.subscribe((darkMode) => {
      this.isDarkMode.emit(darkMode ?? false);
    });
  }
}
