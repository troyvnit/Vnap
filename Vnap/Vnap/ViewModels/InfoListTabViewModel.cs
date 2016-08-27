using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Plant;

namespace Vnap.ViewModels
{
    public class InfoListTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPostService _postService;

        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> Posts => _posts;

        private int _totalPosts;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Post> LoadMoreCommand { get; set; }

        public InfoListTabViewModel(INavigationService navigationService, IPostService postService)
        {
            _postService = postService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<Post>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            _posts = new ObservableCollection<Post>();
            await LoadPosts(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Post item)
        {
            return IsNotBusy && _posts.Count > _totalPosts;
        }

        public async Task ExecuteLoadMoreCommand(Post item)
        {
            IsBusy = true;

            var skip = _posts.Count;
            await LoadPosts(skip);

            IsBusy = false;
        }

        public async Task LoadPosts(int skip)
        {
            var rq = new GetPostsRq()
            {
                Skip = skip
            };
            var newPosts = await _postService.GetPosts(rq);
            _totalPosts = await _postService.GetPostsCount();
            
            foreach (var post in newPosts)
            {
                if (!_posts.Select(p => p.Id).Contains(post.Id))
                {
                    _posts.Add(Mapper.Map<Post>(post));
                }
            }
        }

        public void PostListItemSelectedHandler(Post post)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PostDiseasePage?PostId={post.Id}", animated: false);
        }
    }
}
