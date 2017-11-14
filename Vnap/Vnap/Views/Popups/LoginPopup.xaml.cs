using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Vnap.Views.Popups
{
    public partial class LoginPopup : PopupPage
    {
        public LoginPopup()
        {
            InitializeComponent();
        }

        private async void City_OnFocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry?.Unfocus();
            var citiesPopup = new CitiesPopup();
            citiesPopup.BindingContext = this;
            await PopupNavigation.PushAsync(citiesPopup);
        }

        private async void Plant_OnFocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry?.Unfocus();
            var plantsPopup = new PlantsPopup();
            plantsPopup.BindingContext = this;
            await PopupNavigation.PushAsync(plantsPopup);
        }
    }
}
