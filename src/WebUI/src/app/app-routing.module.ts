import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PokemonDetailComponent } from './modules/pokemon/components/pokemon-detail/pokemon-detail.component';
import { PokemonGridComponent } from './modules/pokemon/components/pokemon-grid/pokemon-grid.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/pokemon' },
  { path: 'pokemon', component: PokemonGridComponent },
  { path: 'pokemon/:id', component: PokemonDetailComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
  static components = [];

  static forRoot() {
    return {
      ngModule: AppRoutingModule,
      providers: [
        // { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        // { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
      ],
    };
  }

  static forChild() {
    return {
      ngModule: AppRoutingModule,
      providers: [],
    };
  }

  static forLazy() {
    return {
      ngModule: AppRoutingModule,
      providers: [],
    };
  }
}
