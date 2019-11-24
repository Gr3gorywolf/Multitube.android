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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net.Http;
using VideoLibrary;
using System.Text.RegularExpressions;
using Plugin.DownloadManager;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Descargar desde multitube" , Icon = "@drawable/icon",ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.UserDialog")]
   // [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/plain")]

    public class actdownloadcenterofflinedialog : Activity
    {
        public bool enporceso = false;
        public string linkvid = "";
        public int quality = 0;
        string rutadedescarga = "";
     
        public ImageView iv3;
     
        public Android.Media.MediaPlayer musicaplayer = new Android.Media.MediaPlayer();
    //    public IEnumerable<VideoInfo> videoinfoss;
        public bool enmov = false;
        public ProgressBar pv;
      public  Byte[] bitess = null;
        public string colores = "";
       
        public TextView tv4;
        public Thread trer;
        public bool parador = true;
        public LinearLayout llayout;
        public string archivocompleto = "";
        public LinearLayout lineall2;
        public ProgressBar progreso;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            ISharedPreferencesEditor prefEditor = prefs.Edit();

            SetContentView(Resource.Layout.Downloadcenterofflinedialog);


            base.OnCreate(savedInstanceState);

            var perrito = this.Theme;
            //////////////////////////////////TCpclientconnect/////////////
            var intento = Intent;
            var share = intento.Action;
            var tipo = intento.Type;
            if (Intent.ActionSend.Equals(share))
            {
                if (tipo.Contains("text/plain"))
                {
                    string krecibio = intento.GetStringExtra(Intent.ExtraText);
                    if (krecibio.Contains("youtu.be") || krecibio.Contains("youtube.com"))
                    {
                        linkvid = "https://www.youtube.com/watch?v=" + krecibio.Split('/')[3];
                        colores = "DarkGray";
                    }
                    else
                    {
                        Toast.MakeText(this, "Este enlace no proviene de youtube", ToastLength.Long).Show();
                        this.Finish();
                    }



                }
                else
                {
                    Toast.MakeText(this, "Este enlace no proviene de youtube", ToastLength.Long).Show();
                    this.Finish();
                }
            }
            else
            {
                linkvid = Intent.GetStringExtra("zelda");
                colores = Intent.GetStringExtra("color");

            }


            Intent.Extras.Clear();
            TextView titulo = FindViewById<TextView>(Resource.Id.textView4);
            titulo.Selected = true;
            this.SetFinishOnTouchOutside(true);
     
            new Thread(() =>
            {

                RunOnUiThread(() =>
                {
                    var videoTitle = VideosHelper.GetVideoTitle(linkvid) ;
                    titulo.Text = videoTitle;
                });

            }).Start();


            //////////////////////////////////DEclaraciones////////////////
            RadioGroup radio1 = FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            progreso = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            Button iv2 = FindViewById<Button>(Resource.Id.imageView2);


            ImageView iv4 = FindViewById<ImageView>(Resource.Id.imageView3);

            progreso.Visibility = ViewStates.Gone;

            RadioButton rb4 = FindViewById<RadioButton>(Resource.Id.radioButton4);
            RadioButton rb5 = FindViewById<RadioButton>(Resource.Id.radioButton5);
            RadioButton rb6 = FindViewById<RadioButton>(Resource.Id.radioButton6);
            ImageView fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            ImageView imagenredonda = FindViewById<ImageView>(Resource.Id.imageView4);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            llayout = FindViewById<LinearLayout>(Resource.Id.LinearLayout0);
            progreso.Visibility = ViewStates.Gone;
          

       
            new Thread(() =>
            {
                var imagenklk = ImageHelper.GetImageBitmapFromUrl("https://i.ytimg.com/vi/" + linkvid.Split('=')[1] + "/mqdefault.jpg");
                RunOnUiThread(() =>
                {


                    imagenredonda.BringToFront();
                    imagenredonda.BringToFront();
                 
                    imagenredonda.SetImageBitmap(ImageHelper.GetRoundedShape(imagenklk));
                    fondo.SetImageBitmap(imagenklk);
                    animar4(imagenredonda);
                    
                });
            }).Start();
         
           
        
            ///////////////////////////////////////mp3load+durationbarload//////////////////////
         
           
            
          //  lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
         //   animar2(lineall2);
            /////////////////////////////miselaneo///////////////////////////////////////////////
        

          
            if (colores != "none" && colores != "" && colores!=" ") { 
          //  llayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(colores));
            }
       rutadedescarga = prefs.GetString("rutadescarga", null);



        //   lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));


            ///////////////////////////////////events//////////////////////////////////////




            iv4.Click += delegate
            {
                animar(iv4); 
            
               
                parador = false;
                this.Finish();
               

            };
            iv2.Click += delegate
        {
            animar(iv2);
            progreso.Visibility = ViewStates.Visible;
            if (enporceso == false &&rb4.Checked==true)
            {
                
                quality =720;
                Thread tree = new Thread(new ThreadStart(descvids));
                tree.Start();
            }
            else
                if (enporceso == false && rb5.Checked == true)
            {
                quality = 360;
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

        public void animar4(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }
        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }

        public override void Finish()
        {

            MultiHelper.ExecuteGarbageCollection();
            base.Finish();

        }
        public override void OnBackPressed()
        {


            parador = false;
       
            this.Finish();
        }
        public void descvids()
        {
            progreso.Visibility = ViewStates.Visible;
            enporceso = true;
            string video2 = "";
            string title = "";
            RunOnUiThread(() => Toast.MakeText(this, "obteniendo informacion del video", ToastLength.Long).Show());
            RunOnUiThread(() => progreso.Progress = 25);

            var elemento = VideosHelper.GetShortVideoInfo(linkvid, false, quality);
            if (elemento != null)
            {
                title = elemento.Title;
                video2 = elemento.DownloadUrl;

                RunOnUiThread(() => progreso.Progress = 50);

                var pathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                string documentsPath = pathFile.AbsolutePath;
                RunOnUiThread(() => progreso.Progress = 75);
                string localFilename = RemoveIllegalPathCharacters(title).Trim() + ".mp4";
                string localPath = Path.Combine(rutadedescarga, localFilename);

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
                RunOnUiThread(() => progreso.Progress = 100);
                RunOnUiThread(() => Toast.MakeText(this, "Descarga iniciada", ToastLength.Long).Show());
            }
            else
            {
                RunOnUiThread(() =>
                {

                    Toast.MakeText(this, "Error al extraer el video posiblemente los servidores esten en mantenimiento", ToastLength.Long).Show();
                });
            }

}
        public void descmp3()
        {
            progreso.Visibility = ViewStates.Visible;
            enporceso = true;
            /*
           videoinfoss = DownloadUrlResolver.GetDownloadUrls(linkvid, false);

           VideoInfo video2 = videoinfoss.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 0);
           */
            RunOnUiThread(() => Toast.MakeText(this, "obteniendo informacion del video", ToastLength.Long).Show());
            RunOnUiThread(() => progreso.Progress = 25);
            var asd = VideosHelper.GetShortVideoInfo(linkvid, false, -1);
            if (asd != null)
            {
                RunOnUiThread(() => progreso.Progress = 50);
                //  Intent intento = new Intent(this, typeof(serviciodownload));
                string localFilename = RemoveIllegalPathCharacters(asd.Title).Trim() + ".mp3";
                string localPath = Path.Combine(rutadedescarga, localFilename);
                //  intento.PutExtra("path", localPath);
                //  intento.PutExtra("archivo", asd.downloadurl);
                //  intento.PutExtra("titulo", asd.titulo);
                //  intento.PutExtra("link", linkvid);


                if (serviciodownload.gettearinstancia() != null)
                {
                    serviciodownload.gettearinstancia().descargar(localPath, asd.DownloadUrl, asd.Title, linkvid);
                }
                else
                {
                    StartService(new Intent(this, typeof(serviciodownload)));
                    Thread.Sleep(300);
                    serviciodownload.gettearinstancia().descargar(localPath, asd.DownloadUrl, asd.Title, linkvid);
                }









                RunOnUiThread(() => progreso.Progress = 100);
                ///  StartService(intento);
                RunOnUiThread(() => this.Finish());
                RunOnUiThread(() => Toast.MakeText(this, "Descarga iniciada", ToastLength.Long).Show());

            }
            else
            {
                RunOnUiThread(() =>
                {

                    Toast.MakeText(this, "Error al extraer el video posiblemente los servidores esten en mantenimiento", ToastLength.Long).Show();
                });
            }

        }
        public void mostrarnotificacion(int porcentaje, string titulo, string link)
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