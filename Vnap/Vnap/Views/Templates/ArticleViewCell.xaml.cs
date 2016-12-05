using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.Views.Templates
{
    public partial class ArticleViewCell : ViewCell
    {
        public ArticleViewCell()
        {
            InitializeComponent();
            BindingContext = new Article();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}
