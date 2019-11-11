using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Utils
{
    public class UiHelper
    {

        public static void SetBackgroundAndRefresh(Activity context)
        {
            bool isOnline = false;
            if (MainmenuOffline.gettearinstancia() != null)
            {
                isOnline = false;
            }
            else
            if (Mainmenu.gettearinstancia() != null)
            {
                isOnline = true;
            }
            var thread = new Thread(() =>
            {

                KeepRefreshingBackground(isOnline ? "online" : "", context);
            });
            thread.IsBackground = true;
            thread.Start();
        }

        public static void KeepRefreshingBackground(string onlineoofline, Activity instancia)
        {
            ImageView fondin = instancia.FindViewById<ImageView>(Resource.Id.fondo1);
            if (onlineoofline == "online")
            {

                instancia.RunOnUiThread(() =>
                {

                    fondin.SetBackgroundColor(Android.Graphics.Color.ParseColor("#353535"));



                });
                MultiHelper.ExecuteGarbageCollection();
            }
            else
            {
                instancia.RunOnUiThread(() =>
                {
                    fondin.SetBackgroundColor(Android.Graphics.Color.ParseColor("#353535"));

                });
                MultiHelper.ExecuteGarbageCollection();

            }
        }
        public static void SetInmersiveMode(Window window, bool isInmersive)
        {
            int uiOptions = 0;
            uiOptions = (int)window.DecorView.SystemUiVisibility;
            if (!isInmersive)
            {
                uiOptions |= (int)SystemUiFlags.LowProfile;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;
            }
            else
            {
                uiOptions &= (int)SystemUiFlags.LowProfile;
                uiOptions &= (int)SystemUiFlags.Fullscreen;
                uiOptions &= (int)SystemUiFlags.HideNavigation;
                uiOptions &= (int)SystemUiFlags.ImmersiveSticky;

            }
            window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        }
    }
}