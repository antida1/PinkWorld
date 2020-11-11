﻿using PinkWorld.Prism.Views.Catalog;
using Prism.Commands;
using Prism.Navigation;

namespace PinkWorld.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand _initCommand;
        private readonly INavigationService _navigationService;
        private bool _isEnabled;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Main Page";
            IsEnabled = true;

        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public DelegateCommand InitCommand => _initCommand ?? (_initCommand = new DelegateCommand(InitAsync));

        private async void InitAsync()
        {
            await _navigationService.NavigateAsync(nameof(NavigationTravelPage));
        }
    }
}
