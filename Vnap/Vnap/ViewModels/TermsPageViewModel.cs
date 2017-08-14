using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Vnap.Models;
using Vnap.Service.Utils;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class TermsPageViewModel : BaseViewModel
    {
        INavigationService _navigationService;

        public DelegateCommand AgreeCommand { get; set; }

        private WebViewSource _content;

        public WebViewSource Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public TermsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AgreeCommand = new DelegateCommand(ExecuteAgreeCommand);
        }

        private async void ExecuteAgreeCommand()
        {
            await _navigationService.NavigateAsync("LeftMenu/Navigation/MainPage/PlantListTab", animated: false);;
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var term = LocalDataStorage.GetArticles().FirstOrDefault(a => a.ArticleType == Entity.ArticleType.Terms);
            if (!string.IsNullOrEmpty(term?.Content))
            {
                Content = new HtmlWebViewSource() { Html = term.Content };
            }
        }
    }
}
