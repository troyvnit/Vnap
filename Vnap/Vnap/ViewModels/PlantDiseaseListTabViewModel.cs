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
    public class PlantDiseaseListTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPlantDiseaseService _plantDiseaseService;

        private ObservableCollection<PlantDiseaseGroup> _plantDiseaseGroups = new ObservableCollection<PlantDiseaseGroup>()
        {
            new PlantDiseaseGroup()
            {
                Id = 0,
                Name = "DỊCH BỆNH",
                ShortName = "DB",
                Icon = "dead_plant.png"
            },
            new PlantDiseaseGroup()
            {
                Id = 1,
                Name = "SÂU BỆNH",
                ShortName = "SB",
                Icon = "pests.png"
            }
        };
        public ObservableCollection<PlantDiseaseGroup> PlantDiseaseGroups => _plantDiseaseGroups;

        private int _totalPlantDiseases;
        public string Plant { get; set; }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand LoadMoreCommand { get; set; }
        public DelegateCommand<PlantDisease> ItemClickCommand { get; set; }

        public PlantDiseaseListTabViewModel(INavigationService navigationService, IPlantDiseaseService plantDiseaseService)
        {
            _plantDiseaseService = plantDiseaseService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
            ItemClickCommand = new DelegateCommand<PlantDisease>(ExecuteItemClickCommand, CanExecuteItemClickCommand);
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

        public bool CanExecuteLoadMoreCommand()
        {
            return IsNotBusy && _plantDiseaseGroups.Count > _totalPlantDiseases;
        }

        public async Task ExecuteLoadMoreCommand()
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
                Skip = skip,
                Plant = Plant
            };

            var newPlantDiseases = await _plantDiseaseService.GetPlantDiseases(rq);
            _totalPlantDiseases = await _plantDiseaseService.GetPlantDiseasesCount();

            foreach (var plantDiseaseGroup in _plantDiseaseGroups)
            {
                plantDiseaseGroup.RemoveRange(plantDiseaseGroup.ToList());
                plantDiseaseGroup.AddRange(newPlantDiseases.Where(pd => (int)pd.PlantDiseaseType == plantDiseaseGroup.Id).Select(Mapper.Map<PlantDisease>));
                for (int i = 0; i < plantDiseaseGroup.Count; i++)
                {
                    plantDiseaseGroup[i].IsEven = i % 2 == 0;
                }
            }
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await ExecuteLoadMoreCommand();
        }

        public bool CanExecuteItemClickCommand(PlantDisease plant)
        {
            return IsNotBusy;
        }

        public void ExecuteItemClickCommand(PlantDisease plantDisease)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseaseDetailPage?PlantDisease={plantDisease.Name}", animated: false);
        }
    }
}
