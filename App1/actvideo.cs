using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.IO;
using System.Net.Sockets;
using Android.Util;
using System.Threading;
using System.Net.Http;
using YoutubeSearch;
using System.Text.RegularExpressions;
using VideoLibrary;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class actvideo : Activity, ISurfaceHolderCallback
    {
     


      bool  buscando = false;
        bool    parar = false;
        int porciento = 0;
        public bool videofullscreen = false;
        public List<Videosimage> listaimagen = new List<Videosimage>();
        public List<string> playlistas = new List<string>();
        public List<string> nombreses = new List<string>();
        public List<string> duracioneses = new List<string>();
        public List<string> linkeses = new List<string>();
        public List<Android.Graphics.Bitmap> imageneses = new List<Bitmap>();
        public string termino = "";
  
        public string downloadurlmp3 = "";
        public int index = 0;

      public   string linkactual = "";
        public SurfaceView video;
        public ImageView playpause;
        public LinearLayout ll1;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        public ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        public bool encontro = false;
        public ImageView portada;
        public ListView listabuscador;
        public ListView listaa;
        public TextView titulo;
        public EditText textbox;
        public bool menuabierto = false;
        public ImageView anterior;
        public ImageView siguiente;
        public ImageView adelantar;
        public ImageView atrazar;
        public int indiceactual = 0;
        public List<string> lapara = new List<string>();
        public List<string> laparalink = new List<string>();
        public bool detenedor = true;
        public bool primeravez = true;
        public SeekBar barraprogreso;
        public TcpClient clientesillo;
        public static actvideo instancia;
        LinearLayout layoutmenues;
        ImageView botonminimize;
        public bool videoenholder = false;

        public ISurfaceHolder holder;
        public static actvideo gettearinstacia()
        {
            return instancia;
        }



        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum]Format formato, int klk, int klk2)
        {

        }
        public void SurfaceCreated(ISurfaceHolder holder)
        {

            Clouding_service.gettearinstancia().musicaplayer.SetDisplay(holder);
            videoenholder = true;
        }
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Clouding_service.gettearinstancia().musicaplayer.SetDisplay(null);
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutvideo);
            instancia = this;
               linkactual= Intent.GetStringExtra("link");
            indiceactual = int.Parse(Intent.GetStringExtra("posactual"));
            clientesillo = new TcpClient("localhost", 1024);
            video = FindViewById<SurfaceView>(Resource.Id.videoView1);
            playpause = FindViewById<ImageView>(Resource.Id.imageView4);
         
            siguiente = FindViewById<ImageView>(Resource.Id.imageView3);
          var ll4 = FindViewById<LinearLayout>(Resource.Id.linearLayout9);
            var listviewlistareproduccion = FindViewById<ListView>(Resource.Id.listView5);
          listabuscador = FindViewById<ListView>(Resource.Id.listView2);
            var botonlistareproduccion = FindViewById<ImageView>(Resource.Id.imageView20);
            anterior = FindViewById<ImageView>(Resource.Id.imageView5);
            playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
            var botonmenu = FindViewById<ImageView>(Resource.Id.imageView7);
            var botonfastsearcher = FindViewById<ImageView>(Resource.Id.imageView13);
            var terminobuscarrapido = FindViewById<EditText>(Resource.Id.edittext2);
            var botonenfastsearcher = FindViewById<ImageView>(Resource.Id.imageView19);
         listaa = FindViewById<ListView>(Resource.Id.listView1);
            botonminimize = FindViewById<ImageView>(Resource.Id.botonminimize);
    
            var activarmenu = FindViewById<ImageView>(Resource.Id.imageView12);
           
             layoutmenues= FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            var botonyoutube = FindViewById<ImageView>(Resource.Id.imageView11);
            ll1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            var ll3 = FindViewById<LinearLayout>(Resource.Id.laprra);
            textbox = FindViewById<EditText>(Resource.Id.editText1);
            adelantar = FindViewById<ImageView>(Resource.Id.imageView2);
     var layoutbuscador = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
           atrazar = FindViewById<ImageView>(Resource.Id.imageView6);
            portada = FindViewById<ImageView>(Resource.Id.imageView8);
            titulo = FindViewById<TextView>(Resource.Id.textView1);
            barraprogreso = FindViewById<SeekBar>(Resource.Id.progressBar1);
          listaa.Visibility = ViewStates.Invisible;
            listviewlistareproduccion.Visibility = ViewStates.Invisible;
            holder = video.Holder;
            holder.AddCallback(this);
     
            string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");

            titulo.Selected = true;
            playlistas.Clear();
         
            for (int i = 0; i < items.Length; i++)

            {
            
                playlistas.Add(System.IO.Path.GetFileNameWithoutExtension(items[i]));
            }
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, playlistas);

            var parcelable = listviewlistareproduccion.OnSaveInstanceState();

            listviewlistareproduccion.Adapter = adaptadol;
            listviewlistareproduccion.OnRestoreInstanceState(parcelable);

            ll4.Visibility = ViewStates.Invisible;
            layoutbuscador.Visibility= ViewStates.Invisible;
            layoutmenues.Visibility = ViewStates.Invisible;

            ll1.SetBackgroundColor(Android.Graphics.Color.ParseColor("#323538"));
            ll3.SetBackgroundColor(Android.Graphics.Color.ParseColor("#323538"));
            ll4.SetBackgroundColor(Android.Graphics.Color.ParseColor("#323538"));
            layoutmenues.SetBackgroundColor(Android.Graphics.Color.ParseColor("#323538"));
            listviewlistareproduccion.SetBackgroundColor(Android.Graphics.Color.ParseColor("#323538"));
            layoutbuscador.SetBackgroundColor(Android.Graphics.Color.ParseColor("#323538"));
            listaa.SetBackgroundColor(Android.Graphics.Color.ParseColor("#323538"));

            mainmenu_Offline.gettearinstancia().envideo = true;
            botonminimize.Visibility = ViewStates.Gone;
            if (linkactual.Trim().Length > 1) {
                new Thread(() =>
                {
                   
                    buscaryreproducir();
                }).Start();
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso.SetCanceledOnTouchOutside(false);
            dialogoprogreso.SetCancelable(false);
            dialogoprogreso.SetTitle("Cargando...");
            dialogoprogreso.SetMessage("Por favor espere");
            dialogoprogreso.Show();
            }
            else
            {
                primeravez = false;
            }

            ll1.BringToFront();

            ll1.BringToFront();
          
            ll1.BringToFront();


            Thread proces = new Thread(new ThreadStart(cargardesdecache));
            proces.Start();
  
            layoutbuscador.Click += delegate
            {

            };
          
            layoutmenues.Click += delegate
            {

            };
            ll4.Click += delegate
            {

            };
            ll1.Click += delegate
            {

            };
            ll3.Click += delegate
            {

            };

            textbox.KeyPress += (aaxx, e) =>
            {
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    // Code executed when the enter key is pressed down

                    if (buscando == false)
                    {
                     

                        clasesettings.recogerbasura();
                        Thread process = new Thread(new ThreadStart(buscar));
                        process.Start();
                    }

                }
                else
                {
                    e.Handled = false;
                }
            };

            listviewlistareproduccion.ItemClick += (delegado, aasdsa) =>
            {
                if (playlistas.Count > 0) {
                if (File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" +playlistas[aasdsa.Position]).Split('$')[0].Split(';').ToList().Count >= 1 && File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + playlistas[aasdsa.Position]).Split('$')[0].Split(';')[0].Trim() != "")
                {
                    Intent internado = new Intent(this, typeof(Reproducirlistadialog));
                    internado.PutExtra("ip", "localhost");
                    internado.PutExtra("nombrelista", playlistas[aasdsa.Position]);
                    internado.PutExtra("index", aasdsa.Position.ToString());
                    StartActivity(internado);
                }
                else
                {
                    Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                }
                }

            };
            botonlistareproduccion.Click += delegate
            {
                animar(botonlistareproduccion);
                if (listviewlistareproduccion.Visibility == ViewStates.Visible)
                {

                    listviewlistareproduccion.Visibility = ViewStates.Invisible;
                }
                else
                {
                    listviewlistareproduccion.BringToFront();
                    listviewlistareproduccion.Visibility = ViewStates.Visible;
                    ll4.Visibility = ViewStates.Invisible;
                    listaa.Visibility = ViewStates.Invisible;
                    layoutbuscador.Visibility = ViewStates.Invisible;
                    animar2(listviewlistareproduccion);
                 
                }
               
            };

            botonenfastsearcher.Click += delegate
            {
                animar(botonenfastsearcher);
             
                    termino = terminobuscarrapido.Text;
                    new Thread(() => { buscaryabrir(); }).Start();
                    Toast.MakeText(this, "Buscando resultados...", ToastLength.Long).Show();
                
               

            };


            botonfastsearcher.Click += delegate
            {
                animar(botonfastsearcher);
                if (ll4.Visibility == ViewStates.Visible)
                {

                    ll4.Visibility = ViewStates.Invisible;
                }
                else
                {
                    ll4.Visibility = ViewStates.Visible;
                    listaa.Visibility = ViewStates.Invisible;
                    layoutbuscador.Visibility = ViewStates.Invisible;
                    listviewlistareproduccion.Visibility = ViewStates.Invisible;
                    animar2(ll4);
                }

            };
      
           
            listabuscador.ItemClick += (easter, sender) =>
             {
                 if (linkeses.Count > 0)
                 {
                     if (sender.Position >= 0)
                     {

                         Intent intentar = new Intent(this, typeof(customdialogact));

                         intentar.PutExtra("ipadress", "localhost");
                      
                         intentar.PutExtra("imagen", @"https://i.ytimg.com/vi/" + linkeses[sender.Position].Split('=')[1] + "/hqdefault.jpg");
                         intentar.PutExtra("url", linkeses[sender.Position]);
                         intentar.PutExtra("titulo", nombreses[sender.Position]);
                         intentar.PutExtra("color", "DarkGray");
                         StartActivity(intentar);
                     }
                 }
             };

            Clouding_service.gettearinstancia().musicaplayer.Completion += delegate
            {
                if (this != null)
                {
                    playpause.SetBackgroundResource(Resource.Drawable.playbutton2);
                }

                mainmenu_Offline.gettearinstancia().siguiente();
              ///  clientesillo.Client.Send(Encoding.Default.GetBytes("next()"));
            };

            Thread proc = new Thread(new ThreadStart(ponerporciento));
            proc.Start();
            Thread proc2 = new Thread(new ThreadStart(receptor));
            proc2.Start();
            activarmenu.Click += delegate
            {
                animar(activarmenu);
                if (layoutmenues.Visibility == ViewStates.Visible)
                {
                    menuabierto = false;
                    layoutbuscador.Visibility = ViewStates.Invisible;
                    listaa.Visibility = ViewStates.Invisible;
                    animar7(layoutmenues);
                    ll4.Visibility = ViewStates.Invisible;
                    listviewlistareproduccion.Visibility = ViewStates.Invisible;
                }
                else
                {
                    menuabierto = true;
                    layoutmenues.Visibility = ViewStates.Visible;
                    animar3(layoutmenues);
                }
            };

            botonyoutube.Click += delegate
            {
               
                animar(botonyoutube);
                if (layoutbuscador.Visibility == ViewStates.Visible)
                {
                 
                    layoutbuscador.Visibility = ViewStates.Invisible;
                }
                else
                {
                    listviewlistareproduccion.Visibility = ViewStates.Invisible;
                    ll4.Visibility = ViewStates.Invisible;
                    listaa.Visibility = ViewStates.Invisible;
                    layoutbuscador.Visibility = ViewStates.Visible;
                    animar2(layoutbuscador);
                }
            };
            listaa.ItemClick += (sender, easter) =>
            {

                if (lapara.Count > 0) { 
                    Intent intentoo = new Intent(this, typeof(deletedialogact));

                intentoo.PutExtra("index", easter.Position.ToString());
                intentoo.PutExtra("color", "DarkGray");
                intentoo.PutExtra("titulo", lapara[easter.Position]);
                intentoo.PutExtra("ipadress","localhost");
                intentoo.PutExtra("url", laparalink[easter.Position]);
                intentoo.PutExtra("imagen", @"https://i.ytimg.com/vi/" + laparalink[easter.Position].Split('=')[1] + "/hqdefault.jpg");
                StartActivity(intentoo);
                }
            };
            botonminimize.Click += delegate
            {
                if (menuabierto)
                {
                    layoutmenues.Visibility = ViewStates.Visible;
                }
                else
                {
                    layoutmenues.Visibility = ViewStates.Invisible;
                }
                botonminimize.Visibility = ViewStates.Gone;
                ll1.Visibility = ViewStates.Visible;
                ll3.Visibility = ViewStates.Visible;
                videofullscreen = false;
            };
            siguiente.Click += delegate
            {
                animar(siguiente);
                clientesillo.Client.Send(Encoding.Default.GetBytes("next()"));
            };
            anterior.Click += delegate
            {
                animar(anterior);
                clientesillo.Client.Send(Encoding.Default.GetBytes("back()"));
            };

         
          
            atrazar.Click += delegate
            {
                animar(atrazar);
                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition - Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
            };
            adelantar.Click += delegate
            {
                animar(adelantar);
                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition + Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
            };
             

            botonmenu.Click += delegate
            {
                animar(botonmenu);
                listaa.BringToFront();
                if (listaa.Visibility == ViewStates.Visible)
                {
                 
                    listaa.Visibility = ViewStates.Invisible;

                }
                else
                {
                    listviewlistareproduccion.Visibility = ViewStates.Invisible;
                    ll4.Visibility = ViewStates.Invisible;
                    layoutbuscador.Visibility= ViewStates.Invisible;
                    listaa.Visibility = ViewStates.Visible;
                    animar2(listaa);
                }
                listaa.BringToFront();
            };
            barraprogreso.ProgressChanged += (aa, aaaa) =>
            {
                if (aaaa.FromUser)
                {
                    Clouding_service.gettearinstancia().musicaplayer.SeekTo(aaaa.Progress);
                }
            };
           
            video.Touch += (aa, aaaa)=>
            {
                if (aaaa.Event.Action == MotionEventActions.Up)
                {
                    if (!videofullscreen)
                    {
                        ll1.Visibility = ViewStates.Invisible;
                        listviewlistareproduccion.Visibility = ViewStates.Invisible;
                        ll3.Visibility = ViewStates.Invisible;
                        layoutbuscador.Visibility = ViewStates.Invisible;
                        listaa.Visibility = ViewStates.Invisible;
                        layoutmenues.Visibility= ViewStates.Invisible;
                        ll4.Visibility = ViewStates.Invisible;
                        botonminimize.Visibility = ViewStates.Visible;
                        videofullscreen = true;
                    }
                    else
                    {
                        if (menuabierto) {
                            layoutmenues.Visibility = ViewStates.Visible;
                        } else
                        {
                            layoutmenues.Visibility = ViewStates.Invisible;
                        }
                        botonminimize.Visibility = ViewStates.Gone;
                        ll1.Visibility = ViewStates.Visible;
                        ll3.Visibility = ViewStates.Visible;
                        videofullscreen = false ;
                    }
                }
            };
           
            // Create your application here
            playpause.Click += delegate
            {
                animar(playpause);
                if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying == true)
                {
                    playpause.SetBackgroundResource(Resource.Drawable.playbutton2);

                    Clouding_service.gettearinstancia().musicaplayer.Pause();
                }
                else

                {

                    playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
                    Clouding_service.gettearinstancia().musicaplayer.Start();

                }
            };
        }

       
        public void buscaryreproducir(bool invoked=false)
        {


            /*
                     var   videoinfoss = DownloadUrlResolver.GetDownloadUrls(linkactual, false);

                        VideoInfo video2 = videoinfoss.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);
                        VideoInfo video3 = videoinfoss.First(info => info.VideoType == VideoType.Mp4 && info.Resolution ==0);
                        */

            if (!mainmenu_Offline.gettearinstancia().contienevideo || invoked) { 
             //////////////////////////////////////////////////////cuando no es un video
            var videito = Client.For(YouTube.Default);
            var videoo = videito.GetAllVideosAsync(linkactual);
            var resultados = videoo.Result;
            var title = resultados.First().Title.Replace("- YouTube", "");
            string video2 = resultados.First(info => info.Resolution == 360 && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;

            string video3 = "";
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                video3 = resultados.First(info => info.Resolution == -1 && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
            }
            else
            {
                video3 = resultados.First(info => info.Resolution == 240 && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
            }
     
          //clasesettings.guardarsetting("cquerry", "data()>" + title.Replace('>', ' ') + ">" + linkactual.Replace('>', ' '));
            string imagensilla = "https://i.ytimg.com/vi/" + linkactual.Split('=')[1] + "/mqdefault.jpg";
          var  imgurl = imagensilla;
            downloadurlmp3 = video3;
            RunOnUiThread(() => portada.SetImageBitmap(GetImageBitmapFromUrl(imagensilla)));

            RunOnUiThread(() => {
                titulo.Text = title; 
               /// video.SetFitsSystemWindows(true);
              
               //Clouding_service.gettearinstancia().musicaplayer.SetVideoURI(Android.Net.Uri.Parse(video2));


                Clouding_service.gettearinstancia().musicaplayer.Reset();


                Clouding_service.gettearinstancia().musicaplayer = Android.Media.MediaPlayer.Create(this, Android.Net.Uri.Parse(video2));

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                Clouding_service.gettearinstancia().musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                Clouding_service.gettearinstancia().musicaplayer.SetWakeMode(this, WakeLockFlags.Partial);
                Clouding_service.gettearinstancia().musicaplayer.SetDisplay(null);
                Clouding_service.gettearinstancia().musicaplayer.SetDisplay(holder); 
                Clouding_service.gettearinstancia().musicaplayer.Prepared += delegate
                {
                    if (clasesettings.gettearvalor("progresovideoactual") != "")
                    {
                        Clouding_service.gettearinstancia().musicaplayer.SeekTo(int.Parse(clasesettings.gettearvalor("progresovideoactual")));
                        clasesettings.guardarsetting("progresovideoactual", "");
                    }
                 
                };

                Clouding_service.gettearinstancia().musicaplayer.Completion += delegate
                 {
                     mainmenu_Offline.gettearinstancia().RunOnUiThread(() =>
                     {
                         mainmenu_Offline.gettearinstancia().adelante.PerformClick();
                     });
                 };
                Clouding_service.gettearinstancia().musicaplayer.Start();

                // video.RequestFocus();

                playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
                ll1.BringToFront();
                encontro = true;
                if (primeravez)
                {
                    dialogoprogreso.Dismiss();
                    primeravez = false;
                }
          
           

            });

            }
            else {


                ////////////////////////////////////////////////////////////////cuando ya es un video de por si :V
                Clouding_service.gettearinstancia().musicaplayer.Prepare();
                RunOnUiThread(() => {
                 mainmenu_Offline.gettearinstancia().RunOnUiThread(()=> {

                     titulo.Text = mainmenu_Offline.gettearinstancia().label.Text;
                });
               portada.SetImageBitmap(GetImageBitmapFromUrl("https://i.ytimg.com/vi/" + mainmenu_Offline.gettearinstancia().linkactual.Split('=')[1] + "/mqdefault.jpg"));

                playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
                /// video.SetFitsSystemWindows(true);

                //Clouding_service.gettearinstancia().musicaplayer.SetVideoURI(Android.Net.Uri.Parse(video2));


            
                Clouding_service.gettearinstancia().musicaplayer.SetDisplay(holder);
                Clouding_service.gettearinstancia().musicaplayer.Start();

           /*     Clouding_service.gettearinstancia().musicaplayer.Completion += delegate
                {
                    mainmenu_Offline.gettearinstancia().RunOnUiThread(() =>
                    {
                        mainmenu_Offline.gettearinstancia().adelante.PerformClick();
                    });
                };*/
           //  Clouding_service.gettearinstancia().musicaplayer.Start();

                // video.RequestFocus();

                playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
                ll1.BringToFront();
                encontro = true;
                if (primeravez)
                {
                    dialogoprogreso.Dismiss();
                    primeravez = false;
                }



            });
            }

        }
        public void animar(Java.Lang.Object imagen)
        {
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();
        }

        public void animar3(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }

        public void animar2(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }
        public void animar7(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 0, 1000);
            animacion.SetDuration(500);
            animacion.Start();
            animacion.AnimationEnd += delegate
            {
                layoutmenues.Visibility = ViewStates.Invisible;
            };
          
        }
        public void ponerporciento()
        {
            while (detenedor)
            {

      
            if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
            {

        
            RunOnUiThread(() => {
                barraprogreso.Max = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.Duration );
                barraprogreso.Progress = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition);
                 
            });
            }
                Thread.Sleep(1000);
            }
        }

        public void receptor()
        {

            while (detenedor)
            {

                if (mainmenu_Offline.gettearinstancia().listacaratulas.Except(lapara).ToList().Count>0) { 
              
             
                    lapara = mainmenu_Offline.gettearinstancia().listacaratulas;
                    laparalink = mainmenu_Offline.gettearinstancia().laparalinks;

                    var adaptadol = new adapterlistaremoto(this, lapara, laparalink, mainmenu_Offline.gettearinstancia().linkactual );
                  
                        RunOnUiThread(() => listaa.Adapter = adaptadol);
                if (lapara.Count == 0)
                {
                    var adaptadolllll = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                    RunOnUiThread(() => listaa.Adapter = adaptadolllll);
                }

            

                /*    RunOnUiThread(()=>  listaa.Adapter = adaptadol);
                      new Thread(() => { 
                          foreach(string perro in lapara)
                          {
                          if (perro.StartsWith(">") && perro.EndsWith("<"))
                          {
                              if (indiceactual != lapara.IndexOf(perro) || ( indiceactual==0 && lapara.IndexOf(perro)==0 && !video.IsPlaying)) { 
                                  indiceactual = lapara.IndexOf(perro);
                                  linkactual = laparalink[indiceactual];
                                  buscaryreproducir();
                                  }
                              }
                          }
                          }
                       ).Start();
                      */

                RunOnUiThread(()=>  ll1.BringToFront());
                }
                Thread.Sleep(5000);
            }

        }
        public override void OnBackPressed()
        {
          //  Toast.MakeText(this, "espere mientras carga el audio...", ToastLength.Long).Show();
            this.Finish();
           
            base.OnBackPressed();
        }
        public override void Finish()
        {
           
            detenedor = false;
          
            clientesillo.Client.Disconnect(false);
            /*  if (linkactual.Trim() != "")
              {
                  mainmenu_Offline.gettearinstancia().reproducir(downloadurlmp3);
              }
              else
              {

              }*/

            if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
            {
                mainmenu_Offline.gettearinstancia().RunOnUiThread(() =>
                {
                    mainmenu_Offline.gettearinstancia().play.SetBackgroundResource(Resource.Drawable.pausebutton2);
                });
                    
                    
                  
            }
            mainmenu_Offline.gettearinstancia().envideo = false;
     
            clasesettings.recogerbasura();
            Clouding_service.gettearinstancia().musicaplayer.SetDisplay(null);
         
            base.Finish();
        }


        public void buscar()
        {
            buscando = true;
            parar = true;
            

       
            listaimagen.Clear();
            nombreses.Clear();
            linkeses.Clear();
            duracioneses.Clear();
            VideoSearch buscavideos = new VideoSearch();
         //   RunOnUiThread(() => Toast.MakeText(this, "Espere mientras se buscan resultados...", ToastLength.Long).Show());





            index = 0;
            WebClient clienteee = new WebClient();
            RunOnUiThread(() =>
            {
       #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso = new ProgressDialog(this);
       #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
          #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Buscando resultados...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
            });

            try
            {
              var aa = buscavideos.SearchQuery(textbox.Text, 2);

               
                foreach (var ec in aa)
                {

                    if (parar == true)
                    {
              
                        nombreses.Add(WebUtility.HtmlDecode( RemoveIllegalPathCharacters(ec.Title.Replace("&quot;", "").Replace("&amp;", ""))));
                        /*
                          Byte[] biteimagen =  clienteee.DownloadData(ec.Thumbnail);

                          using (MemoryStream memoria = new MemoryStream(biteimagen))
                          {
                              Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeStream(memoria);
                              video.imagen = imagen;
                              videoimagen.imagen = imagen;
                          }
                          */


                        //  Byte[] biteimagen = clienteee.DownloadData(ec.Thumbnail);



                        //   Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeByteArray(biteimagen, 0, biteimagen.Length);
                        //   var aah = File.Create(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + index);
                        //   aah.Write(biteimagen, 0, biteimagen.Length);
                        //    aah.Close();

                        // biteimagen = new byte[0];

                        linkeses.Add(ec.Url);
                        duracioneses.Add(ec.Duration);
                       








                        index++;
                     

                    }
                    //  ArrayAdapter<string> adaptadorvids = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaimagen);




                }
              
                buscando = false;
                parar = false;
                RunOnUiThread(() => dialogoprogreso.Dismiss());
                Thread proc = new Thread(new ThreadStart(enthread));
                proc.Start();


            }
            catch (Exception)
            {
              RunOnUiThread(()=>  dialogoprogreso.Dismiss());
                RunOnUiThread(() => Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                parar = false;
                buscando = false;
            }
        }

        public void enthread()
        {
            try
            {
                imageneses.Clear();
              
         
                if (!buscando)
                {
                    var asdd = listabuscador.FirstVisiblePosition;
                    adapterlistaremoto adaltel = new adapterlistaremoto(this, nombreses, linkeses, null);
                    RunOnUiThread(() => listabuscador.Adapter = adaltel);
                    RunOnUiThread(() => listabuscador.SetSelection(asdd));
                    if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3"))
                    {
                        File.Delete(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");
                    }
                    var ee = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");

                    string todosnombres = String.Join("¹", nombreses.ToArray());
                    string todoslinks = string.Join("¹", linkeses.ToArray());
                    string todasduraciones = string.Join("¹", duracioneses.ToArray());
                    ee.Write(todosnombres + "²" + todoslinks + "²"+todoslinks+ "²" +todasduraciones);
                    ee.Close();
                }


            }


            catch (Exception)
            {
                var asdd = listabuscador.FirstVisiblePosition;
                adapterlistaremoto adaltel = new adapterlistaremoto(this, nombreses, linkeses, null);
                RunOnUiThread(() => listabuscador.Adapter = adaltel);
                RunOnUiThread(() => listabuscador.SetSelection(asdd));
            }
        }
        public Bitmap getRoundedShape(Bitmap scaleBitmapImage)
        {
            try
            {
                int targetWidth = 110;
                int targetHeight = 110;
                Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                    targetHeight, Bitmap.Config.Argb4444);

                Canvas canvas = new Canvas(targetBitmap);
                Android.Graphics.Path path = new Android.Graphics.Path();
                path.AddCircle(((float)targetWidth - 1) / 2,
                    ((float)targetHeight - 1) / 2,
                    (Math.Min(((float)targetWidth),
                        ((float)targetHeight)) / 2),
                    Android.Graphics.Path.Direction.Ccw);

                canvas.ClipPath(path);
                Bitmap sourceBitmap = scaleBitmapImage;
                canvas.DrawBitmap(sourceBitmap,
                    new Rect(0, 0, sourceBitmap.Width,
                        sourceBitmap.Height),
                    new Rect(0, 0, targetWidth, targetHeight), null);
                return targetBitmap;
            }
            catch (Exception)
            {
                return scaleBitmapImage;
            }
           
          
        }
        public void cargardesdecache()
        {
            try
            {
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3"))
                {

               
                    listaimagen.Clear();
                    nombreses.Clear();
                    imageneses.Clear();
                    linkeses.Clear();
                    duracioneses.Clear();
                    var asdsa = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");

                    nombreses = asdsa.Split('²')[0].Split('¹').ToList();
                    linkeses = asdsa.Split('²')[1].Split('¹').ToList();
                    duracioneses = asdsa.Split('²')[3].Split('¹').ToList();
                    adapterlistaremoto adaltel = new adapterlistaremoto(this, nombreses, linkeses, null);
                    RunOnUiThread(() => listabuscador.Adapter = adaltel);
                    if (nombreses.Count == 0)
                    {
                        var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                        RunOnUiThread(() => listabuscador.Adapter = adaptadolo);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }

        public void buscaryabrir()
        {
            try { 
            string url = getearurl(termino);
         
            if (url != "%%nulo%%")
            {


                var a = clasesettings.gettearvideoid(url, true,-1);
              
                Intent intentar = new Intent(this, typeof(customdialogact));

                RunOnUiThread(() => {
                    intentar.PutExtra("ipadress", "localhost");
                    intentar.PutExtra("imagen", "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg");
                    intentar.PutExtra("url", url);
                    intentar.PutExtra("titulo", a.titulo);
                    intentar.PutExtra("color", "DarkGray");
                    StartActivity(intentar);
                   
                });
               
            }

            }
            catch (Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "No se encontraron resultados", ToastLength.Long).Show());

            }

        }
        public string getearurl(string titttt)
        {

            try
            {

                string linkkk = "";
                string prra = "";
                bool esunlink = false;
                try
                {
                    var dfasdsf = titttt.Split('=')[1];
                    esunlink = true;
                }
                catch (Exception)
                {

                }
                if (!esunlink)
                {


                    HttpClient cliente = new HttpClient(new ModernHttpClient.NativeMessageHandler());
                    var retorno = cliente.GetStringAsync("https://decapi.me/youtube/videoid?search=" + titttt);
                    prra = retorno.Result;


                    linkkk = "http://www.youtube.com/watch?v=" + retorno.Result;
                }
                else
                {
                    linkkk = "http://www.youtube.com/watch?v=" + titttt.Split('=')[1];
                }


                if (prra.ToLower().Trim() == "invalid video id or search string." && !esunlink)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                    return "%%nulo%%";
                }
                else
                {
                    return linkkk;
                }

            }
            catch (Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                return "%%nulo%%";
            }

        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {

            Bitmap imageBitmap = null;
            try
            {


                if (url.Trim() != "")
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                            RunOnUiThread(() => portada.SetImageBitmap(imageBitmap));
                        }

                    }

            }
            catch (Exception) { RunOnUiThread(() => portada.SetImageBitmap(null)); }
            return imageBitmap;
        }


        public void Onresume()
        {
         
            Clouding_service.gettearinstancia().musicaplayer.SeekTo(porciento);
            base.OnResume();
        }
        protected override void OnDestroy()
        {

            if (mainmenu_Offline.gettearinstancia() != null) {
                mainmenu_Offline.gettearinstancia().contienevideo = true;
                
                }
            base.OnDestroy();
        }

        public void Onpause()
        {
            
            Clouding_service.gettearinstancia().musicaplayer.Pause();
            porciento = Clouding_service.gettearinstancia().musicaplayer.CurrentPosition;
            base.OnPause();
        }
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        
         
            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
          
                Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
              
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {

                Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
           
             
            }
        }

    }  
}