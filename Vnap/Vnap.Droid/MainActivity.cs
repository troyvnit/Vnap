using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using FormsPlugin.Iconize.Droid;
using Microsoft.Practices.Unity;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Prism.Unity;
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

