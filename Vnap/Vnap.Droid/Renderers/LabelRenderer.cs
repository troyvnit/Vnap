using Android.Graphics;
using Android.Widget;
using Vnap.Droid.Renderers;
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
            var label = (TextView)GetChildAt(0);
            var fontFilePath = "Fonts/";
            switch (e.NewElement.FontAttributes)
            {
                case FontAttributes.Bold:
                    fontFilePath += "OpenSans-Bold.ttf";
                    break;
                case FontAttributes.Italic:
                    fontFilePath += "OpenSans-Italic.ttf";
                    break;
                default:
                    fontFilePath += "OpenSans-Regular.ttf";
                    break;
            }
            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, fontFilePath);
            label.Typeface = font;
        }
    }
}