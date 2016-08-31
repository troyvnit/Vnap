using Android.Graphics.Drawables;
using Vnap.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRender))]
namespace Vnap.Droid.Renderers
{
    public class EntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;
                nativeEditText.SetBackgroundColor(Color.Transparent.ToAndroid());
                nativeEditText.SetHintTextColor(Color.FromRgba(255, 255, 255, 0.5).ToAndroid());
            }
        }
    }
}