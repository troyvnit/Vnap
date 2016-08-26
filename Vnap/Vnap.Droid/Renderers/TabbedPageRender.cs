using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using FormsPlugin.Iconize.Droid;
using Plugin.Iconize.Droid.Controls;
using Vnap.Droid.Renderers;
using Vnap.Droid.Utils.Typeface;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Vnap.Droid.Renderers
{
    public class TabbedPageRender : TabbedPageRenderer
    {
        private readonly List<String> _icons = new List<String>();
        private TabLayout _tabLayout;
        private bool _onLayoutFinished;

        protected override void OnAttachedToWindow()
        {
            UpdateTabbedIcons(Context);

            base.OnAttachedToWindow();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            _icons.Clear();
            if (e.NewElement != null)
            {
                foreach (var page in e.NewElement.Children)
                {
                    if (page.Icon != null)
                    {
                        _icons.Add(page.Icon.File);
                        page.Icon = null;
                    }
                }
            }

            base.OnElementChanged(e);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (_onLayoutFinished)
                return;

            _tabLayout = (TabLayout)GetChildAt(1);
            if (_icons.Count > 0 || _tabLayout.TabCount <= 4)
            {
                _tabLayout.TabMode = 1;
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


        private void UpdateTabbedIcons(Context context)
        {
            var tabLayout = FindViewById<TabLayout>(IconControls.TabLayoutId);
            if (tabLayout == null || tabLayout.TabCount == 0 || _icons.Count == 0)
                return;

            for (var i = 0; i < tabLayout.TabCount; i++)
            {
                var tab = tabLayout.GetTabAt(i);

                var icon = Plugin.Iconize.Iconize.FindIconForKey(_icons[i]);
                if (icon != null)
                {
                    var drawable = new IconDrawable(context, icon).Color(Color.White.ToAndroid()).SizeDp(20);
                    tab.SetIcon(drawable);
                }
                else
                {
                    var drawable = Resources.GetDrawable(_icons[i]);
                    tab.SetIcon(drawable);
                }
            }
        }
    }
}