import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    InfiniteScrollModule,
    MatProgressSpinnerModule,
  ],
  exports: [InfiniteScrollModule, HttpClientModule, MatProgressSpinnerModule],
})
export class SharedModule {}
