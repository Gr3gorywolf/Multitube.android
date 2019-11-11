using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Appwidget;
using Android.Runtime;
using Android.Views;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using ZXing;
using System.IO;
using System.Collections.Generic;
using App1.Utils;

namespace App1
{

  
    [Activity( Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize,Theme = "@style/Theme.DesignDemo")]
  
    public class MainActivity : Activity
    {
        public EditText textboxl;
        public TcpClient cliente;
        public TextView texto;
        public TextView texto2;
        ImageView botonconfigs;
      
       
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
        ISharedPreferencesEditor prefEditor;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            // Set our view from the "main" layout resource
            ////////////////////////////////////mappings///////////////////////////////////////////
            texto2 = FindViewById<TextView>(Resource.Id.textView2);
            botonconfigs = FindViewById<ImageView>(Resource.Id.imageView2);
            textboxl = FindViewById<EditText>(Resource.Id.editText1);
           texto= FindViewById<TextView>(Resource.Id.textView1);
            Button boton = FindViewById<Button>(Resource.Id.button1);
            Button boton2 = FindViewById<Button>(Resource.Id.button2);
            ImageView comprobaryabrir = FindViewById<ImageView>(Resource.Id.imageView1);

            ////////////////////////////////////miselaneo///////////////////////////////////////////
               ////////////////mimicv2//////////////////////
            
          
            List<string> arraydatos = new List<string>();
            arraydatos.Add(Android.Manifest.Permission.Camera);
            arraydatos.Add(Android.Manifest.Permission.ReadExternalStorage);
            arraydatos.Add(Android.Manifest.Permission.WriteExternalStorage);
          
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                RequestPermissions(arraydatos.ToArray(), 0);
            }

         
            /////////////////////////////////////////////
            ///
         
            if (prefs.Contains("video"))
                SettingsHelper.SaveSetting("video", "-1");
            prefEditor = prefs.Edit();
            if (prefs.Contains("ipanterior"))
            {
                textboxl.Text = prefs.GetString("ipanterior",null);
                Thread proc = new Thread(new ThreadStart(tryear2));
                proc.Start();

            }
            string klk = "";
            if (!prefs.Contains("rutadedescarga"))
            {
                if (Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads"))
                {
                    klk = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads";
                    prefEditor.PutString("rutadescarga", klk);
                    prefEditor.Commit();
                }
                else
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads");
                    klk = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads";
                    prefEditor.PutString("rutadescarga", klk);
                    prefEditor.Commit();
                }
            }


        
            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist"))
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
            }

            texto2.Text = "Toque para conectar a servidor distinto";
            texto2.Visibility = ViewStates.Invisible;
         
            ////////////////////////////////////events///////////////////////////////////////////
            texto2.Click += delegate
            {
                textboxl.Text = "";
               textboxl.Visibility = ViewStates.Visible;
                texto2.Visibility = ViewStates.Invisible;

                texto.Text = "Inserte el codigo del servidor";
            };
            comprobaryabrir.Click += delegate
            {
                animar(comprobaryabrir);
            
                Thread tree = new Thread(new ThreadStart(tryear));
                tree.Start();
            };
            boton2.Click += delegate
                 {
                StartActivity(typeof(MainmenuOffline));
                 };
            boton.Click += async(semde,e)=>
                {
                    textboxl.Text = "";
                    ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
                    var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                   
                    var resultado = await scanner.Scan();
                    if(resultado!=null)
                    {

                   
                        textboxl.Text = resultado.Text.Trim();
                        Thread proc = new Thread(new ThreadStart(tryear));
                        proc.Start();
                    }
                };


            botonconfigs.Click += delegate
            {
                  // StartActivity( typeof(playeroffline));
                 // this.Finish();
                  StartActivity( typeof(actmenuprincipal));
                //  StartActivity( typeof(actividadsync));
                //  this.Finish();

                //  StartActivity(typeof(configuraciones));
            };







            // SetContentView (Resource.Layout.Main);

        }
     
        public override void FinishAndRemoveTask()
        {
            base.FinishAndRemoveTask();
            SettingsHelper.SaveSetting("servicio", "matar");
        }
        public override void Finish()
        {
            base.Finish();
            SettingsHelper.SaveSetting("elementosactuales", "");
            MultiHelper.ExecuteGarbageCollection();


        }
        public bool probarpath(string pth)
        {
            try
            {
                File.CreateText(pth + "/prro.gr3");
                File.Delete(pth + "/prro.gr3");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void tryear()
        {
            try
            {
                TcpClient cliente = new TcpClient();
                string pasasion;
                cliente.Client.Connect(textboxl.Text, 1024);
                cliente.Client.Disconnect(false);
                pasasion = textboxl.Text;

                Intent activity2 = new Intent(this, typeof(Mainmenu));
               
                activity2.PutExtra("MyData", pasasion);
                RunOnUiThread(() => textboxl.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => textboxl.Text = pasasion);
                RunOnUiThread(() => texto.Text = "Presione continuar para conectar");
                RunOnUiThread(() => texto2.Visibility = ViewStates.Visible);
                RunOnUiThread(() => StartActivity(activity2));

         


             
                    prefEditor.PutString("ipanterior", textboxl.Text);
                    prefEditor.Commit();

            

            }
            catch (SocketException)
            {
                RunOnUiThread(() => textboxl.Text = "");
                RunOnUiThread(() => Toast.MakeText(this, "Error de conexion", ToastLength.Short).Show());
            }
        }
        public void tryear2()
        {
            try
            {
                TcpClient cliente = new TcpClient();
                string pasasion;
                cliente.Client.Connect(textboxl.Text, 1024);
                cliente.Client.Disconnect(false);
                pasasion = textboxl.Text;

                Intent activity2 = new Intent(this, typeof(Mainmenu));

                activity2.PutExtra("MyData", pasasion);
                RunOnUiThread(() => textboxl.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => textboxl.Text = pasasion);
                RunOnUiThread(() => texto.Text = "Presione continuar para conectar");
                RunOnUiThread(() => texto2.Visibility = ViewStates.Visible);
                prefEditor.PutString("ipanterior", textboxl.Text);
                prefEditor.Commit();

            }
            catch (SocketException)
            {
                RunOnUiThread(() => textboxl.Text = "");
                RunOnUiThread(() => Toast.MakeText(this, "Error de conexion", ToastLength.Short).Show());
            }
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
        public void animar2(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(700);
            animacion.Start();

        }
    }
}

