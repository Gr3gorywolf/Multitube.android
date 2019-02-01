using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Renderscripts;
using System.Threading;
using System.Net;
using System.IO;
using Android.Content.PM;
using Android.Support.V4.Provider;

namespace App1
{
 
    [Activity(Label = "Multitube", MainLauncher = true, Icon = "@drawable/icon" ,ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class actmenuprincipal : Activity
    {
        ImageView fondito;
        List<Bitmap> bitmapeses = new List<Bitmap>();
        TextView tv3;
        public string ipanterior = "";

        ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
        ISharedPreferencesEditor prefEditor;

        public List<string> misips = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.perfectmainmenu);
            List<string> arraydatos = new List<string>();
            arraydatos.Add(Android.Manifest.Permission.Camera);
            arraydatos.Add(Android.Manifest.Permission.ReadExternalStorage);
            arraydatos.Add(Android.Manifest.Permission.WriteExternalStorage);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                RequestPermissions(arraydatos.ToArray(), 0);



            }
         

            fondito = FindViewById<ImageView>(Resource.Id.fondo1);
           
            var botoncontrolremoto = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            var botonserver = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            var botonplayer = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            var botonsettings = FindViewById<LinearLayout>(Resource.Id.linearLayout5);

            var botoninfo = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            tv3 = FindViewById<TextView>(Resource.Id.textView3);
            //bitmapeses.Add(CreateBlurredImageoffline(this, 20, Resource.Drawable.fondo1));
           // bitmapeses.Add(CreateBlurredImageoffline(this, 20, Resource.Drawable.fondosparaminiaturasdeyoutubeminimalist1920x1080wallpaper203803));
            tv3.Selected = true;
            // tv3.Selected = true;
   
            // bitmapeses.Add(CreateBlurredImageoffline(this, 20, Resource.Drawable.fondo3));
            //  bitmapeses.Add(CreateBlurredImageoffline(this, 20, Resource.Drawable.fondo4));
          //  RunOnUiThread(() => fondito.SetImageBitmap(bitmapeses[0]));
          
            ////////////////mimicv2//////////////////////
            clasesettings.guardarsetting("comando", "");
            clasesettings.guardarsetting("listaactual", "");
            clasesettings.guardarsetting("videocerrado", "");
            clasesettings.guardarsetting("musica", "");
            clasesettings.guardarsetting("servicio", "");
            clasesettings.guardarsetting("playerstatus", "");
            clasesettings.guardarsetting("duracion", "");
            clasesettings.guardarsetting("progreso", "");
            clasesettings.guardarsetting("cquerry", "");
            clasesettings.guardarsetting("acaboelplayer", "");
            clasesettings.guardarsetting("patra", "");
            clasesettings.guardarsetting("palante", "");
            clasesettings.guardarsetting("videoactivo", "no");
            clasesettings.guardarsetting("progresovideoactual", "");
            clasesettings.guardarsetting("tapnumber", "");

         
            animar4(botoncontrolremoto,500);
            animar4(botonserver, 1000);
            animar4(botonplayer, 1500);
            animar4(botonsettings, 2000);
            animar4(botoninfo, 2500);
            animar4(FindViewById<ImageView>(Resource.Id.imageView1), 250);
            prefEditor = prefs.Edit();




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





            if (!clasesettings.probarsetting("color"))
            {
                clasesettings.guardarsetting("color", "black");
            }
            if (!clasesettings.probarsetting("mediacache"))
            {
                clasesettings.guardarsetting("mediacache", "");
            }

            if (!clasesettings.probarsetting("ordenalfabeto"))
            {
                clasesettings.guardarsetting("ordenalfabeto", "si");
            }
            if (!clasesettings.probarsetting("video"))
            {
                clasesettings.guardarsetting("video", "-1");
            }
            if (!clasesettings.probarsetting("automatica"))
            {
                clasesettings.guardarsetting("automatica", "si");
            }

            botoninfo.Click += delegate
            {


                 var intento = new Intent(this, typeof(actbio));
                animar20(botoninfo, intento);            
                clasesettings.recogerbasura();

            };
            botonplayer.Click += delegate
            { 

               
                if (clasesettings.tieneelementos())
                {
                    var intento = new Intent(this, typeof(playeroffline));
                    animar3(botonserver);
                    animar3(botonsettings);
                    animar3(botoncontrolremoto);
                    animar3(botoninfo);
                    animar2(botonplayer, intento);
                    clasesettings.recogerbasura();
                }else
                {
                    Toast.MakeText(this, "No tiene elementos descargados para reproducir", ToastLength.Long).Show();
                }
            };
            botonsettings.Click += delegate
            {

                var intento = new Intent(this, typeof(configuraciones));
                animar3(botonserver);
                 
                 animar3(botoncontrolremoto);
                 animar3(botonplayer);
                animar3(botoninfo);
                animar2(botonsettings,intento);
                clasesettings.recogerbasura();
            };
            botonserver.Click += delegate
            {
                if (clasesettings.tieneconexion())
                {
                    var intento = new Intent(this, typeof(mainmenu_Offline));

                    animar3(botonsettings);
                    animar3(botoncontrolremoto);
                    animar3(botonplayer);
                    animar3(botoninfo);
                    animar2(botonserver, intento);
                    clasesettings.recogerbasura();
                }
                else {
                    Toast.MakeText(this, "Error al conectar a youtube", ToastLength.Long).Show();
                }
            };
            botoncontrolremoto.Click += delegate
            {

                // var intento = new Intent(this, typeof(mainmenu_Offline));
                if (clasesettings.tieneconexion())
                {
                    animar3(botonsettings);
                animar3(botonserver);
                animar3(botonplayer);
                animar3(botoninfo);
                animar2(botoncontrolremoto, new Intent(this,typeof(actconectarservidor)));
                clasesettings.recogerbasura();
                }
                else
                {
                    Toast.MakeText(this, "Error al conectar a youtube", ToastLength.Long).Show();
                }
            };




         
            string klk = "";
            try {
            if (!clasesettings.probarsetting("color"))
            {
                clasesettings.guardarsetting("color", "#000000");
            }
            if (!clasesettings.probarsetting("rutadescarga"))
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


            // Create your application here
        }


        /// <summary>
        /// animar3(botonserver);
        ///  animar3(botonsettings);
        //  animar3(botoncontrolremoto);
        //  animar3(botonplayer);
        /// </summary>
        /// <param name="imagen"></param>
        /// 
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


       
        public void animar4(Java.Lang.Object imagen,int duracion)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "alpha", 0, 1);
            animacion.SetDuration(duracion);
            animacion.Start();

        }
        public void animar3(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "alpha", 1,0);
            animacion.SetDuration(500);                  
            animacion.Start();
           
        }
        public void animar3inverse(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY",0,1000);
            animacion.SetDuration(1000);
            animacion.Start();

        }
        public void animar2(Java.Lang.Object imagen,Intent intento)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "alpha",1,1);
            animacion.SetDuration(500);
           
         
            animacion.Start();
            animacion.AnimationEnd += delegate
            {
                this.Finish();
                StartActivity(intento);
            };
           
        }

        public void animar20(Java.Lang.Object imagen, Intent intento)
        {
            StartActivity(intento);
            

        }
        public  Bitmap CreateBlurredImageoffline(Context contexto, int radius, int resid)
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


     
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            bool acepted = true;
            foreach (var permi in grantResults)
            {
                if (permi == Permission.Denied)
                {
                    acepted = false;
                }
             
            }
            if (acepted) {
               
                    if (clasesettings.probarsetting("abrirserver"))
                    {
                        if (clasesettings.gettearvalor("abrirserver") == "si")
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
                
           
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



    }
}
