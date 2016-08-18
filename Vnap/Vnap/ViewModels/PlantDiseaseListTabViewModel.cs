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

        private readonly ObservableCollection<PlantDisease> _plantDiseases = new ObservableCollection<PlantDisease>();
        public ObservableCollection<PlantDisease> PlantDiseases => _plantDiseases;

        private int _totalPlantDiseases;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<PlantDisease> LoadMoreCommand { get; set; }

        public PlantDiseaseListTabViewModel(INavigationService navigationService, IPlantDiseaseService plantDiseaseService)
        {
            _plantDiseaseService = plantDiseaseService;
            _navigationService = navigationService;
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<PlantDisease>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;

            await LoadPlantDiseases(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(PlantDisease item)
        {
            return IsNotBusy && _plantDiseases.Count < _totalPlantDiseases;
        }

        public async Task ExecuteLoadMoreCommand(PlantDisease item)
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
                Skip = skip
            };

            var newPlantDiseases = await _plantDiseaseService.GetPlantDiseases(rq);
            _totalPlantDiseases = await _plantDiseaseService.GetPlantDiseasesCount();

            var isEven = _plantDiseases.LastOrDefault() != null && _plantDiseases.LastOrDefault().IsEven;
            foreach (var plantDisease in newPlantDiseases)
            {
                isEven = !isEven;
                _plantDiseases.Add(new PlantDisease()
                {
                    Id = plantDisease.Id,
                    Description = plantDisease.Description,
                    Name = plantDisease.Name,
                    Avatar = plantDisease.Avatar,
                    IsEven = isEven
                });
            }
        }

        public void PlantDiseaseListItemSelectedHandler(PlantDisease plantDisease)
        {
            _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseaseDetailPage?PlantDiseaseId={plantDisease.Id}", animated: false);
        }
    }
}
