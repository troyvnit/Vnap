using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Plant;
using Vnap.Views.ExtendedControls;
using Vnap.Views.Templates;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class PlantListTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPlantService _plantService;

        private ObservableRangeCollection<Plant> _plants = new ObservableRangeCollection<Plant>();
        public ObservableRangeCollection<Plant> Plants
        { 
            get { return _plants ?? (_plants = new ObservableRangeCollection<Plant>()); }
            set { SetProperty(ref _plants, value); }
        }

        private int _totalPlants;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand LoadMoreCommand { get; set; }
        public DelegateCommand<Plant> ItemClickCommand { get; set; }

        public PlantListTabViewModel(INavigationService navigationService, IPlantService plantService)
        {
            _plantService = plantService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
            ItemClickCommand = new DelegateCommand<Plant>(ExecuteItemClickCommand, CanExecuteItemClickCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            _plants = new ObservableRangeCollection<Plant>();
            await LoadPlants(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand()
        {
            return IsNotBusy && _plants.Count < _totalPlants;
        }

        public async Task ExecuteLoadMoreCommand()
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
            
            Plants.AddRange(newPlants.Select(Mapper.Map<Plant>));
            for (int i = 0; i < Plants.Count; i++)
            {
                Plants[i].IsEven = i % 2 == 0;
            }
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await ExecuteLoadMoreCommand();
        }

        public bool CanExecuteItemClickCommand(Plant plant)
        {
            return IsNotBusy;
        }

        public void ExecuteItemClickCommand(Plant plant)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseasePage?PlantId={plant.Id}", animated: false);
        }
    }
}
