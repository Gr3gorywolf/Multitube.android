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
using System.Threading;
using System.IO;
using Plugin.DownloadManager;
using System.Text.RegularExpressions;

namespace App1
{
    [Service(Exported = true)]
    public class serviciodownload : Service
    {
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
        ISharedPreferencesEditor prefEditor;
        public bool completada = false;
        public string tituloo = "";
        public int random = 0;

        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
        public void descargar2(string path,string archivo,string titulo,string link)
        {
        }



        public void agregaralacola(string path,string archivo,string titulo,string link)
        {
            new Thread(() =>
            {
                descargar2(path, archivo, titulo, link);
               

            }).Start();
        }
        public static serviciodownload instance;
        public static serviciodownload gettearinstancia()
        {
            return instance;
        }
        public override void OnCreate()
        {
      
            base.OnCreate();
            instance = this;
            prefEditor = prefs.Edit();
          //  descargar(intent.GetStringExtra("path"), intent.GetStringExtra("archivo"), intent.GetStringExtra("titulo"), intent.GetStringExtra("link"));

        }


        public override void OnTaskRemoved(Intent rootIntent)
        {

           
            base.OnTaskRemoved(rootIntent);


        }
       
      

        public void descargar(string path,string archivo,string titulo,string link)
        {

            WebClient cliente2 = new WebClient();
            cliente2.DownloadFileAsync(new Uri("https://i.ytimg.com/vi/" + link.Split('=')[1] + "/mqdefault.jpg"), Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]);
            //new
            if (titulo.Trim().Length > 45)
            {
                titulo = titulo.Remove(45);
            }
            tituloo = titulo;
         

            var manige = DownloadManager.FromContext(this);
            var requ = new DownloadManager.Request(Android.Net.Uri.Parse(archivo));
            requ.SetDescription("Espere por favor");
            requ.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
            requ.SetTitle( tituloo );
            var destino = Android.Net.Uri.FromFile(new Java.IO.File(path));
            requ.SetDestinationUri(destino);
         
            requ.SetVisibleInDownloadsUi(true);


         
            manige.Enqueue(requ);

            if (Path.GetFileName(path).EndsWith(".mp3"))
            {
                ////////////////si es mp3
                string datosviejos = "";
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d"))
                {
                  
                   datosviejos = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
                }
                else
                {
                 var asss=   File.Create(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
                    asss.Close();
                }
                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits");
                }

                if (!datosviejos.Contains(Path.GetFileNameWithoutExtension(path) + "²" + link + "²" +path + "¤"))
                {
                    var aafff = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
                    aafff.Write(datosviejos + Path.GetFileNameWithoutExtension(path) + "²" + link + "²" + path + "¤");
                    aafff.Close();

                }


            }
            //////////////////////////////////////////////si es mp4
            else
            {

                string datosviejos = "";
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
                {
                   
                    datosviejos = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");
                }
                else
                {
                    var asss = File.Create(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");
                    asss.Close();
                }
                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits");
                }

                if (!datosviejos.Contains(Path.GetFileNameWithoutExtension(path) + "²" + link + "²" + path + "¤"))
                {
                    var aafff = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");
                    aafff.Write(datosviejos + Path.GetFileNameWithoutExtension(path) + "²" + link + "²" + path + "¤");
                    aafff.Close();

                }
                clasesettings.recogerbasura();
            }



















