using App.Models;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace App.ViewModels.View
{
    public partial class PokemonGridViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;
        [ObservableProperty]
        private ObservableCollection<Pokemon> pokemonList;
        [ObservableProperty]
        private int pageNumber = 1;
        [ObservableProperty]
        private int pageSize = 10;

        private readonly IPokemonService _pokemonService;
        public PokemonGridViewModel(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        void ToggleIsBusy()
        {
            IsBusy = !IsBusy;
            return;
        }

        //[RelayCommand]
        //async Task SayHelloMethodAsync()
        //{
        //    ToggleIsBusy();
        //    Console.WriteLine("Hello World!");

        //    var x = await _pokemonService.GetPokemon(1, 10);

        //    ToggleIsBusy();
        //    return;
        //}

        [RelayCommand]
        public async Task GetPokemonsPaginated()
        {
            ToggleIsBusy();

            try
            {
                var result = await _pokemonService.GetPokemon(PageNumber, PageSize);

                if (result != null)
                {
                    PokemonList = new ObservableCollection<Pokemon>(result.Items);
                    PageNumber++;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ToggleIsBusy();
            }
        }
    }
}
