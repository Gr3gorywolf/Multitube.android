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
using App1.Utils;

namespace App1
{
    [Activity(Label = "Abrir con multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize,Theme = "@style/Theme.UserDialog")]
    [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "text/plain")]
    public class customdialogact : Activity
    {
        public string imagem;

       public ImageView imagenview;
        public ImageView fondo;
        public Thread proc;
        LinearLayout buscar;
        string url;
        bool buscando = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Agregardialog);
            ProgressBar barra = FindViewById<ProgressBar>(Resource.Id.progressBar1);
          TextView texto= FindViewById<TextView>(Resource.Id.textView1);
            imagenview = FindViewById<ImageView>(Resource.Id.imageView1);
            LinearLayout descargar = FindViewById<LinearLayout>(Resource.Id.imageView5);
            LinearLayout addlista = FindViewById<LinearLayout>(Resource.Id.imageView6);
            LinearLayout agregar = FindViewById<LinearLayout>(Resource.Id.imageView2);
             buscar = FindViewById<LinearLayout>(Resource.Id.imageView3);
            ImageView volver = FindViewById<ImageView>(Resource.Id.imageView4);
            fondo= FindViewById<ImageView>(Resource.Id.fondo1);
            LinearLayout lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            LinearLayout lineaa = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            buscar.Visibility = ViewStates.Visible;
            //  animar2(lineaa);
            texto.Selected = true;




          
            string titulo = "";
            texto.Text = "";
            this.SetFinishOnTouchOutside(true);






            //receiver from intent

            var intentoo = Intent;
            var share = intentoo.Action;
            var tipo = intentoo.Type;
          
                if (Intent.ActionSend.Equals(share))
                {
                    if (tipo.Contains("text/plain"))
                    {
                        string krecibio = intentoo.GetStringExtra(Intent.ExtraText);
                        if (krecibio.Contains("youtu.be") || krecibio.Contains("youtube.com"))
                        {
                            url = "https://www.youtube.com/watch?v=" + krecibio.Split('/')[3];
                            new Thread(() =>
                            {
                             
                               buscando = true;
                                proc = new Thread(new ThreadStart(ponerimagen));
                                proc.Start();
                                titulo = VideosHelper.GetVideoTitle(url);
                               
                                buscando = false;
                                RunOnUiThread(() => {
                                    texto.Text = titulo;
                                    barra.Visibility = ViewStates.Gone;
                                });
                            }).Start();
                        }
                        else
                        {
                            RunOnUiThread(() => barra.Visibility = ViewStates.Gone);
                            Toast.MakeText(this, "Este enlace no proviene de youtube", ToastLength.Long).Show();
                            this.Finish();
                        }



                    }
                    else
                    {
                        RunOnUiThread(() => barra.Visibility = ViewStates.Gone);
                        Toast.MakeText(this, "Este enlace no proviene de youtube", ToastLength.Long).Show();
                        this.Finish();
                    }
                }
                else
                {
                    RunOnUiThread(() => barra.Visibility = ViewStates.Gone);
                    titulo = Intent.GetStringExtra("titulo");
                    texto.Text = titulo;
                    url = Intent.GetStringExtra("url");
                    url = "https://www.youtube.com/watch?v=" + url.Split('=')[1];
                    imagem = Intent.GetStringExtra("imagen");
                    proc = new Thread(new ThreadStart(ponerimagen));
                    proc.Start();
                }
         








            //    animar2(lineall2);

           
            addlista.Click += delegate
            {
                animar(addlista);
                if (!buscando) { 
                   
                Intent intentar = new Intent(this, typeof(actividadagregarlistahecha));
                intentar.PutExtra("nombrevid", titulo);
                intentar.PutExtra("linkvid", url);
                StartActivity(intentar);
                this.Finish();
               
                   
                }
                else {
                    Toast.MakeText(this, "Aun se esta buscando la info del video", ToastLength.Long).Show();
                }
            };
            volver.Click += delegate
             {
                 animar(volver);

              
                
                 this.Finish();

             };
            descargar.Click += delegate
            {
                animar(descargar);
                if (!buscando)
                {
                    Intent intento = new Intent(this, typeof(actdownloadcenterofflinedialog));
                    intento.PutExtra("ip", "localhost");
                    intento.PutExtra("zelda", url);
                    intento.PutExtra("color","Black");
                    StartActivity(intento);
                    this.Finish();
                }
                else
                {
                    Toast.MakeText(this, "Aun se esta buscando la info del video", ToastLength.Long).Show();
                }

            };
            agregar.Click += delegate
            {
                animar(agregar);
                if (!buscando)
                {
                    if (Mainmenu.gettearinstancia() != null || MainmenuOffline.gettearinstancia() != null)
                    {
                        if (MainmenuOffline.gettearinstancia() == null)
                    {
                        Mainmenu.gettearinstancia().clientela.Client.Send(Encoding.Default.GetBytes("agregar()"));
                        Thread.Sleep(250);
                        Mainmenu.gettearinstancia().clientela.Client.Send(Encoding.Default.GetBytes(url));
                    }
                    else
                    {
                        new Thread(() =>
                        {
                            MainmenuOffline.gettearinstancia().agregarviddireckt(url, titulo);
                        }).Start();

                    }
                        this.Finish();
                    }
                    else
                        Toast.MakeText(this, "No esta conectado a ningun servidor ni tiene la aplicacion abierta en modo de reproductor online", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Aun se esta buscando la info del video", ToastLength.Long).Show();
                }
            };
            buscar.Click += delegate
            {
                animar(buscar);
                if (!buscando)
                {
                    if (Mainmenu.gettearinstancia() != null || MainmenuOffline.gettearinstancia() != null)
                    {
                        if (MainmenuOffline.gettearinstancia() == null)
                        {
                            if (Mainmenu.gettearinstancia().clientela.Connected == true)
                            {
                                Mainmenu.gettearinstancia().clientela.Client.Send(Encoding.Default.GetBytes(url));


                            }
                        }
                        else
                        {
                            new Thread(() =>
                            {
                                MainmenuOffline.gettearinstancia().buscarviddireckt(url, false);
                            }).Start();

                        }
                        this.Finish();
                    }
                    else
                        Toast.MakeText(this, "No esta conectado a ningun servidor ni tiene la aplicacion abierta en modo de reproductor online", ToastLength.Long).Show();
                    }
                else
                {
                    Toast.MakeText(this, "Aun se esta buscando la info del video", ToastLength.Long).Show();
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
          byte[]losbits=  clienteweb.DownloadData("http://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg");
            Bitmap imagen = BitmapFactory.DecodeByteArray(losbits, 0, losbits.Length);

            RunOnUiThread(() =>imagenview.SetImageBitmap(ImageHelper.GetRoundedShape( imagen)));
                RunOnUiThread(() => fondo.SetImageBitmap(imagen));
                RunOnUiThread(() => animar4(imagenview));
            }
            catch(Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "Disculpe no se pudo cargar la informacion del video por favor abra y cierre la ventana", ToastLength.Long).Show());
            }


        }

        public void animar4(Java.Lang.Object imagen)
        {

         /*   Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(400);
            animacion.Start();*/
          /*  animacion.AnimationEnd += delegate
            {

               
            };*/
           // animarxd(buscar);
        }

        public void animarxd(Java.Lang.Object imagen)
        {
            var ax = (ImageView)imagen;
            ax.Visibility = ViewStates.Visible;
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0f, 1f);
            animacion.SetDuration(400);
            animacion.Start();
            Android.Animation.ObjectAnimator animacionx = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleY", 0f, 1f);
            animacionx.SetDuration(400);
            animacionx.Start();
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
        public override void Finish()
        {
   
            base.Finish();
            MultiHelper.ExecuteGarbageCollection();
        }





    }

    }