using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Solution;

namespace Vnap.ViewModels
{
    public class PlantDiseaseSolutionListPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ISolutionService _solutionService;

        private ObservableCollection<Solution> _solutions = new ObservableCollection<Solution>();
        public ObservableCollection<Solution> Solutions => _solutions;
        
        private int PlantDiseaseId;

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Solution> LoadMoreCommand { get; set; }

        public PlantDiseaseSolutionListPageViewModel(INavigationService navigationService, ISolutionService solutionService)
        {
            _solutionService = solutionService;
            _navigationService = navigationService;
            RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = new DelegateCommand<Solution>(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public bool CanExecuteRefreshCommand()
        {
            return IsNotBusy;
        }

        public void ExecuteRefreshCommand()
        {
            IsBusy = true;

            LoadSolutions(0);

            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Solution item)
        {
            return IsNotBusy;
        }

        public void ExecuteLoadMoreCommand(Solution item)
        {
            IsBusy = true;

            var skip = _solutions.Count;
            LoadSolutions(skip);

            IsBusy = false;
        }

        public async void LoadSolutions(int skip)
        {
            var rq = new GetSolutionsRq()
            {
                Skip = skip,
                PlantDiseaseId = this.PlantDiseaseId
            };

            var newSolutions = await _solutionService.GetSolutions(rq);
            
            foreach (var solution in newSolutions.Where(n => !_solutions.Any(s => s.Id == n.Id)))
            {
                _solutions.Add(Mapper.Map<Solution>(solution));
            }
        }

        public async void SolutionListItemSelectedHandler(Solution solution)
        {
            solution.PlantDiseaseId = PlantDiseaseId;
            var navigationParameters = new NavigationParameters { { "Solution", solution } };
            await _navigationService.NavigateAsync($"PlantDiseaseSolutionPage", navigationParameters, animated: false);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var plantDiseaseIdParameter = (int)parameters[parameters.Keys.FirstOrDefault(k => k == "PlantDiseaseId")];
            PlantDiseaseId = plantDiseaseIdParameter;
            LoadSolutions(0);
            base.OnNavigatedTo(parameters);
        }
    }
}
