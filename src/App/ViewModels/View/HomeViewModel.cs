using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels.View
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;
        [ObservableProperty]
        private string pageTitle;
        [ObservableProperty]
        private string pageDescription;

        private readonly IPokemonService _pokemonService;
        public HomeViewModel(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

       void ToggleIsBusy()
        {
            IsBusy = !IsBusy;
            return;
        }

        [RelayCommand]
        async Task SayHelloMethodAsync()
        {
            ToggleIsBusy();
            PageTitle = "Hello World!";
            PageDescription = "This is a description of the page.";
            Console.WriteLine("Hello World!");

            var x = await _pokemonService.GetPokemon(1, 10);

            ToggleIsBusy();
            return;
        }

        [RelayCommand]
        void ShowAlert()
        {
            Shell.Current.DisplayAlert("Alert", "Hello World!", "OK");
            return;
        }
    }
}
