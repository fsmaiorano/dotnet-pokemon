import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';

@Injectable()
export class PokemonService {
  private readonly baseUrl = 'http://localhost:5268';

  constructor(private http: HttpClient, private store: AngularFirestore) {

  }

  public async getPokemon() {
    // return this.http.get(`${this.baseUrl}/pokemon`);
    console.log('getPokemon');
    return await this.store
      .collection('pokemons')
      .valueChanges()
      .subscribe((response) => {
        console.log(response);
      });
  }

  public getPokemonWithPagination(page: number, pageSize: number) {
    return this.http.get(`${this.baseUrl}/pokemon/${page}/${pageSize}`);
  }
}