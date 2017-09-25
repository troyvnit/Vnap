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
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

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
            var param = parameters[parameters.Keys.FirstOrDefault(k => k == "Article")];
            var article = param as Article;
            if (!string.IsNullOrEmpty(article?.Content))
            {
                Title = article.Title;
                Description = article.Description;
                Content = new HtmlWebViewSource() { Html = article.Content };
            }
        }
    }
}
