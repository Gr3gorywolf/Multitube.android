using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.Media.Session;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using Android.Speech;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using YoutubeSearch;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState = true)]

#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
    public class mainmenu_Offline : Android.Support.V7.App.AppCompatActivity, AudioManager.IOnAudioFocusChangeListener, ISurfaceHolderCallback
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
    {

        public bool connected = true;
        public int videoquality = -1;
        public bool videoon = false;
        public MediaSession mSession;
        public string downloadurrrl = "";
        public bool detenedor = true;
        public ListView lista;
        adapterlistaremoto adaptadol;
        public ImageView caratula2;
        public TextView label;
        public RelativeLayout lineall;
        public bool envideo = false;
        public bool contienevideo = false;
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
        public TcpListener oidorlistas;
        Thread threadstream;
        public ProgressBar progresobuffering;
        Thread threadlistas;
        public int previousprogress = 0;
        public bool qualitychanged;
        public Android.Support.V7.Widget.RecyclerView listasugerencias;
        public bool fromotherinstance = false;
        //   public IEnumerable<VideoInfo> videoinfoss;
        public Android.Media.MediaPlayer musicaplayer;
        public List<string> lapara = new List<string>();
        public List<string> laparalinks = new List<string>();
        public List<string> listacaratulas = new List<string>();
        public LinearLayout Layoutsugerencias;
        public List<TcpClient> clienteses = new List<TcpClient>();
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
        public SeekBar volumen;
        public string termino = "";
        bool bloquearporcentaje = false;
        public bool automated = true;
        public string ipadre = "";
        public int indiceactual = 0;
        public string imgurl = "";
        public int locanterior = 0;
        public int volumenactual = 100;
        public string nombreactual;
        public WebClient clientedelamusica = new WebClient();
        public string linkactual = "";
        ScrollView menuham;
        public  bool backupprompted = false;
        public static mainmenu_Offline instance;
        public ImageView fondo;
        public Bitmap fondoblurreado;
        broadcast_receiver br;
        DrawerLayout sidem;
        NavigationView itemsm;
        public string estadovolumen = "vol100()";
        public ImageView botonaccion;
        Thread threadmediasession;
        public SurfaceView video;
        public ISurfaceHolder holder;
        Android.Support.V7.Widget.CardView layoutbuttons;
        public ProgressBar barracarga;
        List<playlist> listalocal = new List<playlist>();
        public Dictionary<int, int> diccalidad;
        string JSONlistalocal = "";
        int noticode = new Random().Next(23, 999999);
        public historial objetohistorial;
        public Dictionary<string, playlistelements> listafavoritos = new Dictionary<string, playlistelements>();
        public Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout panel;
        Android.Support.V7.Widget.CardView solapa;
        ImageView blacksquare;
        int sizevideoportrait;
        int sizevideonormal;
        bool videoenholder = false;
        ImageView botonlike;
        PowerManager.WakeLock wake;
        public bool headeradded = false;
        public List<elementosugerencia> sugerenciasdeelemento = new List<elementosugerencia>();
        public List<YoutubeSearch.VideoInformation> sugerencias = new List<YoutubeSearch.VideoInformation>();
        ImageView vervideo;
        public static mainmenu_Offline gettearinstancia()
        {
            return instance;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.perfectmainoffline5);
            PowerManager manejador = (PowerManager)GetSystemService(PowerService);
             wake = manejador.NewWakeLock(WakeLockFlags.Partial, "MyApp::MyWakelockTag");
            wake.Acquire();
           
            instance = this;
     
            bool restaurada = false;
            if (savedInstanceState != null)
            {
                restaurada = savedInstanceState.ContainsKey("restaurado");
            }

            clasesettings.guardarsetting("onlineactivo", "si");
            /////////////////////////////////declaraciones//////////////////////////
            musicaplayer = new Android.Media.MediaPlayer();
            ipadre = clasesettings.gettearip();
           
            if (!restaurada)
            {
                oidor = new TcpListener(IPAddress.Parse(ipadre), 1024);
                oidor.Start();
                oidorlistas = new TcpListener(IPAddress.Parse(ipadre), 9856);
                oidorlistas.Start();

            }








            //  Console.WriteLine( );





            ///////////////////////////////#Botones#////////////////////////////////
            ///
            progresobuffering = FindViewById<ProgressBar>(Resource.Id.progresobuffering);
            menuham = FindViewById<ScrollView>(Resource.Id.linearLayout9);
            ImageView botonabrirmenu = FindViewById<ImageView>(Resource.Id.imageView22);
            TextView estadomenu = FindViewById<TextView>(Resource.Id.textView9);
            Layoutsugerencias = FindViewById<LinearLayout>(Resource.Id.layoutsugerencias);
            botonaccion = FindViewById<ImageView>(Resource.Id.btnaccion);
            LinearLayout layoutvolumen = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            ImageView listareprod = FindViewById<ImageView>(Resource.Id.imageView16);
            laimagen = FindViewById<ImageView>(Resource.Id.laimagen);
            barracarga = FindViewById<ProgressBar>(Resource.Id.progresoind);
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
            vervideo = FindViewById<ImageView>(Resource.Id.imageView6);
            blacksquare= FindViewById<ImageView>(Resource.Id.blacksquare);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout7);
            ImageView buscar = FindViewById<ImageView>(Resource.Id.imageView12);
            textbox = FindViewById<EditText>(Resource.Id.editText1);
            ImageView abrirbrowser = FindViewById<ImageView>(Resource.Id.imageView14);
            var botonvideo = FindViewById<ImageView>(Resource.Id.imageView19);
            botonlike = FindViewById<ImageView>(Resource.Id.imglike);
            //   lineall = FindViewById<RelativeLayout>(Resource.Id.linearLayout0);
            label = FindViewById<TextView>(Resource.Id.textView1);
            caratula2 = FindViewById<ImageView>(Resource.Id.imageView13);
            lista = FindViewById<ListView>(Resource.Id.listView1);
            volumen = FindViewById<SeekBar>(Resource.Id.seekBar2);
            botonsincronizar = FindViewById<ImageView>(Resource.Id.imageView18);
            porcientoreproduccion = FindViewById<SeekBar>(Resource.Id.seekBar1);
            var ll10 = FindViewById<LinearLayout>(Resource.Id.linearLayout10);
            layoutbuttons = FindViewById<Android.Support.V7.Widget.CardView>(Resource.Id.linearLayout6);
            var ll11 = FindViewById<LinearLayout>(Resource.Id.linearLayout11);
            var ll12 = FindViewById<LinearLayout>(Resource.Id.linearLayout12);
            var ll13 = FindViewById<LinearLayout>(Resource.Id.linearLayout13);
            var ll14 = FindViewById<LinearLayout>(Resource.Id.linearLayout14);
            var ll15 = FindViewById<LinearLayout>(Resource.Id.linearLayout15);
            var ll16 = FindViewById<LinearLayout>(Resource.Id.linearLayout16);
            sidem = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
            itemsm = FindViewById<NavigationView>(Resource.Id.content_frame);
            fondo = FindViewById<ImageView>(Resource.Id.imageView45);
            var barra1 = FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            video = FindViewById<SurfaceView>(Resource.Id.videoView1);
            var searchview = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);
            var botoncalidad = FindViewById<ImageView>(Resource.Id.imgcalidad);
            botoncalidad.SetBackgroundResource(Resource.Drawable.mp3);
            listasugerencias = FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.listasugerencias);
            video.SetBackgroundColor(Color.Transparent);
            diccalidad = new Dictionary<int, int>() {
            { -1,Resource.Drawable.mp3},
            { 360,Resource.Drawable.mq},
            { 720,Resource.Drawable.hq}
        };
            ////////////////////////////////////////////////////////////////////////
            ///

           
            Layoutsugerencias.Visibility = ViewStates.Gone;
       
            LinearLayoutManager enlinea = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listasugerencias.SetLayoutManager(enlinea);

            panel = FindViewById<Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout>(Resource.Id.sliding_layout);
            panel.IsUsingDragViewTouchEvents = true;
            panel.DragView = FindViewById<RelativeLayout>(Resource.Id.scrollable);
     solapa = FindViewById<Android.Support.V7.Widget.CardView>(Resource.Id.solapita);
            solapa.Click += delegate
            {

                if (panel.IsExpanded)
                    panel.CollapsePane();
                else
                    panel.ExpandPane();
            };
            lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
            //  lineall.SetBackgroundColor(Color.DarkGray);
         
            holder = video.Holder;
          
            videoquality = int.Parse(clasesettings.gettearvalor("video"));
           
            botoncalidad.SetBackgroundResource(diccalidad[videoquality]);
            lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            //   layoutbuttons.SetBackgroundColor(Color.ParseColor("#2b2e30"));
            layoutvolumen.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            //     barra1.SetBackgroundColor(Color.ParseColor("#2b2e30"));
            menuham.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            play.SetBackgroundResource(Resource.Drawable.pausebutton2);
            botonaccion.SetBackgroundResource(Resource.Drawable.pausebutton2);
            if (videoquality > 0)
                vervideo.Visibility = ViewStates.Visible;
            else
            {
               
                vervideo.Visibility = ViewStates.Gone;
            }
            video.Visibility = ViewStates.Invisible;
            vervideo.SetBackgroundResource(Resource.Drawable.video);
            animar2(lineall2);
            clasesettings.guardarsetting("elementosactuales", "");
            volumen.Max = 100;
            volumen.Progress = volumenactual;
            layoutvolumen.Visibility = ViewStates.Invisible;
            menuham.Visibility = ViewStates.Invisible;
            estadomenu.Text = "";
            botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
            codigoqr.SetImageBitmap(GetQRCode());
            SetSupportActionBar(toolbar);
           


            //Enable support action bar to display hamburger

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
          
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Reproductor";




           /* Drawable progressDrawable = porcientoreproduccion.ProgressDrawable.Mutate();
            progressDrawable.SetColorFilter( Color.DarkGray, Android.Graphics.PorterDuff.Mode.SrcIn);*/
            porcientoreproduccion.SecondaryProgressTintList=ColorStateList.ValueOf(Android.Graphics.Color.Gray);


            // SupportActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#2b2e30")));
            /////////////////////////////////////////////////
            ///
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });

            RunOnUiThread(() =>
            {
                var parcelable = lista.OnSaveInstanceState();             
                lista.Adapter = adaptadol;
                
                lista.OnRestoreInstanceState(parcelable);
            });
            ((ViewGroup)Layoutsugerencias.Parent).RemoveView(Layoutsugerencias);
            lista.AddHeaderView(Layoutsugerencias, null, false);
            lista.SetHeaderDividersEnabled(true);
            if (!restaurada)
            {



               
                new Thread(() =>
                {
                    llenarplaylist();
                }).Start();
                threadlistas = new Thread(() =>
                  {
                      cojerlistas();
                  });
               threadlistas.Start();
                threadstream = new Thread(new ThreadStart(cojerstream));
                threadstream.Start();

                new Thread(() =>
                {
                    ipchangerlistener();
                }).Start();
                new Thread(() =>
                {
                    iniciarservicio();
                    ponerporciento();                  
                }).Start();
            
                new Thread(() =>
                {
                   loadsuggestionslistener();
                }).Start();
                new Thread(() =>
                {
                   
                    listenplaylistchanges();
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
                mSession = new MediaSession(this, PackageName);
                Intent intent = new Intent(this, typeof(broadcast_receiver));
                PendingIntent pintent = PendingIntent.GetBroadcast(this, 4564, intent, PendingIntentFlags.UpdateCurrent);
                mSession.SetMediaButtonReceiver(pintent);
                mSession.Active = (true);
                mSession.SetPlaybackState(pbs);

            }
            catch (Exception)
            {

            }
            threadmediasession = new Thread(new ThreadStart(actualizarmediasesion));
            threadmediasession.Start();


            label.Selected = true;
            WallpaperManager wm = WallpaperManager.GetInstance(this);
            Drawable d = wm.Drawable;
            fondoblurreado = clasesettings.CreateBlurredImageformbitmap(this, 20, ((BitmapDrawable)d).Bitmap);
            sizevideoportrait = video.LayoutParameters.Height / 2;
            sizevideonormal = video.LayoutParameters.Height;






            if (File.Exists(clasesettings.rutacache + "/history.json"))
            {

                objetohistorial = JsonConvert.DeserializeObject<historial>(File.ReadAllText(clasesettings.rutacache + "/history.json"));
                if (objetohistorial == null)
                {
                    objetohistorial = new historial();
                    objetohistorial.videos = new List<playlistelements>();
                    objetohistorial.links = new Dictionary<string, int>();
                }
                else {
                    if (objetohistorial.videos == null)
                    {
                        objetohistorial.videos = new List<playlistelements>();

                    }
                    else
                    if(objetohistorial.links==null)
                    {
                        objetohistorial.links = new Dictionary<string, int>();
                    }
                }

            }
            else {

               objetohistorial  = new historial();
                objetohistorial.videos = new List<playlistelements>();
                objetohistorial.links = new Dictionary<string, int>();
            }

            if (File.Exists(clasesettings.rutacache + "/favourites.json"))
            {
                listafavoritos = JsonConvert.DeserializeObject<Dictionary<string, playlistelements>>(File.ReadAllText(clasesettings.rutacache + "/favourites.json"));
            }
          

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            fondo.SetBackgroundColor(Color.ParseColor("#2d3033"));

            clasesettings.recogerbasura();
            StartActivity(new Intent(this, typeof(actividadinicio)));
            OverridePendingTransition(0, 0);

            ///////////////////////////////#clicks#/////////////////////////////////


            caratula2.Click += delegate
            {

                if (panel.IsExpanded)
                    panel.CollapsePane();
                else
                    panel.ExpandPane();
            };
            botonlike.Click += delegate
            {
                animar(botonlike);
                if (linkactual.Trim() != "") {
                    var link = linkactual.Replace("https", "http");
                    var elemento = new playlistelements() {
                        link=link,
                        nombre=nombreactual
                    };
                  listafavoritos=  clasesettings.agregarfavoritos(this, listafavoritos, elemento);

                    if (!listafavoritos.ContainsKey(link))
                        botonlike.SetBackgroundResource(Resource.Drawable.heartoutline);
                    else 
                        botonlike.SetBackgroundResource(Resource.Drawable.heartcomplete);
                  
                    }

            };
            botoncalidad.Click += delegate
            {
                animar(botoncalidad);
                var spinner = new Spinner(this);
                spinner.SetPadding(60, 30, 60, 30);

                var listaitems = new List<IDictionary<string, object>>();
                string[] nombres = { "Solo audio", "Video 360p", "Video 720p" };
                int[] imagenes = { Resource.Drawable.mp3, Resource.Drawable.mq, Resource.Drawable.hq };
                for (int i = 0; i < 3; i++)
                {
                    var mapa = new JavaDictionary<string, object>();
                    mapa.Add("Imagen", imagenes[i]);
                    mapa.Add("Nombre", nombres[i]);
                    listaitems.Add(mapa);

                }

                string[] desde = { "Imagen", "Nombre" };
                int[] a = { Resource.Id.imageView1, Resource.Id.textView1 };
                spinner.Adapter = new SimpleAdapter(this, listaitems, Resource.Layout.layoutlistaplayerindependiente, desde, a);
                spinner.SetSelection(diccalidad.Keys.ToList().IndexOf(videoquality));
                new Android.Support.V7
                .App
                .AlertDialog
                .Builder(this)
                .SetTitle("Seleccione la calidad de reproduccion")
                .SetView(spinner)
                .SetPositiveButton("Listo", (aas, dfe) =>
                {
                    int quality = diccalidad.Keys.ToList()[spinner.SelectedItemPosition];
                    if (videoquality != quality)
                    {
                        videoquality = quality;
                        botoncalidad.SetBackgroundResource(diccalidad[videoquality]);
                        if (linkactual.Trim() != "")
                        {
                            new Thread(() =>
                            {

                                qualitychanged = true;
                     
                                buscarviddireckt(linkactual, true);
                            }).Start();
                        }
                        if (videoquality == -1)
                        {
                            vervideo.Visibility = ViewStates.Gone;

                            videoon = false;
                            animar(vervideo);
                            layoutbuttons.Alpha = 1f;
                            solapa.Alpha = 1f;
                            vervideo.SetBackgroundResource(Resource.Drawable.video);
                            video.Visibility = ViewStates.Invisible;
                            FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Visible;
                            blacksquare.Visibility = ViewStates.Visible;
                           // Clouding_service.gettearinstancia().musicaplayer.SetDisplay(null);
                            video.KeepScreenOn = false;
                            Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
                        }
                        else
                        {
                            vervideo.Visibility = ViewStates.Visible;
                        }

                    }
                })

                .Create()
                .Show();



            };
            panel.PanelExpanded += delegate
            {
                if (videoon) { 
                    layoutbuttons.Alpha = 0.7f;
                solapa.Alpha = 0.7f;
                    botoncalidad.Alpha = 1f;
                    botonlike.Alpha = 1f;
                    clasesettings.modoinmersivo(this.Window, false);
                }
                if (volumenactual == 0)
                    botonaccion.SetBackgroundResource(Resource.Drawable.volumelow);
                else
                if (volumenactual == 50)
                    botonaccion.SetBackgroundResource(Resource.Drawable.volumemedium);
                else
                if (volumenactual == 100)
                    botonaccion.SetBackgroundResource(Resource.Drawable.volumehigh);


            };
            panel.PanelCollapsed += delegate
            {

                if(videoon)
                    clasesettings.modoinmersivo(this.Window, true);
                layoutbuttons.Alpha = 1f;
                solapa.Alpha = 1f;
                if (Clouding_service.gettearinstancia() != null)
                {
                    if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                        botonaccion.SetBackgroundResource(Resource.Drawable.pausebutton2);
                    else
                        botonaccion.SetBackgroundResource(Resource.Drawable.playbutton2);
                }



            };
            searchview.QueryTextSubmit += delegate
            {

                if (searchview.Query.Trim().Length >= 3)
                {
                    new Thread(() =>
                    {

                        buscaryabrir(searchview.Query.Trim());

                    }).Start();
                }
                else
                {

                    Toast.MakeText(this, "La busqueda debe contener almenos 3 caracteres", ToastLength.Long).Show();
                }

            };

            video.Click += delegate
            {
                /* if (FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility == ViewStates.Visible)
                     FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility = ViewStates.Gone;
                 else
                     FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility = ViewStates.Visible;*/

                if (layoutbuttons.Alpha > 0f)
                {
                    botonlike.Alpha =0f;
                    solapa.Alpha = 0f;
                    botoncalidad.Alpha = 0f;
                    layoutbuttons.Alpha = 0f;
                }
                else {
                    botonlike.Alpha = 1f;
                    solapa.Alpha = 0.7f;
                    botoncalidad.Alpha = 1f;
                    layoutbuttons.Alpha = 0.7f;
                }

            };
            vervideo.Click += delegate
            {


                animar(vervideo);
                if (!videoon)
                    {
                        videoon = true;
                      
                        solapa.Alpha = 0.7f;
                        layoutbuttons.Alpha = 0.7f;
                        video.Visibility = ViewStates.Visible;
                        FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Gone;
                        blacksquare.Visibility = ViewStates.Gone;
                        vervideo.SetBackgroundResource(Resource.Drawable.videooff);
                        video.KeepScreenOn = true;
                        if (!videoenholder) {
                            // Clouding_service.gettearinstancia().musicaplayer.SetDisplay(holder);
                          
                            videoenholder = true;
                        }
                        
                       
                        Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                       setVideoSize();
                        clasesettings.modoinmersivo(this.Window, false);
                    }
                    else
                    {


                        videoon = false;
                        animar(vervideo);
                        layoutbuttons.Alpha = 1f;
                        solapa.Alpha = 1f;
                        vervideo.SetBackgroundResource(Resource.Drawable.video);
                        video.Visibility = ViewStates.Invisible;
                        FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Visible;
                        blacksquare.Visibility = ViewStates.Visible;
                      //  Clouding_service.gettearinstancia().musicaplayer.SetDisplay(null);
                        video.KeepScreenOn = false;
                        Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
                        clasesettings.modoinmersivo(this.Window, true);
                    }
                    
              
            };
            FindViewById<RelativeLayout>(Resource.Id.scrollable).Click += delegate
            {
                if (videoon)
                    video.PerformClick();

            };
            itemsm.NavigationItemSelected += (sender, e) =>
            {
                //  e.MenuItem.SetChecked(true);

                //react to click here and swap fragments or navigate
                e.MenuItem.SetChecked(true);
                e.MenuItem.SetChecked(true);



                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Descargar")
                {
                    if (zelda.Trim().Length > 0)
                    {

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


                        estadomenu.Text = "";
                    }
                }
                else
                  if (e.MenuItem.TitleFormatted.ToString().Trim() == "Navegador personalizado")
                {
                    Intent intento = new Intent(this, typeof(customsearcheract));
                    intento.PutExtra("ipadre", ipadre);
                    intento.PutExtra("color", colol);
                    StartActivity(intento);


                    estadomenu.Text = "";

                }
                else
                    if (e.MenuItem.TitleFormatted.ToString().Trim() == "Listas de reproduccion")
                {

                    Intent intentoo = new Intent(this, typeof(menulistaoffline));
                    intentoo.PutExtra("ipadre", ipadre);
                    if (laparalinks.Count >= 1)
                    {
                        string parte1 = "";
                        string parte2 = "";

                        foreach (string it in lapara)
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

                        intentoo.PutExtra("listaenlinea", listenlinea);
                    }
                    else
                    {
                        intentoo.PutExtra("listaenlinea", "");
                    }
                    StartActivity(intentoo);

                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";

                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Conectar clientes")
                {

                    animar(codigoqr);
                    Intent intetno = new Intent(this, typeof(qrcodigoact));
                  
                    StartActivity(intetno);
                 
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";
                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Sincronizar listas")
                {
                    Intent intento = new Intent(this, typeof(actividadsincronizacion));
                    intento.PutExtra("ipadre", ipadre);

                    StartActivity(intento);


                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";

                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Reproductor de videos")
                {
                    play.SetBackgroundResource(Resource.Drawable.playbutton2);


                    Clouding_service.gettearinstancia().musicaplayer.Stop();
                    Intent intento = new Intent(this, typeof(actvideo));
                    intento.PutExtra("link", linkactual);
                    intento.PutExtra("posactual", indiceactual.ToString());
                    clasesettings.guardarsetting("listaactual", string.Join("�", listacaratulas) + "�" + string.Join("�", laparalinks));
                    clasesettings.guardarsetting("progresovideoactual", Clouding_service.gettearinstancia().musicaplayer.CurrentPosition.ToString());
                    StartActivity(intento);

                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";

                    envideo = true;
                    Thread.Sleep(100);

                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Busqueda rapida")
                {
                    Intent intento = new Intent(this, typeof(actfastsearcher));
                    intento.PutExtra("ipadres", "localhost");
                    StartActivity(intento);

                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";


                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Inicio")
                {
                    if (actividadinicio.gettearinstancia() == null)
                    {
                        Intent intento = new Intent(this, typeof(actividadinicio));

                        StartActivity(intento);
                    }
                    else {

                        actividadinicio.gettearinstancia().Finish();
                        Intent intento = new Intent(this, typeof(actividadinicio));

                        StartActivity(intento);
                    }


                }
                e.MenuItem.SetChecked(false);
                e.MenuItem.SetChecked(false);
                sidem.CloseDrawers();

            };




            porcientoreproduccion.ProgressChanged += (aa, aaaa) =>
            {
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
                    estadomenu.Text = "Cerrar men�";
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
            botonaccion.Click += delegate
           {

               /* if(layoutvolumen.Visibility ==ViewStates.Visible)
                  {
                      layoutvolumen.Visibility = ViewStates.Invisible;
                  }
                  else
                  {

                      layoutvolumen.BringToFront();
                      animar2(layoutvolumen);
                      layoutvolumen.Visibility = ViewStates.Visible;
                  }*/
               animar(vervideo);
               if (panel.IsExpanded)
               {
                   Intent intentoo = new Intent(this, typeof(actmenuvolumen));
                   intentoo.PutExtra("ipadre", ipadre);
                   StartActivity(intentoo);
               }
               else
               {
                   play.PerformClick();
               }
           };

            botonbusqueda.Click += delegate
            {



            };


            botonvideo.Click += delegate
            {




                ///   clasesettings.guardarsetting("videoactivo", "si");
            };

            botonsincronizar.Click += delegate
            {

            };

            volumen.ProgressChanged += delegate
            {
                volumenactual = volumen.Progress;
                Clouding_service.gettearinstancia().musicaplayer.SetVolume(volumenactual * 0.01f, volumenactual * 0.01f);
                //  clasesettings.guardarsetting("cquerry","volact()"+  ">"+ ().ToString() + ">"+ ().ToString());


            };
            escuchar.Click += delegate
            {


                if (tienemicrofono() == true)
                {
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


                if (lapara.Count > 0)
                {

                    var posicion = 0;
                    if (headeradded)
                        posicion = easter.Position - 1;
                    else
                        posicion = easter.Position;
                    Intent intentoo = new Intent(this, typeof(deletedialogact));

                    intentoo.PutExtra("index", posicion.ToString());
                    intentoo.PutExtra("color", "DarkGray");
                    intentoo.PutExtra("titulo", lapara[posicion]);
                    intentoo.PutExtra("ipadress", ipadre);
                    intentoo.PutExtra("url", laparalinks[posicion]);
                    intentoo.PutExtra("imagen", @"https://i.ytimg.com/vi/" + laparalinks[posicion].Split('=')[1] + "/mqdefault.jpg");
                    StartActivity(intentoo);
                }








            };
            lista.ItemSelected += (sender, easter) =>
            {

            };


            atras.Click += (sender, easter) =>
        {
            if (buscando == false)
            {
                animar(atras);
                Thread proc = new Thread(new ThreadStart(anterior));
                proc.Start();

            }


        };
            abrirbrowser.Click += (sender, easter) =>
            {


            };
            listareprod.Click += delegate
                  {


                  };
            adelante.Click += (sender, easter) =>
             {
                 if (buscando == false)
                 {
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



            };




 



            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////

        }
        public override void OnBackPressed()
        {

            if (sidem.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                RunOnUiThread(() => { sidem.CloseDrawers(); });
            else
            {
                if (!panel.IsExpanded)
                    clasesettings.preguntarsimenuosalir(this);
                else
                    RunOnUiThread(() => panel.CollapsePane());
            }
            // base.OnBackPressed();
        }

        public void iniciarservicio()
        {
            StopService(new Intent(this, typeof(cloudingserviceonline)));
            StopService(new Intent(this, typeof(Clouding_serviceoffline)));
            StopService(new Intent(this, typeof(Clouding_service)));
            StopService(new Intent(this, typeof(serviciodownload)));
            StartService(new Intent(this, typeof(Clouding_service)));
            if (videoquality > 0)
            {
              
                RunOnUiThread(() =>
                {
                    videoon = true;
                   
                    solapa.Alpha = 1f;
                    layoutbuttons.Alpha = 1f;
                    video.Visibility = ViewStates.Visible;
                    FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Gone;
                    blacksquare.Visibility = ViewStates.Gone;
                    vervideo.SetBackgroundResource(Resource.Drawable.videooff);
                    video.KeepScreenOn = true;
                    if (!videoenholder)
                    {
                        // Clouding_service.gettearinstancia().musicaplayer.SetDisplay(holder);

                        videoenholder = true;
                    }


                    Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                    setVideoSize();
                    clasesettings.modoinmersivo(this.Window, false);
                });

            }
        }
     




        public void buscarvid()
        {


            string url = "";
            if (buscando == false)
            {



                // Searchercocinado cocina = new Searchercocinado();
                // cocina.devolverurl(textbox.Text);

                url = getearurl(termino);

                if (url != "%%nulo%%")
                {



                    RunOnUiThread(() => Toast.MakeText(this, "Cargando...", ToastLength.Long).Show());
                    buscando = true;
                    actualizarcaratula();
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
                    var asdd = clasesettings.gettearvideoid(url, envideo, videoquality);

                    if (asdd != null)
                    {
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
                                clasesettings.mostrarnotificacion(this, asdd.titulo, " Agregado a la cola!", url, noticode);
                            }
                            else
                            {
                                buscando = false;
                                RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                            }

                        }
                        adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);

                        RunOnUiThread(() =>
                        {
                            var parcelable = lista.OnSaveInstanceState();                         
                            lista.Adapter = adaptadol;
                            
                            lista.OnRestoreInstanceState(parcelable);
                        });

                        RunOnUiThread(() => label.Text = asdd.titulo);


                        string imagensilla = "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg";
                        imgurl = imagensilla;
                        Bitmap blur = null;
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                        {
                            //  blur = CreateBlurredImage(20, GetImageBitmapFromUrl(imagensilla));
                        }

                        Thread.Sleep(20);
                        var sinblur = GetImageBitmapFromUrl(imagensilla);
                        RunOnUiThread(() => caratula2.SetImageBitmap(sinblur));

                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                        {
                            //  RunOnUiThread(() => fondo.SetImageBitmap(blur));
                            RunOnUiThread(() => fondo.SetBackgroundColor(Color.ParseColor("#2d3033")));
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
                            contienevideo = false;
                            actvideo.gettearinstacia().buscaryreproducir(true);
                            Clouding_service.gettearinstancia().mostrarnotificacion();
                            actualizarcaratula();

                        }
                    }
                    else
                    {
                        RunOnUiThread(() =>
                        {
                           
                            Toast.MakeText(this, "Error al extraer el video posiblemente los servidores esten en mantenimiento", ToastLength.Long).Show();
                        });
                    }
                }
            }
            else
            {
                buscando = false;
            }
        }

        private Bitmap CreateBlurredImage(int radius, Bitmap img)
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
            if (connected)
            {
                string urll = getearurl(termino);
                if (urll.Length > 0)
                {
                    if (!encontroparecido(urll, laparalinks))
                    {
                        GR3_UiF.Geteartitulo titulo = new GR3_UiF.Geteartitulo();


                        if (lapara.Count == 0)
                        {

                            buscarvidth(urll, false);

                        }
                        else
                        {

                            laparalinks.Add(urll);
                            lapara.Add(titulo.GetVideoTitle(titulo.LoadJson(urll)));
                            listacaratulas.Add(lapara[lapara.Count - 1]);

                            adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();                             
                                lista.Adapter = adaptadol;
                            
                                lista.OnRestoreInstanceState(parcelable);
                            });
                            RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());
                            clasesettings.mostrarnotificacion(this, titulo.GetVideoTitle(titulo.LoadJson(urll)), " Agregado a la cola!", urll, noticode);
                            actualizarlista();

                        }


                    }
                    else
                    {

                        RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                    }


                }
            }
            else {

                RunOnUiThread(() => Toast.MakeText(this, "No hay conexion a internet", ToastLength.Long).Show());
            }
        }

        public void buscarvidth(string terminoi, bool automatedi)
        {


            string url = "";
            if (connected)
            {
                if (buscando == false)
                {

                    // Searchercocinado cocina = new Searchercocinado();
                    // cocina.devolverurl(textbox.Text);

                    url = getearurl(terminoi);

                    if (url != "%%nulo%%")
                    {



                        RunOnUiThread(() => Toast.MakeText(this, "Cargando...", ToastLength.Long).Show());
                        buscando = true;
                        actualizarcaratula();
                        zelda = url;
                        linkactual = url;

                        GR3_UiF.Geteartitulo getter = new GR3_UiF.Geteartitulo();
                        var Titulo = getter.GetVideoTitle(getter.LoadJson(url));

                        if (automatedi == false)
                        {
                            if (!encontroparecido(url, laparalinks))
                            {
                                lapara.Add(Titulo);
                                laparalinks.Add(url);
                                if (listacaratulas.Count > 0)
                                {
                                    listacaratulas[locanterior] = lapara[locanterior];
                                }
                                listacaratulas.Add(">" + Titulo + "<");
                                locanterior = lapara.Count - 1;
                                indiceactual = locanterior;
                                adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                                clasesettings.mostrarnotificacion(this, Titulo, " Agregado a la cola!", url, noticode);
                                RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());

                            }
                            else
                            {
                                //  buscando = false;
                                RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                            }
                        }



                        adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                        RunOnUiThread(() =>
                        {
                            var parcelable = lista.OnSaveInstanceState();                           
                            lista.Adapter = adaptadol;
                          
                            lista.OnRestoreInstanceState(parcelable);

                        });
                        RunOnUiThread(() => label.Text = Titulo);


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
                            //  RunOnUiThread(() => fondo.SetImageBitmap(blur));
                            RunOnUiThread(() => fondo.SetBackgroundColor(Color.ParseColor("#2d3033")));
                            fondoblurreado = blur;
                        }

                        actualizartodo();
                        var downloadurl = clasesettings.gettearvideoid(url, envideo, videoquality);
                        buscando = false;
                        if (downloadurl != null)
                            reproducir(downloadurl.downloadurl);
                        else
                            reproducir(null);
                        /// actualizartodo();

                    }
                    else
                    {

                        buscando = false;
                    }
                }
            }
            else {
                RunOnUiThread(() => Toast.MakeText(this, "No hay conexion a internet", ToastLength.Long).Show());
            }
        }






        public void reproddelistadireckt(int pos)
        {



            new Thread(() =>
            {
                termino = laparalinks[pos];
                listacaratulas[locanterior] = lapara[locanterior];
                locanterior = pos;
                listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";
                indiceactual = locanterior;
                adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                RunOnUiThread(() =>
                {
                    var parcelable = lista.OnSaveInstanceState();                   
                    lista.Adapter = adaptadol;
                   
                    lista.OnRestoreInstanceState(parcelable);
                });
                buscarvidth(termino, true);
            }).Start();
        }


        public void buscarviddireckt(string url, bool automatedi)
        {


            if (connected)
            {
                if (buscando == false)
                {

                    // Searchercocinado cocina = new Searchercocinado();
                    // cocina.devolverurl(textbox.Text);



                    if (url != "%%nulo%%")
                    {



                        RunOnUiThread(() => Toast.MakeText(this, "Cargando...", ToastLength.Long).Show());
                        buscando = true;
                        actualizarcaratula();
                        zelda = url;
                        linkactual = url;
                        GR3_UiF.Geteartitulo getter = new GR3_UiF.Geteartitulo();
                        var asdd = getter.GetVideoTitle(getter.LoadJson(url));




                        if (automatedi == false)
                        {
                            if (!encontroparecido(url, laparalinks))
                            {
                                lapara.Add(asdd);
                                laparalinks.Add(url);
                                if (listacaratulas.Count > 0)
                                {
                                    listacaratulas[locanterior] = lapara[locanterior];
                                }
                                listacaratulas.Add(">" + asdd + "<");
                                locanterior = lapara.Count - 1;
                                indiceactual = locanterior;
                                adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                                /*    clasesettings.mostrarnotificacion(this, asdd.titulo, " Agregado a la cola!", url, noticode);*/
                            }
                            else
                            {
                                // buscando = false;
                                RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                            }
                        }



                        adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                        RunOnUiThread(() =>
                        {
                            try
                            {
                                var parcelable = lista.OnSaveInstanceState();
                                lista.Adapter = adaptadol;
                                lista.OnRestoreInstanceState(parcelable);
                            }
                            catch (Exception) {
                            }

                        });
                        RunOnUiThread(() => label.Text = asdd);


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
                            //RunOnUiThread(() => fondo.SetImageBitmap(blur));

                            RunOnUiThread(() => fondo.SetBackgroundColor(Color.ParseColor("#2d3033")));
                            fondoblurreado = blur;
                        }



                        actualizartodo();
                        var video = clasesettings.gettearvideoid(url, envideo, videoquality);
                        buscando = false;
                        if (video != null)
                            reproducir(video.downloadurl);
                        else
                            reproducir(null);
                        //actualizartodo();



                    }
                    else
                    {

                        buscando = false;
                    }
                }
            }
            else {
                RunOnUiThread(() => Toast.MakeText(this, "No hay conexion a internet", ToastLength.Long).Show());
            }
        }


        public void agregarvidth(string terminoi)
        {
            string urll = "";
            if (connected)
            {
                urll = getearurl(terminoi);
                if (urll.Length > 0)
                {
                    if (!encontroparecido(urll, laparalinks))
                    {


                        GR3_UiF.Geteartitulo titulo = new GR3_UiF.Geteartitulo();


                        if (lapara.Count == 0)
                        {

                            buscarvidth(urll, false);

                        }
                        else
                        {
                            laparalinks.Add(urll);
                            lapara.Add(titulo.GetVideoTitle(titulo.LoadJson(urll)));
                            listacaratulas.Add(lapara[lapara.Count - 1]);

                            adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();                              
                                lista.Adapter = adaptadol;
                               
                                lista.OnRestoreInstanceState(parcelable);
                            });
                            RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());
                            clasesettings.mostrarnotificacion(this, titulo.GetVideoTitle(titulo.LoadJson(urll)), " Agregado a la cola!", urll, noticode);
                            actualizarlista();

                        }


                    }
                    else
                    {
                        RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                    }

                }
            }
            else
            {

                RunOnUiThread(() => Toast.MakeText(this, "No hay conexion a internet", ToastLength.Long).Show());
            }
        }



        public void agregarviddireckt(string urll, string titulo)
        {
            if (connected)
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
                        
                            adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();                            
                               lista.Adapter = adaptadol;
                              
                          
                                lista.OnRestoreInstanceState(parcelable);
                            });
                        
                            RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());
                            /* clasesettings.mostrarnotificacion(this, titulo, " Agregado a la cola!", urll, noticode);*/
                            actualizarlista();

                        }


                    }
                    else
                    {
                        RunOnUiThread(() => Toast.MakeText(this, "El elemento ya existe en la lista de reproduccion", ToastLength.Long).Show());
                    }

                }
            }
            else {
                RunOnUiThread(() => Toast.MakeText(this, "No hay conexion a internet", ToastLength.Long).Show());
            }
        }

        public void siguiente()
        {
            try
            {
              
               
                    if (laparalinks.ToArray().Length-1 >= indiceactual+1 && !buscando)
                    {
                    if (lapara.Count > 0 && lapara[indiceactual + 1].Trim().Length > 0 )
                      {
                        termino = laparalinks[indiceactual + 1];


                        listacaratulas[locanterior] = lapara[locanterior];
                        locanterior = indiceactual + 1;
                        listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";

                        adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                        RunOnUiThread(() =>
                        {
                            var parcelable = lista.OnSaveInstanceState();                          
                            lista.Adapter = adaptadol;
                            
                            lista.OnRestoreInstanceState(parcelable);
                        });
                        automated = true;
                        indiceactual++;
                        new Thread(() =>
                        {
                            buscarvidth(laparalinks[indiceactual], true);
                        }).Start();
                      }
                    }
                    else {
                        if (sugerenciasdeelemento.Count > 0 && !buscando) {

                             if(clasesettings.gettearvalor("automatica")=="si")
                                   buscarviddireckt(sugerenciasdeelemento[0].link, false);
                        }
                        
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
                    listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";

                    adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                    RunOnUiThread(() => lista.Adapter = adaptadol);
                    automated = true;
                    indiceactual--;
                    new Thread(() =>
                    {
                        buscarvidth(laparalinks[indiceactual], true);
                    }).Start();


                }
            }
            catch (Exception)
            {

            }

        }




        public void reproducir(string downloadurl)
        {




            if (downloadurl != null)
            {

                try
                {
                    contienevideo = false;
                    nombreactual = label.Text;

                    Clouding_service.gettearinstancia().tituloactual = label.Text;
                    Clouding_service.gettearinstancia().linkactual = linkactual;
                    previousprogress = Clouding_service.gettearinstancia().musicaplayer.CurrentPosition;
                    Clouding_service.gettearinstancia().reproducir(downloadurl);
                    if (videoquality > 0 && actividadinicio.gettearinstancia()!=null) {

                        var instinicio = actividadinicio.gettearinstancia();
                        instinicio.RunOnUiThread(() =>
                        {
                            if (instinicio.alertareproducirvideo != null)
                            {
                                if (instinicio.alertareproducirvideo.IsShowing)
                                    instinicio.alertareproducirvideo.Dismiss();
                            }
                            instinicio.alertareproducirvideo = new Android.Support.V7.App.AlertDialog
                                 .Builder(instinicio).SetTitle("Atencion")
                                 .SetMessage("Se esta reproduciendo un video. Desea verlo en el reproductor?\n\n Nota:Al deslizar el video hacia abajo podra ver la cola de reproduccion")
                                 .SetCancelable(false)
                                 .SetPositiveButton("Si", (aaa, aaaaa) =>
                                 {
                                     instinicio.Finish();
                                     mainmenu_Offline.gettearinstancia().RunOnUiThread(() =>
                                     {
                                         mainmenu_Offline.gettearinstancia().solapa.PerformClick();
                                     });

                                 })
                                 .SetNegativeButton("No", (aa, aaaa) =>
                                 {

                                 })
                                 .Create();

                            instinicio.alertareproducirvideo.Show();
                        });
                    }

                    RunOnUiThread(() => { play.SetBackgroundResource(Resource.Drawable.pausebutton2); });
                    new Thread(() =>
                    {
                        actualizartodo();
                    }).Start();

                    clasesettings.recogerbasura();
                }
                catch (Exception) {
                    RunOnUiThread(() => Toast.MakeText(this, "Error al reproducir el elemento", ToastLength.Long).Show());
                }

                try
                {
                    rechargestartmenu();
                }
                catch (Exception e)
                {
                    var ex = e;
                    RunOnUiThread(() => Toast.MakeText(this, "Ha ocurrido un error al registrar el elemento en el historial.", ToastLength.Long).Show());
                }
            }
            else {
                RunOnUiThread(() => Toast.MakeText(this, "Error al conectar al obtener el elemento. Posiblemente haya problemas de conexion", ToastLength.Long).Show());
                new Thread(() =>
                {
                    actualizartodo();
                }).Start();

            }
            //TODO whatever you wanted to happen on completion









        }



        public void rechargestartmenu() {

            string normalizedlink = linkactual.Replace("https", "http");


            objetohistorial.videos.RemoveAll(ax => ax.link == normalizedlink);


            objetohistorial.videos.Add(new playlistelements
            {
                nombre = label.Text,
                link = normalizedlink

            });

            RunOnUiThread(() =>
            {
                if (!listafavoritos.ContainsKey(normalizedlink))
                    botonlike.SetBackgroundResource(Resource.Drawable.heartoutline);
                else
                    botonlike.SetBackgroundResource(Resource.Drawable.heartcomplete);
            });

            if (objetohistorial.links.ContainsKey(normalizedlink))
                objetohistorial.links[normalizedlink]++;
            else
            {

                objetohistorial.links.Add(normalizedlink, 1);

            }
            new Thread(() =>
            {
                savehistory();
            }).Start();
            if (actividadinicio.gettearinstancia() != null)
            {
                actividadinicio.gettearinstancia().recargarhistorial();

            }



        }
        public void savehistory() {

            try
            {
                var archivo = File.CreateText(clasesettings.rutacache + "/history.json");
                archivo.Write(JsonConvert.SerializeObject(objetohistorial));
                archivo.Close();
            }
            catch (Exception)
            {
                Thread.Sleep(5000);
                savehistory();
            }
        }

        public void ponerporciento()
        {
            var activo = false;

            while (true)
            {


                if (instance == null)
                    break;

                try
                {
                    if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying == true  && !bloquearporcentaje)
                    {

                        if (!panel.IsExpanded)
                        {

                            RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.pausebutton2));
                        }
                        else {
                            RunOnUiThread(() => porcientoreproduccion.Max = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.Duration));
                            RunOnUiThread(() => porcientoreproduccion.Progress = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition));
                        }
                    }
                    else
                    {
                        if (!panel.IsExpanded)
                        {

                            RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.playbutton2));
                        }
                    }
                }
                catch (Exception)
                {
                    

                }


                if (buscando != activo)
                {
                    activo = buscando;
                    RunOnUiThread(() =>
                    {
                        if (barracarga.Visibility == ViewStates.Gone)
                        {

                            barracarga.Visibility = ViewStates.Visible;
                            if (videoon)
                                progresobuffering.Visibility = ViewStates.Visible;

                        }
                        else
                        {
                            barracarga.Visibility = ViewStates.Gone;
                            progresobuffering.Visibility = ViewStates.Gone;
                        }


                    });


                }

               
                Thread.Sleep(1000);
            }





        }
        public void actualizarlista()
        {
            List<TcpClient> clientesact = new List<TcpClient>();








            var JSONlista = JsonConvert.SerializeObject(lapara);
            var JSONlinks = JsonConvert.SerializeObject(laparalinks);
            var headers = $"[%%CARATULA%%][%%LINKS%%]{JSONlinks}[%%TITLES%%]{JSONlista}";



            List<string> listaips = new List<string>();
            foreach (TcpClient c in clienteses)
            {

                var ipactual = ((IPEndPoint)c.Client.RemoteEndPoint).Address.ToString();
                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true && listaips.IndexOf(ipactual)==-1)
                {
                   
                    new Thread(() =>
                    {

                        clientesact.Add(c);


                        try
                        {

                           
                            c.Client.Send(System.Text.Encoding.Default.GetBytes(headers));
                            listaips.Add(ipactual);


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
            /* clasesettings.guardarsetting("listaactual", string.Join("�", listacaratulas) + "�" + string.Join("�", laparalinks));
             clasesettings.guardarsetting("elementosactuales", listenlinea + "$" + listenlinea2);*/
            listaips = new List<string>();
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
            var JSONlista = JsonConvert.SerializeObject(lapara.ToArray());
            var JSONlinks = JsonConvert.SerializeObject(laparalinks.ToArray());
            var JSONcaratula = JsonConvert.SerializeObject(new string[] { imgurl, label.Text, zelda, estadovolumen, buscando.ToString(), Android.OS.Build.Model });
            var headers = $"[%%CARATULA%%]{JSONcaratula}[%%LINKS%%]{JSONlinks}[%%TITLES%%]{JSONlista}";




            List<string> ipenviadas = new List<string>();
            foreach (TcpClient c in clienteses)
            {
                var ipactual = ((IPEndPoint)c.Client.RemoteEndPoint).Address.ToString();

                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true && ipenviadas.IndexOf(ipactual)==-1)
                {
                    new Thread(() =>
                    {
                        try
                        {


                            clientesact.Add(c);

                            c.Client.Send(System.Text.Encoding.Default.GetBytes(headers));
                            ipenviadas.Add(ipactual);
                        }
                        catch (Exception)
                        {

                        }

                    }).Start();



                }


            }
            ipenviadas.Clear();
            /*     clasesettings.guardarsetting("listaactual", string.Join("�", listacaratulas) + "�" + string.Join("�", laparalinks));
                 clasesettings.guardarsetting("elementosactuales", listenlinea + "$" + listenlinea2);*/
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
                if (ee.Trim() != "")
                {
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
        public void llenarplaylist()
        {

            listalocal.Clear();
            if (System.IO.Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/"))
            {
                string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");

                foreach (var elementos in items)
                {
                    var playlist = new playlist();
                    playlist.nombre = System.IO.Path.GetFileNameWithoutExtension(elementos);
                    var text = File.ReadAllText(elementos).Trim();
                    try
                    {
                        playlist.portrait = text.Split('$')[1].Split(';')[0];
                    }
                    catch (Exception)
                    {
                        playlist.portrait = "";
                    }
                    playlist.elementos = new List<playlistelements>();
                    var nombreses = text.Split('$')[0].Split(';').ToList();
                    var linkeses = text.Split('$')[1].Split(';').ToList();

                    for (int e = 0; e < nombreses.Count; e++)
                    {
                        if (nombreses[e].Trim() != "" && linkeses[e].Trim() != "")
                        {
                            playlist.elementos.Add(new playlistelements()
                            {
                                nombre = nombreses[e].Trim()
                              ,
                                link = linkeses[e].Trim()

                            });
                        }

                    }




                    listalocal.Add(playlist);

                }

            }

            JSONlistalocal = JsonConvert.SerializeObject(listalocal);
            Console.WriteLine(JSONlistalocal);


        }

        public void cojerlistas()
        {


            byte[] bites = new byte[1000000];
            int byteslenght = 0;
            string jsoncompleto = "";
            string datacompleta = "";
            int lenviejo = 0;
            bool enviolistas = false;




            TcpClient clieente = new TcpClient();
            try
            {
                clieente = oidorlistas.AcceptTcpClient();
                if (clieente.Connected == true)
                {



                    Thread procc = new Thread(new ThreadStart(cojerlistas));
                    procc.Start();

                }
            }
            catch (Exception)
            {

            }
            while (detenedor == true && clieente.Connected)
            {

               
                if (JSONlistalocal.Trim() != "" && (!enviolistas || lenviejo != JSONlistalocal.Length) )
                {
                    lenviejo = JSONlistalocal.Length;
                    enviolistas = true;
                    clieente.Client.Send(System.Text.Encoding.UTF8.GetBytes(JSONlistalocal));
                }






                var stream = clieente.GetStream();
                while (stream.DataAvailable)
                {
                    byteslenght = stream.Read(bites, 0, bites.Length);
                    datacompleta += System.Text.Encoding.UTF8.GetString(bites, 0, byteslenght);
                }


                var listareproduccion = new playlist();
                if (datacompleta.Trim().Length > 0 && datacompleta.Trim() != "")
                {
                    var querry = datacompleta.Split(new[] { "__==__==__" }, StringSplitOptions.None)[0];
                    jsoncompleto = datacompleta.Split(new[] { "__==__==__" }, StringSplitOptions.None)[1];

                    if (jsoncompleto.Trim() != "none")
                        listareproduccion = JsonConvert.DeserializeObject<playlist>(jsoncompleto);


                    if (querry == "Fromremote")
                    {


                        RunOnUiThread(() =>
                    {

                        var alerta = new Android.Support.V7.App.AlertDialog.Builder(this)
                                 .SetTitle("Advertencia")
                                 .SetMessage("Desea reproducir la lista de reproduccion remota: " + listareproduccion.nombre + "??")
                                 .SetPositiveButton("Si", (aa, aaa) =>
                                 {
                                     new Thread(() =>
                                     {
                                         reproducirlistaremota(listareproduccion);
                                     }).Start();
                                 })
                                 .SetNegativeButton("No", (aa, aaa) => { })

                                 .Create();
                        alerta.Window .SetType(WindowManagerTypes.Toast);
                        alerta.Show();
                    });


                    }
                    else
                    if (querry == "Fromlocal")
                    {

                        RunOnUiThread(() =>
                        {

                            var aler = new Android.Support.V7.App.AlertDialog.Builder(this)
                                   .SetTitle("Advertencia")
                                   .SetMessage("Desea reproducir la lista de reproduccion local: " + listareproduccion.nombre + "??")
                                   .SetPositiveButton("Si", (aa, aaa) =>
                                   {
                                       new Thread(() =>
                                       {
                                           reproducirlalistalocal(listareproduccion.nombre);
                                       }).Start();
                                   })
                                   .SetNegativeButton("No", (aa, aaa) => { })
                                   .Create();
                            aler.Window.SetType(WindowManagerTypes.Toast);
                            aler.Show();

                        });
                    }
                    else
                    if (querry == "Receive")
                    {

                        RunOnUiThread(() =>
                        {

                            var alerta = new Android.Support.V7.App.AlertDialog.Builder(this)
                                   .SetTitle("Advertencia")
                                   .SetMessage("Desea guardar la lista de reproduccion remota: " + listareproduccion.nombre + "??\n NOTA:\nsi hay una lista con este mismo nombre sera sustituida por esta")
                                   .SetPositiveButton("Si", (aa, aaa) =>
                                   {
                                       new Thread(() =>
                                       {
                                           var nombreses = string.Join(";", listareproduccion.elementos.Select(axx => axx.nombre).ToArray()) + ";";
                                           var linkeses = string.Join(";", listareproduccion.elementos.Select(axx => axx.link).ToArray()) + ";";
                                           var archi = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listareproduccion.nombre);
                                           archi.Write(nombreses + "$" + linkeses);
                                           archi.Close();

                                           RunOnUiThread(() => Toast.MakeText(this, "Lista guardada exitosamente", ToastLength.Long).Show());
                                           new Thread(() => { llenarplaylist(); }).Start();
                                       }).Start();
                                   })
                                   .SetNegativeButton("No", (aa, aaa) => { })
                                   .Create();
                            alerta.Window.SetType(WindowManagerTypes.Toast);
                            alerta.Show();

                        });
                    }

                    else
                    if (querry == "Sendplaylist")
                    {

                        var listilla = new List<playlistelements>();
                        var texto = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listareproduccion.nombre);
                        var nombreses = texto.Split('$')[0].Split(';').ToList();
                        var links = texto.Split('$')[1].Split(';').ToList();

                        var listaelementos = new List<playlistelements>();
                        for (int i = 0; i < nombreses.Count; i++)
                        {

                            if (nombreses[i].Trim() != "" || links[i].Trim() != "")
                            {
                                var elemento = new playlistelements()
                                {
                                    nombre = nombreses[i],
                                    link = links[i]
                                };
                                listaelementos.Add(elemento);
                            }
                        }

                        /*  mainmenu.gettearinstancia()
                               .clientelalistas
                               .Client
                               .Send(Encoding.UTF8.GetBytes(query + "__==__==__" + JsonConvert.SerializeObject(inst.listaslocales[inst.playlistidx])));*/

                        clieente.Client.Send(System.Text.Encoding.UTF8.GetBytes("__==__==__" + JsonConvert.SerializeObject(listaelementos)));
                    }

                }







                datacompleta = "";
                Thread.Sleep(1000);
            }



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
                    byte[] bites = new byte[200000];


                    int o;

                    while ((o = stream.Read(bites, 0, bites.Length)) != 0 && detenedor == true && clieente.Connected)
                    {

                        string capturado = System.Text.Encoding.UTF8.GetString(bites, 0, o);
                        if (capturado.Trim() == "fullscreen()")
                        {
                            RunOnUiThread(() =>
                            {

                                if (panel.IsExpanded)
                                {
                                    panel.CollapsePane();
                                }
                                else
                                {
                                    panel.ExpandPane();
                                }
                                if (videoquality > 0 && !videoon)
                                    vervideo.PerformClick();
                            });
                        }
                        else
                      if (capturado.Trim() == "notificar()")
                        {

                        }
                        else
                        if (capturado.Trim() == "eliminarelemento()")
                        {

                            eliminarelemento = true;
                        }
                        else
                          if (eliminarelemento == true)
                        {
                            eliminarelemento = false;
                            eliminarelementodireckt(Convert.ToInt32(capturado));
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

                                ///  reproducirlalista(Convert.ToInt32(capturado.Trim()));
                            }).Start();
                            //   capturado = "";
                        }
                        else
                      if (capturado.Trim() == "playpause()")
                        {

                            // capturado = "";
                            if (!envideo)
                            {
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
                            if (capturado.Trim() == "vol0()")
                        {
                            estadovolumen = "vol0()";
                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(0 * 0.01f, 0 * 0.01f);
                            RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.volumelowrojo));
                            actualizarcaratula();
                        }
                        else
                             if (capturado.Trim() == "vol50()")
                        {
                            estadovolumen = "vol50()";
                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(50 * 0.01f, 50 * 0.01f);
                            RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.volumemediumrojo));
                            actualizarcaratula();
                        }
                        else
                             if (capturado.Trim() == "vol100()")
                        {
                            estadovolumen = "vol100()";
                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(100 * 0.01f, 100 * 0.01f);
                            RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.volumehighrojo));
                            actualizarcaratula();
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
                        if (capturado.Trim() == "deviceinfo()")
                        {
                            //   clieente.Client.Send("[%%DEVICEINFO%%]" +);

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
                                new Thread(() =>
                                {
                                    termino = laparalinks[int.Parse(capturado)];
                                    listacaratulas[locanterior] = lapara[locanterior];
                                    locanterior = int.Parse(capturado);
                                    listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";
                                    indiceactual = locanterior;
                                    adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                                    RunOnUiThread(() =>
                                    {
                                        var parcelable = lista.OnSaveInstanceState();                                  
                                        lista.Adapter = adaptadol;
                                     
                                        lista.OnRestoreInstanceState(parcelable);
                                    });
                                    buscarvidth(termino, true);
                                }).Start();
                                //  capturado = "";
                            }



                        }
                        else
                        if (agregado == true)
                        {
                            agregado = false;
                            new Thread(() =>
                            {
                                agregarvidth(capturado);
                            }).Start();
                            //  capturado = "";
                        }
                        else
                        if (agregado != true && pidiendoindex == false && capturado.Trim().Length > 1 && !nocontienequerry(capturado))
                        {

                            new Thread(() =>
                            {
                                buscarvidth(capturado, false);
                            }).Start();
                            //  capturado = "";
                        }




                    }
                }
                catch (Exception) { }

          
                Thread.Sleep(500);
                
            }

            oidor.Stop();
        }

        public void actualizarcaratula()
        {

            List<TcpClient> clientesact = new List<TcpClient>();

            var JSONcaratula = JsonConvert.SerializeObject(new string[] { imgurl, label.Text, zelda, estadovolumen, buscando.ToString(), Android.OS.Build.Model });
            var headers = $"[%%CARATULA%%]{ JSONcaratula }[%%LINKS%%][%%TITLES%%]";
            List<string> listaips = new List<string>();
            foreach (TcpClient c in clienteses)
            {
                var ipactual = ((IPEndPoint)c.Client.RemoteEndPoint).Address.ToString();
                if (prueba_de_lista_generica.SocketExtensions.IsConnected(c) == true && listaips.IndexOf(ipactual)==-1)
                {
                    new Thread(() =>
                    {
                        try
                        {

                            clientesact.Add(c);
                            listaips.Add(ipactual);
                            c.Client.Send(System.Text.Encoding.Default.GetBytes(headers));
                        }
                        catch (Exception)
                        {

                        }

                    }).Start();


                }

            }
            listaips.Clear();
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
                            RunOnUiThread(() => FindViewById<ImageView>(Resource.Id.bgimg).SetImageBitmap(imageBitmap));
                        }

                    }

            }
            catch (Exception) { RunOnUiThread(() => caratula2.SetImageBitmap(null)); }
            return imageBitmap;
        }
        public bool tienemicrofono()
        {
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
        public void animar(Java.Lang.Object imagen)
        {
            if (!fromotherinstance)
            {
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
                animacion.SetDuration(300);
                animacion.Start();
                Android.Animation.ObjectAnimator animacion2 = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleY", 0.5f, 1f);
                animacion2.SetDuration(300);
                animacion2.Start();
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
        private Bitmap GetQRCode()
        {

            var writer = new ZXing.Mobile.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options.Margin = 1;
            writer.Options.Height = 125;
            writer.Options.Width = 125;





            return writer.Write(ipadre);
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



                    c.Client.Send(System.Text.Encoding.Default.GetBytes("caratula()><;" + imgurl + ";" + label.Text.Replace('$', ' ') + ";" + "darkgray" + ";" + zelda));

                    Thread.Sleep(400);

                    c.Client.Send(System.Text.Encoding.Default.GetBytes("listar()><;" + listaenlinea));



                }
            }


        }

        public void loadbackupplaylist()
        {
            var back = JsonConvert.DeserializeObject<backupplaylists>(File.ReadAllText(clasesettings.rutacache + "/backupplaylist.json"));
            lapara.Clear();
            laparalinks.Clear();
            listacaratulas.Clear();
            lapara = back.titles;
            laparalinks = back.links;
            if (laparalinks[lapara.Count - 1].Trim() == "")
            {
                laparalinks.RemoveAt(lapara.Count - 1);
            }
            listacaratulas = new List<string>(lapara);
            listacaratulas[back.posactual] = ">" + lapara[back.posactual] + "<";
            locanterior = back.posactual;
            indiceactual = back.posactual;
            linkactual = laparalinks[back.posactual];


            adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
            RunOnUiThread(() =>
            {
                var parcelable = lista.OnSaveInstanceState();

                lista.Adapter = adaptadol;

                lista.OnRestoreInstanceState(parcelable);

            });

            new Thread(() =>
            {
                buscarvidth(laparalinks[back.posactual], true);
            }).Start();

        }
        public void listenplaylistchanges() {
            int currentcount = 0;
            int currentpos = 0;
            while (detenedor && instance!=null) {
                if ((lapara.Count != currentcount  || indiceactual!=currentpos)&& lapara.Count > 0 && backupprompted)
                {
                    currentcount = lapara.Count;
                    currentpos = indiceactual;
                    savebackupplaylist();
                }
                else
                if (lapara.Count == 0 && backupprompted)
                {

                    if (File.Exists(clasesettings.rutacache + "/backupplaylist.json"))
                        File.Delete(clasesettings.rutacache + "/backupplaylist.json");
                   
                }
                Thread.Sleep(3000);
                
            }

        }
        public void savebackupplaylist() {

           
            backupplaylists back = new backupplaylists() {
                titles = lapara,
                links = laparalinks,
                posactual = indiceactual,
                listacaratulas=listacaratulas
            };
         var archivo=File.CreateText(clasesettings.rutacache + "/backupplaylist.json");
            archivo.Write(JsonConvert.SerializeObject(back));
            archivo.Close();
            
            }

        public void reproducirlistaremota(playlist elementos)
        {

          


            if (elementos.elementos.Count > 0)
            {
                lapara.Clear();
                laparalinks.Clear();
                listacaratulas.Clear();
                lapara = elementos.elementos.Select(x => x.nombre).ToList();
                laparalinks = elementos.elementos.Select(x => x.link).ToList();
                if (laparalinks[lapara.Count - 1].Trim() == "")
                {
                    laparalinks.RemoveAt(lapara.Count - 1);
                }
                listacaratulas = new List<string>(lapara);
                listacaratulas[0] = ">" + lapara[0] + "<";
                locanterior = 0;
                indiceactual = 0;
                linkactual = laparalinks[0];


                adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                RunOnUiThread(() =>
                {
                    var parcelable = lista.OnSaveInstanceState();        
                   
                    lista.Adapter = adaptadol;
                    
                    lista.OnRestoreInstanceState(parcelable);
          
                });


                buscarvidth(laparalinks[0], true);
            }
            else
            {
                RunOnUiThread(() => Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show());
            }
        }



        public void reproducirlalistalocal(string nombre)
        {
            string listainterna;


            StreamReader tupara = File.OpenText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + nombre);
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
            indiceactual = 0;
            linkactual = laparalinks[0];

            adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
            RunOnUiThread(() =>
            {
                try { 
                var parcelable = lista.OnSaveInstanceState();            
                lista.Adapter = adaptadol;
               
                lista.OnRestoreInstanceState(parcelable);
                 }
                            catch (Exception)
                       {
                     }
        });


            buscarvidth(laparalinks[0], true);


        }
        public void actualizartodaslaslistas()
        {

            foreach (TcpClient cl in clienteses)
            {
                if (prueba_de_lista_generica.SocketExtensions.IsConnected(cl) == true)
                {
                    cl.Client.Send(System.Text.Encoding.Default.GetBytes("actualizaa()"));
                    Thread.Sleep(10);
                }
            }
        }




        public void eliminarelementodireckt(int indice)
        {

            new Thread(() =>
            {
                int indicedelmedio = laparalinks.IndexOf(linkactual);

                if (lapara.Count > 1)
                {

                    if (indice == indicedelmedio)
                    {
                        RunOnUiThread(() => Toast.MakeText(this, "No se puede eliminar el elemento actual", ToastLength.Long).Show());



                    }
                    else
                    {

                        clasesettings.mostrarnotificacion(this, lapara[indice], " Eliminado de la cola!", laparalinks[indice], noticode);
                        laparalinks.RemoveAt(indice);
                        lapara.RemoveAt(indice);
                        listacaratulas.RemoveAt(indice);
                        indiceactual = laparalinks.FindIndex(x=>x.Split('=')[1].ToLower()==linkactual.Split('=')[1].ToLower());
                        locanterior = indiceactual;

                        adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                        RunOnUiThread(() =>
                        {
                            var parcelable = lista.OnSaveInstanceState();                       
                            lista.Adapter = adaptadol;
                           
                            lista.OnRestoreInstanceState(parcelable);
                        });

                        actualizarlista();
                    }
                }
            }).Start();

        }


        protected override void OnDestroy()
        {
            detenedor = false;

            try
            {
                oidor.Stop();
                oidorlistas.Stop();
            }
            catch (Exception e)
            {
                var mensaje = e;

            }



            wake.Release();
            clasesettings.recogerbasura();
            if (Clouding_service.gettearinstancia() != null)
            {
                Clouding_service.gettearinstancia().musicaplayer.Reset();
            }
            StopService(new Intent(this, typeof(Clouding_service)));
            //   

            clasesettings.guardarsetting("onlineactivo", "no");
            //   threadmediasession.Abort();
            instance = null;
            try { UnregisterReceiver(br); } catch (Exception) { }

        
           
        
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

                    if (instance != null)
                    {

                        try
                        {
                            UnregisterReceiver(br);
                        }
                        catch (Exception) { }
                        br = new broadcast_receiver();
                        IntentFilter filtro = new IntentFilter(Intent.ActionMediaButton);
                        filtro.Priority = 1000;
                        if (instance != null)
                        {
                            RegisterReceiver(br, filtro);
                        }

                        PlaybackState pbs = new PlaybackState.Builder()
                                .SetActions(PlaybackState.ActionFastForward | PlaybackState.ActionPause | PlaybackState.ActionPlay | PlaybackState.ActionPlayPause | PlaybackState.ActionSeekTo | PlaybackState.ActionSkipToNext | PlaybackState.ActionSkipToPrevious)
                                .SetState(PlaybackStateCode.Playing, 0, 1f, SystemClock.ElapsedRealtime())
                                .Build();
                        mSession = new MediaSession(this, PackageName);
                        Intent intent = new Intent(this, typeof(broadcast_receiver));
                        PendingIntent pintent = PendingIntent.GetBroadcast(this, 4564, intent, PendingIntentFlags.UpdateCurrent);
                        mSession.SetMediaButtonReceiver(pintent);
                        mSession.Active = (true);
                        mSession.SetPlaybackState(pbs);

                    }
                    else
                    {
                       
                        try
                        {
                            UnregisterReceiver(br);
                           
                        }
                        catch (Exception)
                        {

                         
                        }
                        break;
                    }
                    Thread.Sleep(60000);
                }
            }
            catch (Exception)
            {
            }



        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    sidem.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public void OnAudioFocusChange(AudioFocus focusChange)
        {
            switch (focusChange)
            {
                case AudioFocus.Gain:
                    if (Clouding_service.gettearinstancia().musicaplayer == null)


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
        public void loadsuggestionslistener() {
            var Innerlink = "";
            while (instance!=null) {

           
                    if (linkactual != Innerlink && linkactual.Trim()!="")
                    {
                    Innerlink = linkactual;

                        sugerenciasdeelemento = clasesettings.gettearsugerencias(linkactual,laparalinks);
                        RunOnUiThread(() =>
                        {
                            adaptadorsugerencias adap = new adaptadorsugerencias(this, sugerenciasdeelemento);
                            adap.ItemClick += (aa, aaa) =>
                            {
                                RunOnUiThread(() =>
                                {
                                    Intent intentar = new Intent(this, typeof(customdialogact));
                                    intentar.PutExtra("ipadress", ipadre);
                                    intentar.PutExtra("imagen", sugerenciasdeelemento[aaa.Position].portada);
                                    intentar.PutExtra("url", sugerenciasdeelemento[aaa.Position].link);
                                    intentar.PutExtra("titulo", sugerenciasdeelemento[aaa.Position].nombre);
                                    intentar.PutExtra("color", "DarkGray");
                                    StartActivity(intentar);

                                });
                            };
                            adap.ItemLongClick += (aa, aaa) =>
                            {

                                RunOnUiThread(() =>
                                {
                                    Toast.MakeText(this, sugerenciasdeelemento[aaa.Position].nombre, ToastLength.Long).Show();
                                });
                            };
                            listasugerencias.SetAdapter(adap);
                            if (!headeradded) {
                               
                                headeradded = true;
                            Layoutsugerencias.Visibility = ViewStates.Visible;
                          
                            }
                        });
                    }

              

                Thread.Sleep(2000);
            }


        }
        public void ipchangerlistener() {
      
               while (detenedor) {

                   string currentip = clasesettings.gettearip();
                   if (ipadre != currentip) {
                    
                       ipadre = currentip;

                    detenedor = false;
                    Thread.Sleep(1500);
                    detenedor = true;
                    threadlistas.Abort();
                    threadstream.Abort();
                    clienteses.Clear();
                       oidor.Stop();            
                       oidorlistas.Stop();               
                       oidor = new TcpListener(IPAddress.Parse(ipadre), 1024);
                       oidor.Start();
                       oidorlistas= new TcpListener(IPAddress.Parse(ipadre), 9856);
                       oidorlistas.Start();
                    
                    threadlistas = new Thread(() =>
                    {
                        cojerlistas();
                    });
                    threadstream = new Thread(() => {
                        cojerstream();
                    });
                    threadlistas.Start();
                    threadstream.Start();
                    if (!clasesettings.tieneconexion())
                    {
                        RunOnUiThread(() =>
                        {

                            var alerta = new AlertDialog.Builder(this).SetTitle("Error")
                                .SetMessage("No hay conexion a internet.\n Es posible que no pueda realizar ciertas funciones dentro de la app")
                                .SetPositiveButton("Entendido", (aa, aaa) =>
                                {

                                })

                                .Create();
                            alerta.Window.SetType(WindowManagerTypes.Toast);
                            alerta.SetCancelable(false);
                            alerta.Show();



                        });
                        connected = false;

                    }
                    else {
                     RunOnUiThread(()=>   Toast.MakeText(this, "Conexion reestablecida", ToastLength.Long).Show());
                        connected = true;
                    }

                   }

                   Thread.Sleep(3000);
               }
                   
        }
        public void buscaryabrir(string termino)
        {


            RunOnUiThread(() =>
            {
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Buscando resultados...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
            });
            try
            {

                //  RunOnUiThread(() => progreso.Progress = 50);
                VideoSearch src = new VideoSearch();
               var results= src.SearchQuery(termino, 1);
                if (results.Count>0)
                {
                    var listatitulos = results.Select(ax => WebUtility.HtmlDecode(RemoveIllegalPathCharacters(ax.Title.Replace("&quot;", "").Replace("&amp;", "")))).ToList();
                    var listalinks = results.Select(ax =>ax.Url).ToList();
                     RunOnUiThread(() =>
                    {
                        ListView lista = new ListView(this);
                        lista.ItemClick += (o, e) =>
                        {
                            var posicion = 0;                       
                            posicion = e.Position;
                            Intent intentoo = new Intent(this, typeof(customdialogact));

                            intentoo.PutExtra("index", posicion.ToString());
                            intentoo.PutExtra("color", "DarkGray");
                            intentoo.PutExtra("titulo", listatitulos[posicion]);
                            intentoo.PutExtra("ipadress", ipadre);
                            intentoo.PutExtra("url", listalinks[posicion]);
                            intentoo.PutExtra("imagen", @"https://i.ytimg.com/vi/" + listalinks[posicion].Split('=')[1] + "/mqdefault.jpg");
                            StartActivity(intentoo);

                        };
                        adapterlistaremoto adapt = new adapterlistaremoto(this,listatitulos, listalinks);
                        lista.Adapter = adapt;
                       
                        new AlertDialog.Builder(this)
                        .SetTitle("Resultados de la busqueda")
                        .SetView(lista).SetPositiveButton("Cerrar", (dd, fgf) => { })
                        .Create()
                        .Show();
                    });

                }
                RunOnUiThread(() =>
                {
                    dialogoprogreso.Dismiss();
                });
            }
            catch (Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "No se encotraron resultados", ToastLength.Long).Show());
                dialogoprogreso.Dismiss();
            }
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


        public override void OnConfigurationChanged(Configuration newConfig)
        {



            base.OnConfigurationChanged(newConfig);
            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait && videoon)
            {
              //  video.LayoutParameters =new RelativeLayout.LayoutParams(video.LayoutParameters.Width, sizevideoportrait)  ;
                  setVideoSize();

          
            }
            else
            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape && videoon)
            {
                setVideoSize();
             //   video.LayoutParameters = new RelativeLayout.LayoutParams(video.LayoutParameters.Width,sizevideonormal);
            }

        }


        public void Onresume() {
            if(videoon)
            video.Visibility = ViewStates.Visible;
        
            base.OnResume();
        }
        public void Onpause()
        {



            video.Visibility = ViewStates.Gone;

            base.OnPause();
        }

        public void setVideoSize()
        {

            try
            {
                // // Get the dimensions of the video
                int videoWidth = Clouding_service.gettearinstancia().musicaplayer.VideoWidth;
                int videoHeight = Clouding_service.gettearinstancia().musicaplayer.VideoHeight;
                float videoProportion = videoWidth / (float)videoHeight;

                // Get the width of the screen
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
                int screenWidth = WindowManager.DefaultDisplay.Width;
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
                int screenHeight = WindowManager.DefaultDisplay.Height;
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
                float screenProportion = screenWidth / (float)screenHeight;

                // Get the SurfaceView layout parameters
                Android.Views.ViewGroup.LayoutParams lp = video.LayoutParameters;
                if (videoWidth > videoHeight)
                {
                    lp.Width = screenWidth;
                    lp.Height = screenWidth * videoHeight / videoWidth;
                }
                else
                {
                    lp.Width = screenHeight * videoWidth / videoHeight;
                    lp.Height = screenHeight;
                }
                // Commit the layout parameters
                var relative = new RelativeLayout.LayoutParams(lp.Width, lp.Height);
                relative.AddRule(LayoutRules.CenterInParent);
                video.LayoutParameters = relative;
            }
            catch (Exception) { }
        }



    }

}







