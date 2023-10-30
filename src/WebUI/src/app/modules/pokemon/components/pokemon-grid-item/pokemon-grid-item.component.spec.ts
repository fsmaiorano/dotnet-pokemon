import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonGridItemComponent } from './pokemon-grid-item.component';

describe('PokemonGridItemComponent', () => {
  let component: PokemonGridItemComponent;
  let fixture: ComponentFixture<PokemonGridItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PokemonGridItemComponent]
    });
    fixture = TestBed.createComponent(PokemonGridItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
