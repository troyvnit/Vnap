using Vnap.Services;
using Vnap.Droid.Services;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(AppService))]
namespace Vnap.Droid.Services
{
    class AppService : IAppService
    {
        public void CloseApp()
        {
            CrossCurrentActivity.Current.Activity.Finish();
        }
    }
}