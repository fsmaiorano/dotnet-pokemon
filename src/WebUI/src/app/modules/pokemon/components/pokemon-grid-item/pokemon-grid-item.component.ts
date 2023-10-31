import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  ViewChild,
} from '@angular/core';
import { Pokemon } from '../../models/pokemon.model';
import { pokemonTypeColor } from '../../utils/pokemon-type-color';

@Component({
  selector: 'app-pokemon-grid-item',
  templateUrl: './pokemon-grid-item.component.html',
  styleUrls: ['./pokemon-grid-item.component.scss'],
})
export class PokemonGridItemComponent implements AfterViewInit {
  @Input() pokemon?: Pokemon;
  @ViewChild('item') input: ElementRef<HTMLInputElement> | undefined;

  constructor() {}

  ngAfterViewInit(): void {
    let colors = this.getColors();
    this.setBackgroundColor(colors);
  }

  goToDetail = (pokemon: Pokemon) => {
    console.log(pokemon);
  };

  private getColors(): string[] {
    let types: string[] = [];
    this.pokemon?.types.map((pokemonType) => {
      types.push(pokemonType.name);
    });

    if (types.length === 1) {
      types.push(types[0]);
    }

    return pokemonTypeColor(types);
  }

  private setBackgroundColor(colors: string[]): void {
    if (this.input) {
      const inputElement = this.input.nativeElement;
      if (inputElement) {
        inputElement.style.backgroundImage = `linear-gradient(90deg, ${colors[0]} 0%, ${colors[1]} 100%)`;
        // inputElement.style.boxShadow = `0px 0px 0 0px ${colors[0]}, -1px 0 28px 0 rgba(34, 33, 81, 0.01),
        //    28px 28px 28px 0 rgba(34, 33, 81, 0.25);`;

        // inputElement.addEventListener('mouseenter', () => {
        //   inputElement.style.boxShadow = `1px 1px 0 1px ${colors[1]}, -1px 0 28px 0 rgba(34, 33, 81, 0.01),
        //          54px 54px 28px -10px rgba(34, 33, 81, 0.15);`;
        // });
      }
    }
  }
}
