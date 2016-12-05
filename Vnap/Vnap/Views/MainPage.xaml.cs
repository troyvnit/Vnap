using FormsPlugin.Iconize;

namespace Vnap.Views
{
    public partial class MainPage : IconTabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Children.Add(new PlantListTab());
            Children.Add(new AdvisoryTab());
            Children.Add(new NoticeListTab());
            Children.Add(new NewsListTab());
        }
    }
}
