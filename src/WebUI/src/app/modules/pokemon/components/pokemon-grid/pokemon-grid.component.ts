import { Component, OnInit, Output } from '@angular/core';
import { Pokemon } from '../../models/pokemon.model';
import { PokemonService } from '../../services/pokemon.service';

@Component({
  selector: 'app-pokemon-grid',
  templateUrl: './pokemon-grid.component.html',
  styleUrls: ['./pokemon-grid.component.scss'],
})
export class PokemonGridComponent implements OnInit {
  @Output() pokemons: Pokemon[] = [];
  isLoading = true;
  hasNextPage = false;
  currentPage = 1;
  itemsPerPage = 20;

  constructor(private pokemonService: PokemonService) {}

  ngOnInit(): void {
    this.loadData();
  }

  toggleLoading = () => (this.isLoading = !this.isLoading);

  loadData = async () => {
    this.toggleLoading();
    (
      await this.pokemonService.getPokemonWithPagination(
        this.currentPage,
        this.itemsPerPage
      )
    ).subscribe({
      next: (response) => {
        this.pokemons = response.items;
        this.hasNextPage = response.nextPage !== null;
      },
      error: (err) => console.log(err),
      complete: () => this.toggleLoading(),
    });
  };

  appendData = async () => {
    this.toggleLoading();
    (
      await this.pokemonService.getPokemonWithPagination(
        this.currentPage,
        this.itemsPerPage
      )
    ).subscribe({
      next: (response) =>
        {
          this.hasNextPage = response.nextPage !== null;
          this.pokemons = [...this.pokemons, ...response.items]
        },
      error: (err) => console.log(err),
      complete: () => this.toggleLoading(),
    });
  };

  onScroll = () => {
    if(!this.hasNextPage) {
      this.toggleLoading();
      return;
    };

    this.currentPage++;
    this.appendData();
  };

  // public async getPokemon(): Promise<void> {
  //   (await this.pokemonService.getPokemonWithPagination(1, 8)).subscribe({
  //     next: (response) => {
  //       console.log(response);
  //       this.pokemons = response.items;
  //     },
  //     error: (e) => console.error(e),
  //     complete: () => console.info('complete'),
  //   });
  // }
}
