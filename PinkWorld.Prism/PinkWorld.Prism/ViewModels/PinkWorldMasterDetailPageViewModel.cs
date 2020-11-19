using Newtonsoft.Json;
using PinkWorld.Common.Helpers;
using PinkWorld.Common.Models;
using PinkWorld.Common.Responses;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.ItemViewModel;
using PinkWorld.Prism.Views;
using PinkWorld.Prism.Views.Catalog;
using PinkWorld.Prism.Views.Forms;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace PinkWorld.Prism.ViewModels
{
    public class PinkWorldMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;

        public PinkWorldMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
            Name = Languages.Menu;
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public string Name { get; set; }
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public void LoadUser()
        {
            if (Settings.IsLogin)
            {
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                User = token.User;

            }
        }
        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
        {
            new Menu
            {
                Icon = "ic_launcher_home",
                PageName = $"{nameof(NavigationTravelPage)}",
                Title = Languages.Main
            },
            new Menu
            {
                Icon = "ic_launcher_selfexamination",
                PageName = $"{nameof(ListQuizzesPage)}",
                Title = Languages.RegisterSelfExamination,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_launcher_history",
                PageName = "",
                Title = Languages.ShowQuizzesHistory,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_launcher_stadistic",
                PageName = "",
                Title = Languages.ViewStadistics
            },
            new Menu
            {
                Icon = "ic_launcher_map",
                PageName = "",
                Title = Languages.SeeMaps,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_launcher_person",
                PageName = "" ,
                Title = Languages.ModifyUser,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_launcher_exit",
                PageName = $"{nameof(SimpleLoginPage)}",
                Title = Languages.Login
            }
        };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }
    }

}
