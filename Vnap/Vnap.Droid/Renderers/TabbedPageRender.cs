using System;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Vnap.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Vnap.Droid.Renderers
{
    public class TabbedPageRender : TabbedPageRenderer
    {
        private TabLayout _tabLayout;

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            _tabLayout = (TabLayout)GetChildAt(1);

            if (_tabLayout.TabCount > 3)
            {
                _tabLayout.TabMode = 0;
            }

            var fontFilePath = "Fonts/OpenSans-Bold.ttf";
            var font = Typeface.CreateFromAsset(Forms.Context.Assets, fontFilePath);
            var vg = (ViewGroup)_tabLayout.GetChildAt(0);
            var tabsCount = vg.ChildCount;
            for (var j = 0; j < tabsCount; j++)
            {
                var vgTab = (ViewGroup)vg.GetChildAt(j);
                var tabChildsCount = vgTab.ChildCount;
                for (var i = 0; i < tabChildsCount; i++)
                {
                    var tabViewChild = vgTab.GetChildAt(i);
                    var view = tabViewChild as TextView;
                    if (view != null)
                    {
                        view.Typeface = font;
                    }
                }
            }
        }
    }
}