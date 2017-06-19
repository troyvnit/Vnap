using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Vnap.Service;

namespace Vnap.ViewModels
{
    public class NavigationViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public DelegateCommand SearchCommand { get; set; }

        public NavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SearchCommand = new DelegateCommand(ExecuteSearchCommand);
        }

        private void ExecuteSearchCommand()
        {
            if (!string.IsNullOrEmpty(App.SearchKey))
            {
                _navigationService.NavigateAsync($"MainPage/PlantListTab/PlantDiseasePage?SearchKey={App.SearchKey}", animated: false, useModalNavigation: true);
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
