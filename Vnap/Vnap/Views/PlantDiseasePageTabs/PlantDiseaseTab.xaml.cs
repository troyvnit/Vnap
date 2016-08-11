using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class PlantDiseaseTab : ContentPage
    {
        public PlantDiseaseTab()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected_(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
