using Acr.UserDialogs;
using FormsPlugin.Iconize;
using Vnap.Services;
using Vnap.ViewModels;
using Xamarin.Forms;

namespace Vnap.Views
{
    public partial class MainPage : IconTabbedPage
    {
        IAppService _appService;

        public MainPage(IAppService appService)
        {
            _appService = appService;
            InitializeComponent();
            Children.Add(new PlantListTab());
            Children.Add(new AdvisoryTab());
            //Children.Add(new NoticeListTab());
            Children.Add(new NewsListTab());
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
            var context = BindingContext as MainPageViewModel;
            if (context != null) await context.SyncData();
            base.OnAppearing();
        }
    }
}
