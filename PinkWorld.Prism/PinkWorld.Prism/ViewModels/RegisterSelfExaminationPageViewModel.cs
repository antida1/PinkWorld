using Newtonsoft.Json;
using PinkWorld.Common.Helpers;
using PinkWorld.Common.Models;
using PinkWorld.Common.Responses;
using PinkWorld.Common.Services;
using PinkWorld.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace PinkWorld.Prism.ViewModels
{
    public class RegisterSelfExaminationPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IRegexHelper _regexHelper;
        private List<QuestionnaireResponse> _quizes;
        private AnswerResponse _answer;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _answercommand;


        public RegisterSelfExaminationPageViewModel(
            INavigationService navigationService,
            IApiService apiService,
            IRegexHelper regexHelper) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _regexHelper = regexHelper;
            LoadQuestions();
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

        public DelegateCommand AnswerCommand => _answercommand ?? (_answercommand = new DelegateCommand(LoadAnswer));
        public List<QuestionnaireResponse> Questions { get => _quizes; set => SetProperty(ref _quizes, value); }

        private async void LoadQuestions()

        {
            IsRunning = true;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<QuestionnaireResponse>(url,"/api", "/Questionnaires", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            Questions = (List<QuestionnaireResponse>)response.Result;


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

        }


        private async void LoadAnswer()

        {
            IsRunning = true;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync(url, "/api", "/Questionnaires", token.Token,Questions);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
           


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, "Your answer was save successfull", Languages.Accept);



        }
    }
}
