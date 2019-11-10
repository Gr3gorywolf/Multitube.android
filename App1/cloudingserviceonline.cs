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
using Android.Graphics;
using System.Net;
using System.Threading;
using App1.Utils;

namespace App1
{
    [Service(Exported = true)]
    public class cloudingserviceonline : Service
    {
        public IBinder Binder { get; private set; }
        public bool activo = true;
      public  string tituloactual = "";
        public string linkactual = "";
       public string ipactual = "";
      public static  cloudingserviceonline instancia;
        public static cloudingserviceonline gettearinstancia()
        {
            return instancia;
        }
        public void receiver()
        {
            while (activo)
            {
                if (SettingsHelper.GetSetting("servicio") == "matar")
                {
                    SettingsHelper.SaveSetting("servicio", "");
                  activo = false;
                    StopSelf();

                }
                if (SettingsHelper.GetSetting("cquerry").Trim() != "")
                {
                    string qvalue = SettingsHelper.GetSetting("cquerry").Trim();
                    SettingsHelper.SaveSetting("cquerry", "");
                    if (qvalue.StartsWith("data()"))
                    {
                        linkactual = (qvalue.Split('>')[2]);
                        tituloactual = (qvalue.Split('>')[1]);
                        ipactual = (qvalue.Split('>')[3]);
                        if (linkactual.Trim().Length > 1 && tituloactual.Trim().Length > 1)
                        {
                            mostrarnotificacion();
                        }
                      
                    }
                }
                Thread.Sleep(1500);
            }
        }
        public void mostrarnotificacion()
        {

            var listapending = listapendingintents();


            RemoteViews contentView = new RemoteViews(PackageName, Resource.Layout.layoutminiplayeronline);
            if (linkactual.Trim().Length > 1)
            {
                try
                {
                    contentView.SetImageViewBitmap(Resource.Id.imageView1, ImageHelper.GetRoundedShape( GetImageBitmapFromUrl(linkactual)));
                    contentView.SetImageViewBitmap(Resource.Id.fondo1, clasesettings.CreateBlurredImageonline(this, 20, linkactual));
                }
                catch (Exception)
                {

                }

            }
            contentView.SetOnClickPendingIntent(Resource.Id.imageView1, listapending[5]);
            contentView.SetTextViewText(Resource.Id.textView1, tituloactual);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView2, listapending[0]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView4, listapending[1]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView3, listapending[2]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView6, listapending[3]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView5, listapending[4]);


            /*

            1-playpause
            2-siguiente
            3-anterior
            4-adelantar
            5-atrazar

            */

            Notification.Action accion1 = new Notification.Action(Resource.Drawable.playpause, "Playpause", listapending[0]);
            Notification.Action accion2 = new Notification.Action(Resource.Drawable.skipnext, "Siguiente", listapending[1]);
            Notification.Action accion3 = new Notification.Action(Resource.Drawable.skipprevious, "Anterior", listapending[2]);
            Notification.Action accion4 = new Notification.Action(Resource.Drawable.skipforward, "adelantar", listapending[3]);
            Notification.Action accion5 = new Notification.Action(Resource.Drawable.skipbackward, "atrazar", listapending[4]);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            var nBuilder = new Notification.Builder(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            Notification.MediaStyle estilo = new Notification.MediaStyle();
            if (Mainmenu.gettearinstancia() != null)
            {
              //  estilo.SetMediaSession(mainmenu.gettearinstancia().mSession.SessionToken);

                estilo.SetShowActionsInCompactView(1, 2, 3);

            }
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
#pragma warning disable 414
                try {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    nBuilder.SetContent(contentView);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                }
                catch (Exception) { 
                    }
            
#pragma warning restore 414
            }
            else
            {


                nBuilder.SetStyle(estilo);
                nBuilder.SetLargeIcon(clasesettings.GetImageBitmapFromUrl(linkactual));
                nBuilder.SetContentTitle(tituloactual);
                nBuilder.SetContentText("Desde: "+Mainmenu.gettearinstancia().devicename);
                nBuilder.AddAction(accion5);
                nBuilder.AddAction(accion3);
                nBuilder.AddAction(accion1);
                nBuilder.AddAction(accion2);
                nBuilder.AddAction(accion4);
                nBuilder.SetContentIntent(listapending[5]);
                nBuilder.SetColor(Android.Graphics.Color.ParseColor("#ce2c2b"));
            }
            nBuilder.SetOngoing(true);

