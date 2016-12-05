using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Commands;
using Prism.Navigation;
using Vnap.Models;
using Vnap.Service;
using Xamarin.Forms;
using Image = Vnap.Models.Image;

namespace Vnap.ViewModels
{
    public class PlantDiseaseDetailTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPlantDiseaseService _plantDiseaseService;

        private readonly ObservableCollection<Image> _images = new ObservableCollection<Image>();

        public ObservableCollection<Image> Images => _images;

        public int PlantDiseaseId { get; set; }

        private ImageSource _previewImage = null;
        public ImageSource PreviewImage
        {
            get { return _previewImage; }
            set
            {
                SetProperty(ref _previewImage, value);
            }
        }

        private string _previewCaption = null;
        public string PreviewCaption
        {
            get { return _previewCaption; }
            set
            {
                SetProperty(ref _previewCaption, value);
            }
        }

        private WebViewSource _description = null;
        public WebViewSource Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        private Solution _solution = null;
        public Solution Solution
        {
            get { return _solution; }
            set
            {
                SetProperty(ref _solution, value);
            }
        }

        public DelegateCommand<Image> PreviewImageCommand { get; set; }
        public DelegateCommand NavigateCommand { get; set; }

        public PlantDiseaseDetailTabViewModel(INavigationService navigationService, IPlantDiseaseService plantDiseaseService)
        {
            _navigationService = navigationService;
            _plantDiseaseService = plantDiseaseService;
            PreviewImageCommand = new DelegateCommand<Image>(ExecutePreviewImageCommand, CanExecutePreviewImageCommand);
            NavigateCommand = DelegateCommand.FromAsyncHandler(Navigate);
        }

        private async Task Navigate()
        {
            if (Solution != null)
            {
                var navigationParameters = new NavigationParameters { { "Solution", Solution } };
                await _navigationService.NavigateAsync($"LeftMenu/Navigation/PlantDiseaseSolutionPage", navigationParameters, animated: false);
            }
        }

        public bool CanExecutePreviewImageCommand(Image image)
        {
            return IsNotBusy;
        }

        public void ExecutePreviewImageCommand(Image image)
        {
            IsBusy = true;

            SetPreview(image);

            IsBusy = false;
        }

        public void SetPreview(Image image)
        {
            PreviewImage = image?.Url;
            PreviewCaption = image?.Caption;
        }

        public void LoadPlantDiseaseDetails()
        {
            var plantDisease = _plantDiseaseService.GetPlantDisease(PlantDiseaseId);
            Description = new HtmlWebViewSource() { Html = plantDisease.Description };
            foreach (var image in plantDisease.Images)
            {
                _images.Add(new Image()
                {
                    Caption = image.Caption,
                    Url = image.Url ?? string.Empty
                });
            }
            Title = plantDisease.Name;
            SetPreview(Images.FirstOrDefault());
            var solution = plantDisease.Solutions.FirstOrDefault(s => s.Prime);
            if (solution != null)
            {
                Solution = Mapper.Map<Solution>(solution);
            }
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var plantDiseaseIdParameter = int.Parse((string)parameters[parameters.Keys.FirstOrDefault(k => k == "PlantDiseaseId")]);
            PlantDiseaseId = plantDiseaseIdParameter;
            LoadPlantDiseaseDetails();
            base.OnNavigatedTo(parameters);
        }
    }
}
