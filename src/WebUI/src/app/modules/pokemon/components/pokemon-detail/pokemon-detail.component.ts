import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Pokemon } from '../../models/pokemon.model';
import { Specie } from '../../models/specie.model';
import { PokemonService } from '../../services/pokemon.service';
import { pokemonTypeColor } from '../../utils/pokemon-type-color';

@Component({
  selector: 'app-pokemon-detail',
  templateUrl: './pokemon-detail.component.html',
  styleUrls: ['./pokemon-detail.component.scss'],
})
export class PokemonDetailComponent {
  isLoading = true;
  pokemon!: Pokemon;
  specie!: Specie;
  description: string | undefined;
  @ViewChild('section') input: ElementRef<HTMLInputElement> | undefined;

  constructor(private router: Router, private pokemonService: PokemonService) {
    this.getPokemonDetail();
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    let colors = this.getColors();
    this.setBackgroundColor(colors);
    this.getPokemonSpecieByExternalId(this.pokemon.externalId);
  }

  toggleLoading = () => (this.isLoading = !this.isLoading);

  getPokemonDetail = () => {
    try {
      let id = this.router.url.split('/').pop();
      const navigation = this.router.getCurrentNavigation();

      if (navigation && navigation.extras && navigation.extras.state) {
        this.pokemon = navigation.extras.state['pokemon'] as Pokemon;
        if (this.pokemon.externalId !== parseInt(id as string)) {
          this.getPokemonByExternalId(parseInt(id as string));
        }
      } else if (id) {
        this.getPokemonByExternalId(parseInt(id as string));
      } else {
        this.router.navigate(['']);
      }
    } catch (error) {
      console.log(error);
    } finally {
      this.toggleLoading();
    }
  };

  getPokemonByExternalId = async (externalId: number) => {
    (await this.pokemonService.getPokemonByExternalId(externalId)).subscribe({
      next: (response: Pokemon) => {
        this.pokemon = response;
      },
      error: (err) => console.log(err),
      complete: () => (this.isLoading === true ? this.toggleLoading() : null),
    });
  };

  getPokemonSpecieByExternalId = async (externalId: number) => {
    (
      await this.pokemonService.getPokemonSpecieByExternalId(externalId)
    ).subscribe({
      next: (response: Specie) => {
        this.specie = response;

        const englishDescriptions = response
          .flavor_text_entries!.filter(
            (description) => description.language!.name === 'en'
          )
          .map((description) =>
            description.flavor_text?.replace(/[\r\n\f]/gm, ' ')
          );

        console.log(englishDescriptions);

        const counter = englishDescriptions.length;

        const random = Math.floor(Math.random() * counter);
        this.description = englishDescriptions![random];
      },
      error: (err) => console.log(err),
      complete: () => (this.isLoading === true ? this.toggleLoading() : null),
    });
  };

  getColors = (): string[] => {
    let types: string[] = [];
    this.pokemon?.types.map((pokemonType) => {
      types.push(pokemonType.name);
    });

    if (types.length === 1) {
      types.push(types[0]);
    }

    return pokemonTypeColor(types);
  };

  goBack = () => {
    this.router.navigate(['']);
  };

  private setBackgroundColor(colors: string[]): void {
    if (this.input) {
      const inputElement = this.input.nativeElement;
      if (inputElement) {
        inputElement.style.backgroundImage = `linear-gradient(90deg, ${colors[0]} 0%, ${colors[1]} 100%)`;
      }
    }
  }
}
