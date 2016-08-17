using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Xamarin.Forms;
using View = Android.Views.View;

namespace Vnap.Droid.Utils.Typeface
{
    public static class TypefaceUtil
    {
        public static void SetTypeface(TextView textView, FontAttributes fontAttributes = FontAttributes.None)
        {
            var fontFilePath = "Fonts/";
            switch (fontAttributes)
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
            var font = Android.Graphics.Typeface.CreateFromAsset(Forms.Context.Assets, fontFilePath);
            textView.Typeface = font;
        }
    }
}