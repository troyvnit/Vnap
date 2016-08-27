using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.Views.Templates
{
    public partial class PostViewCell : ViewCell
    {
        public PostViewCell()
        {
            InitializeComponent();
            BindingContext = new Plant();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}
