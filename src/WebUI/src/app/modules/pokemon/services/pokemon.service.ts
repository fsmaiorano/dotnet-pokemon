import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { Observable } from 'rxjs';
import { PaginatedList } from '../models/paginated-list';
import { Pokemon } from '../models/pokemon.model';

@Injectable()
export class PokemonService {
  private readonly baseUrl = 'http://localhost:5268';

  constructor(private http: HttpClient, private store: AngularFirestore) {}

  public async getPokemon(): Promise<Observable<any>> {
    // this.store
    //   .collection('pokemons')
    //   .valueChanges()
    //   .subscribe((result) => {
    //     // console.log(result);
    //     return result;
    //   });

    return this.http.get(`${this.baseUrl}/pokemon`);
  }

  public async getPokemonWithPagination(
    page: number,
    pageSize: number
  ): Promise<Observable<PaginatedList<Pokemon>>> {
    return this.http.get<PaginatedList<Pokemon>>(
      `${this.baseUrl}/pokemonWithPagination?pageNumber=${page}&pageSize=${pageSize}`
    );
  }
}
