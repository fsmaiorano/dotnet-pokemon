import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';

@Injectable()
export class PokemonService {
  private readonly baseUrl = 'http://localhost:5268';

  constructor(private http: HttpClient, private store: AngularFirestore) {}

  public async getPokemon(): Promise<any> {
    this.store
      .collection('pokemons')
      .valueChanges()
      .subscribe((result) => {
        debugger;
        console.log(result);
        return result;
      });
  }

  public getPokemonWithPagination(page: number, pageSize: number) {
    return this.http.get(`${this.baseUrl}/pokemon/${page}/${pageSize}`);
  }
}
