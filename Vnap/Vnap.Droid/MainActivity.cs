using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Views;
using Android.Widget;
using FormsPlugin.Iconize.Droid;
using Microsoft.Practices.Unity;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Prism.Unity;
using Vnap.Droid.Services;
using Vnap.Droid.Services.BackgroundServices;
using Vnap.Droid.Utils.IconizeModules;
using Xamarin.Forms;

namespace Vnap.Droid
{
    [Activity(Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private bool subscribed;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
            FormsPlugin.Iconize.Droid.IconControls.Init();
            Plugin.Iconize.Iconize.With(new LinearModule());
            Plugin.Iconize.Iconize.With(new FlatModule());
            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);
            CrossCurrentActivity.Current.Activity = this;
            UserDialogs.Init(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var app = new App(new Androidinitializer());
            LoadApplication(app);

            App.IsPlayServicesAvailable = IsPlayServicesAvailable();
            // Check for Google Play Services on the device:
            if (App.IsPlayServicesAvailable)
            {
                // Start the registration intent service; try to get a token:
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }

            MessagingCenter.Subscribe<NotificationMessage>(this, "NotificationBackgroundService", message =>
            {
                if (!subscribed)
                {
                    var notificationIntent = new Intent(this, typeof(NotificationBackgroundService));
                    StartService(notificationIntent);

                    subscribed = true;
                }
            });
        }

        // Utility method to check for the presence of the Google Play Services APK:
        public bool IsPlayServicesAvailable()
        {
            // These methods are moving to GoogleApiAvailability soon:
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                // Google Play Service check failed - display the error to the user:
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    // Give the user a chance to download the APK:
                    //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    //msgText.Text = "Sorry, this device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var search = FindViewById<EditText>(Resource.Id.search);
            //var searchIcon = (ImageView)search.FindViewById(Resource.Id.search_mag_icon);
            //var viewGroup = (ViewGroup)searchIcon.Parent;
            //viewGroup.RemoveView(searchIcon);
            //viewGroup.AddView(searchIcon);

            //search.QueryTextChange += (sender, args) =>
            //{
            //    if (!string.IsNullOrEmpty(search.Query))
            //    {
            //        if (viewGroup.GetChildAt(viewGroup.ChildCount - 1) != searchIcon)
            //        {
            //            viewGroup.AddView(searchIcon);
            //        }
            //    }
            //    else
            //    {
            //        viewGroup.RemoveView(searchIcon);
            //    }
            //};

            //searchIcon.Click += (o, eventArgs) =>
            //{
            //    app.Search(search.Query);
            //};

            if (search != null)
            {
                search.TextChanged += (sender, args) =>
                {
                    App.SearchKey = search.Text;
                };
            }
            return base.OnCreateOptionsMenu(menu);
        }

        //public override View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        //{

        //    if (name.Contains("SearchView"))
        //    {
        //        var search = parent.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.search);
        //        if (search != null)
        //        {
        //            var searchIcon = (ImageView)search.FindViewById(Resource.Id.search_mag_icon);
        //            var viewGroup = (ViewGroup)searchIcon.Parent;
        //            viewGroup.RemoveView(searchIcon);
        //            viewGroup.AddView(searchIcon);
        //        }
        //    }
        //    return base.OnCreateView(parent, name, context, attrs); ;
        //}
    }

    public class Androidinitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

