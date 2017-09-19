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
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = new DelegateCommand(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
            ItemClickCommand = new DelegateCommand<Plant>(ExecuteItemClickCommand, CanExecuteItemClickCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public void ExecuteRefreshCommand()
        {
            IsBusy = true;

            _plants = new ObservableRangeCollection<Plant>();
            LoadPlants(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand()
        {
            return IsNotBusy;
        }

        public void ExecuteLoadMoreCommand()
        {
            IsBusy = true;

            var skip = _plants.Count;
            LoadPlants(skip);

            IsBusy = false;
        }

        public void LoadPlants(int skip)
        {
            var rq = new GetPlantsRq()
            {
                Skip = skip
            };
            var newPlants = _plantService.GetPlants(rq);
            _totalPlants = _plantService.GetPlantsCount();
            
            Plants.AddRange(newPlants.Where(p => Plants.All(ep => ep.Id != p.Id)).Select(Mapper.Map<Plant>));
            for (int i = 0; i < Plants.Count; i++)
            {
                Plants[i].IsEven = i % 2 == 0;
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            ExecuteLoadMoreCommand();
        }

        public bool CanExecuteItemClickCommand(Plant plant)
        {
            return IsNotBusy;
        }

        public void ExecuteItemClickCommand(Plant plant)
        {
            App.SearchKey = string.Empty;
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseasePage?Plant={plant.Name}", animated: false);
        }
    }
}
