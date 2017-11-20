using Vnap.Models;
using Xamarin.Forms;

namespace Vnap.Views.Templates
{
    public partial class AdvisoryMessageViewCell : ViewCell
    {
        public AdvisoryMessageViewCell()
        {
            InitializeComponent();
            BindingContext = new AdvisoryMessage();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var message = BindingContext as AdvisoryMessage;
            if(message != null)
            {
                if (message.IsAdviser)
                {
                    ContentFrame.Margin = ImageFrame.Margin = new Thickness(5, 5, 20, 5);
                    ContentFrame.HorizontalOptions = ImageFrame.HorizontalOptions = LayoutOptions.Start;
                    ContentFrame.BackgroundColor = ImageFrame.BackgroundColor = Color.White;
                    CreatedDate.Margin = new Thickness(5, 0, 0, 5);
                    CreatedDate.HorizontalOptions = LayoutOptions.Start;
                }
                
                ContentFrame.IsVisible = !string.IsNullOrEmpty(message.Content);
                ImageFrame.IsVisible = !string.IsNullOrEmpty(message.ImageUrl);
            }
        }
    }
}
