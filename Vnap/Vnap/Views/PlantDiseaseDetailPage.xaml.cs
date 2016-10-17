using System.Linq;
using Vnap.ViewModels;
using Vnap.Views.ExtendedControls;

namespace Vnap.Views
{
    public partial class PlantDiseaseDetailPage : BindableTabbedPage
    {
        public PlantDiseaseDetailPage()
        {
            InitializeComponent();
            CurrentPageChanged += async (sender, args) =>
            {
                var context = CurrentPage?.BindingContext as PlantDiseaseDetailTabViewModel;
                if (context != null)
                {
                    context.PlantDisease = CurrentPage.Title;
                    var command = context?.LoadPlantDiseaseDetails();
                    await command;
                }
                var plantDiseaseDetailPageViewModel = BindingContext as PlantDiseaseDetailPageViewModel;
                if (plantDiseaseDetailPageViewModel != null)
                {
                    if (CurrentPage != null) plantDiseaseDetailPageViewModel.CurrentPlantDisease = CurrentPage.Title;
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var plantDiseaseDetailPageViewModel = BindingContext as PlantDiseaseDetailPageViewModel;
            if (plantDiseaseDetailPageViewModel != null)
            {
                await plantDiseaseDetailPageViewModel.LoadAsync();
                CurrentPage = Children.FirstOrDefault(c => c.Title == plantDiseaseDetailPageViewModel.CurrentPlantDisease);
            }
        }
    }
}
