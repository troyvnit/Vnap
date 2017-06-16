using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Widget;
using System.Threading.Tasks;
using Android.Graphics;
using Android.App;
using Android.Views;
using Widget = Android.Support.Design.Widget;
using Android.Content.Res;
using Android.Support.V4.View;
using Vnap.Droid.Renderers;
using Vnap.Views.ExtendedControls;

[assembly: ExportRenderer(typeof(ExtendedFloatingActionButton), typeof(FloatingActionButtonRender))]
namespace Vnap.Droid.Renderers
{
    public class FloatingActionButtonRender : ViewRenderer<ExtendedFloatingActionButton, Widget.FloatingActionButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ExtendedFloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                this.ViewGroup.SetClipChildren(false);
                this.ViewGroup.SetClipToPadding(false);
                this.UpdateControlForSize();

                this.UpdateStyle();
            }

            if (e.NewElement != null)
            {
                this.Control.Click += Fab_Click;
            }
            else if (e.OldElement != null)
            {
                this.Control.Click -= Fab_Click;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ExtendedFloatingActionButton.SizeProperty.PropertyName)
            {
                this.UpdateControlForSize();
            }
            else if (e.PropertyName == ExtendedFloatingActionButton.NormalColorProperty.PropertyName ||
                     e.PropertyName == ExtendedFloatingActionButton.DisabledColorProperty.PropertyName)
            {
                this.SetBackgroundColors();
            }
            else if (e.PropertyName == ExtendedFloatingActionButton.HasShadowProperty.PropertyName)
            {
                this.SetHasShadow();
            }
            else if (e.PropertyName == ExtendedFloatingActionButton.SourceProperty.PropertyName)
            {
                this.SetImage();
            }
            else if (e.PropertyName == ExtendedFloatingActionButton.IsEnabledProperty.PropertyName)
            {
                this.UpdateEnabled();
            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Control.Click -= this.Fab_Click;
            }

            base.Dispose(disposing);
        }

        private void UpdateControlForSize()
        {
            LayoutInflater inflater = (LayoutInflater)this.Context.GetSystemService(Android.Content.Context.LayoutInflaterService);

            Widget.FloatingActionButton fab = (Widget.FloatingActionButton)inflater.Inflate(FAB.Droid.Resource.Layout.mini_fab, null);

            //if (this.Element.Size == FabSize.Mini)
            //{
            //    fab = (Widget.FloatingActionButton)inflater.Inflate(FAB.Droid.Resource.Layout.mini_fab, null);
            //}
            //else // then normal
            //{
            //    fab = (Widget.FloatingActionButton)inflater.Inflate(FAB.Droid.Resource.Layout.normal_fab, null);
            //}

            this.SetNativeControl(fab);
            this.UpdateStyle();
        }

        private void UpdateStyle()
        {
            this.SetBackgroundColors();

            this.SetHasShadow();

            this.SetImage();

            this.UpdateEnabled();
        }

        private void SetBackgroundColors()
        {
            this.Control.BackgroundTintList = ColorStateList.ValueOf(this.Element.NormalColor.ToAndroid());
            try
            {
                this.Control.RippleColor = this.Element.RippleColor.ToAndroid();
            }
            catch (MissingMethodException)
            {
                // ignore
            }
        }

        private void SetHasShadow()
        {
            try
            {
                if (this.Element.HasShadow)
                {
                    ViewCompat.SetElevation(this.Control, 20);
                }
                else
                {
                    ViewCompat.SetElevation(this.Control, 0);
                }
            }
            catch { }
        }

        private void SetImage()
        {
            Task.Run(async () =>
            {
                var bitmap = await this.GetBitmapAsync(this.Element.Source);

                (this.Context as Activity).RunOnUiThread(() =>
                {
                    this.Control?.SetImageBitmap(bitmap);
                });
            });
        }

        private void UpdateEnabled()
        {
            this.Control.Enabled = this.Element.IsEnabled;

            if (this.Control.Enabled == false)
            {
                this.Control.BackgroundTintList = ColorStateList.ValueOf(this.Element.DisabledColor.ToAndroid());
            }
            else
            {
                this.UpdateBackgroundColor();
            }
        }

        private async Task<Bitmap> GetBitmapAsync(ImageSource source)
        {
            var handler = GetHandler(source);
            var returnValue = (Bitmap)null;

            returnValue = await handler.LoadImageAsync(source, this.Context);

            return returnValue;
        }

        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Element.SendClicked();
        }
    }
}