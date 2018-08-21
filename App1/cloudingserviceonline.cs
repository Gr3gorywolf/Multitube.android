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
                if (clasesettings.gettearvalor("servicio") == "matar")
                {
                    clasesettings.guardarsetting("servicio", "");
                  activo = false;
                    StopSelf();

                }
                if (clasesettings.gettearvalor("cquerry").Trim() != "")
                {
                    string qvalue = clasesettings.gettearvalor("cquerry").Trim();
                    clasesettings.guardarsetting("cquerry", "");
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
                    contentView.SetImageViewBitmap(Resource.Id.imageView1,clasesettings.getRoundedShape( GetImageBitmapFromUrl(linkactual)));
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

            var nBuilder = new Notification.Builder(this);

            nBuilder.SetContent(contentView);
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
            Intent internado6 = new Intent(this, typeof(mainmenu));

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