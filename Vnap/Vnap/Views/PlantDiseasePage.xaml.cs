using System.Linq;
using Vnap.ViewModels;
using Vnap.Views.ExtendedControls;

namespace Vnap.Views
{
    public partial class PlantDiseasePage : BindableTabbedPage
    {
        public PlantDiseasePage()
        {
            InitializeComponent();
            CurrentPageChanged += async (sender, args) =>
            {
                var context = CurrentPage?.BindingContext as PlantDiseaseListTabViewModel;
                if (context != null)
                {
                    context.Plant = CurrentPage.Title;
                    var plantDiseasePageViewModel = BindingContext as PlantDiseasePageViewModel;
                    if (plantDiseasePageViewModel != null)
                    {
                        plantDiseasePageViewModel.CurrentPlant = CurrentPage.Title;
                    }
                    if (context.PlantDiseases.Count == 0)
                    {
                        await context.LoadPlantDiseases(0, App.SearchKey);
                    }
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var plantDiseasePageViewModel = BindingContext as PlantDiseasePageViewModel;
            if (plantDiseasePageViewModel != null)
            {
                await plantDiseasePageViewModel.LoadAsync();
                CurrentPage = plantDiseasePageViewModel.PlantDiseaseListTabs.FirstOrDefault(c => c.Title.ToLower() == plantDiseasePageViewModel.CurrentPlant.ToLower());
            }
        }
    }
}
