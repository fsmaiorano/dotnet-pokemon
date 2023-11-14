using App.View;
using App.ViewModels;
using App.ViewModels.View;

namespace App
{
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new MainPage());
        }

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<HomeView>();
            services.AddSingleton<MainPage>();

            return services.BuildServiceProvider();
        }
    }
}