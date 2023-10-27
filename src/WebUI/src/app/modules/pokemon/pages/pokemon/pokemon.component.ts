import { Component } from '@angular/core';
import { PokemonService } from '../../services/pokemon.service';

@Component({
  selector: 'app-pokemon',
  templateUrl: './pokemon.component.html',
  styleUrls: ['./pokemon.component.scss'],
})
export class PokemonComponent {
  constructor(private pokemonService: PokemonService) {
    this.getPokemon();
  }

  public async getPokemon() {
    await this.pokemonService.getPokemon();
  }
}
