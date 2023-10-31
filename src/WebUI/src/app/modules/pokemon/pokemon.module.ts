import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { PokemonGridItemComponent } from './components/pokemon-grid-item/pokemon-grid-item.component';
import { PokemonGridComponent } from './components/pokemon-grid/pokemon-grid.component';
import { PokemonService } from './services/pokemon.service';

@NgModule({
  declarations: [PokemonGridComponent, PokemonGridItemComponent],
  imports: [CommonModule, InfiniteScrollModule],
  exports: [PokemonGridComponent],
  providers: [PokemonService],
})
export class PokemonModule {}
