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

        public string PlantDisease { get; set; }


        ImageSource _previewImage = null;
        public ImageSource PreviewImage
        {
            get { return _previewImage; }
            set
            {
                SetProperty(ref _previewImage, value);
            }
        }

        string _previewCaption = null;
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
        
        public DelegateCommand<Image> PreviewImageCommand { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }

        public PlantDiseaseDetailTabViewModel(INavigationService navigationService, IPlantDiseaseService plantDiseaseService)
        {
            _navigationService = navigationService;
            _plantDiseaseService = plantDiseaseService;
            PreviewImageCommand = new DelegateCommand<Image>(ExecutePreviewImageCommand, CanExecutePreviewImageCommand);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string name)
        {
            _navigationService.NavigateAsync(name);
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

        public async Task LoadPlantDiseaseDetails()
        {
            var plantDisease = await _plantDiseaseService.GetPlantDisease(PlantDisease);
            Description = new HtmlWebViewSource() { Html = plantDisease.Description };
            foreach (var image in plantDisease.Images)
            {
                _images.Add(new Image()
                {
                    Caption = image.Caption,
                    Url = image.Url ?? string.Empty
                });
            }
            SetPreview(Images.FirstOrDefault());
        }
    }
}
