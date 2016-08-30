using System.Threading.Tasks;
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
            await _navigationService.NavigateAsync("LeftMenu/Navigation/MainPage/PlantListTab", animated: false);
        }
    }
}
