using Prism.Navigation;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class Navigation : NavigationPage, INavigationPageOptions
    {
        public Navigation()
        {
            InitializeComponent();
        }

        public bool ClearNavigationStackOnNavigation => true;
    }
}
