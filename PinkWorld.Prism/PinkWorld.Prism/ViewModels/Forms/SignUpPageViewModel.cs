using PinkWorld.Common.Helpers;
using PinkWorld.Common.Request;
using PinkWorld.Common.Responses;
using PinkWorld.Common.Services;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.Views.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using CountryResponse = PinkWorld.Common.Responses.CountryResponse;

namespace PinkWorld.Prism.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private readonly IRegexHelper _regexHelper;

        private readonly IApiService _apiService;

        private readonly IFilesHelper _filesHelper;

        #region Fields
        private string password;

        private string confirmPassword;

        private ImageSource _image;

        private UserRequest _user;

        private CityResponse _city;

        private ObservableCollection<CityResponse> _cities;

        private DepartmentResponse _department;

        private ObservableCollection<DepartmentResponse> _departments;

        private CountryResponse _country;

        private ObservableCollection<CountryResponse> _countries;
        #endregion

        private bool _isRunning;

        private bool _isEnabled;

        private MediaFile _file;

        private DelegateCommand _changeImageCommand;



        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel(INavigationService navigationService,
        IRegexHelper regexHelper,
        IApiService apiService,
        IFilesHelper filesHelper) : base(navigationService)
        {
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClickedAsync);
            _navigationService = navigationService;
            _regexHelper = regexHelper;
            _apiService = apiService;
            _filesHelper = filesHelper;
            Title = Languages.Register;
            Image = App.Current.Resources["UrlNoImage"].ToString();
            IsEnabled = true;
            User = new UserRequest();
            LoadCountriesAsync();

        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;

            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
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

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public UserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public CountryResponse Country
        {
            get => _country;
            set
            {
                Departments = value != null ? new ObservableCollection<DepartmentResponse>(value.Departments) : null;
                Cities = new ObservableCollection<CityResponse>();
                Department = null;
                City = null;
                SetProperty(ref _country, value);
            }
        }

        public ObservableCollection<CountryResponse> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        public DepartmentResponse Department
        {
            get => _department;
            set
            {
                Cities = value != null ? new ObservableCollection<CityResponse>(value.Cities) : null;
                City = null;
                SetProperty(ref _department, value);
            }
        }

        public ObservableCollection<DepartmentResponse> Departments
        {
            get => _departments;
            set => SetProperty(ref _departments, value);
        }

        public CityResponse City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public ObservableCollection<CityResponse> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }
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

        public DelegateCommand ChangeImageCommand => _changeImageCommand ??
            (_changeImageCommand = new DelegateCommand(ChangeImageAsync));



        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            await _navigationService.NavigateAsync(nameof(SimpleLoginPage));
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClickedAsync(object obj)
        {
            bool isValid = await ValidateDataAsync();
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

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            User.ImageArray = imageArray;

            User.CityId = City.Id;

            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.RegisterUserAsync(url, "/api", "/Account/Register", User);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                if (response.Message == "Error002")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error002, Languages.Accept);
                }
                else if (response.Message == "Error003")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error003, Languages.Accept);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                }

                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.RegisterMessge, Languages.Accept);
            await _navigationService.GoBackAsync();
        }

        private async void LoadCountriesAsync()
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
            Response response = await _apiService.GetListAsync<CountryResponse>(url, "/api", "/Countries");
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            List<CountryResponse> list = (List<CountryResponse>)response.Result;
            Countries = new ObservableCollection<CountryResponse>(list.OrderBy(c => c.Name));
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DocumentError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.AddressError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.Email) || !_regexHelper.IsValidEmail(User.Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.PhoneNumber))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PhoneError, Languages.Accept);
                return false;
            }

            if (Country == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CountryError, Languages.Accept);
                return false;
            }

            if (Department == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DepartmentError, Languages.Accept);
                return false;
            }

            if (City == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CityError, Languages.Accept);
                return false;
            }

            return true;
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.PictureSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.FromCamera)
            {
                if (!CrossMedia.Current.IsCameraAvailable)
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoCameraSupported, Languages.Accept);
                    return;
                }

                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoGallerySupported, Languages.Accept);
                    return;
                }

                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }
        #endregion

    }
}