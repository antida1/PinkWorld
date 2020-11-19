using PinkWorld.Common.Services;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.ViewModels;
using PinkWorld.Prism.ViewModels.Catalog;
using PinkWorld.Prism.ViewModels.Forms;
using PinkWorld.Prism.ViewModels.Transaction;
using PinkWorld.Prism.Views;
using PinkWorld.Prism.Views.Catalog;
using PinkWorld.Prism.Views.Forms;
using PinkWorld.Prism.Views.Transaction;
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


        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzQ5MTM1QDMxMzgyZTMzMmUzMGRLcWRTT0MzWlBzS3A3UThScFpCUnRuSERuV21GYVVjZlcvMUVJSHRVL009");

            InitializeComponent();




            await NavigationService.NavigateAsync($"{nameof(PinkWorldMasterDetailPage)}/NavigationPage/{nameof(NavigationTravelPage)}");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ListQuizzesPage, ListQuizzesPageViewModel>();
            containerRegistry.RegisterForNavigation<SimpleLoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<NavigationTravelPage, NavigationTravelPageViewModel>();
            containerRegistry.RegisterForNavigation<PinkWorldMasterDetailPage, PinkWorldMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<SimpleForgotPasswordPage, ForgotPasswordViewModel>();
            containerRegistry.RegisterForNavigation<PaymentSuccessPage, PaymentViewModel>();

        }

        public static string BaseImageUrl { get; } = "https://demoonsale.blob.core.windows.net/users/";
    }
}
