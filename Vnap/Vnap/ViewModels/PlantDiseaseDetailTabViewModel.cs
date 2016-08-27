using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Image = Vnap.Models.Image;

namespace Vnap.ViewModels
{
    public class PlantDiseaseDetailTabViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ObservableCollection<Image> _images = new ObservableCollection<Image>();
        public ObservableCollection<Image> Images => _images;

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

        public PlantDiseaseDetailTabViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
            PreviewImage = image.Source;
            PreviewCaption = image.Caption;
            Description = new HtmlWebViewSource() { Html = @"<strong>BỆNH ĐẠO ÔN</strong> - Tên khoa học: <i>Pirycularia oryzae Cav</i>.<br/><label> Bệnh đạo ôn có thể phát sinh từ thời kỳ mạ đến lúa chín và có thể gây hại ở bẹ lá, lá, lóng thân, cổ bông, gié và hạt.
<br/>- Bệnh trên mạ: Vết bệnh trên lá mạ lúc đầu hình bầu dục nhỏ sau tạo thành hình thoi hoặc dạng tương tự hình thoi,
                màu nâu hồng hoặc nâu vàng.Khi bệnh nặng từng đám vết bệnh kế tiếp nhau làm cây mạ có thể héo khô hoặc chết.<br/>- Vết bệnh trên lá lúa: thông thường vết bệnh lúc đầu là những chấm nhỏ màu xanh hoặc mờ vết dầu, sau chuyển sang màu xám nhạt,  trên các giống lúa mẫn cảm các vết bệnh to, hình thoi, dày, màu nâu nhạt, có khi có quầng màu vàng nhạt, phần giữa vết bệnh có màu nâu xám.  </label>" };
        }

        public void LoadImages()
        {
            for (var i = 0; i <= 5; i++)
            {
                var source = ImageSource.FromFile(i % 2 == 0 ? "daoon.jpg" : "caylua.jpg");
                _images.Add(new Image
                {
                    Source = source,
                    Caption = "Troy Lee Le Cao Tri"
                });
            }
            var image = _images.FirstOrDefault();

            SetPreview(image);
        }
    }
}
