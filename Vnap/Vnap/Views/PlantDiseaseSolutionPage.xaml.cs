using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseSolutionPage : ContentPage
    {
        public PlantDiseaseSolutionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var context = BindingContext as PlantDiseaseSolutionPageViewModel;
            context?.LoadSolutionDetail();
            base.OnAppearing();
        }
    }
}
