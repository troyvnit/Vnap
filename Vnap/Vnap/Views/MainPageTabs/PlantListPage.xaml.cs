using Vnap.Models;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantListPage : ContentPage
    {
        public PlantListPage()
        {
            InitializeComponent();
        }

        private void PlantList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(PlantListView.SelectedItem == null) return;
            var context = BindingContext as PlantListPageViewModel;
            var selectedItem = PlantListView.SelectedItem as Plant;
            context?.PlantListItemSelectedHandler(selectedItem);
            PlantListView.SelectedItem = null;
        }
    }
}
