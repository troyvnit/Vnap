using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service.Utils;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class LeftMenuViewModel : BaseViewModel
    {
        INavigationService _navigationService;

        string _userName = "???";
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
            }
        }

        string _city = "???";
        public string City
        {
            get { return _city; }
            set
            {
                SetProperty(ref _city, value);
            }
        }

        string _plant = "???";
        public string Plant
        {
            get { return _plant; }
            set
            {
                SetProperty(ref _plant, value);
            }
        }

        public ObservableCollection<LeftMenuItem> MenuItems
        {
            get
            {
                var menuItems = new ObservableCollection<LeftMenuItem>()
                {
                    new LeftMenuItem()
                   {
                       Icon = "flaticon-home",
                       Text = "Trang chủ",
                       Command = "Navigation/MainPage/PlantListTab",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "flaticon-house",
                       Text = "Giới thiệu về Vnap",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "lnr-bubble",
                       Text = "Tư vấn",
                       Command = "Navigation/MainPage/AdvisoryTab",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "flaticon-alarm",
                       Text = "Thông tin",
                       Command = "Navigation/MainPage/InfoListTab",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "flaticon-smartphone-10",
                       Text = "Hướng dẫn sử dụng",
                       IsActived = true
                   },
                    new LeftMenuItem()
                   {
                       Icon = "flaticon-users-1",
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
                       Icon = "flaticon-id-card-2",
                       Text = "Hồ sơ",
                       IsActived = true
                   },
                   new LeftMenuItem()
                   {
                       Icon = "flaticon-exit-1",
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