            nBuilder.SetSmallIcon(Resource.Drawable.play);

            Notification notification = nBuilder.Build();
            StartForeground(19248736, notification);

        }
        public override void OnTaskRemoved(Intent rootIntent)
        {
            base.OnTaskRemoved(rootIntent);
            activo = false;
            
            StopSelf();

        }
        private Bitmap GetImageBitmapFromUrl(string url)
        {

            Bitmap imageBitmap = null;
            try
            {


                if (url.Trim() != "")
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

                        }

                    }

            }
            catch (Exception) { }
            return imageBitmap;
        }
        public override void OnCreate()
        {

           /* new Thread(() => {

                receiver();

            }).Start();*/
            instancia = this;
            base.OnCreate();
           
        }
        public List<PendingIntent> listapendingintents()
        {
            ///   string[] querrys = { "playpause()", "siguiente()", "anterior()", "adelantar()", "atrazar()" };
            ///   
            List<PendingIntent> listapending = new List<PendingIntent>();
            Random brandom = new Random();
            /////1

            Intent internado = new Intent(this, typeof(serviciointerpreter2));
            internado.PutExtra("ipadre", ipactual);
            internado.PutExtra("querry1", "si");
            internado.PutExtra("querry2", "");
            internado.PutExtra("querry3", "");
            internado.PutExtra("querry4", "");
            internado.PutExtra("querry5", "");

            var pendingIntent = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado, 0);
            listapending.Add(pendingIntent);
            /////2
            Intent internado2 = new Intent(this, typeof(serviciointerpreter2));
            internado2.PutExtra("ipadre", ipactual);
            internado2.PutExtra("querry1", "");
            internado2.PutExtra("querry2", "si");
            internado2.PutExtra("querry3", "");
            internado2.PutExtra("querry4", "");
            internado2.PutExtra("querry5", "");

            var pendingIntent2 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado2, 0);
            listapending.Add(pendingIntent2);
            /////3
            Intent internado3 = new Intent(this, typeof(serviciointerpreter2));
            internado3.PutExtra("ipadre", ipactual);
            internado3.PutExtra("querry1", "");
            internado3.PutExtra("querry2", "");
            internado3.PutExtra("querry3", "si");
            internado3.PutExtra("querry4", "");
            internado3.PutExtra("querry5", "");

            var pendingIntent3 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado3, 0);
            listapending.Add(pendingIntent3);

            /////4
            Intent internado4 = new Intent(this, typeof(serviciointerpreter2));
            internado4.PutExtra("ipadre", ipactual);
            internado4.PutExtra("querry1", "");
            internado4.PutExtra("querry2", "");
            internado4.PutExtra("querry3", "");
            internado4.PutExtra("querry4", "si");
            internado4.PutExtra("querry5", "");

            var pendingIntent4 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado4, 0);
            listapending.Add(pendingIntent4);

            /////5
            Intent internado5 = new Intent(this, typeof(serviciointerpreter2));
            internado5.PutExtra("ipadre", ipactual);
            internado5.PutExtra("querry1", "");
            internado5.PutExtra("querry2", "");
            internado5.PutExtra("querry3", "");
            internado5.PutExtra("querry4", "");
            internado5.PutExtra("querry5", "si");

            var pendingIntent5 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado5, 0);
            listapending.Add(pendingIntent5);
            /////6
            Intent internado6 = new Intent(this, typeof(Mainmenu));

            var pendingIntent6 = PendingIntent.GetActivity(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado6, PendingIntentFlags.UpdateCurrent);
            listapending.Add(pendingIntent6);



            return listapending;
        }
        public override IBinder OnBind(Intent intent)
        {



            return this.Binder;
        }

    }
}