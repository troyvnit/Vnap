using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as SplashScreenViewModel;
            if (context != null) await context.SyncData();
            base.OnAppearing();
        }

        private async void City_OnFocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry?.Unfocus();
            var context = BindingContext as SplashScreenViewModel;
            if (context != null) await context.ExecuteOpenCitiesPopupCommand();
        }

        private async void Plant_OnFocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            entry?.Unfocus();
            var context = BindingContext as SplashScreenViewModel;
            if (context != null) await context.ExecuteOpenPlantsPopupCommand();
        }
    }
}
