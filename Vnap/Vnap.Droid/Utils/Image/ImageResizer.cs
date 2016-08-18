using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Vnap.Droid.Utils.Image;
using Vnap.Utils;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResizer))]
namespace Vnap.Droid.Utils.Image
{
    public class ImageResizer : IImageResizer
    {
        async Task<byte[]> IImageResizer.CropResize(byte[] imageData, float width, float height)
        {
            var unscaledBitmap = await DecodeImage(imageData, (int)width, (int)height, ScalingLogic.Crop);
            var scaledBitmap = CreateScaledBitmap(unscaledBitmap, (int)width, (int)height, ScalingLogic.Crop);

            using (var ms = new MemoryStream())
            {
                scaledBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        async Task<byte[]> IImageResizer.FitResize(byte[] imageData, float maxWidth, float maxHeight)
        {
            var unscaledBitmap = await DecodeImage(imageData, (int)maxWidth, (int)maxHeight, ScalingLogic.Fit);
            var scaledBitmap = CreateScaledBitmap(unscaledBitmap, (int)maxWidth, (int)maxHeight, ScalingLogic.Fit);

            using (var ms = new MemoryStream())
            {
                scaledBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        private async Task<Bitmap> DecodeImage(byte[] imageData, int dstWidth, int dstHeight, ScalingLogic scalingLogic)
        {
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InPurgeable = true;
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);
            options.InSampleSize = CalculateSampleSize(options.OutWidth, options.OutHeight, dstWidth, dstHeight, scalingLogic);

            options.InJustDecodeBounds = false;
            Bitmap unscaledBitmap = await BitmapFactory.DecodeByteArrayAsync(imageData, 0, imageData.Length, options);

            return unscaledBitmap;
        }

        private int CalculateSampleSize(int srcWidth, int srcHeight, int dstWidth, int dstHeight, ScalingLogic scalingLogic)
        {
            if (scalingLogic == ScalingLogic.Fit)
            {
                var srcAspect = (float)srcWidth / srcHeight;
                var dstAspect = (float)dstWidth / dstHeight;

                if (srcAspect > dstAspect)
                {
                    return srcWidth / dstWidth;
                }

                return srcHeight / dstHeight;
            }
            else
            {
                var srcAspect = (float)srcWidth / srcHeight;
                var dstAspect = (float)dstWidth / dstHeight;

                if (srcAspect > dstAspect)
                {
                    return srcHeight / dstHeight;
                }

                return srcWidth / dstWidth;
            }
        }

        private Bitmap CreateScaledBitmap(Bitmap originalBitmap, int dstWidth, int dstHeight, ScalingLogic scalingLogic)
        {
            var srcRect = CalculateSrcRect(originalBitmap.Width, originalBitmap.Height, dstWidth, dstHeight, scalingLogic);
            var dstRect = CalculateDstRect(originalBitmap.Width, originalBitmap.Height, dstWidth, dstHeight, scalingLogic);
            var scaledBitmap = Bitmap.CreateBitmap(dstRect.Width(), dstRect.Height(), Bitmap.Config.Argb8888);
            var canvas = new Canvas(scaledBitmap);
            canvas.DrawBitmap(originalBitmap, srcRect, dstRect, new Paint(PaintFlags.FilterBitmap));

            return scaledBitmap;
        }

        private Rect CalculateSrcRect(int srcWidth, int srcHeight, int dstWidth, int dstHeight, ScalingLogic scalingLogic)
        {
            if (scalingLogic == ScalingLogic.Crop)
            {
                var srcAspect = (float)srcWidth / srcHeight;
                var dstAspect = (float)dstWidth / dstHeight;

                if (srcAspect > dstAspect)
                {
                    var srcRectWidth = (int)(srcHeight * dstAspect);
                    var srcRectLeft = (srcWidth - srcRectWidth) / 2;
                    return new Rect(srcRectLeft, 0, srcRectLeft + srcRectWidth, srcHeight);
                }

                var srcRectHeight = (int)(srcWidth / dstAspect);
                var scrRectTop = (srcHeight - srcRectHeight) / 2;
                return new Rect(0, scrRectTop, srcWidth, scrRectTop + srcRectHeight);
            }

            return new Rect(0, 0, srcWidth, srcHeight);
        }

        private Rect CalculateDstRect(int srcWidth, int srcHeight, int dstWidth, int dstHeight, ScalingLogic scalingLogic)
        {
            if (scalingLogic == ScalingLogic.Fit)
            {
                var srcAspect = (float)srcWidth / srcHeight;
                var dstAspect = (float)dstWidth / dstHeight;

                if (srcAspect > dstAspect)
                {
                    return new Rect(0, 0, dstWidth, (int)(dstWidth / srcAspect));
                }

                return new Rect(0, 0, (int)(dstHeight * srcAspect), dstHeight);
            }

            return new Rect(0, 0, dstWidth, dstHeight);
        }

        enum ScalingLogic
        {
            Fit,
            Crop
        }
    }
}
