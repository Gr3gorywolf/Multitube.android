using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Media;
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
using App1.Utils;

namespace App1
{ 
    [Service(Exported = true)]
    [IntentFilter(new[] { Intent.ActionMediaButton })]
    public class Clouding_serviceoffline : Service , AudioManager.IOnAudioFocusChangeListener
    {
        public static Clouding_serviceoffline instancia;
        public  bool detenedor = true;
        public string musica = "";
        private AudioManager audioManager;
        public IBinder Binder { get; private set; }
     public    Android.Media.MediaPlayer musicaplayer = new Android.Media.MediaPlayer();
        public string linkactual = "";
        public  string tituloactual = "";
        public string ipactual = "";
        public bool desdecache = true;
        public string ponerestado()
        {
            if (musicaplayer.IsPlaying)
            {
                return "reproduciendo";
            }
            else
            {
                return "pausado";
            }
          
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            ///   broadcast_receiver receptor = new broadcast_receiver();
            ///  
            if( intent != null){

       
            if (intent.Action == Intent.ActionMediaButton )
            { 

            var keyEvent = (KeyEvent)intent.GetParcelableExtra(Intent.ExtraKeyEvent);

            switch (keyEvent.KeyCode)
            {
                case Keycode.MediaPlay:
                    break;
                case Keycode.MediaPlayPause:
                    break;
                case Keycode.MediaNext:
                    break;
                case Keycode.MediaPrevious:
                    break;
            }
            }
            }
            return base.OnStartCommand(intent, flags, startId);
          
        }

        public static Clouding_serviceoffline gettearinstancia()
        {
            return instancia;
        }

        public static Bitmap scaleDown(Bitmap realImage)
        {

            Bitmap newBitmap = Bitmap.CreateScaledBitmap(realImage, 250, 250, false);

            return newBitmap;
        }
        public void receiver()
        {

           
          





        }
     
        public void reproducir(string downloadurl,bool desdecache)
        {




            // musicaplayer.SetDataSource(downloadurl);
            if (playeroffline.gettearinstancia() != null)
            {

                try
                {
                    musicaplayer.Release();


                    musicaplayer = new MediaPlayer();
              
#pragma warning disable 414
                    if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.N)
                    {
                        musicaplayer.SetAudioAttributes(new AudioAttributes.Builder()
                            .SetUsage(AudioUsageKind.Media)
                            .SetContentType(AudioContentType.Music)
                            .Build());
                    }
                    else
                    {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                        musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    }
                  
#pragma warning restore 414
                    musicaplayer.SetWakeMode(this, WakeLockFlags.Partial);
#pragma warning disable 414
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    var focusResult = audioManager.RequestAudioFocus(this, Stream.Music, AudioFocus.Gain);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
#pragma warning restore 414
                    if (focusResult != AudioFocusRequest.Granted)
                    {
                        //could not get audio focus
                        Console.WriteLine("Could not get audio focus");
                    }
              
                 
                    musicaplayer.Prepared += delegate
                    {
                      
                        musicaplayer.Start();
                        if (this.desdecache)
                            musicaplayer.Pause();
                        if (playeroffline.gettearinstancia() != null)
                        {
                            if (playeroffline.gettearinstancia().video.Visibility == ViewStates.Visible)
                            {

                                musicaplayer.SetDisplay(null);
                                musicaplayer.SetDisplay(playeroffline.gettearinstancia().holder);
                            }

                            if (SettingsHelper.HasKey("posactual") && this.desdecache) {
                                var posicion = 0;
                                try
                                {
                                    posicion = int.Parse(SettingsHelper.GetSetting("posactual"));
                                    musicaplayer.SeekTo(posicion);
                                }
                                catch (Exception) { }
                               
                            }

                        }

                    };
                    musicaplayer.Completion += delegate
                    {
                         playeroffline.gettearinstancia().RunOnUiThread(() =>
                         {
                             playeroffline.gettearinstancia().RunOnUiThread(() =>
                             {
                                 playeroffline.gettearinstancia().siguiente.PerformClick();
                             });
                             
                         });
                    };
                  
                    musicaplayer.SetDataSource(this, Android.Net.Uri.Parse(downloadurl.Trim()));
                    mostrarnotificacion();
                    musicaplayer.PrepareAsync();
                  
                }
                catch (Exception)
                {
                    //if()
                
                    playeroffline.gettearinstancia().RunOnUiThread(() =>
                    {
                        Toast.MakeText(playeroffline.gettearinstancia(), "Error al reproducir", ToastLength.Long).Show();
                    });
                }
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
           
             instancia = this;
            audioManager = (AudioManager)GetSystemService(AudioService);
            receiver();
       
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
                   // contentView.SetImageViewBitmap(Resource.Id.imageView1, playeroffline.gettearinstancia().imageneses.First(info => info.GenerationId == playeroffline.gettearinstancia().diccionario[tituloactual]));
                   contentView.SetImageViewBitmap(Resource.Id.fondo1, ImageHelper.CreateBlurredImageFromPortrait(this, 20, linkactual));
                }
                catch (Exception){

                }
               
