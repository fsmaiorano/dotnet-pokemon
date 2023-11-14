using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels.View
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private string pageTitle;
        [ObservableProperty]
        private string pageDescription;

        public HomeViewModel() { }

        [RelayCommand]
        async Task SayHelloMethod()
        {
            PageTitle = "Hello World!";
            PageDescription = "This is a description of the page.";
            Console.WriteLine("Hello World!");
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
