using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Unity.Navigation;
using Vnap.Extensions;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Plant;
using Vnap.Views;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class PlantDiseasePageViewModel : BaseViewModel
    {
        private readonly IPlantService _plantService;

        public string CurrentPlant;

        private ObservableCollection<Page> _plantDiseaseTabs = new ObservableCollection<Page>();

        public ObservableCollection<Page> PlantDiseaseListTabs
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

        public override async Task LoadAsync()
        {
            var rq = new GetPlantsRq()
            {
                Skip = PlantDiseaseListTabs.Count
            };

            var plants = _plantService.GetPlants(rq);

            if (plants.Count > PlantDiseaseListTabs.Count)
            {
                var list = new List<Page>();

                list.AddRange(plants.Select(p => new PlantDiseaseListTab()
                {
                    Title = p.Name,
                    Icon = ""
                }));

                PlantDiseaseListTabs = list.ToObservableCollection();
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var plantParameter = (string)parameters[parameters.Keys.FirstOrDefault(k => k == "Plant")];
            CurrentPlant = plantParameter;
            base.OnNavigatedTo(parameters);
        }
    }
}
