using Prism;
using Prism.Ioc;
using PinkWorld.Prism.ViewModels;
using PinkWorld.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using PinkWorld.Prism.Views.Forms;
using Syncfusion.Licensing;
using PinkWorld.Prism.ViewModels.Forms;

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
            SyncfusionLicenseProvider.RegisterLicense("MzQ1NTUxQDMxMzgyZTMzMmUzMEl4dk5FQTczMmNOaFljNzRldS9QWm5mK3VyVXdWRzdzVU03enk3b3ZMUUk9");
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/SimpleLoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<AddProfilePage, BaseViewModel>();
            containerRegistry.RegisterForNavigation<SimpleLoginPage, LoginPageViewModel>();

        }
    }
}
