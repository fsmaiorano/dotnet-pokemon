using App.ViewModels.View;

namespace App.View
{
    public partial class HomeView : ContentPage
    {
        private readonly HomeViewModel _viewModel;
        private readonly PokemonGridViewModel _pokemonGridViewModel;

        public HomeView()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<HomeViewModel>();
            _pokemonGridViewModel = App.Current.Services.GetService<PokemonGridViewModel>();

            _viewModel.PageTitle = "original";
            _viewModel.PageDescription = "original";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = _pokemonGridViewModel.GetPokemonsPaginated();
        }
    }
}