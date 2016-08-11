using FormsPlugin.Iconize;
using Prism.Navigation;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class Navigation : IconNavigationPage, INavigationPageOptions
    {
        public bool ClearNavigationStackOnNavigation => true;

        public Navigation(NavigationPage root) : base(root)
        {
            InitializeComponent();
        }
    }
}
