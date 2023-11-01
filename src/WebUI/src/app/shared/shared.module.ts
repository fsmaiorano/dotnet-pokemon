import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { HeaderComponent } from './components/header/header.component';
import { LoadingComponent } from './components/loading/loading.component';

@NgModule({
  declarations: [LoadingComponent, HeaderComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    InfiniteScrollModule,
    MatProgressSpinnerModule,
    MatSlideToggleModule,
    MatIconModule,
    MatToolbarModule,
    FormsModule,
    ReactiveFormsModule,
    MatRippleModule,
    RouterModule,
    MatChipsModule,
  ],
  exports: [
    InfiniteScrollModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    MatSlideToggleModule,
    MatIconModule,
    LoadingComponent,
    HeaderComponent,
    FormsModule,
    ReactiveFormsModule,
    MatRippleModule,
    RouterModule,
    MatChipsModule,
  ],
})
export class SharedModule {}
