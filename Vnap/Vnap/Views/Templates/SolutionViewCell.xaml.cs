using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.Views.Templates
{
    public partial class SolutionViewCell : ViewCell
    {
        public SolutionViewCell()
        {
            InitializeComponent();
            BindingContext = new Solution();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var solution = BindingContext as Solution;
            Icon.Text = solution != null && solution.Prime ? "flaticon-like" : ""; 
        }
    }
}
