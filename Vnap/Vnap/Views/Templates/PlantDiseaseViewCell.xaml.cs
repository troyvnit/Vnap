using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.Views.Templates
{
    public partial class PlantDiseaseViewCell : ViewCell
    {
        public PlantDiseaseViewCell()
        {
            InitializeComponent();
            BindingContext = new PlantDisease();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var plantDisease = BindingContext as PlantDisease;
            Name.HorizontalTextAlignment = plantDisease != null && plantDisease.IsEven ? TextAlignment.Start : TextAlignment.End;
        }
    }
}
