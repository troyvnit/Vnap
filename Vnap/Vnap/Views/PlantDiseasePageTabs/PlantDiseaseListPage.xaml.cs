using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseListPage : ContentPage
    {
        public PlantDiseaseListPage()
        {
            InitializeComponent();
        }

        private void PlantDiseaseList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PlantDiseaseListView.SelectedItem == null) return;
            var context = BindingContext as PlantDiseaseListPageViewModel;
            var selectedItem = PlantDiseaseListView.SelectedItem as PlantDisease;
            context?.PlantDiseaseListItemSelectedHandler(selectedItem);
            PlantDiseaseListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as PlantDiseaseListPageViewModel;
            if (context != null) await context.LoadPlantDiseases(0);
            base.OnAppearing();
        }
    }
}
