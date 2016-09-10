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
using Vnap.Service.Requests.Plant;
using Vnap.Views;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class PlantDiseaseDetailPageViewModel : BaseViewModel
    {
        private readonly IPlantDiseaseService _plantDiseaseService;

        private int _currentPlantDiseaseId;

        private ObservableCollection<Page> _plantDiseaseDetailTabs = new ObservableCollection<Page>();

        public ObservableCollection<Page> PlantDiseaseDetailTabs
        {
            get
            {
                return _plantDiseaseDetailTabs;
            }
            set { SetProperty(ref _plantDiseaseDetailTabs, value); }
        }

        public PlantDiseaseDetailPageViewModel(IPlantDiseaseService plantDiseaseService)
        {
            _plantDiseaseService = plantDiseaseService;
        }

        public async override Task LoadAsync()
        {
            var rq = new GetPlantDiseasesRq()
            {
                Skip = PlantDiseaseDetailTabs.Count
            };

            var plantDiseases = await _plantDiseaseService.GetPlantDiseases(rq);

            if (plantDiseases.Count != PlantDiseaseDetailTabs.Count)
            {
                var list = new List<Page>();

                list.AddRange(plantDiseases.Select(p => new PlantDiseaseDetailTab()
                {
                    Title = p.Name,
                    Icon = ""
                }));

                PlantDiseaseDetailTabs = list.ToObservableCollection();
            }

        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var plantDiseaseIdParameter = (string)parameters[parameters.Keys.FirstOrDefault(k => k == "PlantDiseaseId")];
            if (!string.IsNullOrEmpty(plantDiseaseIdParameter))
            {
                _currentPlantDiseaseId = int.Parse(plantDiseaseIdParameter);
            }
            base.OnNavigatedTo(parameters);
        }
    }
}
