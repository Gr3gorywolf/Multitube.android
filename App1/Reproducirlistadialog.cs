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
using System.IO;
using System.Net.Sockets;
using System.Threading;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.UserDialog")]
    public class Reproducirlistadialog : Activity
    {
        public bool detenedor = true;

        ImageView botonborrar;
        Button botonreproducir;
        ImageView botonatras;
        LinearLayout llayout;
        LinearLayout llayout2;
        string nombrelista;
     
        public bool enedicion = false;
        ListView listbox;
        ImageView fondo;
        List<string> partes = new List<string>();

        List<string> links = new List<string>();

        void ok(object sender, EventArgs e)
        {
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + nombrelista);
          
            Toast.MakeText(this, "Lista eliminada satisfactoriamente", ToastLength.Long).Show();
            cerraractividad();
           
        }
        void no(object sender, EventArgs e)
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
      
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loadplaylistoffline);
            string ipadre = Intent.GetStringExtra("ip");
           
        
            llayout = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            llayout2 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            botonatras = FindViewById<ImageView>(Resource.Id.imageView1);
           botonreproducir = FindViewById<Button>(Resource.Id.imageView3);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
           botonborrar = FindViewById<ImageView>(Resource.Id.imageView2);
            fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            animar2(llayout2);
            nombrelista = Intent.GetStringExtra("nombrelista");


          //  llayout.SetBackgroundColor(Android.Graphics.Color.DarkGray);
         //  llayout2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            animar2(llayout2);
            // Create your application here
            this.SetFinishOnTouchOutside(true);
            string listaenlinea = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + nombrelista);
            new Thread(() =>
            {
                adart();
            }
            ).Start();

       
          
           partes = listaenlinea.Split('$')[0].Split(';').ToList();
           links = listaenlinea.Split('$')[1].Split(';').ToList();
            if (partes[partes.Count - 1].Trim() == "")
            {
                partes.RemoveAt(partes.Count - 1);
                links.RemoveAt(links.Count - 1);
            }
            int indez = 0;

           botonborrar.SetBackgroundResource(Resource.Drawable.playlistedit);
            foreach (string it in partes.ToArray())
            {

                if (it.StartsWith(">"))
                {
                    string papu = it;
                    StringBuilder ee = new StringBuilder(papu);
                    ee.Replace('>', ' ');
                    ee.Replace('<', ' ');

                    partes[indez] = ee.ToString();


                }

                indez++;
            }

            var lista = partes.ToList();


            adapterlistaremotoconeliminar adalteeel = new adapterlistaremotoconeliminar(this, partes.ToList(), links.ToList(), nombrelista, false, false);

            var parcelable = listbox.OnSaveInstanceState();
            listbox.Adapter = adalteeel;
            listbox.OnRestoreInstanceState(parcelable);
          
            new Thread(() =>
            {
                try
                {

              
                var imagen = clasesettings.GetImageBitmapFromUrl("http://i.ytimg.com/vi/" +links[0].Split('=')[1] + "/mqdefault.jpg");
                RunOnUiThread(() =>
                {

                    fondo.SetImageBitmap(imagen);
                    animar4(fondo);
                });
                }
                catch (Exception)
                {

                }



            }).Start();

          
            //   adart(adalteeel);


       
            listbox.ItemClick += (aa, aaa) =>
            {
                if (links.Count > 0) {
                    Intent intento = new Intent(this, typeof(customdialogact));
                    intento.PutExtra("color", "DarkGray");
                    intento.PutExtra("url", links[aaa.Position]);
                    intento.PutExtra("titulo", partes[aaa.Position]);
                    intento.PutExtra("ipadress", "localhost");
                    intento.PutExtra("imagen", @"https://i.ytimg.com/vi/" + links[aaa.Position].Split('=')[1] + "/hqdefault.jpg");
                    StartActivity(intento);
                }

            };

            botonatras.Click += delegate
            {
                animar(botonatras);
                cerraractividad();


            };
            botonreproducir.Click += delegate
            {
               
                var indice = int.Parse(Intent.GetStringExtra("index"));
                new Thread(() =>
                {
                    MainmenuOffline.gettearinstancia().reproducirlalistalocal(nombrelista);
                }).Start();
                cerraractividad();

            };
            botonborrar.Click += delegate
            {

                animar(botonborrar);


                if (enedicion == false)
                {
                    animar(botonborrar);
                    enedicion = true;
                    botonborrar.SetBackgroundResource(Resource.Drawable.playlistcheck);
                    adapterlistaremotoconeliminar adalteeel33 = new adapterlistaremotoconeliminar(this, partes.ToList(), links.ToList(), nombrelista, false,false);
                    var parcelablex = listbox.OnSaveInstanceState();
                    listbox.Adapter = adalteeel33;
                    listbox.OnRestoreInstanceState(parcelablex);
                }
                else
                {
                    animar(botonborrar);
                    enedicion = false;
                    botonborrar.SetBackgroundResource(Resource.Drawable.playlistedit);
                    adapterlistaremotoconeliminar adalteeel2 = new adapterlistaremotoconeliminar(this, partes.ToList(), links.ToList(), nombrelista, false, false);

                    var parcelablexx = listbox.OnSaveInstanceState();
                    listbox.Adapter = adalteeel2;
                    listbox.OnRestoreInstanceState(parcelablexx);
                }


            };


            }

        public void refrescar()
        {
            detenedor = false;
            this.Finish();
            Intent internado = new Intent(this, typeof(Reproducirlistadialog));
                internado.PutExtra("ip","localhost");
                internado.PutExtra("nombrelista", nombrelista);
            internado.PutExtra("index", Intent.GetStringExtra("index"));
          
                StartActivity(internado);
          
        }
        public void animar4(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }
        public void adart()
        {
            while (detenedor)
            {
                if (SettingsHelper.GetSetting("refrescarlistadatos") =="ok")
                {

                    SettingsHelper.SaveSetting("refrescarlistadatos", "");
                    refrescar();
                }
                Thread.Sleep(150);
            }
        }
        public void cerraractividad()
        {
            detenedor = false;
            this.Finish();

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
        protected override void OnDestroy()
        {
            detenedor = false;
 
            clasesettings.recogerbasura();
            base.OnDestroy();
        }
        public override void Finish()
        {
    
            base.Finish();
            
        }






    }
}