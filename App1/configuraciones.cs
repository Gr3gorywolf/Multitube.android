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
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class configuraciones : Activity
    {
        ImageView botonseleccionarcarpeta;
        TextView localizacion;
        ImageView botonguardar;
        string color = "";
        public bool pathvalido = true;
        string klk;
        public string ordenalfabeto ="si";
        public string abrirserver = "no";
        ProgressDialog dialogoprogreso;
        public bool CheckInternetConnection()
        {
            string CheckUrl = "https://gr3gorywolf.github.io/Multitubeweb/";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);

                iNetRequest.Timeout = 2500;

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
            botonseleccionarcarpeta = FindViewById<ImageView>(Resource.Id.imageView1);
            localizacion = FindViewById<TextView>(Resource.Id.textView3);
            botonguardar = FindViewById<ImageView>(Resource.Id.imageView2);
            var colormuestra = FindViewById<ImageView>(Resource.Id.imageView3);
            colormuestra.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            color = clasesettings.gettearvalor("color");
            var ll1 = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            var ll2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            var ll3 = FindViewById<LinearLayout>(Resource.Id.linearLayout5);
            var ll4 = FindViewById<LinearLayout>(Resource.Id.linearLayout7);
            var ll5 = FindViewById<LinearLayout>(Resource.Id.linearLayout23);
            var fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            var toggle1 = FindViewById<ToggleButton>(Resource.Id.toggleButton1);
            var toggle2 = FindViewById<ToggleButton>(Resource.Id.toggleButton2);
            var botonclearcache = FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            //////////////////////////////////////coloreselector mappings
            var color1 = FindViewById<ImageView>(Resource.Id.imageView4);
            var color2 = FindViewById<ImageView>(Resource.Id.imageView5);
            var color3= FindViewById<ImageView>(Resource.Id.imageView6);
            var color4 = FindViewById<ImageView>(Resource.Id.imageView7);
            var color5 = FindViewById<ImageView>(Resource.Id.imageView8);
            var color6 = FindViewById<ImageView>(Resource.Id.imageView9);
            var color7 = FindViewById<ImageView>(Resource.Id.imageView10);
            var color8 = FindViewById<ImageView>(Resource.Id.imageView11);
            var color9 = FindViewById<ImageView>(Resource.Id.imageView12);
            var color10 = FindViewById<ImageView>(Resource.Id.imageView13);
            var color11 = FindViewById<ImageView>(Resource.Id.imageView14);
            var color12 = FindViewById<ImageView>(Resource.Id.imageView15);
            var color13 = FindViewById<ImageView>(Resource.Id.imageView16);
            var color14 = FindViewById<ImageView>(Resource.Id.imageView17);
            var color15 = FindViewById<ImageView>(Resource.Id.imageView18);
            var color16 = FindViewById<ImageView>(Resource.Id.imageView19);
            color1.Click += delegate { color = "#F44336"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color2.Click += delegate { color = "#E91E63"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color3.Click += delegate { color = "#9C27B0"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color4.Click += delegate { color = "#673AB7"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color5.Click += delegate { color = "#3F51B5"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color6.Click += delegate { color = "#2196F3"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color7.Click += delegate { color = "#03A9F4"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color8.Click += delegate { color = "#00BCD4"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color9.Click += delegate { color = "#009688"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color10.Click += delegate { color = "#FFC107"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color11.Click += delegate { color = "#8BC34A"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color12.Click += delegate { color = "#FF9800"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color13.Click += delegate { color = "#795548"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color14.Click += delegate { color = "#FFEB3B"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color15.Click += delegate { color = "#FF9800"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            color16.Click += delegate { color = "#000000"; colormuestra.SetBackgroundColor(Color.ParseColor(color)); };
            dialogoprogreso = new ProgressDialog(this);
            ///////////////////////////////////////////////////////////



            color = clasesettings.gettearvalor("color");

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
            animar4(ll1);
            animar4(ll2);
            animar4(ll3);
            animar4(ll4);
            animar4(ll5);
            animar4(botonclearcache);
            fondo.SetImageBitmap(CreateBlurredImageoffline(this, 20, 0));
            ////////////////////////////////clicks////////////////////////////


            toggle2.Click += delegate
            {
                if (toggle2.Checked)
                {

                    new Thread(() =>
                    {
                        if ((File.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/downloaded.gr3d") || File.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/downloaded.gr3d2")))
                        {




                            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache/version.gr3v"))
                            {
                                abrirserver = "si";
                                if (serviciostreaming.gettearinstancia() == null)
                                {
                                    StartService(new Intent(this, typeof(serviciostreaming)));
                                }
                            }
                            else

                            {
                                if (CheckInternetConnection())
                                {
                                    abrirserver = "si";
                                    if (serviciostreaming.gettearinstancia() == null)
                                    {
                                        StartService(new Intent(this, typeof(serviciostreaming)));
                                    }
                                }
                                else
                                {
                                    RunOnUiThread(() =>
                                    {
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
                ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
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
                    Toast.MakeText(this, "Los elementos se organizaran alfabeticamente",ToastLength.Long);
                }
                else
                {
                    ordenalfabeto = "no";
                    Toast.MakeText(this, "Los elementos se por fecha de descarga", ToastLength.Long);
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
            botonguardar.Click += delegate
            {
                if (klk.Length > 0 && pathvalido == true)
                {

                   
                
                    clasesettings.guardarsetting("rutadescarga", klk);
                   
                  
                }
                clasesettings.guardarsetting("abrirserver", abrirserver);
                clasesettings.guardarsetting("ordenalfabeto", ordenalfabeto);
                clasesettings.guardarsetting("color", color);
                Toast.MakeText(this, "Guardado exitosamente", ToastLength.Long).Show();
                clasesettings.preguntarsimenuosalir(this);
            };
        }

        public override void OnBackPressed()
        {
            if (klk.Length > 0 && pathvalido == true)
            {

               
                clasesettings.guardarsetting("rutadescarga", klk);


            }
            clasesettings.guardarsetting("abrirserver", abrirserver);
            clasesettings.guardarsetting("ordenalfabeto", ordenalfabeto);
            clasesettings.guardarsetting("color", color);
            Toast.MakeText(this, "Guardado exitosamente", ToastLength.Long).Show();
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



    }
}