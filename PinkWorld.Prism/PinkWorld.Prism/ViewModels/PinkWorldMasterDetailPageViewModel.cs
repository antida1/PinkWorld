
using Newtonsoft.Json;
using PinkWorld.Common.Helpers;
using PinkWorld.Common.Models;
using PinkWorld.Common.Responses;
using PinkWorld.Prism.Helpers;
using PinkWorld.Prism.ItemViewModel;
using PinkWorld.Prism.Views;
using PinkWorld.Prism.Views.Catalog;
using PinkWorld.Prism.Views.Dashboard;
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
        private static PinkWorldMasterDetailPageViewModel _instance;
        public PinkWorldMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
            Name = Languages.Menu;
            _instance = this;
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

        public static PinkWorldMasterDetailPageViewModel GetInstance()
        {
            return _instance;
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
                PageName = $"{nameof(RegisterSelfExaminationPage)}",
                Title = Languages.RegisterSelfExamination,
                IsLoginRequired = true
            },

            new Menu
            {
                Icon = "ic_launcher_history",
                PageName = $"{nameof(ListQuizzesPage)}",
                Title = Languages.ShowQuizzesHistory,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_launcher_stadistic",
                PageName = $"{nameof(StockOverviewPage)}",
                Title = Languages.ViewStadistics,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_launcher_map",
                PageName = $"{nameof(MapPage)}",
                Title = Languages.SeeMaps,
                IsLoginRequired = false

            },
            new Menu
            {
                Icon = "ic_launcher_person",
                PageName =$"{nameof(EditUserPage)}",
                Title = Languages.ModifyUser,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_launcher_exit",
                PageName = $"{nameof(SimpleLoginPage)}",
                Title = Settings.IsLogin ? Languages.Logout : Languages.Login
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
