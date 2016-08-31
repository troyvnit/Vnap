using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Vnap.Droid.Renderers;
using Vnap.Droid.Utils.Typeface;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Vnap.Droid.Renderers
{
    public class TabbedPageRender : TabbedPageRenderer
    {
        private TabLayout _tabLayout;
        private bool _onLayoutFinished;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (_onLayoutFinished)
                return;

            _tabLayout = (TabLayout)GetChildAt(1);
            
            if (_tabLayout.TabCount > 4)
            {
                _tabLayout.TabMode = 0;
            }

            var vg = (ViewGroup)_tabLayout.GetChildAt(0);
            vg.SetPadding(0, 0, 0, 0);
            var tabsCount = vg.ChildCount;
            for (var j = 0; j < tabsCount; j++)
            {
                var vgTab = (ViewGroup)vg.GetChildAt(j);
                var tabChildsCount = vgTab.ChildCount;
                for (var i = 0; i < tabChildsCount; i++)
                {
                    var tabViewChild = vgTab.GetChildAt(i);
                    var textView = tabViewChild as TextView;
                    if (textView != null)
                    {
                        TypefaceUtil.SetTypeface(textView, FontAttributes.Bold);
                    }
                }
            }

            _onLayoutFinished = true;
        }
    }
}