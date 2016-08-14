using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class LeftMenuViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        public ObservableCollection<LeftMenuItem> MenuItems
        {
            get
            {
                var menuItems = new ObservableCollection<LeftMenuItem>()
                {
                    new LeftMenuItem()
                   {
                       Icon = "lnr-home",
                       Text = "Trang chủ",
                       Command = "Navigation/MainPage/PlantListPage",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "lnr-apartment",
                       Text = "Giới thiệu về Vnap",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "lnr-bubble",
                       Text = "Tư vấn",
                       Command = "Navigation/MainPage/AdvisoryPage",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "lnr-bullhorn",
                       Text = "Thông tin",
                       Command = "Navigation/MainPage/InfoPage",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "lnr-users",
                       Text = "Mời bạn bè sử dụng",
                       IsActived = true
                   }
                };
                return menuItems;
            }

        }

        public ObservableCollection<LeftMenuItem> AccountItems
        {
            get
            {
                var accountItems = new ObservableCollection<LeftMenuItem>()
                {
                   new LeftMenuItem()
                   {
                       Icon = "lnr-user",
                       Text = "Hồ sơ",
                       IsActived = true
                   },
                   new LeftMenuItem()
                   {
                       Icon = "lnr-power-switch",
                       Text = "Đăng xuất",
                       IsActived = true
                   }
                };
                return accountItems;
            }

        }

        public DelegateCommand<string> NavigateCommand { get; set; }
        public LeftMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string name)
        {
            _navigationService.NavigateAsync(name);
        }

        public void LeftMenuItemSelectedHandler(LeftMenuItem menuItem)
        {
            ItemSelectedHandler(menuItem);
        }

        public void AccountMenuItemSelectedHandler(LeftMenuItem menuItem)
        {
            ItemSelectedHandler(menuItem);
        }

        private void ItemSelectedHandler(LeftMenuItem menuItem)
        {
            if (menuItem == null) return;
            switch (menuItem.CommandType)
            {
                case CommandType.Navigation:
                    if (!string.IsNullOrEmpty(menuItem.Command))
                    {
                        _navigationService.NavigateAsync(menuItem.Command);
                    }
                    break;
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
