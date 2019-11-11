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

namespace App1.Utils
{
    class NotificationsHelper
    {
        public static void ShowNotification(Activity contexto, string titulo, string mensaje, string link, int codigo)
        {

            contexto.RunOnUiThread(() =>
            {
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                var nBuilder = new Notification.Builder(contexto);
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                nBuilder.SetLargeIcon(ImageHelper.GetImageBitmapFromUrl("http://i.ytimg.com/vi/" + link.Split('=')[1] + "/mqdefault.jpg"));
                nBuilder.SetContentTitle(titulo);
                nBuilder.SetContentText(mensaje);
                nBuilder.SetColor(Android.Graphics.Color.ParseColor("#ce2c2b"));
                nBuilder.SetSmallIcon(Resource.Drawable.list);
                #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                nBuilder.SetSound(Android.Media.RingtoneManager.GetDefaultUri(Android.Media.RingtoneType.Notification));
                #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                Notification notification = nBuilder.Build();
                NotificationManager notificationManager = (NotificationManager)contexto.GetSystemService(Context.NotificationService);
                notificationManager.Notify(5126768, notification);
            });
        }
    }
}