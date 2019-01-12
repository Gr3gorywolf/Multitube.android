using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net.Http;
using VideoLibrary;
using Android.Graphics;

namespace App1
{
    [Activity(Label = "Multitube", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class actdownloadcenteroffline : Activity
    {
        public bool enporceso = false;
        public string linkvid = "";
        public int quality = 0;
        string rutadedescarga = "";
     
        public ImageView iv3;
  
        public Android.Media.MediaPlayer musicaplayer = new Android.Media.MediaPlayer();
    //    public IEnumerable<VideoInfo> videoinfoss;
        public bool enmov = false;
      
      public  Byte[] bitess = null;
        public string colores = "";
    
        public TextView tv4;
        public Thread trer;
        public bool parador = true;
        public LinearLayout llayout;
        public string archivocompleto = "";
        public LinearLayout lineall2;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Downloadcenteroffline);
            

            base.OnCreate(savedInstanceState);
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            ISharedPreferencesEditor prefEditor = prefs.Edit();

            //////////////////////////////////TCpclientconnect/////////////
         
            string ip = Intent.GetStringExtra("ip");
            linkvid = Intent.GetStringExtra("zelda");
            colores = Intent.GetStringExtra("color");
    
            Intent.Extras.Clear();
            //////////////////////////////////DEclaraciones////////////////
            RadioGroup radio1 = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
          
          
            ImageView iv2 = FindViewById<ImageView>(Resource.Id.imageView2);
            ImageView playpause = FindViewById<ImageView>(Resource.Id.imageView7);
          
            ImageView iv4 = FindViewById<ImageView>(Resource.Id.imageView3);
            tv4 = FindViewById<TextView>(Resource.Id.textView6);
        
           
            RadioButton rb4 = FindViewById<RadioButton>(Resource.Id.radioButton4);
            RadioButton rb5 = FindViewById<RadioButton>(Resource.Id.radioButton5);
            RadioButton rb6 = FindViewById<RadioButton>(Resource.Id.radioButton6);
           lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
       llayout = FindViewById<LinearLayout>(Resource.Id.LinearLayout0);
           
          
            clasesettings.ponerfondoyactualizar(this);
            ///////////////////////////////////////mp3load+durationbarload//////////////////////
            tv4.Selected = true;
             trer = new Thread(new ThreadStart(cojerstream));
            trer.Start();
            
        //    lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
          //  animar2(lineall2);
            /////////////////////////////miselaneo///////////////////////////////////////////////
        

          
            if (colores != "none" && colores != "" && colores!=" ") { 
            //llayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(colores));
            }
            rutadedescarga = prefs.GetString("rutadescarga", null);




         //   lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));
            ///////////////////////////////////events//////////////////////////////////////
            playpause.Click += delegate
            {
                animar(playpause);
                mainmenu_Offline.gettearinstancia().play.PerformClick();
               
            };
          
           
         

            iv4.Click += delegate
            {
                animar(iv4);
                trer.Abort();
               
                musicaplayer.Reset();
                parador = false;
                this.Finish();
               

            };
            iv2.Click += delegate
        {
            animar(iv2);

            if (enporceso == false &&rb4.Checked==true)
            {
                
                quality = 360;
                Thread tree = new Thread(new ThreadStart(descvids));
                tree.Start();
            }
            else
                if (enporceso == false && rb5.Checked == true)
            {
                quality = 720;
                Thread tree2 = new Thread(new ThreadStart(descvids));
                tree2.Start();
            }

            else
                if (enporceso == false && rb6.Checked == true)
            {
                quality = 0;
                Thread tree3 = new Thread(new ThreadStart(descmp3));
                tree3.Start();
            }

         
        };
            musicaplayer.SeekComplete += delegate
            {
               
            };


     
          
         


        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////voids///////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 

        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }

        public override void OnBackPressed()
        {
            trer.Abort();
            musicaplayer.Reset();
    
            parador = false;
            this.Finish();
        }
        public void descvids()
        {
            enporceso = true;
            string video2 = "";
            string title = "";

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            dialogoprogreso.SetCanceledOnTouchOutside(false);
            dialogoprogreso.SetCancelable(false);
            dialogoprogreso.SetTitle("Analizando video...");
            dialogoprogreso.SetMessage("Por favor espere");
            dialogoprogreso.Show();
            using (var videito = Client.For(YouTube.Default))
            {
                var video = videito.GetAllVideosAsync(linkvid);
                var resultados = video.Result;
                title = resultados.First().Title.Replace("- YouTube", "");
                video2 = resultados.First(info => info.Resolution ==quality && info.AudioFormat== AudioFormat.Aac).GetUriAsync().Result;

            }



            /*
            videoinfoss = DownloadUrlResolver.GetDownloadUrls(linkvid, false);

            VideoInfo video2 = videoinfoss.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == quality);
            /* WebClient webClient = new WebClient();
             webClient.DownloadDataAsync(new Uri(video2.DownloadUrl));
             RunOnUiThread(() => Toast.MakeText(this, "Descarga iniciada", ToastLength.Long).Show());
             webClient.DownloadProgressChanged +=(sendel,easter)=>
             {



                 pv.Progress = easter.ProgressPercentage;
             };
             webClient.DownloadDataCompleted += (s, e) => {
                 mostrarnotificacion(100, video2.Title, linkvid);
                 RunOnUiThread(() => Toast.MakeText(this, "Descarga completada", ToastLength.Long).Show());
                 var bytes = e.Result; // get the downloaded data

                 var pathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                 string documentsPath = pathFile.AbsolutePath;

                 string localFilename = video2.Title+".mp4";
                 string localPath = Path.Combine(rutadedescarga, localFilename);
                 archivocompleto = localPath;
                 File.WriteAllBytes(localPath, bytes); // writes to local storage
                 enporceso = false;
                 pv.Progress = 0;
             };*/

            var pathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            string documentsPath = pathFile.AbsolutePath;

            string localFilename = RemoveIllegalPathCharacters(title).Trim() + ".mp4";
            string localPath = System.IO.Path.Combine(rutadedescarga, localFilename);
            /*    Intent intento = new Intent(this, typeof(serviciodownload));
                intento.PutExtra("path", localPath);
                intento.PutExtra("archivo", video2);
                intento.PutExtra("titulo", title);
                intento.PutExtra("link", linkvid);
                StartService(intento);*/
            if (serviciodownload.gettearinstancia() != null)
            {
                serviciodownload.gettearinstancia().descargar(localPath, video2, title, linkvid);
            }
            else
            {
                StartService(new Intent(this, typeof(serviciodownload)));
                Thread.Sleep(300);
                serviciodownload.gettearinstancia().descargar(localPath, video2, title, linkvid);
            }
            RunOnUiThread(() => this.Finish());
            RunOnUiThread(() => Toast.MakeText(this, "Descarga iniciada", ToastLength.Long).Show());
            dialogoprogreso.Dismiss();

        }
        public void descmp3()
        {
            enporceso = true;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            dialogoprogreso.SetCanceledOnTouchOutside(false);
            dialogoprogreso.SetCancelable(false);
            dialogoprogreso.SetTitle("Analizando video...");
            dialogoprogreso.SetMessage("Por favor espere");
            dialogoprogreso.Show();

            /*
            videoinfoss = DownloadUrlResolver.GetDownloadUrls(linkvid, false);

            VideoInfo video2 = videoinfoss.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 0);
            */
            var asd = clasesettings.gettearvideoid(linkvid,false,-1);
          //  Intent intento = new Intent(this, typeof(serviciodownload));
            string localFilename = RemoveIllegalPathCharacters(asd.titulo).Trim() + ".mp3";
            string localPath =System.IO.Path.Combine(rutadedescarga, localFilename);
            /*  intento.PutExtra("path", localPath);
               intento.PutExtra("archivo", asd.downloadurl);
               intento.PutExtra("titulo", asd.titulo);
               intento.PutExtra("link", linkvid);
               StartService(intento);*/
            if (serviciodownload.gettearinstancia() != null)
            {
                serviciodownload.gettearinstancia().descargar(localPath, asd.downloadurl, asd.titulo, linkvid);
            }
            else
            {
                StartService(new Intent(this, typeof(serviciodownload)));
                Thread.Sleep(300);
                serviciodownload.gettearinstancia().descargar(localPath, asd.downloadurl, asd.titulo, linkvid);
            }
            RunOnUiThread(() => this.Finish());
            RunOnUiThread(() => Toast.MakeText(this, "Descarga iniciada", ToastLength.Long).Show());
            /*
           WebClient webClient = new WebClient();
            webClient.DownloadDataAsync(new Uri(video2.DownloadUrl));
            RunOnUiThread(() => Toast.MakeText(this, "Descarga iniciada", ToastLength.Long).Show());
            webClient.DownloadProgressChanged += (sendel, easter) =>
            {

                pv.Progress = easter.ProgressPercentage;

            };
            webClient.DownloadDataCompleted += (s, e) => {
                RunOnUiThread(() => Toast.MakeText(this, "Descarga completada", ToastLength.Long).Show());
                        mostrarnotificacion(100, video2.Title, linkvid);
                var bytes = e.Result; // get the downloaded data

                var pathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                string documentsPath = pathFile.AbsolutePath;

                string localFilename = video2.Title + ".mp3";
                string localPath = Path.Combine(rutadedescarga, localFilename);
                archivocompleto = localPath;
                File.WriteAllBytes(localPath, bytes); // writes to local storage
                enporceso = false;
                pv.Progress = 0;
            };*/
            dialogoprogreso.Dismiss();

        }
        public void cojerstream()
        {

            while (!this.IsDestroyed)
            {
                if (mainmenu_Offline.gettearinstancia() != null )
                {

                    if (mainmenu_Offline.gettearinstancia().buscando != true)
                    {
                        if (mainmenu_Offline.gettearinstancia().label.Text != tv4.Text 
                            && mainmenu_Offline.gettearinstancia().label.Text.Trim() != "")
                        {
                            RunOnUiThread(() => tv4.Text = mainmenu_Offline.gettearinstancia().label.Text);
                        }
                    }
                    else {
                        RunOnUiThread(() => tv4.Text = "Buscando...");
                    }

                }
                else
                if (mainmenu.gettearinstancia() != null)
                {
                    if (mainmenu.gettearinstancia().label.Text != tv4.Text
                         &&  mainmenu.gettearinstancia().label.Text.Trim()!=""
                        )
                    {
                        RunOnUiThread(() => tv4.Text = mainmenu.gettearinstancia().label.Text);
                    }
                }
               
                if (tv4.Text.Trim() == "" && tv4.Text.Trim() != "No hay elementos en cola")
                {

                    RunOnUiThread(() => { tv4.Text = "No hay elementos en cola"; });
                }

                Thread.Sleep(1000);
            }
        }
        public void mostrarnotificacion(int porcentaje,string titulo,string link)
        {
            var pathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            string documentsPath = pathFile.AbsolutePath;

            Intent intent00 = new Intent();
            intent00.SetAction(Intent.ActionView);
            intent00.SetData(Android.Net.Uri.Parse(rutadedescarga));
            intent00.SetType("resource/folder");
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent00, PendingIntentFlags.UpdateCurrent);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            Notification.Builder builder = new Notification.Builder(this)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
      .SetContentTitle(titulo)
      .SetContentText(link)
      .SetSubText("Completada")
      .SetSmallIcon(Resource.Drawable.downloadbutton)
      .SetContentIntent(pendingIntent)
      ;
            
       
   
      
       
            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);


        }
        public void animar(Java.Lang.Object imagen)
        {
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();
            Android.Animation.ObjectAnimator animacion2 = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleY", 0.5f, 1f);
            animacion2.SetDuration(300);
            animacion2.Start();
        }
        ///////////////////////////////////////////////////////////voids//////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void animar2(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(700);
            animacion.Start();

        }
        // Create your application here

    }
}