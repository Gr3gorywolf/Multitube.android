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
    [Activity(Label = "Multitube", ScreenOrientation =Android.Content.PM.ScreenOrientation.Portrait,ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
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
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        public SeekBar seek;
        public Android.Media.MediaPlayer musicaplayer = new Android.Media.MediaPlayer();
    //    public IEnumerable<VideoInfo> videoinfoss;
        public bool enmov = false;
        string rutadedescarga = "";
  
      public  Byte[] bitess = null;
        public string colores = "";
     
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
      
            string ip = Intent.GetStringExtra("ip");
            linkvid = Intent.GetStringExtra("zelda");
            colores = Intent.GetStringExtra("color");
         
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
       llayout = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            retroceder = FindViewById<ImageView>(Resource.Id.imageView5);
            adelantar = FindViewById<ImageView>(Resource.Id.imageView6);
      
            clasesettings.ponerfondoyactualizar(this);
            ///////////////////////////////////////mp3load+durationbarload//////////////////////
            Thread tree4 = new Thread(new ThreadStart(mp3play));
            tree4.Start();
             trer = new Thread(new ThreadStart(cojerstream));
            trer.Start();
            
         ///   lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
       //     animar2(lineall2);
            tv4.Selected = true;
            /////////////////////////////miselaneo///////////////////////////////////////////////
         
            iv3.SetBackgroundResource(Resource.Drawable.playbutton2);
            if (colores != "none" && colores != "" && colores!=" ") { 
           /// llayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(colores));
            }

        
                iv3.SetBackgroundResource(Resource.Drawable.playbutton2);

          //  lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));
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
               mainmenu.gettearinstancia().clientela.Client.Send(Encoding.ASCII.GetBytes("playpause()"));
            };
            iv1.Click += delegate
                {
                    animar(iv1);
                    if (rb1.Checked == true)
                    { mainmenu.gettearinstancia().clientela.Client.Send(Encoding.ASCII.GetBytes("descvid360()")); }
                    else
                      if (rb2.Checked == true) { mainmenu.gettearinstancia().clientela.Client.Send(Encoding.ASCII.GetBytes("descvid720()")); }
                    else
                          if (rb3.Checked == true)
                    {
                        mainmenu.gettearinstancia().clientela.Client.Send(Encoding.ASCII.GetBytes("descmp3()"));
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
                quality = -1;
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
                    iv3.SetBackgroundResource(Resource.Drawable.pausebutton2);
                    musicaplayer.Start();
                }
             
                else
                {
                    iv3.SetBackgroundResource(Resource.Drawable.playbutton2);
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
                var prrin = clasesettings.gettearvideoid(linkvid,false,-1);
                if (prrin != null) { 

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                RunOnUiThread(() => tv3.Text = "Cargando..");
                musicaplayer=  Android.Media.MediaPlayer.Create(this, Android.Net.Uri.Parse(prrin.downloadurl));




              

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
                else
                {
                    RunOnUiThread(() =>
                    {

                        Toast.MakeText(this, "Error al extraer el video posiblemente los servidores esten en mantenimiento", ToastLength.Long).Show();
                    });
                }

            }



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
            RunOnUiThread(() =>
            {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Analizando video...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
            });
            string video2 = "";
            string title = "";
            using (var videito = Client.For(YouTube.Default))
            {
                var video = videito.GetAllVideosAsync(linkvid);
                var resultados = video.Result;
                title = resultados.First().Title.Replace("- YouTube", "");
                video2 = resultados.First(info => info.Resolution == quality && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
            }


 
            var pathFile = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            string documentsPath = pathFile.AbsolutePath;
          
            string localFilename =RemoveIllegalPathCharacters( title).Trim() + ".mp4";
            string localPath =  Path.Combine(rutadedescarga, localFilename);
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

            RunOnUiThread(() => dialogoprogreso.Dismiss());
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
            RunOnUiThread(() =>
            {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Analizando video...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
            });

            var asd = clasesettings.gettearvideoid(linkvid, false, -1);
            if (asd != null) { 
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
            else
            {
                RunOnUiThread(() =>
                {

                    Toast.MakeText(this, "Error al extraer el video posiblemente los servidores esten en mantenimiento", ToastLength.Long).Show();
                });
            }
         RunOnUiThread(()=>   dialogoprogreso.Dismiss());


        }
        public void cojerstream()
        {

          
            while (!this.IsDestroyed)
            {
                if (mainmenu_Offline.gettearinstancia() != null)
                {
                    if (mainmenu_Offline.gettearinstancia().buscando != true)
                    {
                        if (mainmenu_Offline.gettearinstancia().label.Text != tv4.Text
                           && mainmenu_Offline.gettearinstancia().label.Text.Trim() != "")
                        {
                            RunOnUiThread(() => tv4.Text = mainmenu_Offline.gettearinstancia().label.Text);
                        }
                    }
                    else
                    {
                        RunOnUiThread(() => tv4.Text = "Buscando...");
                    }

                }
                else
                if (mainmenu.gettearinstancia() != null)
                {
                    if (mainmenu.gettearinstancia().buscando == false)
                    {
                        if (mainmenu.gettearinstancia().label.Text != tv4.Text
                           && mainmenu.gettearinstancia().label.Text.Trim() != ""
                          )
                        {
                            RunOnUiThread(() => tv4.Text = mainmenu.gettearinstancia().label.Text);
                        }
                    }else
                        RunOnUiThread(() => tv4.Text = "Buscando...");
                }
                else
                {

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
            intent00.SetDataAndType(Android.Net.Uri.Parse(documentsPath), "*/*");

            var pendingIntent = PendingIntent.GetActivity(this, 0, intent00, PendingIntentFlags.UpdateCurrent);

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            Notification.Builder builder = new Notification.Builder(this)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
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