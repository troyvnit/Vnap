using System.ComponentModel;
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Views;
using Vnap.Droid.Renderers;
using Vnap.Models;
using Vnap.Views.Customs;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(MessageViewCell), typeof(MessageViewCellRender))]
namespace Vnap.Droid.Renderers
{
    public class MessageViewCellRender : ViewCellRenderer
    {
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var inflatorservice = (LayoutInflater)Forms.Context.GetSystemService(Android.Content.Context.LayoutInflaterService);
            var textMsgVm = item.BindingContext as Message;
            if (textMsgVm != null)
            {
                if (!string.IsNullOrEmpty((textMsgVm.ImageUrl)))
                {
                    var template = (LinearLayout)inflatorservice.Inflate(!textMsgVm.IsAdviser ? Resource.Layout.image_item_owner : Resource.Layout.image_item_opponent, null, false);
                    template.FindViewById<TextView>(Resource.Id.timestamp).Text = textMsgVm.CreatedDate.ToString("HH:mm");
                    template.FindViewById<TextView>(Resource.Id.nick).Text = !textMsgVm.IsAdviser ? "Tôi:" : textMsgVm.AuthorName + ":";
                    var content = template.FindViewById<TextView>(Resource.Id.message);
                    content.Text = "Đang tải hình ảnh...";
                    var _imageView = template.FindViewById<ImageViewAsync>(Resource.Id.image);
                    ImageService.Instance.LoadUrl(textMsgVm.ImageUrl)
                    .Success((size, loadingResult) =>
                    {
                        content.Text = string.Empty;
                    })
                    .Error(exception =>
                    {
                        content.Text = "Lỗi tải hình!";
                        content.SetTextColor(Color.Red);
                    }).Into(_imageView);
                    return template;
                }
                else
                {
                    var template = (LinearLayout)inflatorservice.Inflate(!textMsgVm.IsAdviser ? Resource.Layout.message_item_owner : Resource.Layout.message_item_opponent, null, false);
                    template.FindViewById<TextView>(Resource.Id.timestamp).Text = textMsgVm.CreatedDate.ToString("HH:mm");
                    template.FindViewById<TextView>(Resource.Id.nick).Text = !textMsgVm.IsAdviser ? "Tôi:" : textMsgVm.AuthorName + ":";
                    template.FindViewById<TextView>(Resource.Id.message).Text = textMsgVm.Content;
                    return template;
                }
            }

            return base.GetCellCore(item, convertView, parent, context);
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
        }


        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);
        }
    }
}