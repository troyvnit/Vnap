using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantListTab : ContentPage
    {
        private bool _loaded;

        public PlantListTab()
        {
            InitializeComponent();
        }

        private void PlantList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(PlantListView.SelectedItem == null) return;
            var context = BindingContext as PlantListTabViewModel;
            var selectedItem = PlantListView.SelectedItem as Plant;
            context?.PlantListItemSelectedHandler(selectedItem);
            PlantListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            if (!_loaded)
            {
                var context = BindingContext as PlantListTabViewModel;
                if (context != null) await context.LoadPlants(0);
                _loaded = true;
            }
            base.OnAppearing();
        }
    }
}
