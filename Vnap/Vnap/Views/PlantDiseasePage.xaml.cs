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
                var context = CurrentPage.BindingContext as PlantDiseaseListTabViewModel;
                if (context != null)
                {
                    context.Plant = CurrentPage.Title;
                    var executeLoadMoreCommand = context?.ExecuteLoadMoreCommand();
                    await executeLoadMoreCommand;
                }
                var plantDiseasePageViewModel = BindingContext as PlantDiseasePageViewModel;
                if (plantDiseasePageViewModel != null)
                {
                    if (CurrentPage != null) plantDiseasePageViewModel.CurrentPlant = CurrentPage.Title;
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
                CurrentPage = Children.FirstOrDefault(c => c.Title == plantDiseasePageViewModel.CurrentPlant);
            }
        }
    }
}
