using System;
using System.Threading.Tasks;
using FFImageLoading;
using Prism.Navigation;
using Vnap.Repository;
using Vnap.Service;

namespace Vnap.ViewModels
{
    public class SplashScreenViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPlantService _plantService;
        private IPlantDiseaseService _plantDiseaseService;
        private IPostService _postService;
        private IMessageService _messageService;
        private int _loaded;

        public SplashScreenViewModel(INavigationService navigationService, IPlantService plantService, IPlantDiseaseService plantDiseaseService, IPostService postService, IMessageService messageService)
        {
            _navigationService = navigationService;
            _plantService = plantService;
            _plantDiseaseService = plantDiseaseService;
            _postService = postService;
            _messageService = messageService;
        }

        public async Task InitialDatabase()
        {
            await DatabaseHelper.InitialDatabase();
            await _plantService.Sync();
            await _plantDiseaseService.Sync();
            await _postService.Sync();
            await _messageService.Sync();

            ImageService.Instance.LoadUrl("http://vannghetiengiang.vn/uploads/news/2014_11/cay-lua2.jpg", TimeSpan.FromDays(3))
            .Success(async (size, loadingResult) =>
            {
                _loaded++;
                await Navigate();
            })
            .Error(async exception =>
            {
                _loaded++;
                await Navigate();
            }).Preload();
            ImageService.Instance.LoadUrl("http://hoidap.vinhphucnet.vn/qt/hoidap/PublishingImages/75706PMbenhdaoon.jpg", TimeSpan.FromDays(3))
            .Success(async (size, loadingResult) =>
            {
                _loaded++;
                await Navigate();
            })
            .Error(async exception =>
            {
                _loaded++;
                await Navigate();
            }).Preload();
        }

        private async Task Navigate()
        {
            if(_loaded == 2)
            await _navigationService.NavigateAsync("LeftMenu/Navigation/MainPage/PlantListTab", animated: false);
        }
    }
}
