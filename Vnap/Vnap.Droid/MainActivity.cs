using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using FFImageLoading;
using FormsPlugin.Iconize.Droid;
using Java.Interop;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Vnap.Droid.Utils.IconizeModules;

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
            Plugin.Iconize.Iconize.With(new LinearModule());
            Plugin.Iconize.Iconize.With(new FlatModule());
            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            app = new App(new Androidinitializer());
            LoadApplication(app);
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

            search.KeyPress += (sender, args) =>
            {
                args.Handled = false;
                if (args.Event.Action == KeyEventActions.Down && args.KeyCode == Keycode.Enter)
                {
                    app.Search(search.Text);
                    args.Handled = true;
                }
            };
            return base.OnCreateOptionsMenu(menu);
        }

        public override View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        {

            if (name.Contains("SearchView"))
            {
                var search = parent.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.search);
                if (search != null)
                {
                    var searchIcon = (ImageView)search.FindViewById(Resource.Id.search_mag_icon);
                    var viewGroup = (ViewGroup)searchIcon.Parent;
                    viewGroup.RemoveView(searchIcon);
                    viewGroup.AddView(searchIcon);
                }
            }
            return base.OnCreateView(parent, name, context, attrs); ;
        }
    }

    public class Androidinitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

