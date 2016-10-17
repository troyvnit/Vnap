using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Vnap.Droid.Renderers;
using Vnap.Droid.Utils.Typeface;
using Vnap.Views.ExtendedControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(BindableTabbedPage), typeof(TabbedPageRender))]
namespace Vnap.Droid.Renderers
{
    public class TabbedPageRender : TabbedPageRenderer
    {
        private TabLayout _tabLayout;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            
            _tabLayout = (TabLayout)GetChildAt(1);

            _tabLayout.TabMode = 0;
        }
    }
}