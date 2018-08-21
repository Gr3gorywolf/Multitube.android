using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.Renderscripts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class actconectarservidor : Activity
    {
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
        ISharedPreferencesEditor prefEditor;
        public List<string> misips = new List<string>();
        LinearLayout botonscan;
             LinearLayout botoniralultimo;
        string ultimaipescaneada = "";
        ListView listaelementos;
        TextView textoservers;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(  Resource.Layout.conectaralservidor);
            botoniralultimo = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            botonscan = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            listaelementos = FindViewById<ListView>(Resource.Id.listView1);
             textoservers = FindViewById<TextView>(Resource.Id.textView3);
            ImageView fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            botoniralultimo.Visibility = ViewStates.Gone;
            listaelementos.Visibility = ViewStates.Gone;
            textoservers.Visibility = ViewStates.Gone;
            fondo.SetImageBitmap(CreateBlurredImageoffline(this, 20, 45434));
            //////////////////////////////////miselaneo
            prefEditor = prefs.Edit();
            if (!clasesettings.probarsetting("ips"))
            {
                clasesettings.guardarsetting("ips", "");


            }
            if (clasesettings.probarsetting("ipanterior"))
            {
                /*  textboxl.Text = prefs.GetString("ipanterior", null);
                  Thread proc = new Thread(new ThreadStart(tryear2));
                  proc.Start();*/
                if (estaon(clasesettings.gettearvalor("ipanterior")))
                {
                    ultimaipescaneada = clasesettings.gettearvalor("ipanterior");
                    botoniralultimo.Visibility = ViewStates.Visible;

                    if (clasesettings.gettearvalor("ips").Trim().Length > 1)
                    {
                        try
                        {
                            misips = clasesettings.gettearvalor("ips").Trim().Split('>').ToList();
                            if (misips[misips.Count - 1].Trim() == "")
                            {
                                misips.RemoveAt(misips.Count - 1);
                            }
                            foreach (string prro in misips)
                            {
                                new Thread(() =>
                                {
                                    estaon2(prro);
                                }).Start();
                            }




                        }
                        catch (Exception)
                        {

                        }

                    }

                    var dialogoprogreso = new ProgressDialog(this);
                    dialogoprogreso.SetCanceledOnTouchOutside(false);
                    dialogoprogreso.SetCancelable(false);
                    dialogoprogreso.SetTitle("Cargando lista de servidores...");
                    dialogoprogreso.SetMessage("Por favor espere");
                    dialogoprogreso.Show();
                    Android.OS.Handler mHandler = new Android.OS.Handler();
                    mHandler.PostDelayed(new Action(() => { dialogoprogreso.Dismiss(); }), 3000);


                }else
                {
                    if (clasesettings.gettearvalor("ips").Trim().Length > 1)
                    {
                        try
                        {
                            misips = clasesettings.gettearvalor("ips").Trim().Split('>').ToList();
                            if (misips[misips.Count - 1].Trim() == "")
                            {
                                misips.RemoveAt(misips.Count - 1);
                            }
                            foreach (string prro in misips)
                            {
                                new Thread(() =>
                                {
                                    estaon2(prro);
                                }).Start();
                            }




                        }
                        catch (Exception)
                        {

                        }

                    }

                 /*   var dialogoprogreso = new ProgressDialog(this);
                    dialogoprogreso.SetCanceledOnTouchOutside(false);
                    dialogoprogreso.SetCancelable(false);
                    dialogoprogreso.SetTitle("Cargando lista de servidores...");
                    dialogoprogreso.SetMessage("Por favor espere");
                    dialogoprogreso.Show();
                    Android.OS.Handler mHandler = new Android.OS.Handler();
                    mHandler.PostDelayed(new Action(() => { dialogoprogreso.Dismiss(); }), 3000);*/
                }

            }
           
              
            
            animar4(botoniralultimo);
            animar4(botonscan);
            animar4(textoservers);
            animar4(listaelementos);
            listaelementos.ItemClick += (aaa, aasd) =>
            {
               
                if (estaon(misips[aasd.Position]))
                {
                    clasesettings.guardarsetting("ipanterior", misips[aasd.Position]);
                    Intent activity2 = new Intent(this, typeof(mainmenu));

                    activity2.PutExtra("MyData", misips[aasd.Position]);
                    RunOnUiThread(() => botoniralultimo.Visibility = ViewStates.Visible);
                    animar3(botoniralultimo);
                    animar3(botonscan);
                    animar2(listaelementos, activity2);
                    animar2(textoservers, activity2);
                }
            };

            
            string klk = "";
            botoniralultimo.Click += delegate
            {
                Intent activity2 = new Intent(this, typeof(mainmenu));

                activity2.PutExtra("MyData",ultimaipescaneada);
             /*   RunOnUiThread(() => botoniralultimo.Visibility = ViewStates.Visible);
                RunOnUiThread(() => this.Finish());
                RunOnUiThread(() => StartActivity(activity2));
                */
                animar2(botoniralultimo,activity2);
                animar3(botonscan);
                animar3(listaelementos);
                animar3(textoservers);
            };
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

            botonscan.Click += async (aaaa, aaa) =>
           {
               ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
               var scanner = new ZXing.Mobile.MobileBarcodeScanner();

               var resultado = await scanner.Scan();
               if (resultado != null)
               {

                   ultimaipescaneada = resultado.Text.Trim();
                     //textboxl.Text = resultado.Text.Trim();
                     Thread proc = new Thread(new ThreadStart(tryear));
                   proc.Start();
               }
           };
           

            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist"))
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
            }

                 //////////////////////////////////////


            // Create your application here
        }
        public override void OnBackPressed()
        {
            StartActivity(typeof(actmenuprincipal));
            this.Finish();
            base.OnBackPressed();
        }
        public override void Finish()
        {
         
            base.Finish();
        }
        public void animar3(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000);
            animacion.SetDuration(1000);
            animacion.Start();

        }

        public void animar2(Java.Lang.Object imagen, Intent intento)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", 1000);
            animacion.SetDuration(1000);


            animacion.Start();
            animacion.AnimationEnd += delegate
            {
                this.Finish();
                StartActivity(intento);
            };

        }


        public void animar4(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(1000);
            animacion.Start();

        }
        public bool estaon(string ipadre)
        {
            try
            {
                using (TcpClient cliente = new TcpClient())
            {



                    //  cliente.Client.ConnectAsync(ipadre, 1024);
                    //   cliente.Client.Connect(ipadre, 1024);
                    var result = cliente.Client.BeginConnect(ipadre, 1024, null, null);
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                    if (cliente.Client.Connected)
                    {
                        cliente.Client.Disconnect(true);
                        return true;
                       
                    }
                    else
                    {
                        return false;
                    }
                  
                  
                

                }
               
            }

            catch (Exception)
            {
                return false;
            }
        }
        public void estaon2(string ipadre)
        {
            try
            {
                using (TcpClient cliente = new TcpClient())
                {



                    //  cliente.Client.ConnectAsync(ipadre, 1024);
                    //   cliente.Client.Connect(ipadre, 1024);
                  
                 var result=   cliente.Client.BeginConnect(ipadre, 1024, null, null);
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3));
                    if (cliente.Client.Connected)
                    {
                        cliente.Client.Disconnect(true);
                        RunOnUiThread(() =>
                        {
                            if (ipadre.Trim() == ultimaipescaneada.Trim())
                            {
                                misips.Remove(ipadre);
                            }

                            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>(misips));
                            listaelementos.Adapter = adaptadol;
                            if (misips.Count >= 1)
                            {

                                listaelementos.Visibility = ViewStates.Visible;
                                textoservers.Visibility = ViewStates.Visible;
                            }
                        });
                      
                    }
                    else
                    {
                        RunOnUiThread(() =>
                        {
                            misips.Remove(ipadre);
                        var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>(misips));
                        listaelementos.Adapter = adaptadol;
                        if (misips.Count >= 1)
                        {

                            listaelementos.Visibility = ViewStates.Visible;
                            textoservers.Visibility = ViewStates.Visible;
                        }
                        });
                    }
                  
                   

                }

            }

            catch (Exception)
            {
               
            }
        }



        public void tryear()
        {
            try
            {
               
                using (TcpClient cliente = new TcpClient()) {
                    string pasasion;
                    /*   cliente.Client.Connect(ultimaipescaneada, 1024);
                       cliente.Client.Disconnect(true);**/
                    if (estaon(ultimaipescaneada))
                    {

                 
                pasasion = ultimaipescaneada;

                Intent activity2 = new Intent(this, typeof(mainmenu));

                activity2.PutExtra("MyData", pasasion);
                RunOnUiThread(() => botoniralultimo.Visibility = ViewStates.Visible);
                    /*     RunOnUiThread(() => this.Finish());
                      RunOnUiThread(() => StartActivity(activity2));*/
                    RunOnUiThread(() =>
                    {
                        animar3(botoniralultimo);
                        animar2(botonscan, activity2);
                        animar3(listaelementos);
                        animar3(textoservers);
                    });




                    prefEditor.PutString("ipanterior", ultimaipescaneada);
                prefEditor.Commit();
                    }else
                    {
                        ultimaipescaneada = "";
                        //   RunOnUiThread(() => botonscan.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => Toast.MakeText(this, "Error de conexion", ToastLength.Short).Show());
                    }

                }
            }
            catch (SocketException)
            {
                ultimaipescaneada = "";
                //   RunOnUiThread(() => botonscan.Visibility = ViewStates.Gone);
                RunOnUiThread(() => Toast.MakeText(this, "Error de conexion", ToastLength.Short).Show());
            }


        }
        public Bitmap CreateBlurredImageoffline(Context contexto, int radius, int resid)
        {

            // Load a clean bitmap and work from that.
            WallpaperManager wm = WallpaperManager.GetInstance(this);
            Drawable d = wm.Drawable;
            Bitmap originalBitmap;
            originalBitmap = ((BitmapDrawable)d).Bitmap;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {
                // Create another bitmap that will hold the results of the filter.
                Bitmap blurredBitmap;
                blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

                // Create the Renderscript instance that will do the work.
                RenderScript rs = RenderScript.Create(contexto);

                // Allocate memory for Renderscript to work with
                Allocation input = Allocation.CreateFromBitmap(rs, originalBitmap, Allocation.MipmapControl.MipmapFull, AllocationUsage.Script);
                Allocation output = Allocation.CreateTyped(rs, input.Type);

                // Load up an instance of the specific script that we want to use.
                ScriptIntrinsicBlur script = ScriptIntrinsicBlur.Create(rs, Element.U8_4(rs));
                script.SetInput(input);

                // Set the blur radius
                script.SetRadius(radius);

                // Start the ScriptIntrinisicBlur
                script.ForEach(output);

                // Copy the output to the blurred bitmap
                output.CopyTo(blurredBitmap);

                return blurredBitmap;
            }
            else
            {
                return originalBitmap;
            }
        }
    }
}