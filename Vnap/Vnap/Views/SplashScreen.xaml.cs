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
            if (context != null) await context.InitialDatabase();
            base.OnAppearing();
        }
    }
}
