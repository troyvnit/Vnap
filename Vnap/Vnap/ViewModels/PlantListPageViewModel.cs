using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;

namespace Vnap.ViewModels
{
    public class PlantListPageViewModel : BaseViewModel
    {
        INavigationService _navigationService;
        private IPlantService _plantService;

        private ObservableCollection<Plant> _plants = new ObservableCollection<Plant>();

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Plant> LoadMoreCommand { get; set; }

        public PlantListPageViewModel(INavigationService navigationService, IPlantService plantService)
        {
            _plantService = plantService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<Plant>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public async Task LoadPlants()
        {
            var newPlants = await _plantService.Load(null);

            var isEven = _plants.LastOrDefault() != null && _plants.LastOrDefault().IsEven;
            foreach (var plant in newPlants)
            {
                isEven = !isEven;
                _plants.Add(new Plant()
                {
                    Id = plant.Id,
                    Description = plant.Description,
                    Name = plant.Name,
                    IsEven = isEven
                });
            }

            Title = $"Plants {_plants.Count}";
        }

        public ObservableCollection<Plant> Plants
        {
            get { return _plants; }

        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            await LoadPlants();

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Plant item)
        {
            return IsNotBusy && _plants.Count != 0 && _plants.OrderByDescending(o => o.CreatedDate).Last().CreatedDate == item.CreatedDate;
        }

        public async Task ExecuteLoadMoreCommand(Plant item)
        {
            IsBusy = true;
            
            var items = await _plantService.Load(item.CreatedDate);
            var isEven = _plants.LastOrDefault() != null && _plants.LastOrDefault().IsEven;
            foreach (var plant in items)
            {
                isEven = !isEven;
                _plants.Add(new Plant()
                {
                    Id = plant.Id,
                    Description = plant.Description,
                    Name = plant.Name,
                    IsEven = isEven
                });
            }
            Title = $"Plants {_plants.Count}";
            IsBusy = false;
        }

        public void PlantListItemSelectedHandler(Plant plant)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseasePage?query={plant.Id}", animated: true);
        }
    }
}
