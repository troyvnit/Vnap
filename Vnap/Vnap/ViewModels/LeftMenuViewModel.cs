using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Vnap.ViewModels
{
    public class LeftMenuViewModel : BaseViewModel
    {
        INavigationService _navigationService;

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
