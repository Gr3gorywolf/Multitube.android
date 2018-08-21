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
using System.Net;
using System.Net.Sockets;
using Android.Graphics;
using System.Threading;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.Dialog.NoActionBar")]
    public class deletedialogact : Activity
    {
        public string imagem;
        public TcpClient cliente;
       public ImageView imagenview;
        public Thread proc;
        ImageView fondo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.eliminardialog);
         
          TextView texto= FindViewById<TextView>(Resource.Id.textView1);
            imagenview = FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView agregar = FindViewById<ImageView>(Resource.Id.imageView2);
            ImageView buscar = FindViewById<ImageView>(Resource.Id.imageView3);
            ImageView volver = FindViewById<ImageView>(Resource.Id.imageView4);
            LinearLayout lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            LinearLayout lineaa = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            ImageView descargar = FindViewById<ImageView>(Resource.Id.imageView5);
            ImageView addlista = FindViewById<ImageView>(Resource.Id.imageView6);
            fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            animar2(lineaa);
            texto.Selected = true;
            string colol = Intent.GetStringExtra("color");
            string url = Intent.GetStringExtra("url");
            string posicion = Intent.GetStringExtra("index");
            url = "https://www.youtube.com/watch?v=" + url.Split('=')[1];
            if (colol.Trim() != "none") {
               // lineaa.SetBackgroundColor(Color.ParseColor(colol));
                if (colol == "Black")
                {
                //   lineall2.SetBackgroundColor(Android.Graphics.Color.DarkGray);
                }
                else
                {
                  //  lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
                };
            }
            imagem = Intent.GetStringExtra("imagen");
            string titulo = Intent.GetStringExtra("titulo");
            texto.Text = titulo;
            
           cliente = new TcpClient();
            this.SetFinishOnTouchOutside(false);

            animar2(lineall2);
            cliente.Client.Connect(Intent.GetStringExtra("ipadress"), 1024);
           proc = new Thread(new ThreadStart(ponerimagen));
            proc.Start();
            volver.Click += delegate
             {
                 animar(volver);

                 
                 this.Finish();

               
             };
            addlista.Click += delegate
            {
                animar(addlista);
                Intent intentar = new Intent(this, typeof(actividadagregarlistahecha));
                intentar.PutExtra("nombrevid", titulo);
                intentar.PutExtra("linkvid",url);
                StartActivity(intentar);
                this.Finish();
            };
            agregar.Click += delegate
            {
                animar(agregar);
                cliente.Client.Send(Encoding.Default.GetBytes("eliminarelemento()"));
                Thread.Sleep(250);
                cliente.Client.Send(Encoding.Default.GetBytes(posicion));
               
          

                this.Finish();
              
             
            };
            descargar.Click += delegate
            {
                animar(descargar);
                Intent intento = new Intent(this, typeof(actdownloadcenterofflinedialog));
                intento.PutExtra("ip", "localhost");
                intento.PutExtra("zelda", url);
                intento.PutExtra("color", colol);
                StartActivity(intento);
                this.Finish();

            };
            buscar.Click += delegate
            {
                animar(buscar);
                if (cliente.Connected == true) { 
                cliente.Client.Send(Encoding.Default.GetBytes("pedirindice()"));
                    Thread.Sleep(250);
                    cliente.Client.Send(Encoding.Default.GetBytes(posicion));



                 
                



                    this.Finish();
                }
            };
             
            // Create your application here
        }
        public override void OnBackPressed()
        {
            

            this.Finish();
           
      
        }
        public void ponerimagen()
        {
            try { 
            WebClient clienteweb = new WebClient();
          byte[]losbits=  clienteweb.DownloadData(imagem);
            Bitmap imagen = BitmapFactory.DecodeByteArray(losbits, 0, losbits.Length);

            RunOnUiThread(() =>imagenview.SetImageBitmap(clasesettings.getRoundedShape( imagen)));
            RunOnUiThread(() => fondo.SetImageBitmap(clasesettings.CreateBlurredImageformbitmap(this,20, imagen)));
            RunOnUiThread(() => animar4(imagenview));

            }
            catch(Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "Disculpe no se pudo cargar la informacion del video por favor recargue la pestaña", ToastLength.Long).Show());
            }


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
            cliente.Client.Disconnect(false);
            base.Finish();
            clasesettings.recogerbasura();
        }
        public void animar4(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }
    }
}