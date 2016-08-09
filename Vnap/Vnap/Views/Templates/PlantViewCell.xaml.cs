using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.Views.Templates
{
    public partial class PlantViewCell : ViewCell
    {
        public PlantViewCell()
        {
            InitializeComponent();
            BindingContext = new Plant();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var plant = BindingContext as Plant;
            Name.HorizontalTextAlignment = Description.HorizontalTextAlignment = plant != null && plant.IsEven ? TextAlignment.Start : TextAlignment.End;
        }
    }
}
