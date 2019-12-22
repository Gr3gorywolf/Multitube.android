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
using App1.Utils;

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
          
            if (YoutubePlayerServerActivity.gettearinstancia() != null) {



                musicaplayer.Release();


                musicaplayer = new MediaPlayer();
              
                if ( Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.N) {
                    musicaplayer.SetAudioAttributes(new AudioAttributes.Builder().SetUsage(AudioUsageKind.Media).SetContentType(AudioContentType.Music).Build());
                }
                else {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                }
                musicaplayer.SetWakeMode(this, WakeLockFlags.Partial);
         
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                var focusResult = audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
           //    musicaplayer.SetVideoScalingMode(VideoScalingMode.ScaleToFitWithCropping);
               
                if (focusResult != AudioFocusRequest.Granted)
            {
                //could not get audio focus
                Console.WriteLine("Could not get audio focus");
            }
               
     
                musicaplayer.Error += (aa, aaaa) =>
                {
                    Console.WriteLine("klk aw aw aw");

                };

               musicaplayer.Info += (aa, aaa) =>
                {
                    var instancia = YoutubePlayerServerActivity.gettearinstancia();
                    if (instancia != null)
                    {
                        switch (aaa.What)
                        {
                            case MediaInfo.BufferingStart:
                                if (instancia.prgBuffering.Visibility != ViewStates.Visible)
                                    instancia.prgBuffering.Visibility = ViewStates.Visible;
                                break;
                            case MediaInfo.BufferingEnd:
                                if (instancia.prgBuffering.Visibility != ViewStates.Gone)
                                    instancia.prgBuffering.Visibility = ViewStates.Gone;
                                break;
                            case MediaInfo.VideoRenderingStart:
                                if (instancia.prgBuffering.Visibility != ViewStates.Gone)
                                    instancia.prgBuffering.Visibility = ViewStates.Gone;
                                break;

                        };
                    }
                };
               
           
               
                musicaplayer.Prepared += delegate
                {
                    if (YoutubePlayerServerActivity.gettearinstancia().videoon)
                    {
                        YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                        {
                            try
                            {
                                musicaplayer.SetDisplay(null);
                                musicaplayer.SetDisplay(YoutubePlayerServerActivity.gettearinstancia().videoSurfaceHolder);
                            }
                            catch (Exception) {

                            }

                       
                            YoutubePlayerServerActivity.gettearinstancia().SetVideoSize();
                        });
                    }
                        musicaplayer.Start();
                    if (YoutubePlayerServerActivity.gettearinstancia().qualitychanged)
                    {
                        try
                        {
                         YoutubePlayerServerActivity.gettearinstancia().qualitychanged = false;
                         musicaplayer.SeekTo(YoutubePlayerServerActivity.gettearinstancia().previousprogress);
                        }
                        catch (Exception) { }
                    }
                };
            musicaplayer.Completion += delegate
            {

               
                if ((musicaplayer.Duration > 5 && musicaplayer.CurrentPosition > 5))
                {
                    new Thread(() =>
                    {
                        YoutubePlayerServerActivity.gettearinstancia().NextVideo();
                    }).Start();
                }
            };
                

            mostrarnotificacion();
                musicaplayer.SetDataSource(this, Android.Net.Uri.Parse(downloadurl));
                musicaplayer.PrepareAsync();
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
            MultiHelper.ExecuteGarbageCollection();
             base.OnTaskRemoved(rootIntent);

        }
        public override void OnCreate()
        {
            if (YoutubePlayerServerActivity.gettearinstancia() != null) { 
                 YoutubePlayerServerActivity.gettearinstancia().videoSurfaceHolder.AddCallback(YoutubePlayerServerActivity.gettearinstancia());
       
            }
            audioManager = (AudioManager)GetSystemService(AudioService);
            instance = this;
            base.OnCreate();

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
                    contentView.SetImageViewBitmap(Resource.Id.imageView1, ImageHelper.GetRoundedShape(GetImageBitmapFromUrl("https://i.ytimg.com/vi/" + linkactual.Split('=')[1] + "/mqdefault.jpg")));
                    contentView.SetImageViewBitmap(Resource.Id.fondo1, ImageHelper.CreateBlurredImageFromUrl(this, 20, linkactual));
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

            Notification.Action accion1 = new Notification.Action(Resource.Drawable.playpause, "Playpause", listapending[0]);
            Notification.Action accion2 = new Notification.Action(Resource.Drawable.skipnext, "Siguiente", listapending[1]);
            Notification.Action accion3 = new Notification.Action(Resource.Drawable.skipprevious, "Anterior", listapending[2]);
            Notification.Action accion4 = new Notification.Action(Resource.Drawable.skipforward, "adelantar", listapending[3]);
            Notification.Action accion5 = new Notification.Action(Resource.Drawable.skipbackward, "atrazar", listapending[4]);

            Notification.MediaStyle estilo = new Notification.MediaStyle();
            if (YoutubePlayerServerActivity.gettearinstancia() != null)
            {
                estilo.SetMediaSession(YoutubePlayerServerActivity.gettearinstancia().mSession.SessionToken);

                estilo.SetShowActionsInCompactView(1, 2, 3);

            }

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            var nBuilder = new Notification.Builder(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                try
                {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    nBuilder.SetContent(contentView);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                }
                catch (Exception) {

                }
                
            }
            else {


                nBuilder.SetStyle(estilo);
                nBuilder.SetLargeIcon(ImageHelper.GetImageBitmapFromUrl("http://i.ytimg.com/vi/"+linkactual.Split('=')[1]+"/mqdefault.jpg"));
                nBuilder.SetContentTitle(tituloactual);
                nBuilder.SetContentText(linkactual);
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
            listapending.Clear();
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
            Intent internado6 = new Intent(this, typeof(YoutubePlayerServerActivity));

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