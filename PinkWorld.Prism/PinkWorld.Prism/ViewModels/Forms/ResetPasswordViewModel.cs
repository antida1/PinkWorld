using Newtonsoft.Json;
using PinkWorld.Common.Helpers;
using PinkWorld.Common.Request;
using PinkWorld.Common.Responses;
using PinkWorld.Common.Services;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.Views.Catalog;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PinkWorld.Prism.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for reset password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ResetPasswordViewModel : ViewModelBase
    {
        #region Fields

        private string newPassword;

        private string confirmPassword;

        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        
        private bool _isRunning;
        private bool _isEnabled;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordViewModel" /> class.
        /// </summary>
        public ResetPasswordViewModel(INavigationService navigationService, IApiService apiService):base(navigationService)
        {
            this.SubmitCommand = new Command(this.SubmitClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Title = "Change Password";
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Submit button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Public property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password from user in the reset password page.
        /// </summary>
        public string NewPassword
        {
            get
            {
                return this.newPassword;
            }

            set
            {
                if (this.newPassword == value)
                {
                    return;
                }

                this.newPassword = value;
                
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password confirmation from the user in the reset password page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
               
            }
        }


        public string CurrentPassword { get; set; }

        public string PasswordNew { get; set; }

        public string PasswordConfirm { get; set; }

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
        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SubmitClicked(object obj)
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            ChangePasswordRequest request = new ChangePasswordRequest
            {
                NewPassword = PasswordNew,
                OldPassword = CurrentPassword,
            };

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.ChangePasswordAsync(url, "/api", "/Account/ChangePasswordAPI", request, token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                if (response.Message == "Error001")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error001, Languages.Accept);
                }
                else if (response.Message == "Error005")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error002, Languages.Accept);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                }

                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.ChangePassworrdMessage, Languages.Accept);
            await _navigationService.NavigateAsync(nameof(NavigationTravelPage));
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            
        }
        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CurrentPasswordError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordNew) || PasswordNew?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NewPasswordError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConfirmNewPasswordError1, Languages.Accept);
                return false;
            }

            if (PasswordNew != PasswordConfirm)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConfirmNewPasswordError2, Languages.Accept);
                return false;
            }

            return true;
        }


        #endregion
    }
}