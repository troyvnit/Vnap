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
using Android.Gms.Gcm;
using Android.Util;
using Newtonsoft.Json;
using Vnap.Models;

namespace Vnap.Droid.Services
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
    public class VnapGcmListenerService : GcmListenerService
    {
        public override void OnMessageReceived(string from, Bundle data)
        {
            // Extract the message received from GCM:
            var message = data.GetString("message");
            var messageType = data.GetString("messageType");
            switch (messageType)
            {
                case "advisory":
                    var advisoryMessage = JsonConvert.DeserializeObject<AdvisoryMessage>(message);
                    if (advisoryMessage.ConversationName == App.CurrentUser.UserName && advisoryMessage.IsAdviser)
                    {
                        SendNotification(!string.IsNullOrEmpty(advisoryMessage.Content) ? advisoryMessage.Content : "[Hình ảnh]", "Vnap đã trả lời câu hỏi của bạn!");
                    }
                    break;
                case "article":
                    var article = JsonConvert.DeserializeObject<Article>(message);
                    SendNotification(!string.IsNullOrEmpty(article.Description) ? article.Description : article.Content, article.Title);
                    break;
                default:
                    break;
            }
            Log.Debug("MyGcmListenerService", "From:    " + from);
            Log.Debug("MyGcmListenerService", "Message: " + message);
        }

        // Use Notification Builder to create and launch the notification:
        void SendNotification(string message, string title)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.small_icon)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}