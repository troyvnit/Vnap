using System;
using Android.Support.Design.Widget;
using Vnap.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

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
        }
    }
}