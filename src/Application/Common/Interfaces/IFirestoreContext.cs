using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IFirestoreContext
{
    Task<Pokemon> GetPokemon(string name);
    Task SavePokemon(Pokemon pokemon);
    Task SavePokemons(List<Pokemon> pokemons);
}
