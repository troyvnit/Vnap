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

namespace Vnap.ViewModels
{
    public class NoticeListTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IArticleService _articleService;

        private ObservableCollection<Article> _articles = new ObservableCollection<Article>();
        public ObservableCollection<Article> Articles => _articles;

        private int _totalArticles;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Article> LoadMoreCommand { get; set; }

        public NoticeListTabViewModel(INavigationService navigationService, IArticleService articleService)
        {
            _articleService = articleService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<Article>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            _articles = new ObservableCollection<Article>();
            await LoadArticles(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Article item)
        {
            return IsNotBusy && _articles.Count > _totalArticles;
        }

        public async Task ExecuteLoadMoreCommand(Article item)
        {
            IsBusy = true;

            var skip = _articles.Count;
            await LoadArticles(skip);

            IsBusy = false;
        }

        public async Task LoadArticles(int skip)
        {
            var rq = new GetArticlesRq()
            {
                Skip = skip,
                ArticleType = ArticleType.Notice
            };
            var newArticles = await _articleService.GetArticles(rq);
            _totalArticles = await _articleService.GetArticlesCount();
            
            foreach (var article in newArticles)
            {
                if (!_articles.Select(p => p.Id).Contains(article.Id))
                {
                    _articles.Add(Mapper.Map<Article>(article));
                }
            }
        }

        public async Task ArticleListItemSelectedHandler(Article article)
        {
            var navigationParameters = new NavigationParameters { { "Article", article } };
            await _navigationService.NavigateAsync($"ArticleDetailPage", navigationParameters, animated: false, useModalNavigation: true);
        }
    }
}
