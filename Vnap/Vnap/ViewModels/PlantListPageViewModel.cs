using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Vnap.Models;
using Vnap.Services;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class PlantListPageViewModel : BaseViewModel
    {
        private PlantService _plantService = new PlantService();
        private ObservableCollection<Plant> _plants = new ObservableCollection<Plant>();

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Plant> LoadMoreCommand { get; set; }

        public PlantListPageViewModel()
        {
            LoadPlants();
            RefreshCommand = DelegateCommand.FromAsyncHandler(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            LoadMoreCommand = DelegateCommand<Plant>.FromAsyncHandler(ExecuteLoadMoreCommand, CanExecuteLoadMoreCommand);
        }

        public async Task LoadPlants()
        {
            var newPlants = _plantService.Load(null);

            foreach (var plant in newPlants)
            {
                _plants.Add(plant);
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
            var items = _plantService.Load(item.CreatedDate);
            foreach (var plant in items)
            {
                _plants.Add(plant);
            }
            Title = $"Plants {_plants.Count}";
            IsBusy = false;
        }

    }
}
