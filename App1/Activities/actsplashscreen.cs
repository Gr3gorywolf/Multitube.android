using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class actsplashscreen : Activity
    {
        public bool checkedpremissions = false;
        public bool isonline = false;
        public string updateinfo = "";
        public bool checkedupdate = false;
        TextView estado;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutsplashscreen);
            var portada = FindViewById<ImageView>(Resource.Id.portada2);
            estado = FindViewById<TextView>(Resource.Id.estado);
            animacionrepetitiva(portada, "scaleX");
            animacionrepetitiva(portada, "scaleY");
            animacionfadein(FindViewById<TextView>(Resource.Id.githubinfo), 2000);
            animacionfadein(estado, 3000);
            List<string> arraydatos = new List<string>();
            arraydatos.Add(Android.Manifest.Permission.Camera);
            arraydatos.Add(Android.Manifest.Permission.ReadExternalStorage);
            arraydatos.Add(Android.Manifest.Permission.WriteExternalStorage);
            new Thread(() => getupdate()).Start();
            new Thread(() => waitforcompletition()).Start();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                RequestPermissions(arraydatos.ToArray(), 0);



            }
            else {

                initial_boot();
            }

            try
            {

                StopService(new Intent(this, typeof(cloudingserviceonline)));
                StopService(new Intent(this, typeof(Clouding_serviceoffline)));
                StopService(new Intent(this, typeof(Clouding_service)));
                StopService(new Intent(this, typeof(serviciodownload)));

            }
            catch (Exception)
            {

            }







        }



        public void animacionfadein(Java.Lang.Object objeto,int duracion) {
            var anim = ObjectAnimator.OfFloat(objeto, "alpha", 0f, 1f);
            anim.SetDuration(duracion);
            anim.Start();
        }
        public void animacionrepetitiva(Java.Lang.Object objeto,string propiedad) {
            var anim= ObjectAnimator.OfFloat(objeto, propiedad, 0.8f, 1f);
            anim.SetDuration(1000);
            anim.RepeatCount = int.MaxValue - 200;
            anim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
            anim.Start();
        }



        public void waitforcompletition() {

            while (true) {
                if (checkedupdate && checkedpremissions)
                {
                    RunOnUiThread(() =>
                    {
                        Intent intento = new Intent(this, typeof(actmenuprincipal));
                        intento.PutExtra("fromsplash", true);
                        intento.PutExtra("isonline", isonline);
                        intento.PutExtra("updateinfo", updateinfo);
                        StartActivity(intento);
                        this.Finish();

                    });
                  
                    break;
                }

                if (this.IsFinishing)
                           break;

                Thread.Sleep(500);
            }
          
        }
        public void getupdate() {

            if (MultiHelper.HasInternetConnection())
            {
                RunOnUiThread(() => estado.Text = "Buscando actualizaciones...");
                isonline = true;
                updateinfo = new WebClient().DownloadStringTaskAsync("https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/Updates/newversion.json").Result;
                checkedupdate = true;
            }
            else {
                isonline = false;
                RunOnUiThread(() =>
                {
                    new AlertDialog.Builder(this)
                 .SetTitle("Atencion")
                 .SetMessage("No tiene conexion a internet.\n podra usar el reproductor offline y acceder a las configuraciones.")
                 .SetCancelable(false)
                 .SetPositiveButton("Entrar en modo offline", (aa, aaa) => { checkedupdate = true; })
                 .SetNegativeButton("Salir", (aa, aaa) => { this.Finish(); })
                 .Create()
                 .Show();
                });
            }
           
           

        }
        public void setinitialsettings() {


            try
            {




                if (!SettingsHelper.HasKey("color"))
                {
                    SettingsHelper.SaveSetting("color", "black");
                }
                if (!SettingsHelper.HasKey("mediacache"))
                {
                    SettingsHelper.SaveSetting("mediacache", "");
                }

                if (!SettingsHelper.HasKey("ordenalfabeto"))
                {
                    SettingsHelper.SaveSetting("ordenalfabeto", "si");
                }
                if (!SettingsHelper.HasKey("video"))
                {
                    SettingsHelper.SaveSetting("video", "360");
                }
                if (!SettingsHelper.HasKey("automatica"))
                {
                    SettingsHelper.SaveSetting("automatica", "si");
                }
                if (!SettingsHelper.HasKey("color"))
                {
                    SettingsHelper.SaveSetting("color", "#000000");
                }
                if (!SettingsHelper.HasKey("rutadescarga"))
                {
                    if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads"))
                          Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads");

                    SettingsHelper.SaveSetting("rutadescarga", Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads");
                }

                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache");
                }

                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/portraits"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/portraits");
                }
                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/webbrowser"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/webbrowser");
                }

                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                }

            }
            catch (Exception)
            {

            }



        }
        public async void initial_boot() {
            setinitialsettings();
            if (SettingsHelper.HasKey("abrirserver"))
            {
                if (SettingsHelper.GetSetting("abrirserver") == "si")
                {

                    if (serviciostreaming.gettearinstancia() != null)
                    {
                        StopService(new Intent(this, typeof(serviciostreaming)));
                        StartService(new Intent(this, typeof(serviciostreaming)));
                    }
                    else
                    {
                        StartService(new Intent(this, typeof(serviciostreaming)));
                    }

                }
            }
            if (!File.Exists(Constants.CachePath + "/verified"))
            {
                if (MultiHelper.HasInternetConnection())
                {
                    var firebase = new FirebaseClient(Constants.FirebaseSuggestionsUrl);
                    string serial = StringsHelper.GenerateSerial();
                    await firebase.Child("Descargas").Child(serial).PutAsync("Descargada@" + Android.OS.Build.Model + "@" + System.DateTime.Now);
                    var arch = File.CreateText(Constants.CachePath + "/verified");
                    arch.Write(serial);
                    arch.Close();

                }
            }

            checkedpremissions = true;
        }

        public override   void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            bool acepted = true;
            foreach (var permi in grantResults)
            {
                if (permi == Permission.Denied)
                {
                    acepted = false;
                }

            }
            if (acepted)
            {


                initial_boot();
               
             

            }
            else {
                RunOnUiThread(() =>
                {
                    new AlertDialog.Builder(this)
                        .SetTitle("Atencion")
                        .SetMessage("No se han aceptado todos los permisos.\n Si no se aceptan todos los permisos\n Por favor aceptelos.para que pueda utilizar funciones vitales de la aplicacion")
                        .SetCancelable(false)
                        .SetPositiveButton("Entendido", (aa, aaa) => { checkedpremissions = true; })
                        .Create()
                        .Show();
                });
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}