import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { PokemonGridItemComponent } from './components/pokemon-grid-item/pokemon-grid-item.component';
import { PokemonGridComponent } from './components/pokemon-grid/pokemon-grid.component';
import { PokemonService } from './services/pokemon.service';
import { PokemonDetailComponent } from './components/pokemon-detail/pokemon-detail.component';

@NgModule({
  declarations: [PokemonGridComponent, PokemonGridItemComponent, PokemonDetailComponent],
  imports: [CommonModule, SharedModule],
  exports: [PokemonGridComponent],
  providers: [PokemonService],
})
export class PokemonModule {}
