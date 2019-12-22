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
using Newtonsoft.Json;
using App1.Models;
using App1.Utils;

namespace App1
{
 
    [Activity(Label = "Multitube" , Icon = "@drawable/icon" ,ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class actmenuprincipal : Activity
    {
        ImageView fondito;
        List<Bitmap> bitmapeses = new List<Bitmap>();
        TextView tv3;
        public string ipanterior = "";
        bool estaonline = false;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
      //  ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
        ISharedPreferencesEditor prefEditor;

        public List<string> misips = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.perfectmainmenu);
         

 


            fondito = FindViewById<ImageView>(Resource.Id.fondo1);
           
            var botoncontrolremoto = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            var botonserver = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            var botonplayer = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            var botonsettings = FindViewById<LinearLayout>(Resource.Id.linearLayout5);

            var botoninfo = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            tv3 = FindViewById<TextView>(Resource.Id.textView3);
   
            tv3.Selected = true;
 
            ////////////////mimicv2//////////////////////
         

         
            animar4(botoncontrolremoto,500);
            animar4(botonserver, 1000);
            animar4(botonplayer, 1500);
            animar4(botonsettings, 2000);
            animar4(botoninfo, 2500);
            animar4(FindViewById<ImageView>(Resource.Id.imageView1), 250);
            prefEditor = prefs.Edit();
            if (Intent.GetBooleanExtra("fromsplash", false))
            {
                estaonline = Intent.GetBooleanExtra("isonline", false);
                if (Intent.GetStringExtra("updateinfo").Trim() != "")
                {

                    var info = JsonConvert.DeserializeObject<UpdateInfo>(Intent.GetStringExtra("updateinfo").Trim());
                    var versionNumber = Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionCode;
                    if (info.Numero != versionNumber)
                    {
                        new AlertDialog.Builder(this)
                       .SetTitle("Atencion")
                       .SetMessage("Hay una nueva version disponible de la aplicacion la cual tiene la siguiente descripcion:\n" + info.Descripcion)
                       .SetCancelable(false)
                       .SetPositiveButton("Ir a la descarga", (aa, aaa) =>
                       {
                           var uri = Android.Net.Uri.Parse("https://gr3gorywolf.github.io/getromdownload/youtubepc.html");
                           var intent = new Intent(Intent.ActionView, uri);
                           StartActivity(intent);
                       })
                       .SetNegativeButton("Cancelar", (aa, aaa) => { })
                       .Create()
                       .Show();
                    }
                }

            }
            else {
                estaonline = true;
            }




            botoninfo.Click += delegate
            {


                  var intento = new Intent(this, typeof(actbio));
            
                animar20(botoninfo, intento);            
                MultiHelper.ExecuteGarbageCollection();

            };
            botonplayer.Click += delegate
            { 

               
                if (PlaylistsHelper.HasMediaElements)
                {
                    var intento = new Intent(this, typeof(playeroffline));
                    animar3(botonserver);
                    animar3(botonsettings);
                    animar3(botoncontrolremoto);
                    animar3(botoninfo);
                    animar2(botonplayer, intento);
                    MultiHelper.ExecuteGarbageCollection();
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
                MultiHelper.ExecuteGarbageCollection();
            };
            botonserver.Click += delegate
            {
                if (estaonline)
                {
                    var intento = new Intent(this, typeof(YoutubePlayerServerActivity));

                    animar3(botonsettings);
                    animar3(botoncontrolremoto);
                    animar3(botonplayer);
                    animar3(botoninfo);
                    animar2(botonserver, intento);
                    MultiHelper.ExecuteGarbageCollection();
                }
                else {

                    Toast.MakeText(this, "No hay conexion a internet por favor conectese a internet y reincie la aplicacion", ToastLength.Long).Show();
                }

            };
            botoncontrolremoto.Click += delegate
            {

                // var intento = new Intent(this, typeof(mainmenu_Offline));
                if (estaonline) { 
                    animar3(botonsettings);
                animar3(botonserver);
                animar3(botonplayer);
                animar3(botoninfo);
                animar2(botoncontrolremoto, new Intent(this,typeof(actconectarservidor)));
                MultiHelper.ExecuteGarbageCollection();
                }
                else
                {

                    Toast.MakeText(this, "No hay conexion a internet por favor conectese a internet y reincie la aplicacion", ToastLength.Long).Show();
                }

            };




         
        
           


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


     
     



    }
}
