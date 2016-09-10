using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Vnap.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebView), typeof(WebViewRender))]
namespace Vnap.Droid.Renderers
{
    public class WebViewRender : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.Touch -= Control_Touch;
                Control.ScrollChange -= ControlOnScrollChange;
            }

            if (e.NewElement != null)
            {
                Control.Touch += Control_Touch;
                Control.ScrollChange += ControlOnScrollChange;
            }
        }

        private void ControlOnScrollChange(object sender, ScrollChangeEventArgs scrollChangeEventArgs)
        {
            if (scrollChangeEventArgs.ScrollY == 0 || (scrollChangeEventArgs.ScrollY > scrollChangeEventArgs.OldScrollY && scrollChangeEventArgs.OldScrollY == 0))
            {
                Control.Parent.RequestDisallowInterceptTouchEvent(false);
            }
            else
            {
                Control.Parent.RequestDisallowInterceptTouchEvent(true);
            }
        }

        void Control_Touch(object sender, Android.Views.View.TouchEventArgs e)
        {
            // Executing this will prevent the Scrolling to be intercepted by parent views
            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    Control.Parent.RequestDisallowInterceptTouchEvent(true);
                    break;
                case MotionEventActions.Up:
                    Control.Parent.RequestDisallowInterceptTouchEvent(false);
                    break;
            }
            // Calling this will allow the scrolling event to be executed in the WebView
            Control.OnTouchEvent(e.Event);
        }
    }
}