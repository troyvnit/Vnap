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

namespace Vnap.ViewModels
{
    public class PlantDiseaseListTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPlantDiseaseService _plantDiseaseService;

        private ObservableRangeCollection<PlantDisease> _plantDiseases = new ObservableRangeCollection<PlantDisease>();
        public ObservableRangeCollection<PlantDisease> PlantDiseases => _plantDiseases;

        private int _totalPlantDiseases;
        public string Plant { get; set; }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand LoadMoreCommand { get; set; }
        public DelegateCommand<PlantDisease> ItemClickCommand { get; set; }

        public PlantDiseaseListTabViewModel(INavigationService navigationService, IPlantDiseaseService plantDiseaseService)
        {
            _plantDiseaseService = plantDiseaseService;
            _navigationService = navigationService;
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = new DelegateCommand(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
            ItemClickCommand = new DelegateCommand<PlantDisease>(ExecuteItemClickCommand, CanExecuteItemClickCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async void ExecuteRefreshCommand()
        {
            IsBusy = true;

            _plantDiseases = new ObservableRangeCollection<PlantDisease>();
            await LoadPlantDiseases(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand()
        {
            return IsNotBusy && _plantDiseases.Count < _totalPlantDiseases;
        }

        public async void ExecuteLoadMoreCommand()
        {
            IsBusy = true;

            var skip = _plantDiseases.Count;
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

            var newPlantDiseases = _plantDiseaseService.GetPlantDiseases(rq);
            _totalPlantDiseases = _plantDiseaseService.GetPlantDiseasesCount();

            PlantDiseases.AddRange(newPlantDiseases.Select(Mapper.Map<PlantDisease>));
            for (int i = 0; i < PlantDiseases.Count; i++)
            {
                PlantDiseases[i].IsEven = i % 2 == 0;
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            ExecuteLoadMoreCommand();
        }

        public bool CanExecuteItemClickCommand(PlantDisease plant)
        {
            return IsNotBusy;
        }

        public void ExecuteItemClickCommand(PlantDisease plantDisease)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseaseDetailPage/PlantDiseaseDetailTab?PlantDiseaseId={plantDisease.Id}", animated: false);
        }
    }
}
