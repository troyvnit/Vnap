using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Vnap.Models;
using Xamarin.Forms;
using Prism.Commands;

namespace Vnap.ViewModels
{
    public class PlantDiseaseSolutionPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private int PlantDiseaseId { get; set; }

        ImageSource _avatar = null;
        public ImageSource Avatar
        {
            get { return _avatar; }
            set
            {
                SetProperty(ref _avatar, value);
            }
        }

        string _companyName = null;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                SetProperty(ref _companyName, value);
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

        public DelegateCommand NavigateCommand { get; set; }
        public PlantDiseaseSolutionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(Navigate);
        }

        private async void Navigate()
        {
            var navigationParameters = new NavigationParameters { { "PlantDiseaseId", PlantDiseaseId } };
            await _navigationService.NavigateAsync($"PlantDiseaseSolutionListPage", navigationParameters, animated: false);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var solution = (Solution)parameters[parameters.Keys.FirstOrDefault(k => k == "Solution")];
            if (solution != null)
            {
                Avatar = solution.Avatar;
                CompanyName = solution.CompanyName;
                Description = new HtmlWebViewSource()
                {
                    Html = solution.Description
                };
                PlantDiseaseId = solution.PlantDiseaseId;
            }

            base.OnNavigatedTo(parameters);
        }
    }
}
