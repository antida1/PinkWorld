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

            CarouselPage carouselPage = new CarouselPage();
            carouselPage.Children.Add(new MainPage());
            carouselPage.Children.Add(new Carousel());
            carouselPage.Children.Add(new Carousel2());
            carouselPage.Children.Add(new Carousel3());
            carouselPage.Children.Add(new Carousel4());
            MainPage = new NavigationPage(carouselPage);

            await NavigationService.NavigateAsync($"{nameof(PinkWorldMasterDetailPage)}/NavigationPage/{nameof(MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowQuizzesHistoryPage, ShowQuizzesHistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PinkWorldMasterDetailPage, PinkWorldMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterSelfExaminationPage, RegisterSelfExaminationPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewStadisticsPage, ViewStadisticsPageViewModel>();
            containerRegistry.RegisterForNavigation<SeeMapsPage, SeeMapsPageViewModel>();
        }
    }
}
