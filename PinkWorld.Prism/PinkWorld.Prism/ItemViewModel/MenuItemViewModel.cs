using PinkWorld.Common.Helpers;
using PinkWorld.Common.Models;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.Views;
using PinkWorld.Prism.Views.Forms;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinkWorld.Prism.ItemViewModel
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {

            if (PageName == nameof(SimpleLoginPage) && Settings.IsLogin)
            {
                Settings.IsLogin = false;
                Settings.Token = null;
                
            }

            if (IsLoginRequired && !Settings.IsLogin)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginFirstMessage, Languages.Accept);
                NavigationParameters parameters = new NavigationParameters
                {
                    { "pageReturn", PageName }
                };

                await _navigationService.NavigateAsync($"/{nameof(PinkWorldMasterDetailPage)}/NavigationPage/{nameof(SimpleLoginPage)}", parameters);
            }
            else
            {
                await _navigationService.NavigateAsync($"/{nameof(PinkWorldMasterDetailPage)}/NavigationPage/{PageName}");
            }




        }
    }

}
