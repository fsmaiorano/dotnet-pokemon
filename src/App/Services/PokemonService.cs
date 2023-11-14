using App.Helpers;
using App.Models;
using Domain.Entities;

namespace App.Services
{
    public interface IPokemonService
    {
        Task<PaginatedList<PokemonEntity>> GetPokemon(int pageNumber, int pageSize);
    }

    public class PokemonService : IPokemonService
    {
        public PokemonService()
        {

        }

        public async Task<PaginatedList<PokemonEntity>> GetPokemon(int pageNumber, int pageSize)
        {
            var response = await HttpHelper.GetAsync<PaginatedList<PokemonEntity>>($"http://localhost:5268/pokemonWithPagination?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
    }
}
