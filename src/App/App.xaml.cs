using App.Services;
using App.View;
using App.ViewModels;
using App.ViewModels.View;

namespace App
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public new static App Current => (App)Application.Current;
        public App()
        {
            Services = ConfigureServices();

            InitializeComponent();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new MainPage());
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IPokemonService, PokemonService>();

            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<HomeView>();
            services.AddSingleton<MainPage>();

            return services.BuildServiceProvider();
        }
    }
}