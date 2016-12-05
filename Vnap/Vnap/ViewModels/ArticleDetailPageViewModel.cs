using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class ArticleDetailPageViewModel : BaseViewModel
    {
        private WebViewSource _content;

        public WebViewSource Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var article = (Article)parameters[parameters.Keys.FirstOrDefault(k => k == "Article")];
            if (!string.IsNullOrEmpty(article.Content))
            {
                Content = new HtmlWebViewSource() { Html = article.Content }; 
            }
        }
    }
}
