using Acr.UserDialogs;
using Vnap.Services;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class SplashScreen : ContentPage
    {
        IAppService _appService;

        public SplashScreen(IAppService appService)
        {
            _appService = appService;
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await UserDialogs.Instance.ConfirmAsync("Bạn có chắc muốn thoát ứng dụng?", "Thoát", "Thoát", "Ở lại"))
                {
                    _appService.CloseApp();
                }
            });

            return true;
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
