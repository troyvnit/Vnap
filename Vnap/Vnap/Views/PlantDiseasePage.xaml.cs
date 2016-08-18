using Vnap.ViewModels;
using Vnap.Views.Customs;

namespace Vnap.Views
{
    public partial class PlantDiseasePage : BindableTabbedPage
    {
        public PlantDiseasePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var plantDiseasePageViewModel = BindingContext as PlantDiseasePageViewModel;
            if (plantDiseasePageViewModel != null) await plantDiseasePageViewModel.LoadAsync();
        }
    }
}
