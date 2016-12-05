using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.ViewModels
{
    public class PlantDiseaseSolutionPageViewModel : BaseViewModel
    {

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

        public void LoadSolutionDetail()
        {
            
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
            }
        }
    }
}
