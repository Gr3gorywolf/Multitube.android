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
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.Dialog.NoActionBar")]
    public class Reproducirlistadialog : Activity
    {
        public bool detenedor = true;

        ImageView botonborrar;
        ImageView botonreproducir;
        ImageView botonatras;
        LinearLayout llayout;
        LinearLayout llayout2;
        string nombrelista;
        TcpClient cliente;
        public bool enedicion = false;
        ListView listbox;
        ImageView fondo;
        List<string> partes = new List<string>();

        List<string> links = new List<string>();

        void ok(object sender, EventArgs e)
        {
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + nombrelista);
            cliente.Client.Send(Encoding.Default.GetBytes("actualizarplaylist()"));
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
           cliente = new TcpClient(ipadre, 1024);
        
            llayout = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            llayout2 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            botonatras = FindViewById<ImageView>(Resource.Id.imageView1);
           botonreproducir = FindViewById<ImageView>(Resource.Id.imageView3);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
           botonborrar = FindViewById<ImageView>(Resource.Id.imageView2);
            fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            animar2(llayout2);
            nombrelista = Intent.GetStringExtra("nombrelista");
          //  llayout.SetBackgroundColor(Android.Graphics.Color.DarkGray);
           llayout2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            animar2(llayout2);
            // Create your application here
            this.SetFinishOnTouchOutside(false);
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

           botonborrar.SetBackgroundResource(Resource.Drawable.closecircularbuttonofacross);
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
        

          ArrayAdapter adaptador = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, lista);
           listbox.Adapter = adaptador;
            new Thread(() =>
            {
                try
                {

              
                var imagen = clasesettings.CreateBlurredImageonline(this,20, links[0]);
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

                Intent intento = new Intent(this, typeof(customdialogact));
                intento.PutExtra("color", "DarkGray");
                intento.PutExtra("url", links[aaa.Position]);
                intento.PutExtra("titulo", partes[aaa.Position]);
                intento.PutExtra("ipadress", "localhost");
                intento.PutExtra("imagen", @"https://i.ytimg.com/vi/" + links[aaa.Position].Split('=')[1] + "/hqdefault.jpg");
                StartActivity(intento);    
              
            };

            botonatras.Click += delegate
            {
                animar(botonatras);
                cerraractividad();


            };
            botonreproducir.Click += delegate
            {
                animar(botonreproducir);
                cliente.Client.Send(Encoding.Default.GetBytes("pedirlista()"));
                Thread.Sleep(250);
                string indice = Intent.GetStringExtra("index");
                cliente.Client.Send(Encoding.Default.GetBytes(indice));
                
                cerraractividad();
               

            };
            botonborrar.Click += delegate
            {

                animar(botonborrar);


                if (enedicion == false)
                {
                    animar(botonborrar);
                    enedicion = true;
                    botonborrar.SetBackgroundResource(Resource.Drawable.menucircularbutton);
                    adaptadorlista adalteeel = new adaptadorlista(this, partes.ToList(), links.ToList(), nombrelista, false,false);
                    
                    listbox.Adapter = adalteeel;
                }
                else
                {
                    animar(botonborrar);
                    enedicion = false;
                    botonborrar.SetBackgroundResource(Resource.Drawable.closecircularbuttonofacross);
                    ArrayAdapter adaptadordf = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, partes.ToList());
                    listbox.Adapter = adaptadordf;
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
                if ( clasesettings.gettearvalor("refrescarlistadatos") =="ok")
                {
                  
                    clasesettings.guardarsetting("refrescarlistadatos", "");
                    refrescar();
                }
                Thread.Sleep(100);
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
        }
        public void animar2(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(700);
            animacion.Start();

        }
        public override void Finish()
        {
            detenedor = false;
            cliente.Client.Disconnect(false);
            base.Finish();
            clasesettings.recogerbasura();
        }






    }
}