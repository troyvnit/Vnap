using Android.Graphics;
using Android.Widget;
using Vnap.Droid.Renderers;
using Vnap.Droid.Utils.Typeface;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(LabelRender))]
namespace Vnap.Droid.Renderers
{
    public class LabelRender : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            //var textView = (TextView)GetChildAt(0);
            //TypefaceUtil.SetTypeface(textView, e.NewElement.FontAttributes);
        }
    }
}