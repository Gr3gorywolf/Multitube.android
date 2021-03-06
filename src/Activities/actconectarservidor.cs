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
using Newtonsoft.Json;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using App1.Models;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
    public class actconectarservidor : Android.Support.V7.App.AppCompatActivity
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
    {
      
        public List<string> todasip = new List<string>();
        public List<string> misips = new List<string>();
        public IpData mode;
       FloatingActionButton  botonscan;
   
        string ultimaipescaneada = "";
        ListView listaelementos;
 
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
        public ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
        public int scanedips = 0;
        public int forscan = 0;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.conectaralservidor);
            
            botonscan = FindViewById<FloatingActionButton>(Resource.Id.linearLayout2);
            listaelementos = FindViewById<ListView>(Resource.Id.listView1);
            var action = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
        
            SetSupportActionBar(action);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#2b2e30")));

            if (Directory.Exists(Constants.CachePath))
                Directory.CreateDirectory(Constants.CachePath);



            AnimationHelper.AnimateFAB(botonscan);



            //////////////////////////////////miselaneo
            ///


            if (File.Exists(Constants.CachePath + "/ips.json"))
            {








                var ipheader = SocketHelper.GetHostAddress().Split('.')[0];

           
                try
                {
                    mode = JsonConvert.DeserializeObject<IpData>(File.ReadAllText(Constants.CachePath + "/ips.json"));
                }
                catch (Exception)
                {
                    File.Delete(Constants.CachePath + "/ips.json");
                   mode = new IpData("", new Dictionary<string, string>());
                }
                if (estaon(mode.Ip))
                {
                    ultimaipescaneada = mode.Ip;
                   
                }
                todasip = mode.Ips.Keys.ToList();
                misips = mode.Ips.Keys.ToList().Where(aax => aax.StartsWith(ipheader)).ToList();
                forscan = misips.Count;
                foreach (string prro in new List<string>( misips))
                {
                    new Thread(() =>
                    {
                        estaon2(prro);
                    }).Start();
                }

                var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                RunOnUiThread(() =>
                {
                    var parcelable = listaelementos.OnSaveInstanceState();
                    listaelementos.Adapter = adaptadol;
                    listaelementos.OnRestoreInstanceState(parcelable);
                });
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Cargando lista de servidores...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
                new Thread(() => { remover(); }).Start();

            }
            else {

                mode = new IpData("", new Dictionary<string, string>());
            }
         
                /*  textboxl.Text = prefs.GetString("ipanterior", null);
                  Thread proc = new Thread(new ThreadStart(tryear2));
                  proc.Start();*/


              
           
            
           
                listaelementos.ItemClick += (aaa, aasd) =>
                {
                    if (misips.Count > 0)
                    {
                       


                            //SetActionBar(null);
                            mode.Ip = misips[aasd.Position];
                            SocketHelper.SaveIps(mode);

                            Intent activity2 = new Intent(this, typeof(Mainmenu));

                            activity2.PutExtra("MyData", misips[aasd.Position]);
                            StartActivity(activity2);
                            this.Finish();
                       
                        
                          
                           
                     
                    }
                };




                botonscan.Click +=  (aaaa, aaa) =>
               {


                   new Android.App.AlertDialog.Builder(this)
                   .SetTitle("Agregar nuevo dispositivo")
                   .SetMessage("Para agregar un se debe escanear un codigo el cual puede ser encontrado en el menu de conectar a clientes>boton de agregado (si es un host android) o abrir gestor de clientes(si el host es windows,mac,linux) luego presione escanear")
                   .SetPositiveButton("Escanear", (aa, cdsf)  => 
                   {

                       escaneareir();


                   })
                   .SetNegativeButton("Cancelar", (asd, ffe) => { }).Create().Show();


                  
               };


                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                }

                //////////////////////////////////////


                // Create your application here
         
        }

        public async void escaneareir() {

            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var resultado =await scanner.Scan();
            if (resultado != null)
            {

                ultimaipescaneada = resultado.Text.Trim();
                //textboxl.Text = resultado.Text.Trim();
                Thread proc = new Thread(new ThreadStart(tryear));
                proc.Start();
            }

        }

        public override void OnBackPressed()
        {
        
            this.Finish();
            StartActivity(typeof(actmenuprincipal));
            MultiHelper.ExecuteGarbageCollection();
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
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(4));
                    if (cliente.Client.Connected)
                    {
                        cliente.Client.Shutdown(SocketShutdown.Both);
                        cliente.Client.Close();
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

                    var result = cliente.Client.BeginConnect(ipadre, 1024, null, null);
                    result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(4));
                    
                    if (cliente.Client.Connected)
                    {
                       
                        cliente.Client.Shutdown(SocketShutdown.Both);
                        cliente.Client.Close();
                     

                    

                    }
                    else
                    {
                        misips.Remove(ipadre);
                   
                    }

                    scanedips++;

                }

            }

            catch (Exception)
            {
                scanedips++;
            }
        }


        public void remover() {

            while (scanedips != forscan) { }
            RunOnUiThread(() => dialogoprogreso.Dismiss());

            List<string> presentacion = new List<string>();
            List<string> dummylist = new List<string>();
            foreach (var ax in misips) {

                try
                {
                    if (mode.Ips[ax] == "")
                    {
                        presentacion.Add(ax);

                    }
                    else
                    {
                        presentacion.Add(mode.Ips[ax]);
                    }
                    dummylist.Add("=dummyxcfdfd");
                }
                catch (Exception) { };
               
            }

            RunOnUiThread(() =>
            {

                var adal = new adapterlistaremoto(this, presentacion, dummylist, null, Resource.Drawable.accesspoint);
              

                RunOnUiThread(() => {
                    var parcelable = listaelementos.OnSaveInstanceState();
                    listaelementos.Adapter = adal;
                    listaelementos.OnRestoreInstanceState(parcelable);
                });
                if (misips.Count == 0)
                {
                    var adaptadolss = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay dispositivos disponibles para conectar.." });
                    RunOnUiThread(() => {
                        var parcelable = listaelementos.OnSaveInstanceState();

                        listaelementos.Adapter = adaptadolss;
                        listaelementos.OnRestoreInstanceState(parcelable);
                    });
                }
                if (misips.Count >= 1)
                {
                    RunOnUiThread(() =>
                    {
                        listaelementos.Visibility = ViewStates.Visible;
                       
                    });
                }
            });

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

                        
                        /*     RunOnUiThread(() => this.Finish());
                          RunOnUiThread(() => StartActivity(activity2));*/
                     

                        
                        mode.Ip = ultimaipescaneada;
                       if(!todasip.Contains(ultimaipescaneada))
                          todasip.Add(ultimaipescaneada);

                        if (!mode.Ips.ContainsKey(ultimaipescaneada))
                            mode.Ips.Add(ultimaipescaneada, "");


                        SocketHelper.SaveIps(mode);
                        Intent activity2 = new Intent(this, typeof(Mainmenu));

                        activity2.PutExtra("MyData", pasasion);
                    
                        RunOnUiThread(() =>
                        {

                            StartActivity(activity2);
                           
                        });

                    }
                    else
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


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    StartActivity(typeof(actmenuprincipal));
                    this.Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }

  

 

}