            /*
            Random brandom = new Random();
            WebClient cliente2= new WebClient();
            cliente2.DownloadFileAsync(new Uri("https://i.ytimg.com/vi/" + link.Split('=')[1] + "/mqdefault.jpg"), Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]);
            int randum = brandom.Next(2000, 50000) + brandom.Next(2000, 50000);
            random = randum;
            try {
                NotificationManager notificationManager =
                 GetSystemService(Context.NotificationService) as NotificationManager;
                if (titulo.Trim().Length > 30)
                {
                    titulo = titulo.Remove(30);
                }
                tituloo = titulo;
                var builder = new Notification.Builder(ApplicationContext);
                builder.SetContentTitle("Descargando " + titulo + "...");
                builder.SetContentText("Espere por favor");
               
                builder.SetSmallIcon(Resource.Drawable.downloadbutton);
                
               

                WebClient cliente = new WebClient();
           
            byte[] losbits = null;
                cliente.DownloadProgressChanged += (aasd, asddd) =>
                {

                    builder.SetContentTitle("Descargando "+titulo+"...");
                    builder.SetContentText("Espere por favor");
                    builder.SetSmallIcon(Resource.Drawable.downloadbutton);
                    builder.SetProgress(100, asddd.ProgressPercentage, false);
                    notificationManager.Notify(randum, builder.Build());

                };
            cliente.DownloadDataCompleted += (aa, aaa) =>
            {

                //  Intent intentss = new Intent(this,typeof(actividadacciones));
                //  intentss.PutExtra("prro", path);
                try { 
                var selectedUri = Android.Net.Uri.Parse( prefs.GetString("rutadescarga",null)+ "/");

                Intent intentssdd = new Intent(this,typeof(actividadadinfooffline));
                intentssdd.PutExtra("nombre", Path.GetFileName(path));
                intentssdd.PutExtra("link", link);
                intentssdd.PutExtra("path", path);
              

              
          
                  
                PendingIntent intentosd = PendingIntent.GetActivity(this, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), intentssdd, PendingIntentFlags.UpdateCurrent);


                builder.SetContentTitle("Descarga completada de: "+titulo);
                builder.SetContentText("Toque para abrir");
                builder.SetSmallIcon(Resource.Drawable.downloadbutton);
                builder.SetContentIntent(intentosd);
                    builder.SetOngoing(false);
                    notificationManager.Notify(randum, builder.Build());

                losbits = aaa.Result;
                var a = File.Create(path);
                a.Write(losbits, 0, losbits.Length);
                a.Close();
                string datosviejos = "";
                link = link.Replace('²', ' ');
                link= link.Replace('¤', ' ');
                path =path.Replace('²', ' ');
                path=path.Replace('¤', ' ');


                if (Path.GetFileName(path).EndsWith(".mp3")) { 
                ///////////////////////////si es mp3

                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d"))
                {
                   datosviejos = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
                }
                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits");
                }
             
                if(!datosviejos.Contains(Path.GetFileNameWithoutExtension(path) + "²" + link + "²" + path + "¤"))
                {
                    var aafff = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
                    aafff.Write(datosviejos + Path.GetFileNameWithoutExtension(path) + "²" + link + "²" + path + "¤");
                    aafff.Close();
                 
                }


                }
                //////////////////////////////////////////////si es mp4
                else
                {
                    if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
                    {
                        datosviejos = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");
                    }
                    if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits"))
                    {
                        Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits");
                    }

                    if (!datosviejos.Contains(Path.GetFileNameWithoutExtension(path) + "²" + link + "²" + path + "¤"))
                    {
                        var aafff = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");
                        aafff.Write(datosviejos + Path.GetFileNameWithoutExtension(path) + "²" + link + "²" + path + "¤");
                        aafff.Close();

                    }
                        clasesettings.recogerbasura();
                }
                    completada = true;
                }
                catch (Exception)
                {
                    clasesettings.recogerbasura();
                    Toast.MakeText(this, "ha ocurrido un error al descargar intente de nuevo", ToastLength.Long).Show();
                   // StopForeground(false);
                }
               
            };

                cliente.DownloadData(new Uri(archivo));
            }
            catch (Exception)
            {
                NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;
                var builder = new Notification.Builder(ApplicationContext);
                builder.SetContentTitle("ERROR AL DESCARGAR: " + titulo);
                builder.SetContentText("Intente de nuevo");
                builder.SetSmallIcon(Resource.Drawable.downloadbutton);
                builder.SetOngoing(false);
                notificationManager.Notify(randum, builder.Build());
                clasesettings.recogerbasura();

            }
          */
        }
        public override void OnDestroy()
        {
            clasesettings.recogerbasura();
            base.OnDestroy();
          
               
          
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


    }
   
}