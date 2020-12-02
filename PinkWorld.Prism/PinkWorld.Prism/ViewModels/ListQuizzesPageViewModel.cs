using PinkWorld.Common.Models;
using PinkWorld.Common.Responses;
using PinkWorld.Common.Services;
using PinkWorld.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace PinkWorld.Prism.ViewModels
{
    public class ListQuizzesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<QuizItemViewModel> _quizzes;
        private bool _isRunning;
        private string _search;
        private List<QuizResponse> _myQuizzes;
        private DelegateCommand _searchCommand;

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowQuizzez));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowQuizzez();
            }
        }


        public ListQuizzesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Quizzes;
            LoadQuizzesAsync();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        public ObservableCollection<QuizItemViewModel> Quizzes
        {
            get => _quizzes;
            set => SetProperty(ref _quizzes, value);
        }

        private async void LoadQuizzesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<QuizResponse>(
                url,
                "/api",
                "/Quizzes");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            _myQuizzes = (List<QuizResponse>)response.Result;
            ShowQuizzez();
        }

        private void ShowQuizzez()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Quizzes = new ObservableCollection<QuizItemViewModel>(_myQuizzes.Select(q => new QuizItemViewModel(_navigationService)
                {
                   Id = q.Id,
                   Date = q.DateLocal,
                   Questions = q.Questions
                  
                })
              .ToList());

            }
            else
            {
                Quizzes = new ObservableCollection<QuizItemViewModel>(_myQuizzes.Select(q => new QuizItemViewModel(_navigationService)
                {
                    Id = q.Id,
                    Date = q.DateLocal,
                    Questions = q.Questions
                })
               .ToList());

            }
        }
    }
}