             ///   contentView.SetImageViewBitmap(Resource.Id.fondo1, clasesettings.CreateBlurredImageoffline(this, 20, linkactual));

            }
            contentView.SetTextViewText(Resource.Id.textView1, tituloactual);        
            contentView.SetOnClickPendingIntent(Resource.Id.imageView1, listapending[5]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView2, listapending[0]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView4, listapending[1]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView3, listapending[2]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView6, listapending[3]);
            contentView.SetOnClickPendingIntent(Resource.Id.imageView5, listapending[4]);


            
            Notification.Action accion1 = new Notification.Action(Resource.Drawable.playpause, "Playpause", listapending[0]);
            Notification.Action accion2 = new Notification.Action(Resource.Drawable.skipnext, "Siguiente", listapending[1]);
            Notification.Action accion3 = new Notification.Action(Resource.Drawable.skipprevious, "Anterior", listapending[2]);
            Notification.Action accion4 = new Notification.Action(Resource.Drawable.skipforward, "adelantar", listapending[3]);
            Notification.Action accion5 = new Notification.Action(Resource.Drawable.skipbackward, "atrazar", listapending[4]);
            
            /*
             
             1-playpause
             2-siguiente
             3-anterior
             4-adelantar
             5-atrazar
           
             */



            Notification.MediaStyle estilo = new Notification.MediaStyle();
            if (playeroffline.gettearinstancia() != null)
            {
                estilo.SetMediaSession(playeroffline.gettearinstancia().mSession.SessionToken);
               
                estilo.SetShowActionsInCompactView(1, 2, 3);
                
            }


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            var nBuilder = new Notification.Builder(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
#pragma warning disable 414
                try
                {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    nBuilder.SetContent(contentView);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                }
                catch (Exception) {

                }
               
#pragma warning restore 414
            }
            else {
                nBuilder.SetStyle(estilo);
                nBuilder.SetLargeIcon(GetImageBitmapFromUrl(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + playeroffline.gettearinstancia().linkeses[playeroffline.gettearinstancia().nombreses.IndexOf(tituloactual)].Split('=')[1]));
                nBuilder.SetContentTitle(tituloactual);
                
                nBuilder.SetContentText(playeroffline.gettearinstancia().linkeses[playeroffline.gettearinstancia().nombreses.IndexOf(tituloactual)]);
               nBuilder.AddAction(accion5);
                nBuilder.AddAction(accion3);
                nBuilder.AddAction(accion1);
                nBuilder.AddAction(accion2);
                nBuilder.AddAction(accion4);
                nBuilder.SetContentIntent(listapending[5]);
                nBuilder.SetColor(Android.Graphics.Color.ParseColor("#ce2c2b"));

                //  nBuilder.AddAction()
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
                    if (url.Trim() != "") {
                    imageBitmap = BitmapFactory.DecodeFile(url);
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

            Intent internado = new Intent(this, typeof(serviciointerpreter));
            internado.PutExtra("querry1", "si");
            internado.PutExtra("querry2", "");
            internado.PutExtra("querry3", "");
            internado.PutExtra("querry4", "");
            internado.PutExtra("querry5", "");

            var pendingIntent = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado, 0);
            listapending.Add(pendingIntent);
            /////2
            Intent internado2 = new Intent(this, typeof(serviciointerpreter));
            internado2.PutExtra("querry1", "");
            internado2.PutExtra("querry2", "si");
            internado2.PutExtra("querry3", "");
            internado2.PutExtra("querry4", "");
            internado2.PutExtra("querry5", "");

            var pendingIntent2 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado2, 0);
            listapending.Add(pendingIntent2);
            /////3
            Intent internado3 = new Intent(this, typeof(serviciointerpreter));
            internado3.PutExtra("querry1", "");
            internado3.PutExtra("querry2", "");
            internado3.PutExtra("querry3", "si");
            internado3.PutExtra("querry4", "");
            internado3.PutExtra("querry5", "");

            var pendingIntent3 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado3, 0);
            listapending.Add(pendingIntent3);

            /////4
            Intent internado4 = new Intent(this, typeof(serviciointerpreter));
            internado4.PutExtra("querry1", "");
            internado4.PutExtra("querry2", "");
            internado4.PutExtra("querry3", "");
            internado4.PutExtra("querry4", "si");
            internado4.PutExtra("querry5", "");

            var pendingIntent4 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado4, 0);
            listapending.Add(pendingIntent4);

            /////5
            Intent internado5 = new Intent(this, typeof(serviciointerpreter));
            internado5.PutExtra("querry1", "");
            internado5.PutExtra("querry2", "");
            internado5.PutExtra("querry3", "");
            internado5.PutExtra("querry4", "");
            internado5.PutExtra("querry5", "si");

            var pendingIntent5 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado5, 0);
            listapending.Add(pendingIntent5);
            /////6
            Intent internado6;

            internado6 = new Intent(this, typeof(playeroffline));

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