using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseDetailTab : ContentPage
    {
        public PlantDiseaseDetailTab()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var context = BindingContext as PlantDiseaseDetailTabViewModel;
            context?.LoadImages();
            base.OnAppearing();
        }
    }
}
