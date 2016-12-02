using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Vnap.Droid.Services;
using Vnap.Droid.Utils;
using Vnap.Services;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationService))]
namespace Vnap.Droid.Services
{
    class NotificationService : INotificationService
    {
        //public void RegisterDevice()
        //{
        //    if (IsPlayServicesAvailable())
        //    {
        //        var intent = new Intent(Application.Context, typeof(GcmRegistrationIntentService));
        //        Application.Context.StartService(intent);
        //    }
        //}

        //public async Task UnregisterDevice()
        //{
        //    var deviceId = Common.Helpers.Settings.GetDeviceId();
        //    Common.Helpers.Settings.SetDeviceId(null);
        //    if (!string.IsNullOrWhiteSpace(deviceId))
        //    {
        //        var accountService = Bootstrapper.GetCurrentContainer().Resolve<IAccountService>();
        //        await accountService.UnregisterAndroidDevice(deviceId);
        //    }
        //}

        public void Notify(string title, string content, int badge, string parameter = null)
        {
            // Set up an intent so that tapping the notifications returns to this app
            var startupIntent = new Intent(Application.Context, typeof(MainActivity));
            if (!string.IsNullOrEmpty(parameter))
                startupIntent.PutExtra("parameter", parameter);

            // create pending intent this way does not call MainActivity code
            //var pendingIntent = PendingIntent.GetActivity(Application.Context, 0, startupIntent, PendingIntentFlags.OneShot);

            // create pending intent this way calls MainActivity code so we can pass parameter to Xamarin Forms App
            var stackBuilder = TaskStackBuilder.Create(Application.Context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(startupIntent);
            PendingIntent pendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.OneShot);

            // Build notification
            var notification = new Notification.Builder(Application.Context)
                .SetAutoCancel(true)
                .SetContentTitle(title)
                .SetContentText(content)
                .SetContentIntent(pendingIntent)
                .SetPriority((int)NotificationPriority.High)
                .SetDefaults(NotificationDefaults.All)
                .SetSmallIcon(Icon.CreateWithResource(Application.Context, Resource.Drawable.small_icon))
                .SetColor(Color.Rgb(40,155,71))
                .Build();

            // Notify
            var notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            var random = new Random(); // random notification ID for each notification
            notificationManager.Notify(random.Next(0, 9999), notification);

            SetBadge(badge);
        }

        public void SetBadge(int count)
        {
            NotificationBadgeHelper.SetBadge(Application.Context, count);
        }

        public void ClearBadge()
        {
            NotificationBadgeHelper.ClearBadge(Application.Context);
        }

        //public bool IsPlayServicesAvailable()
        //{
        //    var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Application.Context);
        //    if (resultCode != ConnectionResult.Success)
        //    {
        //        if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
        //        {
        //            Acr.UserDialogs.UserDialogs.Instance.Alert(GoogleApiAvailability.Instance.GetErrorString(resultCode), "Play Service Error");
        //            //new AlertDialog.Builder(Application.Context)
        //            //    .SetMessage(GoogleApiAvailability.Instance.GetErrorString(resultCode))
        //            //    .SetTitle("Error")
        //            //    .Show();
        //        }
        //        else
        //        {
        //            Acr.UserDialogs.UserDialogs.Instance.Alert("Sorry, this device is not supported", "Play Service Error");

        //            //new AlertDialog.Builder(Application.Context)
        //            //    .SetMessage("Sorry, this device is not supported")
        //            //    .SetTitle("Error")
        //            //    .Show();

        //            //Finish();
        //        }

        //        return false;
        //    }

        //    return true;
        //}
    }
}