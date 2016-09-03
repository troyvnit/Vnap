using Vnap.ViewModels;
using Vnap.Views.ExtendedControls;

namespace Vnap.Views
{
    public partial class PlantDiseaseDetailPage : BindableTabbedPage
    {
        public PlantDiseaseDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var plantDiseaseDetailPageViewModel = BindingContext as PlantDiseaseDetailPageViewModel;
            if (plantDiseaseDetailPageViewModel != null) await plantDiseaseDetailPageViewModel.LoadAsync();
        }
    }
}
