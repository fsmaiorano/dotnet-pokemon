import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedList } from '../models/paginated-list';
import { Pokemon } from '../models/pokemon.model';
import { Specie } from '../models/specie.model';

@Injectable()
export class PokemonService {
  private readonly baseUrl = 'http://localhost:5268';

  constructor(private http: HttpClient) {}

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

  public async getPokemonByExternalId(
    externalId: number
  ): Promise<Observable<Pokemon>> {
    return this.http.get<Pokemon>(
      `${this.baseUrl}/pokemonByExternalId?externalId=${externalId}`
    );
  }

  public async getPokemonSpecieByExternalId(
    externalId: number
  ): Promise<Observable<Specie>> {
    return this.http.get<any>(
      `${this.baseUrl}/pokemonDescriptionByExternalId?externalId=${externalId}`
    );
  }
}
