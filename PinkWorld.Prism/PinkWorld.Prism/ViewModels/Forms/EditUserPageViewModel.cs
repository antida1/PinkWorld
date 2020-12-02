using Newtonsoft.Json;
using PinkWorld.Common.Enums;
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

namespace PinkWorld.Prism.ViewModels.Forms
{
    public class EditUserPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRegexHelper _regexHelper;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _changepassword;
        private UserResponse _user;
        private ImageSource _image;
        private bool _isRunning;
        private bool _isEnabled;
        private CityResponse _city;
        private ObservableCollection<CityResponse> _cities;
        private DepartmentResponse _department;
        private ObservableCollection<DepartmentResponse> _departments;
        private CountryResponse _country;
        private ObservableCollection<CountryResponse> _countries;
        private MediaFile _file;
        private bool _isOnSaleUser;


        public EditUserPageViewModel(
            INavigationService navigationService,
            IRegexHelper regexHelper,
            IApiService apiService,
            IFilesHelper filesHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _regexHelper = regexHelper;
            _apiService = apiService;
            _filesHelper = filesHelper;
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Title = Languages.ModifyUser;
            User = token.User;
            Image = User.ImageFullPath;
            IsOnSaleUser = User.LoginType == LoginType.PinkWorld;

            LoadCountriesAsync();
        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ??
            (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public DelegateCommand SaveCommand => _saveCommand ??
            (_saveCommand = new DelegateCommand(SaveAsync));


        public DelegateCommand ChangePassword => _changepassword ??
            (_changepassword = new DelegateCommand(Change));

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
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

        public bool IsOnSaleUser
        {
            get => _isOnSaleUser;
            set => SetProperty(ref _isOnSaleUser, value);
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
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<CountryResponse> list = (List<CountryResponse>)response.Result;
            Countries = new ObservableCollection<CountryResponse>(list.OrderBy(c => c.Name));
            LoadCurrentCountyDepartmentCity();
        }


        private void LoadCurrentCountyDepartmentCity()
        {
            Country = Countries.FirstOrDefault(c => c.Departments.FirstOrDefault(d => d.Cities.FirstOrDefault(ci => ci.Id == User.City.Id) != null) != null);
            Department = Country.Departments.FirstOrDefault(d => d.Cities.FirstOrDefault(c => c.Id == User.City.Id) != null);
            City = Department.Cities.FirstOrDefault(c => c.Id == User.City.Id);
        }

        private async void SaveAsync()
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
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error, Languages.Accept);
                return;
            }

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            UserRequest request = new UserRequest
            {
                Address = User.Address,
                CityId = City.Id,
                Document = User.Document,
                Email = User.Email,
                FirstName = User.FirstName,
                SecondName=User.SecondName,
                ImageArray = imageArray,
                LastName = User.LastName,
                Password = "123456",
                PhoneNumber = User.PhoneNumber
            };


            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.ModifyUserAsync(url, "/api", "/Account/PutUser", request, token.Token);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                if (response.Message == "Error001")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error001, Languages.Accept);
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

            UserResponse updatedUser = (UserResponse)response.Result;
            token.User = updatedUser;
            Settings.Token = JsonConvert.SerializeObject(token);
            PinkWorldMasterDetailPageViewModel.GetInstance().LoadUser();
            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.Ok, Languages.Accept);
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
            if (!IsOnSaleUser)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChangeOnSocialNetwork, Languages.Accept);
                return;
            }

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


        public async void Change() 
        {
            if (!IsOnSaleUser)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChangeOnSocialNetwork, Languages.Accept);
                return;
            }


            await _navigationService.NavigateAsync(nameof(SimpleResetPasswordPage));
        }
    }
}
