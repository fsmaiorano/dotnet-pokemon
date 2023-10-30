import { Component, Output } from '@angular/core';
import { Pokemon } from '../../models/pokemon.model';
import { PokemonService } from '../../services/pokemon.service';

@Component({
  selector: 'app-pokemon-grid',
  templateUrl: './pokemon-grid.component.html',
  styleUrls: ['./pokemon-grid.component.scss'],
})
export class PokemonGridComponent {
  @Output() pokemons: Pokemon[] = [];

  constructor(private pokemonService: PokemonService) {
    this.getPokemon();
  }

  public async getPokemon(): Promise<any> {
    (await this.pokemonService.getPokemon()).subscribe({
      next: (response) => {
        console.log(response);
        this.pokemons = response;
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete'),
    });
  }
}
