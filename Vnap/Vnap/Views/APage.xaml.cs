using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class APage : ContentPage
    {
        public APage(string content = null)
        {
            InitializeComponent();
            Title = content;
        }
    }
}
