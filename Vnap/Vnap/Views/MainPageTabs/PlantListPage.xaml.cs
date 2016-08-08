using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantListPage : ContentPage
    {
        public PlantListPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected_(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
