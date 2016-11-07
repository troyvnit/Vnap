using FormsPlugin.Iconize;

namespace Vnap.Views
{
    public partial class PlantDiseaseDetailPage : IconTabbedPage
    {
        public PlantDiseaseDetailPage()
        {
            InitializeComponent();
            Children.Add(new PlantDiseaseDetailTab());
            Children.Add(new AdvisoryTab());
        }
    }
}
