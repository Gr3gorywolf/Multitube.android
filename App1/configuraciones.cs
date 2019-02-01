using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics.Drawables;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using directorychooser;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Renderscripts;
using System.Net;
using System.Threading;
//using Cheesebaron.MvxPlugins.Settings.Droid;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]

    public class configuraciones : Android.Support.V7.App.AppCompatActivity

    {
        Button botonseleccionarcarpeta;
        TextView localizacion;
        public Spinner calidades;
        // ImageView botonguardar;
        int calidad = -1;
        string color = "";
        public bool pathvalido = true;
        string klk;
        public string ordenalfabeto ="si";
        public string abrirserver = "no";
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
       string reprodautomatica = "";


        public bool CheckInternetConnection()
        {
            string CheckUrl = "https://gr3gorywolf.github.io/Multitubeweb/";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);

                iNetRequest.Timeout = 35000;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());
                iNetResponse.Close();

                return true;

            }
            catch (Exception)
            {

                // Console.WriteLine (".....no connection..." + ex.ToString ());

                return false;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Configuraciones);

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            ISharedPreferencesEditor prefEditor = prefs.Edit();
            botonseleccionarcarpeta = FindViewById<Button>(Resource.Id.imageView1);
            localizacion = FindViewById<TextView>(Resource.Id.textView3);
         //   botonguardar = FindViewById<ImageView>(Resource.Id.imageView2);
            var colormuestra = FindViewById<ImageView>(Resource.Id.imageView3);
            colormuestra.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            color = clasesettings.gettearvalor("color");
            reprodautomatica = clasesettings.gettearvalor("automatica");
            var ll1 = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            var ll2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            var ll3 = FindViewById<LinearLayout>(Resource.Id.linearLayout5);
            var ll4 = FindViewById<LinearLayout>(Resource.Id.linearLayout7);
            var ll5 = FindViewById<LinearLayout>(Resource.Id.linearLayout23);
            var fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            var toggle1 = FindViewById<Android.Support.V7.Widget.SwitchCompat>(Resource.Id.toggleButton1);
            var toggle2 = FindViewById<Android.Support.V7.Widget.SwitchCompat>(Resource.Id.toggleButton2);
            var automatica = FindViewById<Android.Support.V7.Widget.SwitchCompat>(Resource.Id.automatico);
            var botonclearcache = FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            calidades = FindViewById<Spinner>(Resource.Id.spinner1);
            var action = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
            //////////////////////////////////////coloreselector mappings
        
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            ///////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////
            ///
            if (reprodautomatica == "si")
                automatica.Checked = true;
            else
                automatica.Checked = false;


            SetSupportActionBar(action);
            SupportActionBar.Title = "Preferencias";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var adapter =new  ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.AddAll(new string[] { "Audio", "360p", "720p" }.ToList());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            calidades.Adapter = adapter;

            color = clasesettings.gettearvalor("color");
            calidad =int.Parse( clasesettings.gettearvalor("video"));
            switch (calidad) {
                case -1:
                    calidades.SetSelection(0);
                    break;
                case 360:
                    calidades.SetSelection(1);
                    break;
                case 720:
                    calidades.SetSelection(2);
                    break;
            }
           
            if (clasesettings.probarsetting("abrirserver")) {
                abrirserver = clasesettings.gettearvalor("abrirserver");
               
            }
            if (abrirserver == "no")
            {
                toggle2.Checked = false;

            }
            else {
                toggle2.Checked = true;
            }


                if (clasesettings.probarsetting("ordenalfabeto"))
            {
                ordenalfabeto = clasesettings.gettearvalor("ordenalfabeto");

                if (ordenalfabeto == "si")
                {
                    toggle1.Checked = true;
                }
                else
                {
                    toggle1.Checked = false;
                }
            }

            if ( clasesettings.probarsetting("rutadescarga")){
                klk = prefs.GetString("rutadescarga", null);

            }
            else
            {
               if(Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/YTDownloads"))
                {          
                klk = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath +"/YTDownloads";
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

            localizacion.Text = klk;
         
            fondo.SetImageBitmap(CreateBlurredImageoffline(this, 20, 0));
            ////////////////////////////////clicks////////////////////////////

            calidades.ItemSelected += (axx, ssd) => {
                switch (ssd.Position) {
                    case 0:
                        calidad = -1;
                        break;
                    case 1:
                        calidad = 360;
                        break;
                    case 2:
                        calidad = 720;
                        break;

                }
             
            };
            automatica.Click += delegate
            {
                if (automatica.Checked)
                {
                    reprodautomatica = "si";
                    Toast.MakeText(this, "Si no hay mas elementos en cola se reproducira el primer elemento de las sugerencias",ToastLength.Long).Show();
                }
                else
                {
                    reprodautomatica = "no";
                    Toast.MakeText(this, "Si no hay mas elementos en cola no se ejecutara ninguna accion", ToastLength.Long).Show();
                }

            };
            toggle2.Click += delegate
            {
                if (toggle2.Checked)
                {

                    new Thread(() =>
                    {
                        if (clasesettings.tieneelementos())
                        {




                            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/version.gr3v"))
                            {
                                abrirserver = "si";
                              
                                    StartService(new Intent(this, typeof(serviciostreaming)));
                              
                            }
                            else

                            {


                                AlertDialog dialogo = null;
                                RunOnUiThread(() => {

                                    var progresox = new ProgressBar(this);
                                    progresox.Indeterminate = true;

                                    dialogo = new AlertDialog.Builder(this)
                                    .SetTitle("Buscando actualizaciones")
                                    .SetMessage("Por favor espere...")
                                    .SetCancelable(false)
                                    .SetView(progresox)
                                    .Show();

                                });
                                if (CheckInternetConnection())
                                {
                                    abrirserver = "si";
                                    AlertDialog alerta = null;
                                  RunOnUiThread(() =>
                                    {
                                        dialogo.Dismiss();
                                        var progreso = new ProgressBar(this);
                                        progreso.Indeterminate = true;

                                        alerta = new AlertDialog.Builder(this)
                                        .SetTitle("Descargando archivos necesarios")
                                        .SetMessage("Por favor espere...")
                                        .SetCancelable(false)
                                        .SetView(progreso)
                                        .Show();
                                    });
                                    new Thread(() =>
                                    {

                                        WebClient cliente = new WebClient();
                                        var version = cliente.DownloadString("https://gr3gorywolf.github.io/Multitubeweb/version.gr3v");
                                        using (var file = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/version.gr3v")) { 
                                            file.Write(version);
                                        file.Close();
                                        }
                                        clasesettings.updatejavelyn(version);
                                        RunOnUiThread(() =>
                                        {
                                            alerta.Dismiss();
                                            StartService(new Intent(this, typeof(serviciostreaming)));
                                        });
                                      
                                    }).Start();
                           
                                  
                                }
                                else
                                {
                                    RunOnUiThread(() =>
                                    {
                                        dialogo.Dismiss();
                                        Toast.MakeText(this, "Debe tener una conexion a internet para abrir el servicio por primera vez", ToastLength.Long).Show();
                                        toggle2.Checked = false;
                                    });
                                }
                            }





                        }
                        else
                        {
                            RunOnUiThread(() =>
                            {
                                toggle2.Checked = false;
                                Toast.MakeText(this, "Debe tener almenos 1 elemento descargado", ToastLength.Long).Show();
                            });
                           

                        }
                    }).Start();




                }
                else {
                    if (serviciostreaming.gettearinstancia() != null)
                    {
                        StopService(new Intent(this, typeof(serviciostreaming)));
                    }
                    toggle2.Checked = false;
                    abrirserver = "no";
                }


            };
            botonclearcache.Click += delegate
            {
              
                AlertDialog.Builder ad = new AlertDialog.Builder(this);
                ad.SetCancelable(false);
                ad.SetTitle("Advertencia");
                ad.SetIcon(Resource.Drawable.alert);
                ad.SetMessage("Limpiar el cache puede provocar cierta realentizacion a la hora de entrar al reproductor offline y tambien volvera a descargar los datos por lo cual necesitara internet ¿¿quiere borrar cache??");
                ad.SetNegativeButton("No", no);
                ad.SetPositiveButton("Si",si);
                ad.Create();
                ad.Show();

            };
            toggle1.Click += delegate
            {
                if (toggle1.Checked == true)
                {
                    ordenalfabeto = "si";
                    Toast.MakeText(this, "Los elementos se organizaran alfabeticamente",ToastLength.Long).Show();
                }
                else
                {
                    ordenalfabeto = "no";
                    Toast.MakeText(this, "Los elementos se por fecha de descarga", ToastLength.Long).Show();
                }
            };
            botonseleccionarcarpeta.Click += async delegate
           {
               SimpleFileDialog sfd = new SimpleFileDialog(this, SimpleFileDialog.FileSelectionMode.FolderChooseRoot);
               klk = await sfd.GetFileOrDirectoryAsync(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
               if (probarpath(klk))
               {
                   pathvalido = true;

                   localizacion.Text = klk;
               }
               else
               {
                   Toast.MakeText(this, "Ruta invalida seleccione otra", ToastLength.Short).Show();
                   klk = localizacion.Text;
                   pathvalido = false;
               }



           };
       
        }

        public override void OnBackPressed()
        {
         
            clasesettings.preguntarsimenuosalir(this);
   
           // clasesettings.preguntarsimenuosalir(this);
            // base.OnBackPressed();
        }


        public void animar4(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(1000);
            animacion.Start();

        }
        public bool probarpath(string pth)
        {
            try { 
            File.CreateText(pth+"/prro.gr3");
            File.Delete(pth + "/prro.gr3");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void si(object sender, EventArgs e)
        {
        
           if( Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache")){
                if (Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/portraits")){
                    foreach( var alchi in Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/portraits"))
                    {
                        File.Delete(alchi);
                    }
                    
                }
                if(Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/webbrowser"))
                {
                   
                    foreach (var alchi in  Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/webbrowser"))
                    {
                        File.Delete(alchi);
                    }
                }
            }
            Toast.MakeText(this, "Limpieza finalizada exitosamente", ToastLength.Long).Show() ;
        }
        public  void no(object sender, EventArgs e)
        {
          
        }

        protected override void OnDestroy()
        {
            if (klk.Length > 0 && pathvalido == true)
            {



                clasesettings.guardarsetting("rutadescarga", klk);


            }
            clasesettings.guardarsetting("abrirserver", abrirserver);
            clasesettings.guardarsetting("ordenalfabeto", ordenalfabeto);
            clasesettings.guardarsetting("color", color);
            clasesettings.guardarsetting("video", calidad.ToString());
            clasesettings.guardarsetting("automatica", reprodautomatica);
            //  Toast.MakeText(this, "Guardado exitosamente", ToastLength.Long).Show();
            clasesettings.preguntarsimenuosalir(this);
    

            base.OnDestroy();
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
         

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