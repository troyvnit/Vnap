using XLabs.Forms.Controls;

namespace Vnap.Views
{
    public partial class MainPage : ExtendedTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Children.Add(new PlantDiseaseDetailTab());
            Children.Add(new PlantListTab());
            Children.Add(new InfoTab());
            Children.Add(new AdvisoryTab());
        }
    }
}
