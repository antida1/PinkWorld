using Newtonsoft.Json;
using PinkWorld.Common.Helpers;
using PinkWorld.Common.Models;
using PinkWorld.Common.Request;
using PinkWorld.Common.Responses;
using PinkWorld.Common.Services;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.Views;
using PinkWorld.Prism.Views.Catalog;
using PinkWorld.Prism.Views.Forms;
using Plugin.FacebookClient;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace PinkWorld.Prism.ViewModels.Forms
{

    [Preserve(AllMembers = true)]
    public class LoginViewModel : ViewModelBase
    {

        private DelegateCommand _forgotpassword;
        private string _pageReturn;
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _signup;
        private readonly IFacebookClient _facebookService = CrossFacebookClient.Current;
        private DelegateCommand _loginFacebookCommand;


        private bool isInvalidEmail;


        public LoginViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Login;
            IsEnabled = true;

        }



        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the email ID from user in the login page.
        /// </summary>

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        public DelegateCommand ForgotPassword => _forgotpassword ?? (_forgotpassword = new DelegateCommand(ForgotPasswordPage));

        public DelegateCommand SignUpCommand => _signup ?? (_signup = new DelegateCommand(SignUpPage));

        public DelegateCommand LoginFacebookCommand => _loginFacebookCommand ?? (_loginFacebookCommand = new DelegateCommand(LoginFacebookAsync));



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
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Result;
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;


            

            await _navigationService.NavigateAsync($"/{nameof(PinkWorldMasterDetailPage)}/NavigationPage/{nameof(NavigationTravelPage)}");

            Password = string.Empty;

        }

        public bool IsInvalidEmail
        {
            get
            {
                return this.isInvalidEmail;
            }

            set
            {
                if (this.isInvalidEmail == value)
                {
                    return;
                }

                this.isInvalidEmail = value;

            }
        }


        private async void ForgotPasswordPage()
        {

            await _navigationService.NavigateAsync(nameof(SimpleForgotPasswordPage));

        }


        private async void SignUpPage()
        {

            await _navigationService.NavigateAsync(nameof(SimpleSignUpPage));

        }

        private async void LoginFacebookAsync()
        {
            try
            {

                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                async void userDataDelegate(object sender, FBEventArgs<string> e)
                {
                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            FacebookProfile facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            await LoginFacebookAsync(facebookProfile);
                            break;
                        case FacebookActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert(Languages.FacebookAuth, Languages.Canceled, Languages.Ok);
                            break;
                        case FacebookActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert(Languages.FacebookAuth, Languages.Error, Languages.Ok);
                            break;
                        case FacebookActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert(Languages.FacebookAuth, Languages.Unauthorized, Languages.Ok);
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                }

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "picture.width(999)", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async Task LoginFacebookAsync(FacebookProfile facebookProfile)
        {
            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetTokenAsync(url, "api", "/Account/LoginFacebook", facebookProfile);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Result;
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync($"/{nameof(PinkWorldMasterDetailPage)}/NavigationPage/{nameof(NavigationTravelPage)}");
            Password = string.Empty;
        }


        #endregion
    }
}
