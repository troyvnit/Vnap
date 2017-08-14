using Acr.UserDialogs;
using Vnap.Services;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class TermsPage : ContentPage
    {
        IAppService _appService;

        public TermsPage(IAppService appService)
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
    }
}
