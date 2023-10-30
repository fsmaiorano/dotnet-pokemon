import { Component, Input } from '@angular/core';
import { Pokemon } from '../../models/pokemon.model';

@Component({
  selector: 'app-pokemon-grid-item',
  templateUrl: './pokemon-grid-item.component.html',
  styleUrls: ['./pokemon-grid-item.component.scss'],
})
export class PokemonGridItemComponent {
  @Input() pokemon!: Pokemon;

  constructor() {}
}
