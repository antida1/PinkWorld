using PinkWorld.Common.Services;
using PinkWorld.Prism.ViewModels;
using PinkWorld.Prism.ViewModels.Catalog;
using PinkWorld.Prism.ViewModels.Forms;
using PinkWorld.Prism.Views;
using PinkWorld.Prism.Views.Catalog;
using PinkWorld.Prism.Views.Forms;
using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace PinkWorld.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {

            CarouselPage carouselPage = new CarouselPage();
            carouselPage.Children.Add(new MainPage());
            carouselPage.Children.Add(new Carousel());
            carouselPage.Children.Add(new Carousel2());
            carouselPage.Children.Add(new Carousel3());
            carouselPage.Children.Add(new Carousel4());
            MainPage = new NavigationPage(carouselPage);
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzQ5MTM1QDMxMzgyZTMzMmUzMGRLcWRTT0MzWlBzS3A3UThScFpCUnRuSERuV21GYVVjZlcvMUVJSHRVL009");

            InitializeComponent();

            await NavigationService.NavigateAsync(nameof(NavigationTravelPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ListQuizzesPage, ListQuizzesPageViewModel>();
            containerRegistry.RegisterForNavigation<SimpleLoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<NavigationTravelPage, NavigationTravelPageViewModel>();




        }

        public static string BaseImageUrl { get; } = "https://demoonsale.blob.core.windows.net/users/";
    }
}
