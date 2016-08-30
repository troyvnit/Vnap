using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseDetailTab : ContentPage
    {
        private bool _loaded;

        public PlantDiseaseDetailTab()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (!_loaded)
            {
                var context = BindingContext as PlantDiseaseDetailTabViewModel;
                context?.LoadImages();
                _loaded = true;
            }
            base.OnAppearing();
        }
    }
}
