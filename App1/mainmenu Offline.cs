using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Appwidget;
using Android.Runtime;
using Android.Views;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Threading.Tasks;
using Android.Webkit;
using System.Collections.Generic;
using Android.Graphics;
using System.Net;
using System.Collections;
using Android.Speech;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using Android.Media.Session;
using Android.Renderscripts;
using Android.Media;
using Android.Graphics.Drawables;



namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState =true)]

    public class mainmenu_Offline : Activity,AudioManager.IOnAudioFocusChangeListener
    {


     
        public string downloadurrrl = "";
        public bool detenedor = true;
        public ListView lista;
        ArrayAdapter<string> adaptadol;
        public ImageView caratula2;
        public TextView label;
        public RelativeLayout lineall;
        public bool envideo = false;
        ImageView botonsincronizar;
        public string zelda = "";
        ImageView laimagen;
        public string colol = "DarkGray";
        public bool agregando = false;
        public int voz = 9;
        public EditText textbox;
        public LinearLayout lineall2;
        public int duracion = 0;
        public ImageView adelantar;
        public ImageView atrazar;
        public ImageView adelante;
        public ImageView atras;
        public int actual = 0;
        public int counter = 0;
        public long millis = 0;
        public SeekBar porcientoreproduccion;
        public bool buscando = false;
        public ImageView play;
        public TcpListener oidor;
        public bool fromotherinstance = false;
     //   public IEnumerable<VideoInfo> videoinfoss;
        public Android.Media.MediaPlayer musicaplayer;
        public List<string> lapara = new List<string>();
        public List<string> laparalinks = new List<string>();
        public List<string> listacaratulas = new List<string>();
        public List<TcpClient> clienteses = new List<TcpClient>();
        public SeekBar volumen;
        public string termino = "";
        bool bloquearporcentaje = false;
        public bool automated = true;
        public string ipadre = "";
        public int indiceactual = 0;
        public string imgurl = "";
        public int locanterior = 0;
        public int volumenactual = 100;
        public WebClient clientedelamusica = new WebClient();
        public string linkactual = "";
        ScrollView menuham;
        public static mainmenu_Offline instance;
        public ImageView fondo;
        public Bitmap fondoblurreado;
        broadcast_receiver br;
        public static mainmenu_Offline gettearinstancia()
        {
            return instance;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
                base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.perfectmainoffline5);
            instance = this;
           
            bool restaurada = false;
            if (savedInstanceState != null)
            {
                restaurada = savedInstanceState.ContainsKey("restaurado");
            }

            clasesettings.guardarsetting("onlineactivo", "si");
            /////////////////////////////////declaraciones//////////////////////////
            musicaplayer = new Android.Media.MediaPlayer();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipadre = ip.ToString();

                }
            }
         
                if (!restaurada)
                {
                    oidor = new TcpListener(IPAddress.Parse(ipadre), 1024);
                    oidor.Start();
             
                }














            ///////////////////////////////#Botones#////////////////////////////////
            menuham = FindViewById<ScrollView>(Resource.Id.linearLayout9);
            ImageView botonabrirmenu = FindViewById<ImageView>(Resource.Id.imageView22);
            TextView estadomenu = FindViewById<TextView>(Resource.Id.textView9);

            ImageView botonmostrarvolumen = FindViewById<ImageView>(Resource.Id.imageView21);
            LinearLayout layoutvolumen = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            ImageView listareprod = FindViewById<ImageView>(Resource.Id.imageView16);
            laimagen = FindViewById<ImageView>(Resource.Id.laimagen);

           atras = FindViewById<ImageView>(Resource.Id.imageView2);
           adelante = FindViewById<ImageView>(Resource.Id.imageView4);
             play = FindViewById<ImageView>(Resource.Id.imageView3);
          adelantar = FindViewById<ImageView>(Resource.Id.imageView5);
            atrazar = FindViewById<ImageView>(Resource.Id.imageView7);
            ImageView codigoqr = FindViewById<ImageView>(Resource.Id.imageView17);
            ImageView download = FindViewById<ImageView>(Resource.Id.imageView8);
            var botonbusqueda = FindViewById<ImageView>(Resource.Id.imageView20);
            ImageView agregar = FindViewById<ImageView>(Resource.Id.imageView11);
            ImageView escuchar = FindViewById<ImageView>(Resource.Id.imageView15);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout7);
            ImageView buscar = FindViewById<ImageView>(Resource.Id.imageView12);
            textbox = FindViewById<EditText>(Resource.Id.editText1);
            ImageView abrirbrowser = FindViewById<ImageView>(Resource.Id.imageView14);
            var botonvideo = FindViewById<ImageView>(Resource.Id.imageView19);
         //   lineall = FindViewById<RelativeLayout>(Resource.Id.linearLayout0);
            label = FindViewById<TextView>(Resource.Id.textView1);
            caratula2 = FindViewById<ImageView>(Resource.Id.imageView13);
            lista = FindViewById<ListView>(Resource.Id.listView1);
            volumen = FindViewById<SeekBar>(Resource.Id.seekBar2);
           botonsincronizar = FindViewById<ImageView>(Resource.Id.imageView18);
            porcientoreproduccion = FindViewById<SeekBar>(Resource.Id.seekBar1);
           var ll10 = FindViewById<LinearLayout>(Resource.Id.linearLayout10);
            var ll40 = FindViewById<LinearLayout>(Resource.Id.linearlayout40);
            var ll11 = FindViewById<LinearLayout>(Resource.Id.linearLayout11);
            var ll12 = FindViewById<LinearLayout>(Resource.Id.linearLayout12);
            var ll13 = FindViewById<LinearLayout>(Resource.Id.linearLayout13);
            var ll14 = FindViewById<LinearLayout>(Resource.Id.linearLayout14);
            var ll15 = FindViewById<LinearLayout>(Resource.Id.linearLayout15);
            var ll16 = FindViewById<LinearLayout>(Resource.Id.linearLayout16);
            fondo= FindViewById<ImageView>(Resource.Id.imageView45);
            var barra1 = FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            ////////////////////////////////////////////////////////////////////////
            lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
          //  lineall.SetBackgroundColor(Color.DarkGray);
            lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
           ll40.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            layoutvolumen.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            barra1.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            menuham.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            play.SetBackgroundResource(Resource.Drawable.pausebutton2);
            animar2(lineall2);
            clasesettings.guardarsetting("elementosactuales", "");
            volumen.Max = 100;
            volumen.Progress = volumenactual;
            layoutvolumen.Visibility = ViewStates.Invisible;
            menuham.Visibility = ViewStates.Invisible;
            estadomenu.Text = "";
            botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
            codigoqr.SetImageBitmap(GetQRCode());
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() =>lista.Adapter = adaptadol);
            if (!restaurada)
                {
                    Thread procccc = new Thread(new ThreadStart(cojerstream));
                    procccc.Start();
                    Thread proccccc = new Thread(new ThreadStart(receptor));
                    proccccc.Start();
                    new Thread(() =>
                    {
                       
                        iniciarservicio();
                        ponerporciento();
                    }).Start();

                }


             br = new broadcast_receiver();
            IntentFilter filtro = new IntentFilter(Intent.ActionMediaButton);
            filtro.Priority = 1000;
            RegisterReceiver(br, filtro);
            try
            {


                PlaybackState pbs = new PlaybackState.Builder()
                           .SetActions(PlaybackState.ActionFastForward | PlaybackState.ActionPause | PlaybackState.ActionPlay | PlaybackState.ActionPlayPause | PlaybackState.ActionSeekTo | PlaybackState.ActionSkipToNext | PlaybackState.ActionSkipToPrevious)
                           .SetState(PlaybackStateCode.Playing, 0, 1f, SystemClock.ElapsedRealtime())
                           .Build();
                MediaSession mSession = new MediaSession(this, PackageName);
                Intent intent = new Intent(this, typeof(broadcast_receiver));
                PendingIntent pintent = PendingIntent.GetBroadcast(this, 4564, intent, PendingIntentFlags.UpdateCurrent);
                mSession.SetMediaButtonReceiver(pintent);
                mSession.Active = (true);
                mSession.SetPlaybackState(pbs);
       
            }
            catch (Exception)
            {

            }
            new Thread(()=> {

                actualizarmediasesion();
            }).Start();
         

            label.Selected = true;
            WallpaperManager wm = WallpaperManager.GetInstance(this);
            Drawable d = wm.Drawable;
            fondoblurreado = clasesettings.CreateBlurredImageformbitmap(this, 20, ((BitmapDrawable)d).Bitmap);
            fondo.SetImageBitmap(fondoblurreado);

            clasesettings.recogerbasura();
            ///////////////////////////////#clicks#/////////////////////////////////
            porcientoreproduccion.ProgressChanged += (aa, aaaa)=>{
                if (aaaa.FromUser)
                {
                    Clouding_service.gettearinstancia().musicaplayer.SeekTo(aaaa.Progress);
                }
            };
            ll10.Click += delegate
            {
                animar(ll10);
                download.PerformClick();
            };
            ll11.Click += delegate
            {
                animar(ll11);
                abrirbrowser.PerformClick();
            };
            ll12.Click += delegate
            {
                animar(ll12);
                listareprod.PerformClick();
            };
            ll13.Click += delegate
            {
                animar(ll13);
                codigoqr.PerformClick();
            };
            ll14.Click += delegate
            {
                animar(ll14);
                botonsincronizar.PerformClick();
            };
            ll15.Click += delegate
            {
                animar(ll15);
                botonvideo.PerformClick();
            };
            ll16.Click += delegate
            {
                animar(ll16);
                botonbusqueda.PerformClick();
            };
            menuham.Click += delegate
            {
                
            };
            botonabrirmenu.Click += delegate
            {
              
                if (menuham.Visibility == ViewStates.Invisible)
                {
                    animar6(menuham);
                    menuham.Visibility = ViewStates.Visible;
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.leftarrow);
                    estadomenu.Text = "Cerrar menú";
                    layoutvolumen.Visibility = ViewStates.Invisible;
                }
                else
                {
                   
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    animarycerrar(menuham);
                    estadomenu.Text = "";
                }
            };
            layoutvolumen.Click += delegate
            {

            };
            botonmostrarvolumen.Click += delegate
            {
              
              if(layoutvolumen.Visibility ==ViewStates.Visible)
                {
                    layoutvolumen.Visibility = ViewStates.Invisible;
                }
                else
                {

                    layoutvolumen.BringToFront();
                    animar2(layoutvolumen);
                    layoutvolumen.Visibility = ViewStates.Visible;
                }
            };

            botonbusqueda.Click += delegate
            {
               
                Intent intento = new Intent(this, typeof(actfastsearcher));
                intento.PutExtra("ipadres", "localhost");
                StartActivity(intento);
                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";


            };

         
            botonvideo.Click += delegate
            {
              
               
               
                play.SetBackgroundResource(Resource.Drawable.playbutton2);

                
                Clouding_service.gettearinstancia().musicaplayer.Stop();
                Intent intento = new Intent(this, typeof(actvideo));
                    intento.PutExtra("link", linkactual);
                    intento.PutExtra("posactual", indiceactual.ToString());
                    clasesettings.guardarsetting("listaactual", string.Join("¤", listacaratulas) + "¹" + string.Join("¤", laparalinks));
                clasesettings.guardarsetting("progresovideoactual", Clouding_service.gettearinstancia().musicaplayer.CurrentPosition.ToString());
                    StartActivity(intento);
                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";

                envideo = true;
                Thread.Sleep(100);
             ///   clasesettings.guardarsetting("videoactivo", "si");
            };
             
            botonsincronizar.Click += delegate
            {
                Intent intento = new Intent(this,typeof( actividadsincronizacion));
                intento.PutExtra("ipadre", ipadre);

                StartActivity(intento);

                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";
            };

            volumen.ProgressChanged += delegate
            {
                volumenactual = volumen.Progress;
                Clouding_service.gettearinstancia().musicaplayer.SetVolume(volumenactual * 0.01f, volumenactual * 0.01f);
              //  clasesettings.guardarsetting("cquerry","volact()"+  ">"+ ().ToString() + ">"+ ().ToString());
                
            
            };
            escuchar.Click += delegate
            {
             

                if (tienemicrofono() == true) {
                    var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 250);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 500);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 5000);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                    StartActivityForResult(voiceIntent, voz);
                }
                else
                {
                    Toast.MakeText(this, "ningun microfono detectado", ToastLength.Long).Show();
                };

            };

            lista.ItemClick += (sender, easter) =>
            {
                Intent intentoo = new Intent(this, typeof(deletedialogact));
               
                intentoo.PutExtra("index", easter.Position.ToString());
                intentoo.PutExtra("color", "DarkGray");
                intentoo.PutExtra("titulo", lapara[easter.Position]);
                intentoo.PutExtra("ipadress", ipadre);
                intentoo.PutExtra("url", laparalinks[easter.Position]);
                intentoo.PutExtra("imagen", @"https://i.ytimg.com/vi/" + laparalinks[easter.Position].Split('=')[1] + "/hqdefault.jpg");
                StartActivity(intentoo);






                /*
                automated = true;
                termino = laparalinks[easter.Position];
                listacaratulas[locanterior] = lapara[locanterior];
                locanterior = easter.Position;                
                listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";
                adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                RunOnUiThread(() => lista.Adapter = adaptadol);
                Thread ter = new Thread(new ThreadStart(buscarvid));
                ter.Start();
             //   automated = false;*/

            };
            lista.ItemSelected += (sender, easter) =>
            {

            };
          

            atras.Click += (sender, easter) =>
        {
            if (buscando == false)
            {
             
                Thread proc = new Thread(new ThreadStart(anterior));
                proc.Start();

            }


        };
            abrirbrowser.Click += (sender, easter) =>
            {
              
                Intent intento = new Intent(this, typeof(customsearcheract));
                intento.PutExtra("ipadre", ipadre);
                intento.PutExtra("color", colol);
                StartActivity(intento);
                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";
            };
            listareprod.Click += delegate
                  {
                    
                      Intent intentoo = new Intent(this, typeof(menulistaoffline));
                      intentoo.PutExtra("ipadre", ipadre);
                      if (laparalinks.Count >= 1) {
                      string parte1 = "";
                      string parte2 = "";

                      foreach(string it in lapara)
                          {
                              it.Replace(';', ' ');
                              it.Replace('$', ' ');
                              parte1 += it + ";";
                      }
                      foreach (string it in laparalinks)
                      {
                             it.Replace(';', ' ');
                              it.Replace('$', ' ');
                              parte2 += it + ";";
                      }

               
                      string listenlinea = parte1 + "$" + parte2;

                      intentoo.PutExtra("listaenlinea",listenlinea);
                      }
                      else
                      {
                          intentoo.PutExtra("listaenlinea","");
                      }
                      StartActivity(intentoo);
                      animarycerrar(menuham);
                      botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                      estadomenu.Text = "";

                  };
            adelante.Click += (sender, easter) =>
             {
                 if (buscando == false) { 
                 animar(adelante);
                     Thread proc = new Thread(new ThreadStart(siguiente));
                     proc.Start();
                
                 }

             };
            play.Click += (sender, easter) =>
              {
                  animar(play);
                  if (!Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                  {
                      play.SetBackgroundResource(Resource.Drawable.pausebutton2);

                      Clouding_service.gettearinstancia().musicaplayer.Start();
                    //  clasesettings.guardarsetting("cquerry", "play()");
                  }
                  else

                  {
                      play.SetBackgroundResource(Resource.Drawable.playbutton2);
                      Clouding_service.gettearinstancia().musicaplayer.Pause();
                     // clasesettings.guardarsetting("cquerry", "pause()");

                  }
              };
            adelantar.Click += (sender, easter) =>
              {
                  animar(adelantar);
               Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition + Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
                  //   clasesettings.guardarsetting("cquerry", "adelantar()");
              };
            atrazar.Click += (sender, easter) =>
            {
                animar(atrazar);

                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition - Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
            };

            download.Click += (sender, easter) =>
            {
                animar(download);
                if (zelda.Trim().Length>0 ) {

                    Intent internado = new Intent(this, typeof(actdownloadcenteroffline));


                    internado.PutExtra("ip", ipadre);
                    internado.PutExtra("zelda", zelda);

                    internado.PutExtra("color", colol);
                    StartActivity(internado);
                    animarycerrar(menuham);
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";





                }
                else
                {
                    Toast.MakeText(this, "Reproduzca un video para descargar", ToastLength.Long).Show();
                    animarycerrar(menuham);
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";
                }
            };

    
            agregar.Click += (sender, easter) =>
            {
                animar(agregar);
                termino = textbox.Text;
                textbox.Text = "";
                Thread ter = new Thread(new ThreadStart(agregarvid));
                ter.Start();


            };
            codigoqr.Click += (sender, easter) =>
            {
                animar(codigoqr);
                Intent intetno = new Intent(this, typeof(qrcodigoact));
                intetno.PutExtra("ipadre", ipadre);
                StartActivity(intetno);
                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";


            };

            buscar.Click += (sender, easter) =>
            {
                animar(buscar);
                automated = false;
                if (textbox.Text.Trim().Length > 0 &&buscando!=true)
                {
                    termino = textbox.Text;
                    textbox.Text = "";
                    Thread proc = new Thread(new ThreadStart(buscarvid));
                    proc.Start();
                }




            };



            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////

        }
        public override void OnBackPressed()
        {

            clasesettings.preguntarsimenuosalir(this);
            // base.OnBackPressed();
        }

        public void iniciarservicio()
        {
            StopService(new Intent(this, typeof(cloudingserviceonline)));
            StopService(new Intent(this, typeof(Clouding_serviceoffline)));
            StopService(new Intent(this, typeof(Clouding_service)));
            StopService(new Intent(this, typeof(serviciodownload)));
            StartService(new Intent(this, typeof(Clouding_service)));
            StartService(new Intent(this, typeof(serviciodownload)));
        }
       public void lanotii()
        {


            
        }





        public void buscarvid()
        {

         
            string url = "";
            if (buscando == false) { 

         
              
                    // Searchercocinado cocina = new Searchercocinado();
                    // cocina.devolverurl(textbox.Text);

                    url = getearurl(termino);
              
                    if (url != "%%nulo%%")
                    {
                 
              
                     
                        RunOnUiThread(() => Toast.MakeText(this, "Cargando...", ToastLength.Long).Show());
                        buscando = true;
                        zelda = url;
                    linkactual = url;
                    /*
                       videoinfoss = DownloadUrlResolver.GetDownloadUrls(url, false);

                       Thread.Sleep(100);

                       VideoInfo elvid = videoinfoss.First(video => video.Resolution == 0 && video.VideoType == VideoType.Mp4 && video.AudioType == AudioType.Aac);
                       if (elvid == null)
                       {
                           videoinfoss = DownloadUrlResolver.GetDownloadUrls(url, true);
                           Thread.Sleep(100);
                           elvid = videoinfoss.First(video => video.Resolution == 0 && video.VideoType == VideoType.Mp4 && video.AudioType == AudioType.Aac);

                       }
                       if (elvid.RequiresDecryption)
                       {
                           DownloadUrlResolver.DecryptDownloadUrl(elvid);
                       }*/
                    var asdd = clasesettings.gettearvideoid(url,envideo);
                        if (automated == false)
                        {

                        if (!encontroparecido(url, laparalinks))
                        {
                            lapara.Add(asdd.titulo);
                            laparalinks.Add(url);
                            if (listacaratulas.Count > 0)
                            {
                                listacaratulas[locanterior] = lapara[locanterior];
                            }
                            listacaratulas.Add(">" + asdd.titulo + "<");
                            locanterior = lapara.Count - 1;
                            indiceactual = locanterior;
                        }
                        else
                        {
                            buscando = false;
                            RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                        }

                    }
                        adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);

                        RunOnUiThread(() => lista.Adapter = adaptadol);

                        RunOnUiThread(() => label.Text = asdd.titulo);


                        string imagensilla = "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg";
                        imgurl = imagensilla;
                    Bitmap blur = null;
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        blur = CreateBlurredImage(20, GetImageBitmapFromUrl(imagensilla));
                    }
               
                    Thread.Sleep(20);
                        var sinblur = GetImageBitmapFromUrl(imagensilla);
                        RunOnUiThread(() => caratula2.SetImageBitmap(sinblur));

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        RunOnUiThread(() => fondo.SetImageBitmap(blur));
                        fondoblurreado = blur;
                    }
                  
                    actualizartodo();
                    buscando = false;
                  
                    //////////////////////////////////////////mimicv2
                    if (!envideo)
                    {
                        reproducir(asdd.downloadurl);
                    }
                    else
                    ////////////////////////////mimicv2
                    {
                        Clouding_service.gettearinstancia().linkactual = linkactual;
                        Clouding_service.gettearinstancia().tituloactual = label.Text;
                        actvideo.gettearinstacia().indiceactual = indiceactual;
                        actvideo.gettearinstacia().linkactual = linkactual;
                        actvideo.gettearinstacia().buscaryreproducir();
                        Clouding_service.gettearinstancia().mostrarnotificacion();
                        actualizarcaratula();

                    }
                }
            }
            else
            {
                buscando = false;
            }
                }

        private Bitmap CreateBlurredImage(int radius,Bitmap img)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap = img;

            // Create another bitmap that will hold the results of the filter.
            Bitmap blurredBitmap;
            blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

            // Create the Renderscript instance that will do the work.
            RenderScript rs = RenderScript.Create(this);

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
        public void agregarvid()
        {
            string urll = getearurl(termino);
            if (urll.Length > 0)
            {
                if (!encontroparecido(urll, laparalinks))
                {
                    GR3_UiF.Geteartitulo titulo = new GR3_UiF.Geteartitulo();


                    if (lapara.Count == 0)
                    {
                        automated = false;
                        termino = urll;
                        buscarvid();

                    }
                    else
                    {

                        laparalinks.Add(urll);
                        lapara.Add(titulo.GetVideoTitle(titulo.LoadJson(urll)));
                        listacaratulas.Add(lapara[lapara.Count - 1]);

                        adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                        RunOnUiThread(() => lista.Adapter = adaptadol);
                        actualizarlista();
                        RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());
                    
                    }


                }
                else
                {
                  
                    RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                }


            }
        }

        public void buscarvidth(string terminoi,bool automatedi)
        {

       
            string url = "";
            if (buscando == false)
            {
             
            // Searchercocinado cocina = new Searchercocinado();
            // cocina.devolverurl(textbox.Text);

            url = getearurl(terminoi);
         
            if (url != "%%nulo%%")
            {
                 


                        RunOnUiThread(() => Toast.MakeText(this, "Cargando...", ToastLength.Long).Show());
                        buscando = true;
                        zelda = url;
                    linkactual = url;
                  /*
                        videoinfoss = DownloadUrlResolver.GetDownloadUrls(url, false);
                      

                        VideoInfo elvid = videoinfoss.First(video => video.Resolution == 360 && video.VideoType == VideoType.Mp4);
                        if (elvid.RequiresDecryption)
                        {
                            DownloadUrlResolver.DecryptDownloadUrl(elvid);
                        }
                    downloadurrrl = elvid.DownloadUrl;*/
                    var asdd = clasesettings.gettearvideoid(url, envideo);
                    if (automatedi == false)
                        {
                        if (!encontroparecido(url, laparalinks))
                        {
                            lapara.Add(asdd.titulo);
                            laparalinks.Add(url);
                            if (listacaratulas.Count > 0)
                            {
                                listacaratulas[locanterior] = lapara[locanterior];
                            }
                            listacaratulas.Add(">" + asdd.titulo + "<");
                            locanterior = lapara.Count - 1;
                            indiceactual = locanterior;
                            adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                        }
                        else
                        {
                            buscando = false;
                            RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                        }
                    }



                        adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                        RunOnUiThread(() => lista.Adapter = adaptadol);
                        RunOnUiThread(() => label.Text = asdd.titulo);


                    string imagensilla = "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg";
                    imgurl = imagensilla;
                    Bitmap blur = null;
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        blur = CreateBlurredImage(20, GetImageBitmapFromUrl(imagensilla));
                    }

                    Thread.Sleep(20);
                    var sinblur = GetImageBitmapFromUrl(imagensilla);
                    RunOnUiThread(() => caratula2.SetImageBitmap(sinblur));

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        RunOnUiThread(() => fondo.SetImageBitmap(blur));
                        fondoblurreado = blur;
                    }
                    buscando = false;
                    actualizartodo();
                    if (!envideo)
                    {
                        reproducir(asdd.downloadurl);
                    }
                    else
                    ////////////////////////////mimicv2
                    {
                        Clouding_service.gettearinstancia().linkactual = linkactual;
                        Clouding_service.gettearinstancia().tituloactual = label.Text;
                        actvideo.gettearinstacia().indiceactual = indiceactual;
                        actvideo.gettearinstacia().linkactual = linkactual;
                        actvideo.gettearinstacia().buscaryreproducir();
                        Clouding_service.gettearinstancia().mostrarnotificacion();
                        actualizarcaratula();
                    }
                }
            else
            {
                  
                    buscando = false;
            }
            }
        }

        public void buscarviddireckt(string url, bool automatedi)
        {


          
            if (buscando == false)
            {

                // Searchercocinado cocina = new Searchercocinado();
                // cocina.devolverurl(textbox.Text);

             

                if (url != "%%nulo%%")
                {



                    RunOnUiThread(() => Toast.MakeText(this, "Cargando...", ToastLength.Long).Show());
                    buscando = true;
                    zelda = url;
                    linkactual = url;
                    /*
                          videoinfoss = DownloadUrlResolver.GetDownloadUrls(url, false);


                          VideoInfo elvid = videoinfoss.First(video => video.Resolution == 360 && video.VideoType == VideoType.Mp4);
                          if (elvid.RequiresDecryption)
                          {
                              DownloadUrlResolver.DecryptDownloadUrl(elvid);
                          }
                      downloadurrrl = elvid.DownloadUrl;*/
                    var asdd = clasesettings.gettearvideoid(url, envideo);
                    if (automatedi == false)
                    {
                        if (!encontroparecido(url, laparalinks))
                        {
                            lapara.Add(asdd.titulo);
                            laparalinks.Add(url);
                            if (listacaratulas.Count > 0)
                            {
                                listacaratulas[locanterior] = lapara[locanterior];
                            }
                            listacaratulas.Add(">" + asdd.titulo + "<");
                            locanterior = lapara.Count - 1;
                            indiceactual = locanterior;
                            adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                        }
                        else
                        {
                            buscando = false;
                            RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                        }
                    }



                    adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                    RunOnUiThread(() => lista.Adapter = adaptadol);
                    RunOnUiThread(() => label.Text = asdd.titulo);


                    string imagensilla = "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg";
                    imgurl = imagensilla;
                    Bitmap blur = null;
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        blur = CreateBlurredImage(20, GetImageBitmapFromUrl(imagensilla));
                    }

                    Thread.Sleep(20);
                    var sinblur = GetImageBitmapFromUrl(imagensilla);
                    RunOnUiThread(() => caratula2.SetImageBitmap(sinblur));

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        RunOnUiThread(() => fondo.SetImageBitmap(blur));
                        fondoblurreado = blur;
                    }
                    buscando = false;
                    actualizartodo();
                    if (!envideo)
                    {
                        reproducir(asdd.downloadurl);
                    }
                    else
                    ////////////////////////////mimicv2
                    {
                        Clouding_service.gettearinstancia().linkactual = linkactual;
                        Clouding_service.gettearinstancia().tituloactual = label.Text;
                        actvideo.gettearinstacia().indiceactual = indiceactual;
                        actvideo.gettearinstacia().linkactual = linkactual;
                        actvideo.gettearinstacia().buscaryreproducir();
                        Clouding_service.gettearinstancia().mostrarnotificacion();
                        actualizarcaratula();
                    }
                }
                else
                {

                    buscando = false;
                }
            }
        }


        public void agregarvidth(string terminoi)
        {
            string urll = "";
           urll = getearurl(terminoi);
            if (urll.Length > 0)
            {
                if (!encontroparecido(urll, laparalinks))
                {

           
                GR3_UiF.Geteartitulo titulo = new GR3_UiF.Geteartitulo();


                if (lapara.Count == 0)
                {
                
                    buscarvidth(urll,false);

                }
                else
                {
                    laparalinks.Add(urll);
                    lapara.Add(titulo.GetVideoTitle(titulo.LoadJson( urll)));
                    listacaratulas.Add(lapara[lapara.Count - 1]);
                  
                    adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                    RunOnUiThread(() => lista.Adapter = adaptadol);
                    actualizarlista();
                    RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());
                      
                    }


                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                }

            }
        }



        public void agregarviddireckt(string urll,string titulo)
        {
          
            if (urll.Length > 0)
            {
                if (!encontroparecido(urll, laparalinks))
                {


                 //   GR3_UiF.Geteartitulo titulo = new GR3_UiF.Geteartitulo();


                    if (lapara.Count == 0)
                    {

                        buscarvidth(urll, false);

                    }
                    else
                    {
                        laparalinks.Add(urll);
                        lapara.Add(titulo);
                        listacaratulas.Add(lapara[lapara.Count - 1]);

                        adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                        RunOnUiThread(() => lista.Adapter = adaptadol);
                        actualizarlista();
                        RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());

                    }


                }
                else
                {
                    RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                }

            }
        }

        public void siguiente()
        {
            try
            {
                if (lapara.Count > 0 && lapara[indiceactual + 1].Trim().Length > 0 && !buscando)
                {
                   
                    termino = laparalinks[indiceactual + 1];
                  
               
                    listacaratulas[locanterior] = lapara[locanterior];
                    locanterior = indiceactual + 1;
                    listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";
                 
                    adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                    RunOnUiThread(() => lista.Adapter = adaptadol);
                    automated = true;
                    indiceactual++;
                    new Thread(() =>
                    {
                        buscarvid();
                    }).Start();
               
                
                }
            }
            catch (Exception)
            {

            }

            }
        public void anterior()
        {
            try
            {
                if (lapara.Count > 0 && lapara[indiceactual - 1].Trim().Length > 0 && !buscando)
                {
               
                    termino = laparalinks[indiceactual - 1];
                
                  
                    listacaratulas[locanterior] = lapara[locanterior];
                    locanterior = indiceactual - 1;
                    listacaratulas[locanterior] =">"+ lapara[locanterior]+ "<";
                
                    adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                    RunOnUiThread(() => lista.Adapter = adaptadol);
                    automated = true;
                    indiceactual--;
                    new Thread(() =>
                    {
                        buscarvid();
                    }).Start();


                }
            }
            catch (Exception)
            {

            }

        }

      


        public void reproducir(string downloadurl)
        {




            // musicaplayer.SetDataSource(downloadurl);
            try
            {

                /*   clasesettings.guardarsetting("musica", downloadurl);
                   clasesettings.guardarsetting("servicio", "musica");
                   Thread.Sleep(1000);
                   clasesettings.guardarsetting("cquerry", "data()>" + label.Text.Replace('>',' ') + ">" + linkactual.Replace('>', ' '));
                   /*
             musicaplayer.Reset();





                 musicaplayer = Android.Media.MediaPlayer.Create(this, Android.Net.Uri.Parse(downloadurl));
             musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
             musicaplayer.Start();

                 RunOnUiThread(() => play.SetBackgroundResource(Resource.Drawable.pausebutton2));
                 RunOnUiThread(() => volumen.Progress = 100);
                 volumenactual = 100;


        musicaplayer.Error += delegate
             {
                 musicaplayer.Reset();
                 musicaplayer = new Android.Media.MediaPlayer();
             };



                musicaplayer.Completion += delegate
                 {


                     try
                     {
                         if (lapara.Count > 0 && lapara[laparalinks.IndexOf(linkactual) + 1].Trim().Length > 0)
                         {

                             termino = laparalinks[indiceactual + 1];


                             listacaratulas[locanterior] = lapara[locanterior];
                             locanterior = indiceactual + 1;
                             listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";

                             adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                             RunOnUiThread(() => lista.Adapter = adaptadol);

                             indiceactual++;
                             buscarvidth(termino, true);

                         }
                     }
                     catch (Exception)
                     {
                         RunOnUiThread(() => volumen.Progress = 100);
                         volumenactual = 100;


                         RunOnUiThread(() => play.SetBackgroundResource(Resource.Drawable.playbutton2));

                         RunOnUiThread(() => porcientoreproduccion.Progress = 0);

                     }




                 };
             */
           
                Clouding_service.gettearinstancia().tituloactual = label.Text;
                Clouding_service.gettearinstancia().linkactual = linkactual;
               Clouding_service.gettearinstancia().reproducir(downloadurl);
                play.SetBackgroundResource(Resource.Drawable.pausebutton2);
                new Thread(() =>
                {
                    actualizartodo();
                }).Start();
              
                clasesettings.recogerbasura();

            }
            catch (Exception)
            {
                Toast.MakeText(this, "No se puede reproducir el elemento", ToastLength.Long).Show();
            }

                //TODO whatever you wanted to happen on completion









        }

        public void ponerporciento(){   
            while (true)
            {
                try
                {
                    if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying == true && !bloquearporcentaje)
                    {
                        RunOnUiThread(() => porcientoreproduccion.Max = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.Duration));
                        RunOnUiThread(() => porcientoreproduccion.Progress = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition));
                      
                    }
                }
                catch (Exception) {


                }



                Thread.Sleep(1000);
            }


          


        }
        public void actualizarlista()
        {
            List<TcpClient> clientesact = new List<TcpClient>();
            string listenlinea = "";
            string listenlinea2 = "";
            for (int i = 0; i < lapara.Count; i++)
            {
                string perfectstring = listacaratulas[i].Replace('$', ' ');
                listenlinea += perfectstring + ";";
            }
            for (int i = 0; i < laparalinks.Count; i++)
            {
                string perfectstring2 = laparalinks[i].Replace('$', ' ');
                perfectstring2 = RemoveIllegalPathCharacters(perfectstring2);

                listenlinea2 += perfectstring2 + ";";
            }
            foreach (TcpClient c in clienteses)
            {

             
                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true)
                {
                    new Thread(() =>
                    {
                      
                        clientesact.Add(c);


                        try
                        {
                            c.Client.Send(System.Text.Encoding.Default.GetBytes(listenlinea));
                            Thread.Sleep(250);
                            c.Client.Send(System.Text.Encoding.Default.GetBytes("links()><;" + listenlinea2));


                        }
                        catch (Exception)
                        {
                            if (clientesact.Count > 1)
                            {
                                clientesact.RemoveAt(clientesact.Count - 1);
                            }
                        }
                    }).Start();
                }

            }
            clasesettings.guardarsetting("listaactual", string.Join("¤", listacaratulas) + "¹" + string.Join("¤", laparalinks));
            clasesettings.guardarsetting("elementosactuales", listenlinea + "$" + listenlinea2);
            clienteses = clientesact;
        }


        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }



        public void actualizartodo()
        {
            List<TcpClient> clientesact = new List<TcpClient>();
            string listenlinea = "";
            string listenlinea2 = "";

            for (int i = 0; i < lapara.Count; i++)
            {
                string perfectstring = listacaratulas[i].Replace('$', ' ');
                listenlinea += perfectstring + ";";
            }

            for (int i = 0; i < laparalinks.Count; i++)
            {
                string perfectstring2 = laparalinks[i].Replace('$', ' ');
                perfectstring2 = RemoveIllegalPathCharacters(perfectstring2);
                listenlinea2 += perfectstring2 + ";";
            }
            foreach (TcpClient c in clienteses)
            {
              
                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true)
                {
                    new Thread(() =>
                    {
                        try
                        {

                    
                        clientesact.Add(c);
                        string colortostr = "DarkGray";
                        c.Client.Send(System.Text.Encoding.Default.GetBytes("caratula()><;" + imgurl + ";" + label.Text.Replace('$', ' ') + ";" + colortostr + ";" + RemoveIllegalPathCharacters(zelda) + ";$none;none"));
                        Thread.Sleep(250);                        
                        c.Client.Send(System.Text.Encoding.Default.GetBytes("links()><;" + listenlinea2));
                        Thread.Sleep(250);
                        c.Client.Send(System.Text.Encoding.Default.GetBytes(listenlinea));
                        }
                        catch (Exception)
                        {

                        }

                    }).Start();



                }

            }
            clasesettings.guardarsetting("listaactual", string.Join("¤", listacaratulas) + "¹" + string.Join("¤", laparalinks));
            clasesettings.guardarsetting("elementosactuales", listenlinea + "$" + listenlinea2);
            clienteses = clientesact;






        }











        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == voz)
            {
                if (resultCode == Result.Ok)
                {
                 

                       var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {

                        textbox.Text = " " + matches[0];
                        
                    }

                    else
                        Toast.MakeText(this, "No se pudo escuchar nada", ToastLength.Long).Show();
                    }
                }
                base.OnActivityResult(requestCode, resultCode, data);
        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////relay////////////////////////////////////////
        /// </summary>
        ///  if (!encontroparecido(linkvideo, archivoentexto.Split('$')[1].Split(';').ToList())){
        public bool encontroparecido(string link, List<string> listalinks)
        {
            bool encontro = false;
            foreach (string ee in listalinks)
            {
                if (ee.Trim() != "") { 
                if (ee.Split('=')[1] == link.Split('=')[1])
                {
                    encontro = true;
                }
                }
            }
            if (encontro)
            {

                return true;
            }

            else
            {

                return false;
            }
        }



        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            base.OnSaveInstanceState(outState, outPersistentState);
            outState.PutBoolean("restaurado", true);
        }
        public void cojerstream()
        {


            
              
               while (detenedor == true)
               {

                bool agregado = false;
                bool pidiendoindex = false;
                bool pedirlista = false;
                bool eliminarelemento = false;
            
              
                  
                try
                {
                    TcpClient clieente = new TcpClient();
                    clieente = oidor.AcceptTcpClient();


                    if (clieente.Connected == true)
                    {

                        clienteses.Add(clieente);
                        Thread procc = new Thread(new ThreadStart(cojerstream));
                        procc.Start();

                    }
                    var stream = clieente.GetStream();
                    byte[] bites = new byte[125000];


                    int o;

                    while ((o = stream.Read(bites, 0, bites.Length)) != 0 && detenedor == true)
                    {
                      
                        string capturado = System.Text.Encoding.UTF8.GetString(bites, 0, o);
                        if (capturado.Trim() == "fullscreen()")
                        {

                        }
                        else
                      if (capturado.Trim() == "notificar()")
                        {

                        }else
                        if (capturado.Trim() == "eliminarelemento()")
                        {

                            eliminarelemento = true;
                        }
                        else
                          if (eliminarelemento == true)
                        {
                            new Thread(() =>
                            {
                                int indicedelmedio = listacaratulas.IndexOf(">" + lapara[locanterior] + "<");

                            if (lapara.Count > 1)
                            {
                                if (Convert.ToInt32(capturado) > indicedelmedio)
                                {

                                }
                                else

                                if (Convert.ToInt32(capturado) == indicedelmedio)
                                {
                                    siguiente();
                                    locanterior--;
                                    indiceactual--;


                                }
                                else
                                if (Convert.ToInt32(capturado) < indicedelmedio)
                                {

                                    locanterior--;
                                    indiceactual--;
                                }

                                laparalinks.RemoveAt(Convert.ToInt32(capturado));
                                lapara.RemoveAt(Convert.ToInt32(capturado));
                                listacaratulas.RemoveAt(Convert.ToInt32(capturado));

                              
                                adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                                RunOnUiThread(() => lista.Adapter = adaptadol);
                                   
                                        actualizarlista();
                                 

                            }








                            }).Start();
                        }
                        else

                 if (capturado == "pedirlista()")
                        {
                           // capturado = "";

                            pedirlista = true;
                        }
                        else
                        if (pedirlista == true)
                        {
                            pedirlista = false;
                            new Thread(() =>
                            {
                               
                                reproducirlalista(Convert.ToInt32(capturado.Trim()));
                            }).Start();
                         //   capturado = "";
                        }
                        else
                      if (capturado.Trim() == "playpause()")
                        {

                           // capturado = "";
                            if (!envideo) {
                                if (!Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                                {
                                    RunOnUiThread(() => play.SetBackgroundResource(Resource.Drawable.pausebutton2));
                                    Clouding_service.gettearinstancia().musicaplayer.Start();



                                }
                                else

                                {
                                    RunOnUiThread(() => play.SetBackgroundResource(Resource.Drawable.playbutton2));
                                    Clouding_service.gettearinstancia().musicaplayer.Pause();


                                }
                            }
                            else
                            {
                                actvideo.gettearinstacia().RunOnUiThread(() =>
                                {
                                    if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying == true)
                                    {
                                        actvideo.gettearinstacia().playpause.SetBackgroundResource(Resource.Drawable.playbutton2);

                                        Clouding_service.gettearinstancia().musicaplayer.Pause();
                                    }
                                    else

                                    {

                                        actvideo.gettearinstacia().playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
                                        Clouding_service.gettearinstancia().musicaplayer.Start();

                                    }
                                });
                                ///////////////////////////////////////////////mimic v2
                                clasesettings.guardarsetting("comando", "playpause()");
                            }

                        }
                        else
                            if (capturado.Trim() == "recall()")
                        {
                           // capturado = "";
                            new Thread(() =>
                            {

                                actualizartodo();
                            }).Start();
                        }
                        else
                           if (capturado.Trim() == "vol+()")
                        {
                          //  capturado = "";
                            if (volumenactual >= 100)
                            {
                                volumenactual = 100;
                            }
                            else
                            {
                                volumenactual += 10;
                            }

                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(volumenactual * 0.01f, volumenactual * 0.01f);
                            //clasesettings.guardarsetting("cquerry", "volact()>" + () + ">" + ());
                            RunOnUiThread(() => volumen.Progress = volumenactual);

                        }
                        else
                           if (capturado.Trim() == "vol-()")
                        {
                            if (volumenactual <= 0)
                            {
                                volumenactual = 0;
                            }
                            else
                            {
                                volumenactual -= 10;
                            }

                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(volumenactual * 0.01f, volumenactual * 0.01f);
                            RunOnUiThread(() => volumen.Progress = volumenactual);

                        }

                        else

                            if (capturado.Trim() == "actualizarlistaactual()")
                        {
                          //  capturado = "";
                            Thread proc = new Thread(new ThreadStart(actualizarlista));
                            proc.Start();
                      
                        }
                        else

                            if (capturado.Trim() == "actualizarplaylist()")
                        {
                           // capturado = "";
                            try
                            {
                                new Thread(() =>
                                {
                                    actualizartodaslaslistas();
                                }).Start();
                            }
                            catch (Exception)
                            { }
                       
                        }
                        else
                        if (capturado.Trim() == "caratula()")
                        {
                          //  capturado = "";
                            new Thread(() =>
                            {
                                actualizarcaratula();
                            }).Start();
                        }
                        else
                             if (capturado.Trim() == "next()")
                        {
                           // capturado = "";
                            if (buscando == false)
                            {
                                new Thread(() =>
                                {
                                    siguiente();
                                }).Start();
                            }
                        }
                        else
                        if (capturado.Trim() == "pedirindice()" && buscando == false)
                        {
                           // capturado = "";
                            pidiendoindex = true;

                        }


                        else
                        if (capturado.Trim() == "actualizarlalista()")

                        {
                          //  capturado = "";
                            new Thread(() =>
                            {
                                actualizarlistareproduccion();
                            }).Start();


                        }
                        else
                            if (capturado.Trim() == "back()")
                        {
                           /// capturado = "";
                            new Thread(() =>
                            {
                                anterior();
                            }).Start();
                        }
                        else
                             if (capturado.Trim() == "actual+()")
                        {
                           // capturado = "";
                            if (!envideo)
                            {

                               
                                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition + Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
                            }
                            else
                            {
                                //////////////////////////////mimicv2
                                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition + Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
                                clasesettings.guardarsetting("comando", "adelantar()");
                            }
                        
                        }
                        else
                        if (capturado.Trim() == "actual-()")
                        {
                            // capturado = "";
                            if (!envideo)
                            {


                                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition - Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
                            }
                            else
                            {
                                //////////////////////////////mimicv2
                                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition - Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
                                clasesettings.guardarsetting("comando", "adelantar()");
                            }
                        }
                        else
                             if (capturado.Trim() == "agregar()")
                        {
                           // capturado = "";
                            agregado = true;
                        }
                        else


                          if (pidiendoindex == true)
                        {
                            pidiendoindex = false;

                            if (buscando == false)
                            {

                                //////////////////////////////////////////
                                new Thread(() => {
                                    termino = laparalinks[int.Parse(capturado)];
                                listacaratulas[locanterior] = lapara[locanterior];
                                locanterior = int.Parse(capturado);
                                listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";
                                indiceactual = locanterior;
                                adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
                                RunOnUiThread(() => lista.Adapter = adaptadol);
                                buscarvidth(termino, true);
                                }).Start();
                               //  capturado = "";
                            }



                        }
                        else
                        if (agregado == true)
                        {
                            agregado = false;
                            new Thread(() => {
                                agregarvidth(capturado);
                            }).Start();
                          //  capturado = "";
                        }
                        else
                        if (agregado != true && pidiendoindex == false && capturado.Trim().Length>1&&!nocontienequerry(capturado))
                        {

                            new Thread(() => {
                                buscarvidth(capturado, false);
                            }).Start();
                          //  capturado = "";
                        }




                    }
                }
                catch(Exception) { }
                       


        }
        }
      
        public void actualizarcaratula() {

            List<TcpClient> clientesact = new List<TcpClient>();
            foreach (TcpClient c in clienteses)
            {

                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true )
            {
                    new Thread(() =>
                    {
                        try
                        {
                            clientesact.Add(c);
                            string colortostr = "DarkGray";
                            c.Client.Send(System.Text.Encoding.Default.GetBytes("caratula()><;" + imgurl + ";" + label.Text.Replace('$', ' ') + ";" + colortostr + ";" + zelda));
                        }
                        catch (Exception)
                        {

                        }
                       
                    }).Start();

                  
            }

            }
          
            clienteses = clientesact;
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
                            RunOnUiThread(() => caratula2.SetImageBitmap(imageBitmap));

                        }

                    }
                      
            }
            catch (Exception) { RunOnUiThread(() => caratula2.SetImageBitmap(null)); }
            return imageBitmap;
        }
        public bool tienemicrofono() { 
        string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
        if (rec != "android.hardware.microphone")
           {
   

                return false;
            }
           else
            {
                return true;
            }
}
        public void animar (Java.Lang.Object imagen)
        {
            if (!fromotherinstance)
            {
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
                animacion.SetDuration(300);
                animacion.Start();
              
            }
            fromotherinstance = false;

        }
        public void animar2(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(700);
            animacion.Start();

        }



        public void animar6(View imagen)
        {

            RunOnUiThread(() =>
            {

                var animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", -1000, 0);
                animacion.SetDuration(300);
                animacion.Start();
                /* animacion.AnimationEnd += delegate
                  {
                      animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", -1000, 0);
                      animacion.SetDuration(150);
                      animacion.Start();
                  };*/

            });



        }


        public void animarycerrar(Java.Lang.Object imagen)
        {
            RunOnUiThread(() =>
            {
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", 0, -1000);
                animacion.SetDuration(300);
                animacion.Start();
                animacion.AnimationEnd += delegate
                {

             ////   boton.Visibility = ViewStates.Visible;
                    menuham.Visibility = ViewStates.Invisible;

                };
            });
        }



       
        public  string getearurl(string titttt)
        {
          
            try {
               
                string linkkk = "";
                string prra = "";
                bool esunlink = false;
                try
                {
                   var dfasdsf= titttt.Split('=')[1];
                    esunlink = true;
                }
                catch (Exception)
                {
                 
                }
                if (!esunlink)
                {

               
                HttpClient cliente = new HttpClient(new ModernHttpClient.NativeMessageHandler());
               var  retorno =  cliente.GetStringAsync("https://decapi.me/youtube/videoid?search=" + titttt);
                 prra = retorno.Result;
            

                 linkkk = "http://www.youtube.com/watch?v="+ retorno.Result;
                }
                else
                {
                   linkkk = "http://www.youtube.com/watch?v=" + titttt.Split('=')[1];
                }


                if (prra.ToLower().Trim()== "invalid video id or search string." && !esunlink)
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
              RunOnUiThread(()=>  Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                return "%%nulo%%";
            }
           
        }
        private Bitmap GetQRCode()
        {

            var writer = new ZXing.Mobile.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options.Margin = 1;
            writer.Options.Height = 125;
            writer.Options.Width = 125;

            



            return writer.Write(ipadre);
        }

        public void buscarthread()
        {

        }
        public void agregarthread()
        {

        }
        public void actualizarlistareproduccion()
        {
            string listaenlinea = "";
            string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");
            for (int i = 0; i < items.Length; i++)
            {
                listaenlinea += System.IO.Path.GetFileNameWithoutExtension(items[i]) + ";";
            }
            listaenlinea = listaenlinea.Remove(listaenlinea.Length - 1, 1);

            foreach (TcpClient c in clienteses)
            {
                
                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true)
                {


                    
                    c.Client.Send(System.Text.Encoding.Default.GetBytes("caratula()><;" + imgurl + ";" + label.Text.Replace('$',' ') + ";" + "darkgray" + ";" + zelda));

                    Thread.Sleep(250);
                  
                    c.Client.Send(System.Text.Encoding.Default.GetBytes("listar()><;" + listaenlinea));



                }
            }


        }
        public void reproducirlalista(int indice)
        {
            string listainterna;
            string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");

            string name = System.IO.Path.GetFileNameWithoutExtension(items[indice]);

            StreamReader tupara = File.OpenText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + name);
            listainterna = tupara.ReadToEnd();

            string listilla1 = listainterna.Split('$')[0];
            StringBuilder sb = new StringBuilder(listilla1);
            sb.Remove(sb.Length - 1, 1);
            listilla1 = sb.ToString();
            string listilla2 = listainterna.Split('$')[1];
            StringBuilder sb2 = new StringBuilder(listilla2);
        //    sb2.Remove(sb2.Length - 1, 1);
            listilla2 = sb2.ToString();
            string[] partes = listilla1.Split(';');

            int indez = 0;
            foreach (string it in partes)
            {

                if (it.StartsWith(">"))
                {
                    string papu = it;
                    StringBuilder ee = new StringBuilder(papu);
                    ee.Replace('>', ' ');
                    ee.Replace('<', ' ');

                    partes.SetValue(ee.ToString(), indez);


                }

                indez++;
            }
            string[] partes2 = listilla2.Split(';');
            lapara.Clear();
            laparalinks.Clear();
            listacaratulas.Clear();
            var casi = partes2.ToList();
            casi.RemoveAt(casi.Count - 1);
            lapara = partes.ToList();
            laparalinks = casi;
            if (laparalinks[lapara.Count - 1].Trim() == "")
            {
                laparalinks.RemoveAt(lapara.Count - 1);
            }
            listacaratulas = partes.ToList();
            listacaratulas[0] = ">" + lapara[0] + "<";
            locanterior = 0;

           
            adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listacaratulas);
            RunOnUiThread(() => lista.Adapter = adaptadol);
           
      
            buscarvidth(laparalinks[0], true);


        }
        public void actualizartodaslaslistas()
        {

            foreach(TcpClient cl in clienteses)
            {
                if (prueba_de_lista_generica.SocketExtensions.IsConnected(cl)==true)
                {
                    cl.Client.Send(System.Text.Encoding.Default.GetBytes("actualizaa()"));
                    Thread.Sleep(10);
                }
            }
        }

        protected override void OnDestroy()
        {
            foreach (TcpClient c in clienteses)
            {



                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true)
                {

                    c.Client.Send(System.Text.Encoding.UTF8.GetBytes("cerrar();  "));
                    c.Client.Disconnect(true);


                    Thread.Sleep(100);
                }

            }
            oidor.Stop();



            clasesettings.recogerbasura();
            if (Clouding_service.gettearinstancia() != null)
            {
                Clouding_service.gettearinstancia().musicaplayer.Reset();
            }
            StopService(new Intent(this, typeof(Clouding_service)));
            UnregisterReceiver(br);
            instance = null;
            clasesettings.guardarsetting("onlineactivo", "no");

            base.OnDestroy();
        }
        public override void Finish()
        {
          
            base.Finish();
        }

        public override void FinishAndRemoveTask()
        {
            base.FinishAndRemoveTask();
          
        }
        public void receptor()
        {

        }
        public bool nocontienequerry(string querrystring)
        {
            if (querrystring.Contains("fullscreen()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("notificar()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("eliminarelemento()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("pedirlista()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("playpause()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("recall()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("vol+()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("vol-()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("actualizarlistaactual()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("actualizarplaylist()"))
            {
                return true;
            }
            else
                 if (querrystring.Contains("caratula()"))
            {
                return true;
            }
            else
                    if (querrystring.Contains("next()"))
            {
                return true;
            }
            else
                    if (querrystring.Contains("pedirindice()"))
            {
                return true;
            }
            else
                    if (querrystring.Contains("actualizarlalista()"))
            {
                return true;
            }
            else
                    if (querrystring.Contains("back()"))
            {
                return true;
            }
            else
                    if (querrystring.Contains("actual+()"))
            {
                return true;
            }
            else
                   if (querrystring.Contains("actual-()"))
            {
                return true;
            }
            else
                   if (querrystring.Contains("agregar()"))
            {
                return true;
            }
            else
            {

                return false;
            }


        }


        public void actualizarmediasesion()
        {
            try
            {
                while (true)
                {
                    br = new broadcast_receiver();
                    IntentFilter filtro = new IntentFilter(Intent.ActionMediaButton);
                    filtro.Priority = 1000;
                    if (instance != null)
                    {

                        try
                        {
                            UnregisterReceiver(br);
                        }
                        catch (Exception) { }

                        RegisterReceiver(br, filtro);
                        PlaybackState pbs = new PlaybackState.Builder()
                                .SetActions(PlaybackState.ActionFastForward | PlaybackState.ActionPause | PlaybackState.ActionPlay | PlaybackState.ActionPlayPause | PlaybackState.ActionSeekTo | PlaybackState.ActionSkipToNext | PlaybackState.ActionSkipToPrevious)
                                .SetState(PlaybackStateCode.Playing, 0, 1f, SystemClock.ElapsedRealtime())
                                .Build();
                        MediaSession mSession = new MediaSession(this, PackageName);
                        Intent intent = new Intent(this, typeof(broadcast_receiver));
                        PendingIntent pintent = PendingIntent.GetBroadcast(this, 4564, intent, PendingIntentFlags.UpdateCurrent);
                        mSession.SetMediaButtonReceiver(pintent);
                        mSession.Active = (true);
                        mSession.SetPlaybackState(pbs);
                        Thread.Sleep(7500);
                    }
                    else {
                        try
                        {
                            UnregisterReceiver(br);
                            break;
                        }
                        catch (Exception) {

                            break;
                        }
                    }
                }
            }
            catch (Exception) {
            }



        }
        public void OnAudioFocusChange(AudioFocus focusChange)
        {
            switch (focusChange)
            {
                case AudioFocus.Gain:
                    if (Clouding_service.gettearinstancia().musicaplayer== null)
                      

                    if (!Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                    {
                            Clouding_service.gettearinstancia().musicaplayer.Start();
                        
                    }
                    Clouding_service.gettearinstancia().musicaplayer.SetVolume(1.0f, 1.0f);//Turn it up!
                    break;
                case AudioFocus.Loss:
                    //We have lost focus stop!
                    Clouding_service.gettearinstancia().musicaplayer.Pause();
                    break;
                case AudioFocus.LossTransient:
                    //We have lost focus for a short time, but likely to resume so pause
                    Clouding_service.gettearinstancia().musicaplayer.Pause();
                    break;
                case AudioFocus.LossTransientCanDuck:
                    //We have lost focus but should still play at a lower 10% volume
                    if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                        Clouding_service.gettearinstancia().musicaplayer.SetVolume(.1f, .1f);//turn it down!
                    break;
            }
        }









}

}







