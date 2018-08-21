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


namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class actdownloadcenter : Activity
    {
        public bool enporceso = false;
        public string linkvid = "";
        public int quality = 0;
        public TextView tv1;
        public TextView tv2;
        public TextView tv3;
        public TextView tv4;
        public ImageView iv3;
 
        public SeekBar seek;
        public Android.Media.MediaPlayer musicaplayer = new Android.Media.MediaPlayer();
    //    public IEnumerable<VideoInfo> videoinfoss;
        public bool enmov = false;
        string rutadedescarga = "";
        public ProgressBar pv;
      public  Byte[] bitess = null;
        public string colores = "";
        public TcpClient cliente;
        public ImageView retroceder;
        public ImageView adelantar;
        public Thread trer;
        public bool parador = true;
        public LinearLayout llayout;
        public string archivocompleto = "";
        public LinearLayout lineall2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Downloadcenter);
            

            base.OnCreate(savedInstanceState);
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            ISharedPreferencesEditor prefEditor = prefs.Edit();

            //////////////////////////////////TCpclientconnect/////////////
            cliente = new TcpClient();
            string ip = Intent.GetStringExtra("ip");
            linkvid = Intent.GetStringExtra("zelda");
            colores = Intent.GetStringExtra("color");
            cliente.Client.Connect(ip, 1024);
            Intent.Extras.Clear();
            //////////////////////////////////DEclaraciones////////////////
            RadioGroup radio1 = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            RadioGroup radio2 = FindViewById<RadioGroup>(Resource.Id.radioGroup2);
            ImageView iv1 = FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView iv2 = FindViewById<ImageView>(Resource.Id.imageView2);
            ImageView playpause = FindViewById<ImageView>(Resource.Id.imageView7);
            iv3 = FindViewById<ImageView>(Resource.Id.imageView4);
            ImageView iv4 = FindViewById<ImageView>(Resource.Id.imageView3);
            tv1 = FindViewById<TextView>(Resource.Id.textView1);
            tv2 = FindViewById<TextView>(Resource.Id.textView4);
            tv3 = FindViewById<TextView>(Resource.Id.textView5);
            tv4 = FindViewById<TextView>(Resource.Id.textView6);
            seek = FindViewById<SeekBar>(Resource.Id.seekBar1);
            RadioButton rb1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            RadioButton rb2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            RadioButton rb3 = FindViewById<RadioButton>(Resource.Id.radioButton3);
            RadioButton rb4 = FindViewById<RadioButton>(Resource.Id.radioButton4);
            RadioButton rb5 = FindViewById<RadioButton>(Resource.Id.radioButton5);
            RadioButton rb6 = FindViewById<RadioButton>(Resource.Id.radioButton6);
           lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
       llayout = FindViewById<LinearLayout>(Resource.Id.LinearLayout0);
            retroceder = FindViewById<ImageView>(Resource.Id.imageView5);
            adelantar = FindViewById<ImageView>(Resource.Id.imageView6);
            pv = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            clasesettings.ponerfondoyactualizar(this);
            ///////////////////////////////////////mp3load+durationbarload//////////////////////
            Thread tree4 = new Thread(new ThreadStart(mp3play));
            tree4.Start();
             trer = new Thread(new ThreadStart(cojerstream));
            trer.Start();
            
            lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
            animar2(lineall2);
            tv4.Selected = true;
            /////////////////////////////miselaneo///////////////////////////////////////////////
            pv.Max = 100;

            iv3.SetBackgroundResource(Resource.Drawable.playbutton);
            if (colores != "none" && colores != "" && colores!=" ") { 
           /// llayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(colores));
            }

        
                iv3.SetBackgroundResource(Resource.Drawable.playbutton);

            lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            rutadedescarga = prefs.GetString("rutadescarga", null);
            ///////////////////////////////////events//////////////////////////////////////

            seek.ProgressChanged += (aaa, aaaa) =>
            {
                if (aaaa.FromUser)
                {
                    musicaplayer.SeekTo(seek.Progress);
                }
            };
            playpause.Click += delegate
            {
                animar(playpause);
                cliente.Client.Send(Encoding.ASCII.GetBytes("playpause()"));
            };
            iv1.Click += delegate
                {
                    animar(iv1);
                    if (rb1.Checked == true)
                    { cliente.Client.Send(Encoding.ASCII.GetBytes("descvid360()")); }
                    else
                      if (rb2.Checked == true) { cliente.Client.Send(Encoding.ASCII.GetBytes("descvid720()")); }
                    else
                          if (rb3.Checked == true)
                    {
                        cliente.Client.Send(Encoding.ASCII.GetBytes("descmp3()"));
                    }





                };
            adelantar.Click += delegate
            {
                animar(adelantar);
                musicaplayer.Start();
                musicaplayer.SeekTo(Convert.ToInt32(musicaplayer.CurrentPosition + musicaplayer.Duration * 0.10));
            };
          retroceder.Click += delegate
            {
                animar(retroceder);
                musicaplayer.Start();
                musicaplayer.SeekTo(Convert.ToInt32( musicaplayer.CurrentPosition -musicaplayer.Duration*0.10));
            };


            iv4.Click += delegate
            {
                animar(iv4);
                trer.Abort();
                cliente.Client.Disconnect(false);
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


     
            iv3.Click += delegate
            {
                animar(iv3);
                if (tv3.Text!= "Cargando...")
               if (musicaplayer.IsPlaying == false )
                {
                    iv3.SetBackgroundResource(Resource.Drawable.pausebutton);
                    musicaplayer.Start();
                }
             
                else
                {
                    iv3.SetBackgroundResource(Resource.Drawable.playbutton);
                    musicaplayer.Pause();
                }
            
              
            };
         


        }
       
