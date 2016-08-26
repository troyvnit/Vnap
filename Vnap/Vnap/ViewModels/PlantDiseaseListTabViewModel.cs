using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Plant;

namespace Vnap.ViewModels
{
    public class PlantDiseaseListTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPlantDiseaseService _plantDiseaseService;

        private ObservableCollection<PlantDiseaseGroup> _plantDiseaseGroups = new ObservableCollection<PlantDiseaseGroup>()
        {
            new PlantDiseaseGroup()
            {
                Id = 1,
                Name = "DỊCH BỆNH",
                ShortName = "DB",
                Icon = "dead_plant.png"
            },
            new PlantDiseaseGroup()
            {
                Id = 2,
                Name = "SÂU BỆNH",
                ShortName = "SB",
                Icon = "pests.png"
            }
        };
        public ObservableCollection<PlantDiseaseGroup> PlantDiseaseGroups => _plantDiseaseGroups;

        private int _totalPlantDiseases;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<PlantDiseaseGroup> LoadMoreCommand { get; set; }

        public PlantDiseaseListTabViewModel(INavigationService navigationService, IPlantDiseaseService plantDiseaseService)
        {
            _plantDiseaseService = plantDiseaseService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<PlantDiseaseGroup>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            _plantDiseaseGroups = new ObservableCollection<PlantDiseaseGroup>();
            await LoadPlantDiseases(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(PlantDiseaseGroup item)
        {
            return IsNotBusy && _plantDiseaseGroups.Count > _totalPlantDiseases;
        }

        public async Task ExecuteLoadMoreCommand(PlantDiseaseGroup item)
        {
            IsBusy = true;

            var skip = _plantDiseaseGroups.Count;
            await LoadPlantDiseases(skip);

            IsBusy = false;
        }

        public async Task LoadPlantDiseases(int skip)
        {
            var rq = new GetPlantDiseasesRq()
            {
                Skip = skip
            };

            var newPlantDiseases = await _plantDiseaseService.GetPlantDiseases(rq);
            _totalPlantDiseases = await _plantDiseaseService.GetPlantDiseasesCount();

            foreach (var plantDiseaseGroup in _plantDiseaseGroups)
            {
                var isEven = plantDiseaseGroup.LastOrDefault() != null && plantDiseaseGroup.LastOrDefault().IsEven;
                foreach (var plantDisease in newPlantDiseases)
                {
                    isEven = !isEven;
                    plantDiseaseGroup.Add(new PlantDisease()
                    {
                        Id = plantDisease.Id,
                        Description = plantDisease.Description,
                        Name = plantDisease.Name,
                        Avatar = plantDisease.Avatar,
                        IsEven = isEven
                    });
                }
            }
        }

        public void PlantDiseaseListItemSelectedHandler(PlantDisease plantDisease)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseaseDetailPage?PlantDiseaseId={plantDisease.Id}", animated: false);
        }
    }
}
