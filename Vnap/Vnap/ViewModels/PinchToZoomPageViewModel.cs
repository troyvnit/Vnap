using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vnap.ViewModels
{
    public class PinchToZoomPageViewModel : BaseViewModel
    {
        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var imageUrl = (string)parameters[parameters.Keys.FirstOrDefault(k => k == "ImageUrl")];
            if (!string.IsNullOrEmpty(imageUrl))
            {
                ImageUrl = imageUrl.Replace("upload", "upload/a_exif");
            }
        }
    }
}
