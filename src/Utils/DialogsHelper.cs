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
 public static class DialogsHelper
    {


        public static void ShowAskIfMenuOrExit(Context ctx)
        {
           var context = (Activity)ctx;
            AlertDialog.Builder ad = new AlertDialog.Builder(ctx);
            ad.SetCancelable(false);
            ad.SetTitle("Advertencia");
            ad.SetIcon(Resource.Drawable.alert);
            ad.SetMessage("Que desea hacer");
            ad.SetNegativeButton("Ir a la pantalla de selección de modos", (obj, evt) =>
            {
                context.StartActivity(new Intent(context, typeof(actsplashscreen)));
                MultiHelper.ExecuteGarbageCollection();
                context.Finish();
                if (Mainmenu.gettearinstancia() != null)
                    Mainmenu.gettearinstancia().Finish();
                if (YoutubePlayerServerActivity.gettearinstancia() != null)
                    YoutubePlayerServerActivity.gettearinstancia().Finish();
                if (actividadinicio.gettearinstancia() != null)
                    actividadinicio.gettearinstancia().Finish();

            });
            ad.SetNeutralButton("Salir de la aplicacion", (obj, evt) =>
            {
                context.Finish();
                if (Mainmenu.gettearinstancia() != null)
                    Mainmenu.gettearinstancia().Finish();
                if (YoutubePlayerServerActivity.gettearinstancia() != null)
                    YoutubePlayerServerActivity.gettearinstancia().Finish();
                if (actividadinicio.gettearinstancia() != null)
                    actividadinicio.gettearinstancia().Finish();
            });
            ad.SetPositiveButton("Cancelar", (obj, evt) => { });
            ad.Create();
            ad.Show();
        }
    }
}