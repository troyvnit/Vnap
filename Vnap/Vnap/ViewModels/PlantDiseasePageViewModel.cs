using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Vnap.Extensions;
using Vnap.Models;
using Vnap.Service;
using Vnap.Views;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class PlantDiseasePageViewModel : BaseViewModel
    {
        private readonly IPlantService _plantService;

        private int _currentPlantId;

        private ObservableCollection<Page> _plantDiseaseTabs = new ObservableCollection<Page>();

        public ObservableCollection<Page> PlantDiseaseTabs
        {
            get
            {
                return _plantDiseaseTabs;
            }
            set { SetProperty(ref _plantDiseaseTabs, value); }
        }

        public PlantDiseasePageViewModel(IPlantService plantService)
        {
            _plantService = plantService;
        }

        public async override Task LoadAsync()
        {
            var plants = await _plantService.GetPlants(PlantDiseaseTabs.Count, 5, _currentPlantId);

            var list = new List<Page>();

            list.AddRange(plants.Select(p => new PlantDiseaseTab()
            {
                Title = p.Name
            }));

            PlantDiseaseTabs = list.ToObservableCollection();
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var plantIdParameter = (string)parameters[parameters.Keys.FirstOrDefault(k => k == "PlantId")];
            if (!string.IsNullOrEmpty(plantIdParameter))
            {
                _currentPlantId = int.Parse(plantIdParameter);
            }
            base.OnNavigatedTo(parameters);
        }
    }
}
