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
    public class PlantListPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPlantService _plantService;

        private readonly ObservableCollection<Plant> _plants = new ObservableCollection<Plant>();
        public ObservableCollection<Plant> Plants => _plants;

        private int _totalPlants;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Plant> LoadMoreCommand { get; set; }

        public PlantListPageViewModel(INavigationService navigationService, IPlantService plantService)
        {
            _plantService = plantService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<Plant>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            await LoadPlants(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Plant item)
        {
            return IsNotBusy && _plants.Count < _totalPlants;
        }

        public async Task ExecuteLoadMoreCommand(Plant item)
        {
            IsBusy = true;

            var skip = _plants.Count;
            await LoadPlants(skip);

            IsBusy = false;
        }

        public async Task LoadPlants(int skip)
        {
            var rq = new GetPlantsRq()
            {
                Skip = skip
            };
            var newPlants = await _plantService.GetPlants(rq);
            _totalPlants = await _plantService.GetPlantsCount();

            var isEven = _plants.LastOrDefault() != null && _plants.LastOrDefault().IsEven;
            foreach (var plant in newPlants)
            {
                isEven = !isEven;
                _plants.Add(new Plant()
                {
                    Id = plant.Id,
                    Description = plant.Description,
                    Name = plant.Name,
                    Avatar = plant.Avatar,
                    IsEven = isEven
                });
            }
        }

        public void PlantListItemSelectedHandler(Plant plant)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseasePage?PlantId={plant.Id}", animated: false);
        }
    }
}
