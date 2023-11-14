using App.ViewModels.View;

namespace App.View
{
    public partial class HomeView : ContentPage
    {
        private readonly HomeViewModel _viewModel;
        public HomeView()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<HomeViewModel>();
        }
    }
}