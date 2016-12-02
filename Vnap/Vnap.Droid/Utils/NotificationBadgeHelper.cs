using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Vnap.Droid.Utils
{
    /// <summary>
    /// Android does not support notification badge natively
    /// It depends on the manufacture and the custom laucher of the device
    /// Currently, this class supports Samsung and Sony default launchers
    /// </summary>
    /// http://stackoverflow.com/questions/20216806/how-to-add-a-notification-badge-count-to-application-icon-on-sony-xperia-devices
    class NotificationBadgeHelper
    {
        public static void SetBadge(Context context, int count)
        {
            SetBadgeSamsung(context, count);
            SetBadgeSony(context, count);
        }

        public static void ClearBadge(Context context)
        {
            SetBadgeSamsung(context, 0);
            ClearBadgeSony(context);
        }


        private static void SetBadgeSamsung(Context context, int count)
        {
            string launcherClassName = GetLauncherClassName(context);
            if (launcherClassName == null)
            {
                return;
            }
            Intent intent = new Intent("android.intent.action.BADGE_COUNT_UPDATE");
            intent.PutExtra("badge_count", count);
            intent.PutExtra("badge_count_package_name", context.PackageName);
            intent.PutExtra("badge_count_class_name", launcherClassName);
            context.SendBroadcast(intent);
        }

        private static void SetBadgeSony(Context context, int count)
        {
            string launcherClassName = GetLauncherClassName(context);
            if (launcherClassName == null)
            {
                return;
            }

            Intent intent = new Intent();
            intent.SetAction("com.sonyericsson.home.action.UPDATE_BADGE");
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.ACTIVITY_NAME", launcherClassName);
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.SHOW_MESSAGE", true);
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.MESSAGE", count.ToString());
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.PACKAGE_NAME", context.PackageName);

            context.SendBroadcast(intent);
        }


        private static void ClearBadgeSony(Context context)
        {
            string launcherClassName = GetLauncherClassName(context);
            if (launcherClassName == null)
            {
                return;
            }

            Intent intent = new Intent();
            intent.SetAction("com.sonyericsson.home.action.UPDATE_BADGE");
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.ACTIVITY_NAME", launcherClassName);
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.SHOW_MESSAGE", false);
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.MESSAGE", "0");
            intent.PutExtra("com.sonyericsson.home.intent.extra.badge.PACKAGE_NAME", context.PackageName);

            context.SendBroadcast(intent);
        }

        private static string GetLauncherClassName(Context context)
        {

            PackageManager pm = context.PackageManager;

            Intent intent = new Intent(Intent.ActionMain);
            intent.AddCategory(Intent.CategoryLauncher);

            List<ResolveInfo> resolveInfos = pm.QueryIntentActivities(intent, 0).ToList();
            foreach (ResolveInfo resolveInfo in resolveInfos)
            {
                string pkgName = resolveInfo.ActivityInfo.ApplicationInfo.PackageName;
                if (pkgName.Equals(context.PackageName, StringComparison.InvariantCultureIgnoreCase))
                {
                    string className = resolveInfo.ActivityInfo.Name;
                    return className;
                }
            }
            return null;
        }
    }
}