import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';
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
    MatToolbarModule,
  ],
  exports: [
    InfiniteScrollModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    LoadingComponent,
    HeaderComponent,
  ],
})
export class SharedModule {}
