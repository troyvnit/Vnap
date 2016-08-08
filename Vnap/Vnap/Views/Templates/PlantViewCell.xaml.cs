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
    }
}
