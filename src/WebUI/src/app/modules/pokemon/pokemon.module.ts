import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PokemonComponent } from './pages/pokemon/pokemon.component';
import { PokemonService } from './services/pokemon.service';

@NgModule({
  declarations: [PokemonComponent],
  imports: [CommonModule],
  exports: [PokemonComponent],
  providers: [PokemonService],
})
export class PokemonModule {}
