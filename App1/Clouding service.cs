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
using Android.Support.V7.AppCompat;
using Android.Support.V4;
using System.Threading;
using System.Net.Sockets;
using Android.Graphics;
using System.Net;
using Android.Support.V13.App;
using Android.Media.Session;
using Android.Media;
namespace App1
{
    [Service(Exported = true)]
    public class Clouding_service : Service, AudioManager.IOnAudioFocusChangeListener
    {

        public bool detenedor = true;
        public string musica = "";
        public IBinder Binder { get; private set; }
        public    Android.Media.MediaPlayer musicaplayer = new Android.Media.MediaPlayer();
        public string linkactual = "";
        public string tituloactual = "";
        public string ipactual = "";
        private AudioManager audioManager;
        public static Clouding_service instance;
        public static Clouding_service gettearinstancia()
        {
            return instance;
        }
      
     
        public void reproducir(string downloadurl)
        {




            // musicaplayer.SetDataSource(downloadurl);

            if (mainmenu_Offline.gettearinstancia() != null) {

         

                musicaplayer.Reset();


                musicaplayer = Android.Media.MediaPlayer.Create(this, Android.Net.Uri.Parse(downloadurl));

                musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
            musicaplayer.SetWakeMode(this, WakeLockFlags.Partial);
            musicaplayer.Start();
            var focusResult = audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);
            if (focusResult != AudioFocusRequest.Granted)
            {
                //could not get audio focus
                Console.WriteLine("Could not get audio focus");
            }
            musicaplayer.Completion += delegate
            {
                if ((musicaplayer.Duration > 5 && musicaplayer.CurrentPosition > 5) && clasesettings.gettearvalor("acaboelplayer").Trim() == "")
                {

                    mainmenu_Offline.gettearinstancia().siguiente();
                }
            };
            mostrarnotificacion();

            }
            else {
                musicaplayer.Reset();
                StopSelf();
            }
        }



        public override void OnTaskRemoved(Intent rootIntent)
        {
           
            musicaplayer.Reset();
            StopSelf();
             base.OnTaskRemoved(rootIntent);

        }
        public override void OnCreate()
        {


            base.OnCreate();

            audioManager = (AudioManager)GetSystemService(AudioService);
            instance = this;
        
        }

        public override IBinder OnBind(Intent intent)
        {
          
            
          
            return this.Binder;
        }

        public override bool OnUnbind(Intent intent)
        {
        
            
            return base.OnUnbind(intent);
        }
        public  void mostrarnotificacion()
        {

            List<PendingIntent> listapending = listapendingintents();
          
      
            RemoteViews contentView = new RemoteViews(PackageName, Resource.Layout.layoutminiplayer);
            if (linkactual.Trim().Length > 1)
            {
                try
                {
                    contentView.SetImageViewBitmap(Resource.Id.imageView1, clasesettings.getRoundedShape(GetImageBitmapFromUrl("https://i.ytimg.com/vi/" + linkactual.Split('=')[1] + "/mqdefault.jpg")));
                    contentView.SetImageViewBitmap(Resource.Id.fondo1, clasesettings.CreateBlurredImageonline(this, 20, linkactual));
                }
                catch (Exception)
                {

                }
            }
            contentView.SetTextViewText(Resource.Id.textView1, tituloactual);
        
            contentView.SetOnClickPendingIntent(Resource.Id.imageView2, listapending[0]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView4, listapending[1]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView3, listapending[2]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView6, listapending[3]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView5, listapending[4]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView1, listapending[5]);
            listapending.Clear();
            var   nBuilder = new Notification.Builder(this);

            nBuilder.SetContent(contentView);
            nBuilder.SetOngoing(true);
           
            nBuilder.SetSmallIcon(Resource.Drawable.play);
       
            Notification notification = nBuilder.Build();
            StartForeground(19248736, notification);
       
        }


        public override void OnDestroy()
        {
          
           
            base.OnDestroy();
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
        public List<PendingIntent> listapendingintents()
        {
         ///   string[] querrys = { "playpause()", "siguiente()", "anterior()", "adelantar()", "atrazar()" };
         ///   
               List<PendingIntent> listapending = new List<PendingIntent>();
            Random brandom = new Random();
                /////1

                Intent internado = new Intent(this, typeof(serviciointerpreter3));
                internado.PutExtra("querry1","si");
            internado.PutExtra("querry2","");
            internado.PutExtra("querry3","");
            internado.PutExtra("querry4","");
            internado.PutExtra("querry5", "");
         
            var pendingIntent = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado,0);
                listapending.Add(pendingIntent);
            /////2
            Intent internado2 = new Intent(this, typeof(serviciointerpreter3));
            internado2.PutExtra("querry1", "");
            internado2.PutExtra("querry2", "si");
            internado2.PutExtra("querry3", "");
            internado2.PutExtra("querry4", "");
            internado2.PutExtra("querry5", "");

            var pendingIntent2 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado2, 0);
            listapending.Add(pendingIntent2);
            /////3
            Intent internado3 = new Intent(this, typeof(serviciointerpreter3));
            internado3.PutExtra("querry1", "");
            internado3.PutExtra("querry2", "");
            internado3.PutExtra("querry3", "si");
            internado3.PutExtra("querry4", "");
            internado3.PutExtra("querry5", "");

            var pendingIntent3 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado3, 0);
            listapending.Add(pendingIntent3);

            /////4
            Intent internado4 = new Intent(this, typeof(serviciointerpreter3));
            internado4.PutExtra("querry1", "");
            internado4.PutExtra("querry2", "");
            internado4.PutExtra("querry3", "");
            internado4.PutExtra("querry4", "si");
            internado4.PutExtra("querry5", "");

            var pendingIntent4 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado4, 0);
            listapending.Add(pendingIntent4);

            /////5
            Intent internado5 = new Intent(this, typeof(serviciointerpreter3));
            internado5.PutExtra("querry1", "");
            internado5.PutExtra("querry2", "");
            internado5.PutExtra("querry3", "");
            internado5.PutExtra("querry4", "");
            internado5.PutExtra("querry5", "si");

            var pendingIntent5= PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado5, 0);
            listapending.Add(pendingIntent5);
            /////6
            Intent internado6 = new Intent(this, typeof(mainmenu_Offline));

            var pendingIntent6 = PendingIntent.GetActivity(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado6, PendingIntentFlags.UpdateCurrent);
            listapending.Add(pendingIntent6);



            return listapending;
        }
        public void OnAudioFocusChange([GeneratedEnum] AudioFocus focusChange)
        {

            switch (focusChange)
            {
                case AudioFocus.Gain:
                    musicaplayer.SetVolume(1.0f, 1.0f);//Turn it up!
                    break;
                case AudioFocus.Loss:
                    //We have lost focus stop!
                    musicaplayer.SetVolume(0f, 0f);//turn it down!
                    break;
                case AudioFocus.LossTransient:
                    //We have lost focus for a short time, but likely to resume so pause
                    musicaplayer.SetVolume(0f, 0f);
                    break;
                case AudioFocus.LossTransientCanDuck:
                    //We have lost focus but should till play at a muted 10% volume
                    if (musicaplayer.IsPlaying)
                        musicaplayer.SetVolume(.1f, .1f);//turn it down!
                    break;

            }


           
        }

    }
}