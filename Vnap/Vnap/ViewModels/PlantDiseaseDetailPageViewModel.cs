using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Navigation;
using Vnap.Extensions;
using Vnap.Models;
using Vnap.Service;
using Vnap.Service.Requests.Plant;
using Vnap.Views;
using Xamarin.Forms;
using Image = Vnap.Models.Image;

namespace Vnap.ViewModels
{
    public class PlantDiseaseDetailPageViewModel : BaseViewModel
    {
        private readonly IPlantDiseaseService _plantDiseaseService;

        public string CurrentPlantDisease;

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

        public override async Task LoadAsync()
        {
            var rq = new GetPlantDiseasesRq()
            {
                Skip = 0
            };

            var plantDiseases = await _plantDiseaseService.GetPlantDiseases(rq);

            if (plantDiseases.Count != PlantDiseaseDetailTabs.Count)
            {
                var list = new List<Page>();

                list.AddRange(plantDiseases.Select(p => new PlantDiseaseDetailTab()
                {
                    Title = p.Name,
                    Icon = "",
                    //BindingContext = new PlantDiseaseDetailTabViewModel()
                    //{
                    //    PreviewImage = p.Images.FirstOrDefault()?.Url,
                    //    PreviewCaption = p.Images.FirstOrDefault()?.Caption,
                    //    Images = p.Images.Select(Mapper.Map<Image>).ToObservableCollection(),
                    //    Description = new HtmlWebViewSource() { Html = p.Description },
                    //    PlantDisease = Mapper.Map<PlantDisease>(p)
                    //}
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
            var plantDiseaseParameter = (string)parameters[parameters.Keys.FirstOrDefault(k => k == "PlantDisease")];
            CurrentPlantDisease = plantDiseaseParameter;
            base.OnNavigatedTo(parameters);
        }
    }
}
