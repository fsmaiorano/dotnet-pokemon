using App.ViewModels.View;

namespace App.View;

public partial class PokemonGridView : ContentView
{
    private readonly PokemonGridViewModel _viewModel;

    public PokemonGridView()
    {
        InitializeComponent();
        BindingContext = _viewModel = App.Current.Services.GetService<PokemonGridViewModel>();

        _ = _viewModel.GetPokemonsPaginated();
    }
}