/// <summary>
/// //////////////////////////////////////////////////////////////////voids///////////////////////////////////////////////////////////////////////
/// </summary>
/// 
 
        public void mp3play()
        {
            while (parador == true) { 
            RunOnUiThread(() => tv3.Text = "Cargando.");
                /*
            videoinfoss = null;
            VideoInfo video = null;

               
                  
            videoinfoss = DownloadUrlResolver.GetDownloadUrls(linkvid.Trim(), false);
                Thread.Sleep(200);

                
            
                
                video = videoinfoss.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 0);
                

                */
                var prrin = clasesettings.gettearvideoid(linkvid,false);

                musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
                RunOnUiThread(() => tv3.Text = "Cargando..");
                musicaplayer=  Android.Media.MediaPlayer.Create(this, Android.Net.Uri.Parse(prrin.downloadurl));




                RunOnUiThread(() => tv2.Text ="");

                RunOnUiThread(() => tv3.Text = "Cargando...");
                try {
            
            } catch (Exception) { }
      
                RunOnUiThread(() => tv3.Text = prrin.titulo);
                RunOnUiThread(() => seek.Max = musicaplayer.Duration);
                while (parador==true)
            {
                    try
                    {
                  
                        if (musicaplayer.IsPlaying == true) {
                          
                       seek.Progress = musicaplayer.CurrentPosition;
                            Thread.Sleep(400);
                        }
                    }
                    catch(Exception)
                    { };



                //RunOnUiThread(() => seek.Progress = musicaplayer.CurrentPosition);

                }


            }



        }

        public override void OnBackPressed()
        {
            trer.Abort();
            musicaplayer.Reset();
            cliente.Client.Disconnect(false);
            parador = false;
            this.Finish();
        }
        public void descvids()
        {
            enporceso = true;

            string video2 = "";
            string title = "";
            using (var videito = Client.For(YouTube.Default))
            {
                var video = videito.GetAllVideosAsync(linkvid);
                var resultados = video.Result;
                title = resultados.First().Title.Replace("- YouTube", "");
                video2 = resultados.First(info => info.Resolution == quality && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
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
          
            string localFilename =RemoveIllegalPathCharacters( title).Trim() + ".mp4";
            string localPath = Path.Combine(rutadedescarga, localFilename);
            /*  Intent intento = new Intent(this, typeof(serviciodownload));
              intento.PutExtra("path", localPath);
              intento.PutExtra("archivo", video2);
              intento.PutExtra("titulo",title);
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

        }

        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
        public void descmp3()
        {
            enporceso = true;
            /*
            videoinfoss = DownloadUrlResolver.GetDownloadUrls(linkvid, false);

            VideoInfo video2 = videoinfoss.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 0);
            */
            var asd = clasesettings.gettearvideoid(linkvid,false);
            Intent intento = new Intent(this, typeof(serviciodownload));
            string localFilename = RemoveIllegalPathCharacters(asd.titulo).Trim() + ".mp3";
            string localPath = Path.Combine(rutadedescarga, localFilename);
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


        }
        public void cojerstream()
        {
            while (cliente.Client.Connected)
            {
                if (mainmenu_Offline.gettearinstancia() != null)
                {
                    if (mainmenu_Offline.gettearinstancia().label.Text != tv4.Text)
                    {
                        RunOnUiThread(() => tv4.Text = mainmenu_Offline.gettearinstancia().label.Text);
                    }

                }
                else
                if (mainmenu.gettearinstancia() != null)
                {
                    if (mainmenu.gettearinstancia().label.Text != tv4.Text)
                    {
                        RunOnUiThread(() => tv4.Text = mainmenu.gettearinstancia().label.Text);
                    }
                }
                else
                {

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
            intent00.SetDataAndType(Android.Net.Uri.Parse(documentsPath), "*/*");

            var pendingIntent = PendingIntent.GetActivity(this, 0, intent00, PendingIntentFlags.UpdateCurrent);

            Notification.Builder builder = new Notification.Builder(this)
      .SetContentTitle(titulo)
      .SetContentText(link)
      .SetSubText("Completada")
      .SetSmallIcon(Resource.Drawable.downloadbutton)
      .SetContentIntent(pendingIntent);


       
       
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