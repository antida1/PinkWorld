using Newtonsoft.Json;
using PinkWorld.Common.Helpers;
using PinkWorld.Common.Request;
using PinkWorld.Common.Responses;
using PinkWorld.Common.Services;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace PinkWorld.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {

        private string _pageReturn;
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public LoginPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Login;
            IsEnabled = true;

        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("pageReturn"))
            {
                _pageReturn = parameters.GetValue<string>("pageReturn");
            }
        }
        private async void LoginAsync()
        {


            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            TokenRequest request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            Response response = await _apiService.GetTokenAsync(url, "api", "/Account/CreateToken", request);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "invalid email or password ", "Accept");
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Result;
            Settings.Token = JsonConvert.SerializeObject(token);

            await _navigationService.NavigateAsync(nameof(ShowQuizzesHistoryPage));
            Password = string.Empty;

        }


    }
}
