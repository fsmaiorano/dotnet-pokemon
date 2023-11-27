using App.Helpers;
using App.Models;

namespace App.Services
{
    public interface IPokemonService
    {
        Task<GenericResponse<Pokemon>> GetPokemon(int pageNumber, int pageSize);
    }

    public class PokemonService : IPokemonService
    {
        public PokemonService()
        {

        }

        public async Task<GenericResponse<Pokemon>> GetPokemon(int pageNumber, int pageSize)
        {
            //var response = await HttpHelper.GetAsync<GenericResponse<Pokemon>>($"http://10.0.2.2:5268/pokemonWithPagination?pageNumber={pageNumber}&pageSize={pageSize}");
            var response = await HttpHelper.GetAsync<GenericResponse<Pokemon>>($"http://localhost:5268/pokemonWithPagination?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
    }
}
