import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PokemonGridComponent } from './modules/pokemon/components/pokemon-grid/pokemon-grid.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/pokemon' },
  { path: 'pokemon', component: PokemonGridComponent },
  // { path: 'counter', component: CounterComponent },
  // { path: 'fetch-data', component: FetchDataComponent },
  // { path: 'login', component: LoginComponent },
  // { path: 'register', component: RegisterComponent },
  // { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  // { path: 'admin', component: AdminComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
  // { path: 'user', component: UserComponent, canActivate: [AuthGuard], data: { roles: [Role.User] } },
  // { path: 'not-found', component:
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
