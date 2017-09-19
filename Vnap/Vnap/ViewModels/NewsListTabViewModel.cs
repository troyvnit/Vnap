using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Navigation;
using Vnap.Entity;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Article;
using Microsoft.AspNet.SignalR.Client;
using Vnap.Service.Utils;
using Xamarin.Forms;
using Vnap.Services;

namespace Vnap.ViewModels
{
    public class NewsListTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IArticleService _articleService;
        private bool subscribed;

        private ObservableCollection<Article> _articles = new ObservableCollection<Article>();
        public ObservableCollection<Article> Articles => _articles;

        private int _totalArticles;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Article> LoadMoreCommand { get; set; }

        public NewsListTabViewModel(INavigationService navigationService, IArticleService articleService)
        {
            _articleService = articleService;
            _navigationService = navigationService;
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = new DelegateCommand<Article>(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);

            MessagingCenter.Subscribe<Article>(this, "UpdateArticles", newArticle =>
            {
                _articles.Add(newArticle);
                LocalDataStorage.SetArticles(_articles.Select(Mapper.Map<ArticleEntity>).ToList());
            });

            //if (App.HubConnection.State == ConnectionState.Disconnected)
            //{
            //    App.HubConnection.Start().Wait();
            //    App.HubProxy.Invoke("HandShake", App.CurrentUser.UserName).Wait();
            //}
            //if (!subscribed)
            //{
            //    App.HubProxy.Subscribe("PublishArticle").Received += rs => {
            //        var newArticle = rs[0]?.ToObject<Article>();
            //        if (newArticle != null && (newArticle.ArticleType == ArticleType.News || newArticle.ArticleType == ArticleType.Notice))
            //        {
            //            _articles.Add(newArticle);
            //            LocalDataStorage.SetArticles(_articles.Select(Mapper.Map<ArticleEntity>).ToList());
            //        }
            //    };

            //    subscribed = true;
            //}
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public void ExecuteRefreshCommand()
        {
            IsBusy = true;

            _articles = new ObservableCollection<Article>();
            LoadArticles(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Article item)
        {
            return IsNotBusy && _articles.Count > _totalArticles;
        }

        public void ExecuteLoadMoreCommand(Article item)
        {
            IsBusy = true;

            var skip = _articles.Count;
            LoadArticles(skip);

            IsBusy = false;
        }

        public void LoadArticles(int skip)
        {
            var rq = new GetArticlesRq()
            {
                Skip = skip,
                Take = 10
            };
            var newArticles = _articleService.GetArticles(rq);
            _totalArticles = _articleService.GetArticlesCount();
            
            foreach (var article in newArticles)
            {
                _articles.Add(Mapper.Map<Article>(article));
            }
        }

        public async void ArticleListItemSelectedHandler(Article article)
        {
            var navigationParameters = new NavigationParameters {{"Article", article}};
            await _navigationService.NavigateAsync($"ArticleDetailPage", navigationParameters, animated: false, useModalNavigation: true);
        }
    }
}
