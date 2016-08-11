using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading;
using FormsPlugin.Iconize.Droid;
using Java.Interop;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace Vnap.Droid
{
    [Activity(Label = "Vnap", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        App app;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
            FormsPlugin.Iconize.Droid.IconControls.Init();
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());

            global::Xamarin.Forms.Forms.Init(this, bundle);
            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);
            app = new App(new Androidinitializer());
            LoadApplication(app);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var search = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.search);
            search.QueryTextSubmit += (sender, args) =>
            {
                app.Search(search.Query);
            };
            return base.OnCreateOptionsMenu(menu);
        }
    }

    public class Androidinitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

