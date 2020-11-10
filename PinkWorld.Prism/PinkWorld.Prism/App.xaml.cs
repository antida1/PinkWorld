using Prism;
using Prism.Ioc;
using PinkWorld.Prism.ViewModels;
using PinkWorld.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Syncfusion.Licensing;
using PinkWorld.Common.Services;

namespace PinkWorld.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzQ5MTM1QDMxMzgyZTMzMmUzMGRLcWRTT0MzWlBzS3A3UThScFpCUnRuSERuV21GYVVjZlcvMUVJSHRVL009");

            InitializeComponent();

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ListQuizzesPage, ListQuizzesPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

        }
    }
}
