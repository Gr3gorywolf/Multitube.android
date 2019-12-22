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
using App1.Models;
using App1.Utils;
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
    public class YoutubePlayerServerActivity : Android.Support.V7.App.AppCompatActivity, AudioManager.IOnAudioFocusChangeListener, ISurfaceHolderCallback
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
    {

        public bool connected = true;
        public int videoQuality = -1;
        public bool videoon = false;
        public MediaSession mSession;
        public string downloadurrrl = "";
        public bool detenedor = true;
        public ListView lvPlayList;
        adapterlistaremoto adaptadol;
        public ImageView imgPortrait;
        public TextView tvTitle;
        public RelativeLayout lineall;
        public bool envideo = false;
        public bool contienevideo = false;
        public string zelda = "";
        public string colol = "DarkGray";
        public bool agregando = false;
        public int voz = 9;
        public LinearLayout LlBar;
        public int duracion = 0;
        public ImageView imgForeward;
        public ImageView imgBackward;
        public ImageView imgNext;
        public ImageView imgBack;
        public int actual = 0;
        public int counter = 0;
        public long millis = 0;
        public SeekBar SbVideoProgress;
        public bool buscando = false;
        public ImageView imgPlay;
        public TcpListener queryListener;
        public TcpListener playlistListener;
        Thread querysThread;
        public ProgressBar prgBuffering;
        Thread storedPlaylistsThread;
        public int previousprogress = 0;
        public bool qualitychanged;
        public Android.Support.V7.Widget.RecyclerView rvSuggestions;
        public bool fromotherinstance = false;
        //   public IEnumerable<VideoInfo> videoinfoss;
        public Android.Media.MediaPlayer musicaplayer;
        public List<string> lapara = new List<string>();
        public List<string> laparalinks = new List<string>();
        public List<string> listacaratulas = new List<string>();
        public LinearLayout layoutSuggestions;
        public List<TcpClient> clienteses = new List<TcpClient>();
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
        public SeekBar SbVolume;
        public string termino = "";
        bool bloquearporcentaje = false;
        public bool automated = true;
        public string ipAddress = "";
        public int indiceactual = 0;
        public string imgurl = "";
        public int locanterior = 0;
        public int volumenactual = 100;
        public string nombreactual;
        public WebClient clientedelamusica = new WebClient();
        public string linkactual = "";
        public bool backupprompted = false;
        public static YoutubePlayerServerActivity instance;
        public ImageView imgBackground;
        public Bitmap fondoblurreado;
        broadcast_receiver br;
        DrawerLayout DlnavigationDrawer;
        NavigationView nvSideMenuItems;
        public string estadovolumen = "vol100()";
        public ImageView imgSlideDownAction;
        Thread threadmediasession;
        public SurfaceView svVideoLayout;
        public ISurfaceHolder videoSurfaceHolder;
        Android.Support.V7.Widget.CardView cvButtonsLayout;
        public ProgressBar prgLoadingMedia;
        List<PlayList> listalocal = new List<PlayList>();
        public Dictionary<int, int> Qualities;
        string JSONlistalocal = "";
        int noticode = new Random().Next(23, 999999);
        public History objetohistorial;
        public Dictionary<string, PlaylistElement> listafavoritos = new Dictionary<string, PlaylistElement>();
        public Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout spSlidingPanel;
        CardView cvSlideUpDrawer;
        ImageView blacksquare;
        int sizevideoportrait;
        int sizevideonormal;
        bool videoenholder = false;
        ImageView imgVideoLike;
        PowerManager.WakeLock wake;
        public bool headeradded = false;
        public List<Suggestion> sugerenciasdeelemento = new List<Suggestion>();
        public List<YoutubeSearch.VideoInformation> sugerencias = new List<YoutubeSearch.VideoInformation>();
        ImageView imgToggleVideo;
        public static YoutubePlayerServerActivity gettearinstancia()
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

            bool isActivityRestored = false;
            if (savedInstanceState != null)
            {
                isActivityRestored = savedInstanceState.ContainsKey("restaurado");
            }

            SettingsHelper.SaveSetting("onlineactivo", "si");
            /////////////////////////////////declaraciones//////////////////////////
            musicaplayer = new Android.Media.MediaPlayer();
            ipAddress = SocketHelper.GetIp();

            if (!isActivityRestored)
            {
                queryListener = new TcpListener(IPAddress.Parse(ipAddress), 1024);
                queryListener.Start();
                playlistListener = new TcpListener(IPAddress.Parse(ipAddress), 9856);
                playlistListener.Start();

            }








            //  Console.WriteLine( );





            ///////////////////////////////#Botones#////////////////////////////////
            ///
            prgBuffering = FindViewById<ProgressBar>(Resource.Id.progresobuffering);
            layoutSuggestions = FindViewById<LinearLayout>(Resource.Id.layoutsugerencias);
            imgSlideDownAction = FindViewById<ImageView>(Resource.Id.btnaccion);
            prgLoadingMedia = FindViewById<ProgressBar>(Resource.Id.progresoind);
            imgBack = FindViewById<ImageView>(Resource.Id.imageView2);
            imgNext = FindViewById<ImageView>(Resource.Id.imageView4);
            imgPlay = FindViewById<ImageView>(Resource.Id.imageView3);
            imgForeward = FindViewById<ImageView>(Resource.Id.imageView5);
            imgBackward = FindViewById<ImageView>(Resource.Id.imageView7);
            imgToggleVideo = FindViewById<ImageView>(Resource.Id.imageView6);
            blacksquare = FindViewById<ImageView>(Resource.Id.blacksquare);
            LlBar = FindViewById<LinearLayout>(Resource.Id.linearLayout7);
            imgVideoLike = FindViewById<ImageView>(Resource.Id.imglike);
            //   lineall = FindViewById<RelativeLayout>(Resource.Id.linearLayout0);
            tvTitle = FindViewById<TextView>(Resource.Id.textView1);
            imgPortrait = FindViewById<ImageView>(Resource.Id.imageView13);
            lvPlayList = FindViewById<ListView>(Resource.Id.listView1);
            SbVolume = FindViewById<SeekBar>(Resource.Id.seekBar2);
            SbVideoProgress = FindViewById<SeekBar>(Resource.Id.seekBar1);
            cvButtonsLayout = FindViewById<Android.Support.V7.Widget.CardView>(Resource.Id.linearLayout6);
            DlnavigationDrawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
            nvSideMenuItems = FindViewById<NavigationView>(Resource.Id.content_frame);
            imgBackground = FindViewById<ImageView>(Resource.Id.imageView45);
            svVideoLayout = FindViewById<SurfaceView>(Resource.Id.videoView1);
            var svSearchBar = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);
            var imgQualitySelect = FindViewById<ImageView>(Resource.Id.imgcalidad);
            imgQualitySelect.SetBackgroundResource(Resource.Drawable.mp3);
            cvSlideUpDrawer = FindViewById<Android.Support.V7.Widget.CardView>(Resource.Id.solapita);
            rvSuggestions = FindViewById<Android.Support.V7.Widget.RecyclerView>(Resource.Id.listasugerencias);
            svVideoLayout.SetBackgroundColor(Color.Transparent);
            Qualities = new Dictionary<int, int>() {
            { -1,Resource.Drawable.mp3},
            { 360,Resource.Drawable.mq},
            { 720,Resource.Drawable.hq}
        };
            ////////////////////////////////////////////////////////////////////////
            ///

            layoutSuggestions.Visibility = ViewStates.Gone;
            LinearLayoutManager enlinea = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            rvSuggestions.SetLayoutManager(enlinea);
            spSlidingPanel = FindViewById<Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout>(Resource.Id.sliding_layout);
            spSlidingPanel.IsUsingDragViewTouchEvents = true;
            spSlidingPanel.DragView = FindViewById<RelativeLayout>(Resource.Id.scrollable);

            cvSlideUpDrawer.Click += delegate
            {

                if (spSlidingPanel.IsExpanded)
                    spSlidingPanel.CollapsePane();
                else
                    spSlidingPanel.ExpandPane();
            };
            LlBar.SetBackgroundColor(Android.Graphics.Color.Black);
            //  lineall.SetBackgroundColor(Color.DarkGray);

            videoSurfaceHolder = svVideoLayout.Holder;

            videoQuality = int.Parse(SettingsHelper.GetSetting("video"));

            imgQualitySelect.SetBackgroundResource(Qualities[videoQuality]);
            LlBar.SetBackgroundColor(Android.Graphics.Color.ParseColor(SettingsHelper.GetSetting("color")));
            imgPlay.SetBackgroundResource(Resource.Drawable.pausebutton2);
            imgSlideDownAction.SetBackgroundResource(Resource.Drawable.pausebutton2);
            if (videoQuality > 0)
                imgToggleVideo.Visibility = ViewStates.Visible;
            else
            {

                imgToggleVideo.Visibility = ViewStates.Gone;
            }
            svVideoLayout.Visibility = ViewStates.Invisible;
            imgToggleVideo.SetBackgroundResource(Resource.Drawable.video);
            AnimationHelper.ScaleXAnimation(LlBar);
            SettingsHelper.SaveSetting("elementosactuales", "");
            SbVolume.Max = 100;
            SbVolume.Progress = volumenactual;
            SetSupportActionBar(toolbar);



            //Enable support action bar to display hamburger

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Reproductor";




            /* Drawable progressDrawable = porcientoreproduccion.ProgressDrawable.Mutate();
             progressDrawable.SetColorFilter( Color.DarkGray, Android.Graphics.PorterDuff.Mode.SrcIn);*/
            SbVideoProgress.SecondaryProgressTintList = ColorStateList.ValueOf(Android.Graphics.Color.Gray);


            // SupportActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#2b2e30")));
            /////////////////////////////////////////////////
            ///
            var noFoundAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });

            RunOnUiThread(() =>
            {
                var parcelable = lvPlayList.OnSaveInstanceState();
                lvPlayList.Adapter = noFoundAdapter;

                lvPlayList.OnRestoreInstanceState(parcelable);
            });
            ((ViewGroup)layoutSuggestions.Parent).RemoveView(layoutSuggestions);
            lvPlayList.AddHeaderView(layoutSuggestions, null, false);
            lvPlayList.SetHeaderDividersEnabled(true);
            if (!isActivityRestored)
            {
                new Thread(() =>
                {
                    llenarplaylist();
                }).Start();
                storedPlaylistsThread = new Thread(() =>
                  {
                      ListenStoredPlaylists();
                  });
                storedPlaylistsThread.Start();
                querysThread = new Thread(new ThreadStart(ListenQuerys));
                querysThread.Start();

                new Thread(() =>
                {
                    ipchangerlistener();
                }).Start();
                new Thread(() =>
                {
                    StartServices();
                    UpdateVideoProgress();
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


            tvTitle.Selected = true;
            WallpaperManager wm = WallpaperManager.GetInstance(this);
            Drawable d = wm.Drawable;
            fondoblurreado = ImageHelper.CreateBlurredImageFromBitmap(this, 20, ((BitmapDrawable)d).Bitmap);
            sizevideoportrait = svVideoLayout.LayoutParameters.Height / 2;
            sizevideonormal = svVideoLayout.LayoutParameters.Height;






            if (File.Exists(Constants.CachePath + "/history.json"))
            {
                try
                {
                    objetohistorial = JsonConvert.DeserializeObject<History>(File.ReadAllText(Constants.CachePath + "/history.json"));
                }
                catch (Exception)
                {
                    objetohistorial = null;
                    File.Delete(Constants.CachePath + "/history.json");

                }
                if (objetohistorial == null)
                {
                    objetohistorial = new History();
                    objetohistorial.Videos = new List<PlaylistElement>();
                    objetohistorial.Links = new Dictionary<string, int>();
                }
                else
                {
                    if (objetohistorial.Videos == null)
                    {
                        objetohistorial.Videos = new List<PlaylistElement>();

                    }
                    else
                    if (objetohistorial.Links == null)
                    {
                        objetohistorial.Links = new Dictionary<string, int>();
                    }
                }

            }
            else
            {

                objetohistorial = new History();
                objetohistorial.Videos = new List<PlaylistElement>();
                objetohistorial.Links = new Dictionary<string, int>();
            }

            if (File.Exists(Constants.CachePath + "/favourites.json"))
            {
                try
                {

                    listafavoritos = JsonConvert.DeserializeObject<Dictionary<string, PlaylistElement>>(File.ReadAllText(Constants.CachePath + "/favourites.json"));
                }
                catch (Exception)
                {
                    listafavoritos = new Dictionary<string, PlaylistElement>();
                    File.Delete(Constants.CachePath + "/favourites.json");
                }
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            imgBackground.SetBackgroundColor(Color.ParseColor("#2d3033"));

            MultiHelper.ExecuteGarbageCollection();
            StartActivity(new Intent(this, typeof(actividadinicio)));
            OverridePendingTransition(0, 0);

            ///////////////////////////////#clicks#/////////////////////////////////


            imgPortrait.Click += delegate
            {

                if (spSlidingPanel.IsExpanded)
                    spSlidingPanel.CollapsePane();
                else
                    spSlidingPanel.ExpandPane();
            };
            imgVideoLike.Click += delegate
            {
                AnimationHelper.ZoomInAnimation(imgVideoLike);
                if (linkactual.Trim() != "")
                {
                    var link = linkactual.Replace("https", "http");
                    var elemento = new PlaylistElement()
                    {
                        Link = link,
                        Name = nombreactual
                    };
                    listafavoritos = PlaylistsHelper.AddToFavouriteList(this, listafavoritos, elemento);

                    if (!listafavoritos.ContainsKey(link))
                        imgVideoLike.SetBackgroundResource(Resource.Drawable.heartoutline);
                    else
                        imgVideoLike.SetBackgroundResource(Resource.Drawable.heartcomplete);

                }

            };


            imgQualitySelect.Click += delegate
            {
                AnimationHelper.ZoomInAnimation(imgQualitySelect);
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
                spinner.SetSelection(Qualities.Keys.ToList().IndexOf(videoQuality));
                new Android.Support.V7
                .App
                .AlertDialog
                .Builder(this)
                .SetTitle("Seleccione la calidad de reproduccion")
                .SetView(spinner)
                .SetPositiveButton("Listo", (aas, dfe) =>
                {
                    int quality = Qualities.Keys.ToList()[spinner.SelectedItemPosition];
                    if (videoQuality != quality)
                    {
                        videoQuality = quality;
                        imgQualitySelect.SetBackgroundResource(Qualities[videoQuality]);
                        if (linkactual.Trim() != "")
                        {
                            new Thread(() =>
                            {

                                qualitychanged = true;

                                buscarviddireckt(linkactual, true);
                            }).Start();
                        }
                        if (videoQuality == -1)
                        {
                            imgToggleVideo.Visibility = ViewStates.Gone;

                            videoon = false;
                            AnimationHelper.ZoomInAnimation(imgToggleVideo);
                            cvButtonsLayout.Alpha = 1f;
                            cvSlideUpDrawer.Alpha = 1f;
                            imgToggleVideo.SetBackgroundResource(Resource.Drawable.video);
                            svVideoLayout.Visibility = ViewStates.Invisible;
                            FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Visible;
                            blacksquare.Visibility = ViewStates.Visible;
                            // Clouding_service.gettearinstancia().musicaplayer.SetDisplay(null);
                            svVideoLayout.KeepScreenOn = false;
                            Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
                        }
                        else
                        {
                            imgToggleVideo.Visibility = ViewStates.Visible;
                        }

                    }
                })

                .Create()
                .Show();



            };
            spSlidingPanel.PanelExpanded += delegate
            {
                if (videoon)
                {
                    cvButtonsLayout.Alpha = 0.7f;
                    cvSlideUpDrawer.Alpha = 0.7f;
                    imgQualitySelect.Alpha = 1f;
                    imgVideoLike.Alpha = 1f;
                    UiHelper.SetInmersiveMode(this.Window, false);
                }
                if (volumenactual == 0)
                    imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumelow);
                else
                if (volumenactual == 50)
                    imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumemedium);
                else
                if (volumenactual == 100)
                    imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumehigh);


            };
            spSlidingPanel.PanelCollapsed += delegate
            {

                if (videoon)
                    UiHelper.SetInmersiveMode(this.Window, true);
                cvButtonsLayout.Alpha = 1f;
                cvSlideUpDrawer.Alpha = 1f;
                if (Clouding_service.gettearinstancia() != null)
                {
                    if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                        imgSlideDownAction.SetBackgroundResource(Resource.Drawable.pausebutton2);
                    else
                        imgSlideDownAction.SetBackgroundResource(Resource.Drawable.playbutton2);
                }



            };
            svSearchBar.QueryTextSubmit += delegate
            {

                if (svSearchBar.Query.Trim().Length >= 3)
                {
                    new Thread(() =>
                    {

                        buscaryabrir(svSearchBar.Query.Trim());

                    }).Start();
                }
                else
                {

                    Toast.MakeText(this, "La busqueda debe contener almenos 3 caracteres", ToastLength.Long).Show();
                }

            };

            svVideoLayout.Click += delegate
            {
                /* if (FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility == ViewStates.Visible)
                     FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility = ViewStates.Gone;
                 else
                     FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility = ViewStates.Visible;*/

                if (cvButtonsLayout.Alpha > 0f)
                {
                    imgVideoLike.Alpha = 0f;
                    cvSlideUpDrawer.Alpha = 0f;
                    imgQualitySelect.Alpha = 0f;
                    cvButtonsLayout.Alpha = 0f;
                }
                else
                {
                    imgVideoLike.Alpha = 1f;
                    cvSlideUpDrawer.Alpha = 0.7f;
                    imgQualitySelect.Alpha = 1f;
                    cvButtonsLayout.Alpha = 0.7f;
                }

            };
            imgToggleVideo.Click += delegate
            {


                AnimationHelper.ZoomInAnimation(imgToggleVideo);
                if (!videoon)
                {
                    videoon = true;

                    cvSlideUpDrawer.Alpha = 0.7f;
                    cvButtonsLayout.Alpha = 0.7f;
                    svVideoLayout.Visibility = ViewStates.Visible;
                    FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Gone;
                    blacksquare.Visibility = ViewStates.Gone;
                    imgToggleVideo.SetBackgroundResource(Resource.Drawable.videooff);
                    svVideoLayout.KeepScreenOn = true;
                    if (!videoenholder)
                    {
                        // Clouding_service.gettearinstancia().musicaplayer.SetDisplay(holder);

                        videoenholder = true;
                    }


                    Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                    SetVideoSize();
                    UiHelper.SetInmersiveMode(this.Window, false);
                }
                else
                {


                    videoon = false;
                    AnimationHelper.ZoomInAnimation(imgToggleVideo);
                    cvButtonsLayout.Alpha = 1f;
                    cvSlideUpDrawer.Alpha = 1f;
                    imgToggleVideo.SetBackgroundResource(Resource.Drawable.video);
                    svVideoLayout.Visibility = ViewStates.Invisible;
                    FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Visible;
                    blacksquare.Visibility = ViewStates.Visible;
                    //  Clouding_service.gettearinstancia().musicaplayer.SetDisplay(null);
                    svVideoLayout.KeepScreenOn = false;
                    Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
                    UiHelper.SetInmersiveMode(this.Window, true);
                }


            };
            FindViewById<RelativeLayout>(Resource.Id.scrollable).Click += delegate
            {
                if (videoon)
                    svVideoLayout.PerformClick();

            };
            nvSideMenuItems.NavigationItemSelected += (sender, e) =>
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


                        internado.PutExtra("ip", ipAddress);
                        internado.PutExtra("zelda", zelda);

                        internado.PutExtra("color", colol);
                        StartActivity(internado);





                    }
                    else
                    {
                        Toast.MakeText(this, "Reproduzca un video para descargar", ToastLength.Long).Show();
                    }
                }
                else
                  if (e.MenuItem.TitleFormatted.ToString().Trim() == "Navegador personalizado")
                {
                    Intent intento = new Intent(this, typeof(customsearcheract));
                    intento.PutExtra("ipadre", ipAddress);
                    intento.PutExtra("color", colol);
                    StartActivity(intento);

                }
                else
                    if (e.MenuItem.TitleFormatted.ToString().Trim() == "Listas de reproduccion")
                {

                    Intent intentoo = new Intent(this, typeof(menulistaoffline));
                    intentoo.PutExtra("ipadre", ipAddress);
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

                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Conectar clientes")
                {
                    Intent intetno = new Intent(this, typeof(qrcodigoact));

                    StartActivity(intetno);
                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Sincronizar listas")
                {
                    Intent intento = new Intent(this, typeof(actividadsincronizacion));
                    intento.PutExtra("ipadre", ipAddress);

                    StartActivity(intento);

                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Busqueda rapida")
                {
                    Intent intento = new Intent(this, typeof(actfastsearcher));
                    intento.PutExtra("ipadres", "localhost");
                    StartActivity(intento);


                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Inicio")
                {
                    if (actividadinicio.gettearinstancia() == null)
                    {
                        Intent intento = new Intent(this, typeof(actividadinicio));

                        StartActivity(intento);
                    }
                    else
                    {

                        actividadinicio.gettearinstancia().Finish();
                        Intent intento = new Intent(this, typeof(actividadinicio));

                        StartActivity(intento);
                    }


                }
                e.MenuItem.SetChecked(false);
                e.MenuItem.SetChecked(false);
                DlnavigationDrawer.CloseDrawers();

            };




            SbVideoProgress.ProgressChanged += (obj, evt) =>
            {
                if (evt.FromUser)
                {
                    Clouding_service.gettearinstancia().musicaplayer.SeekTo(evt.Progress);
                }
            };
            imgSlideDownAction.Click += delegate
           {
               AnimationHelper.ZoomInAnimation(imgToggleVideo);
               if (spSlidingPanel.IsExpanded)
               {
                   Intent intentoo = new Intent(this, typeof(actmenuvolumen));
                   intentoo.PutExtra("ipadre", ipAddress);
                   StartActivity(intentoo);
               }
               else
               {
                   imgPlay.PerformClick();
               }
           };






            SbVolume.ProgressChanged += delegate
            {
                volumenactual = SbVolume.Progress;
                Clouding_service.gettearinstancia().musicaplayer.SetVolume(volumenactual * 0.01f, volumenactual * 0.01f);
                //  clasesettings.guardarsetting("cquerry","volact()"+  ">"+ ().ToString() + ">"+ ().ToString());


            };

            lvPlayList.ItemClick += (sender, easter) =>
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
                    intentoo.PutExtra("ipadress", ipAddress);
                    intentoo.PutExtra("url", laparalinks[posicion]);
                    intentoo.PutExtra("imagen", @"https://i.ytimg.com/vi/" + laparalinks[posicion].Split('=')[1] + "/mqdefault.jpg");
                    StartActivity(intentoo);
                }
            };


            imgBack.Click += (sender, easter) =>
           {
               if (buscando == false)
               {
                   AnimationHelper.ZoomInAnimation(imgBack);
                   Thread proc = new Thread(new ThreadStart(PreviousVideo));
                   proc.Start();

               }


           };

            imgNext.Click += (sender, easter) =>
             {
                 if (buscando == false)
                 {
                     AnimationHelper.ZoomInAnimation(imgNext);
                     Thread proc = new Thread(new ThreadStart(NextVideo));
                     proc.Start();

                 }

             };
            imgPlay.Click += (sender, easter) =>
              {
                  AnimationHelper.ZoomInAnimation(imgPlay);
                  if (!Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                  {
                      imgPlay.SetBackgroundResource(Resource.Drawable.pausebutton2);

                      Clouding_service.gettearinstancia().musicaplayer.Start();
                      //  clasesettings.guardarsetting("cquerry", "play()");
                  }
                  else

                  {
                      imgPlay.SetBackgroundResource(Resource.Drawable.playbutton2);
                      Clouding_service.gettearinstancia().musicaplayer.Pause();
                      // clasesettings.guardarsetting("cquerry", "pause()");

                  }
              };
            imgForeward.Click += (sender, easter) =>
              {
                  AnimationHelper.ZoomInAnimation(imgForeward);
                  Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition + Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
                  //   clasesettings.guardarsetting("cquerry", "adelantar()");
              };
            imgBackward.Click += (sender, easter) =>
            {
                AnimationHelper.ZoomInAnimation(imgBackward);

                Clouding_service.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition - Clouding_service.gettearinstancia().musicaplayer.Duration * 0.10));
            };






        }
        public override void OnBackPressed()
        {

            if (DlnavigationDrawer.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                RunOnUiThread(() => { DlnavigationDrawer.CloseDrawers(); });
            else
            {
                if (!spSlidingPanel.IsExpanded)
                    DialogsHelper.ShowAskIfMenuOrExit(this);
                else
                    RunOnUiThread(() => spSlidingPanel.CollapsePane());
            }
            // base.OnBackPressed();
        }

        public void StartServices()
        {
            StopService(new Intent(this, typeof(cloudingserviceonline)));
            StopService(new Intent(this, typeof(Clouding_serviceoffline)));
            StopService(new Intent(this, typeof(Clouding_service)));
            StopService(new Intent(this, typeof(serviciodownload)));
            StartService(new Intent(this, typeof(Clouding_service)));
            if (videoQuality > 0)
            {

                RunOnUiThread(() =>
                {
                    videoon = true;

                    cvSlideUpDrawer.Alpha = 1f;
                    cvButtonsLayout.Alpha = 1f;
                    svVideoLayout.Visibility = ViewStates.Visible;
                    FindViewById<ImageView>(Resource.Id.bgimg).Visibility = ViewStates.Gone;
                    blacksquare.Visibility = ViewStates.Gone;
                    imgToggleVideo.SetBackgroundResource(Resource.Drawable.videooff);
                    svVideoLayout.KeepScreenOn = true;
                    if (!videoenholder)
                    {
                        // Clouding_service.gettearinstancia().musicaplayer.SetDisplay(holder);

                        videoenholder = true;
                    }


                    Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                    SetVideoSize();
                    UiHelper.SetInmersiveMode(this.Window, false);
                });

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
                        SendPortraitToClients();
                        zelda = url;
                        linkactual = url;
                        var Titulo = VideosHelper.GetVideoTitle(url);

                        if (automatedi == false)
                        {
                            if (!IsVideoOnList(url, laparalinks))
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
                                NotificationsHelper.ShowNotification(this, Titulo, " Agregado a la cola!", url, noticode);
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
                            var parcelable = lvPlayList.OnSaveInstanceState();
                            lvPlayList.Adapter = adaptadol;

                            lvPlayList.OnRestoreInstanceState(parcelable);

                        });
                        RunOnUiThread(() => tvTitle.Text = Titulo);


                        string imagensilla = "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg";
                        imgurl = imagensilla;
                        Bitmap blur = null;
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                        {
                            blur = ImageHelper.CreateBlurredImageFromUrl(this, 20, imagensilla);
                        }

                        Thread.Sleep(20);
                        var sinblur = GetImageBitmapFromUrl(imagensilla);
                        RunOnUiThread(() => imgPortrait.SetImageBitmap(sinblur));

                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                        {
                            //  RunOnUiThread(() => fondo.SetImageBitmap(blur));
                            RunOnUiThread(() => imgBackground.SetBackgroundColor(Color.ParseColor("#2d3033")));
                            fondoblurreado = blur;
                        }

                        SendAllToClients();
                        var downloadurl = VideosHelper.GetShortVideoInfo(url, envideo, videoQuality);
                        buscando = false;
                        if (downloadurl != null)
                            PlayVideo(downloadurl.DownloadUrl);
                        else
                            PlayVideo(null);
                        /// actualizartodo();

                    }
                    else
                    {

                        buscando = false;
                    }
                }
            }
            else
            {
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
                    var parcelable = lvPlayList.OnSaveInstanceState();
                    lvPlayList.Adapter = adaptadol;

                    lvPlayList.OnRestoreInstanceState(parcelable);
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
                        SendPortraitToClients();
                        zelda = url;
                        linkactual = url;
                        var asdd = VideosHelper.GetVideoTitle(url);




                        if (automatedi == false)
                        {
                            if (!IsVideoOnList(url, laparalinks))
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
                                var parcelable = lvPlayList.OnSaveInstanceState();
                                lvPlayList.Adapter = adaptadol;
                                lvPlayList.OnRestoreInstanceState(parcelable);
                            }
                            catch (Exception)
                            {
                            }

                        });
                        RunOnUiThread(() => tvTitle.Text = asdd);


                        string imagensilla = "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg";
                        imgurl = imagensilla;
                        Bitmap blur = null;
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                        {
                            blur = ImageHelper.CreateBlurredImageFromUrl(this, 20, imagensilla);
                        }

                        Thread.Sleep(20);
                        var sinblur = GetImageBitmapFromUrl(imagensilla);
                        RunOnUiThread(() => imgPortrait.SetImageBitmap(sinblur));

                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                        {
                            //RunOnUiThread(() => fondo.SetImageBitmap(blur));

                            RunOnUiThread(() => imgBackground.SetBackgroundColor(Color.ParseColor("#2d3033")));
                            fondoblurreado = blur;
                        }



                        SendAllToClients();
                        var video = VideosHelper.GetShortVideoInfo(url, envideo, videoQuality);
                        buscando = false;
                        if (video != null)
                            PlayVideo(video.DownloadUrl);
                        else
                            PlayVideo(null);
                        //actualizartodo();



                    }
                    else
                    {

                        buscando = false;
                    }
                }
            }
            else
            {
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
                    if (!IsVideoOnList(urll, laparalinks))
                    {


                        if (lapara.Count == 0)
                        {

                            buscarvidth(urll, false);

                        }
                        else
                        {
                            laparalinks.Add(urll);
                            lapara.Add(VideosHelper.GetVideoTitle(urll));
                            listacaratulas.Add(lapara[lapara.Count - 1]);

                            adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                            RunOnUiThread(() =>
                            {
                                var parcelable = lvPlayList.OnSaveInstanceState();
                                lvPlayList.Adapter = adaptadol;

                                lvPlayList.OnRestoreInstanceState(parcelable);
                            });
                            RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());
                            NotificationsHelper.ShowNotification(this, VideosHelper.GetVideoTitle(urll), " Agregado a la cola!", urll, noticode);
                            SendPlayListToClients();

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
                    if (!IsVideoOnList(urll, laparalinks))
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
                                var parcelable = lvPlayList.OnSaveInstanceState();
                                lvPlayList.Adapter = adaptadol;


                                lvPlayList.OnRestoreInstanceState(parcelable);
                            });

                            RunOnUiThread(() => Toast.MakeText(this, "Elemento agregado", ToastLength.Long).Show());
                            /* clasesettings.mostrarnotificacion(this, titulo, " Agregado a la cola!", urll, noticode);*/
                            SendPlayListToClients();

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

        public void NextVideo()
        {
            try
            {


                if (laparalinks.ToArray().Length - 1 >= indiceactual + 1 && !buscando)
                {
                    if (lapara.Count > 0 && lapara[indiceactual + 1].Trim().Length > 0)
                    {
                        termino = laparalinks[indiceactual + 1];


                        listacaratulas[locanterior] = lapara[locanterior];
                        locanterior = indiceactual + 1;
                        listacaratulas[locanterior] = ">" + lapara[locanterior] + "<";

                        adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                        RunOnUiThread(() =>
                        {
                            var parcelable = lvPlayList.OnSaveInstanceState();
                            lvPlayList.Adapter = adaptadol;

                            lvPlayList.OnRestoreInstanceState(parcelable);
                        });
                        automated = true;
                        indiceactual++;
                        new Thread(() =>
                        {
                            buscarvidth(laparalinks[indiceactual], true);
                        }).Start();
                    }
                }
                else
                {
                    if (sugerenciasdeelemento.Count > 0 && !buscando)
                    {

                        if (SettingsHelper.GetSetting("automatica") == "si")
                            buscarviddireckt(sugerenciasdeelemento[0].Link, false);
                    }

                }

            }

            catch (Exception)
            {

            }

        }
        public void PreviousVideo()
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
                    RunOnUiThread(() => lvPlayList.Adapter = adaptadol);
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




        public void PlayVideo(string downloadurl)
        {
            if (downloadurl != null)
            {

                try
                {
                    contienevideo = false;
                    nombreactual = tvTitle.Text;

                    Clouding_service.gettearinstancia().tituloactual = tvTitle.Text;
                    Clouding_service.gettearinstancia().linkactual = linkactual;
                    previousprogress = Clouding_service.gettearinstancia().musicaplayer.CurrentPosition;
                    Clouding_service.gettearinstancia().reproducir(downloadurl);
                    if (videoQuality > 0 && actividadinicio.gettearinstancia() != null)
                    {

                        var instinicio = actividadinicio.gettearinstancia();
                        instinicio.RunOnUiThread(() =>
                        {
                            if (instinicio.alertareproducirvideo != null)
                            {
                                if (instinicio.alertareproducirvideo.IsShown)
                                    instinicio.alertareproducirvideo.Dismiss();
                            }



                            instinicio.alertareproducirvideo = Snackbar.Make(instinicio.FindViewById<View>(Android.Resource.Id.Content), "Se esta reproduciendo un video", Snackbar.LengthIndefinite);
                            instinicio.alertareproducirvideo.SetAction("Ver", (o) =>
                           {
                               instinicio.Finish();
                               YoutubePlayerServerActivity.gettearinstancia().RunOnUiThread(() =>
                               {
                                   YoutubePlayerServerActivity.gettearinstancia().cvSlideUpDrawer.PerformClick();
                               });
                           });
                            instinicio.alertareproducirvideo.SetDuration(6000);
                            instinicio.alertareproducirvideo.Show();


                        });
                    }

                    RunOnUiThread(() => { imgPlay.SetBackgroundResource(Resource.Drawable.pausebutton2); });
                    new Thread(() =>
                    {
                        SendAllToClients();
                    }).Start();

                    MultiHelper.ExecuteGarbageCollection();
                }
                catch (Exception)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "Error al reproducir el elemento", ToastLength.Long).Show());
                }

                try
                {
                    ReloadHomeMenu();
                }
                catch (Exception e)
                {
                    var ex = e;
                    RunOnUiThread(() => Toast.MakeText(this, "Ha ocurrido un error al registrar el elemento en el historial.", ToastLength.Long).Show());
                }
            }
            else
            {
                RunOnUiThread(() => Toast.MakeText(this, "Error al conectar al obtener el elemento. Posiblemente haya problemas de conexion", ToastLength.Long).Show());
                new Thread(() =>
                {
                    SendAllToClients();
                }).Start();

            }
        }



        public void ReloadHomeMenu()
        {

            string normalizedlink = linkactual.Replace("https", "http");


            objetohistorial.Videos.RemoveAll(ax => ax.Link == normalizedlink);


            objetohistorial.Videos.Add(new PlaylistElement
            {
                Name = tvTitle.Text,
                Link = normalizedlink

            });

            RunOnUiThread(() =>
            {
                if (!listafavoritos.ContainsKey(normalizedlink))
                    imgVideoLike.SetBackgroundResource(Resource.Drawable.heartoutline);
                else
                    imgVideoLike.SetBackgroundResource(Resource.Drawable.heartcomplete);
            });

            if (objetohistorial.Links.ContainsKey(normalizedlink))
                objetohistorial.Links[normalizedlink]++;
            else
            {

                objetohistorial.Links.Add(normalizedlink, 1);

            }
            new Thread(() =>
            {
                BackupVideosHistoy();
            }).Start();
            if (actividadinicio.gettearinstancia() != null)
            {
                actividadinicio.gettearinstancia().recargarhistorial();

            }



        }
        public void BackupVideosHistoy()
        {

            try
            {
                var archivo = File.CreateText(Constants.CachePath + "/history.json");
                archivo.Write(JsonConvert.SerializeObject(objetohistorial));
                archivo.Close();
            }
            catch (Exception)
            {
                Thread.Sleep(5000);
                BackupVideosHistoy();
            }
        }

        public void UpdateVideoProgress()
        {
            var activo = false;

            while (true)
            {


                if (instance == null)
                    break;

                try
                {
                    if (Clouding_service.gettearinstancia().musicaplayer.IsPlaying == true && !bloquearporcentaje)
                    {

                        if (!spSlidingPanel.IsExpanded)
                        {

                            RunOnUiThread(() => imgSlideDownAction.SetBackgroundResource(Resource.Drawable.pausebutton2));
                        }
                        else
                        {
                            RunOnUiThread(() => SbVideoProgress.Max = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.Duration));
                            RunOnUiThread(() => SbVideoProgress.Progress = Convert.ToInt32(Clouding_service.gettearinstancia().musicaplayer.CurrentPosition));
                        }
                    }
                    else
                    {
                        if (!spSlidingPanel.IsExpanded)
                        {

                            RunOnUiThread(() => imgSlideDownAction.SetBackgroundResource(Resource.Drawable.playbutton2));
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
                        if (prgLoadingMedia.Visibility == ViewStates.Gone)
                        {

                            prgLoadingMedia.Visibility = ViewStates.Visible;
                            if (videoon)
                                prgBuffering.Visibility = ViewStates.Visible;

                        }
                        else
                        {
                            prgLoadingMedia.Visibility = ViewStates.Gone;
                            prgBuffering.Visibility = ViewStates.Gone;
                        }


                    });


                }


                Thread.Sleep(1000);
            }





        }
        public void SendPlayListToClients()
        {
            List<TcpClient> clientesact = new List<TcpClient>();
            var JSONlista = JsonConvert.SerializeObject(lapara);
            var JSONlinks = JsonConvert.SerializeObject(laparalinks);
            var headers = $"[%%CARATULA%%][%%LINKS%%]{JSONlinks}[%%TITLES%%]{JSONlista}";
            List<string> listaips = new List<string>();
            foreach (TcpClient c in clienteses)
            {

                var ipactual = ((IPEndPoint)c.Client.RemoteEndPoint).Address.ToString();
                if (SocketHelper.IsConnected(c) == true && listaips.IndexOf(ipactual) == -1)
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
            listaips = new List<string>();
            clienteses = clientesact;
        }






        public void SendAllToClients()
        {
            List<TcpClient> clientesact = new List<TcpClient>();
            var JSONlista = JsonConvert.SerializeObject(lapara.ToArray());
            var JSONlinks = JsonConvert.SerializeObject(laparalinks.ToArray());
            var JSONcaratula = JsonConvert.SerializeObject(new string[] { imgurl, tvTitle.Text, zelda, estadovolumen, buscando.ToString(), Android.OS.Build.Model });
            var headers = $"[%%CARATULA%%]{JSONcaratula}[%%LINKS%%]{JSONlinks}[%%TITLES%%]{JSONlista}";
            List<string> ipenviadas = new List<string>();
            foreach (TcpClient c in clienteses)
            {
                var ipactual = ((IPEndPoint)c.Client.RemoteEndPoint).Address.ToString();

                if (SocketHelper.IsConnected(c) == true && ipenviadas.IndexOf(ipactual) == -1)
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
            clienteses = clientesact;
        }

        public void SendPortraitToClients()
        {

            List<TcpClient> clientesact = new List<TcpClient>();

            var JSONcaratula = JsonConvert.SerializeObject(new string[] { imgurl, tvTitle.Text, zelda, estadovolumen, buscando.ToString(), Android.OS.Build.Model });
            var headers = $"[%%CARATULA%%]{ JSONcaratula }[%%LINKS%%][%%TITLES%%]";
            List<string> listaips = new List<string>();
            foreach (TcpClient c in clienteses)
            {
                var ipactual = ((IPEndPoint)c.Client.RemoteEndPoint).Address.ToString();
                if (SocketHelper.IsConnected(c) == true && listaips.IndexOf(ipactual) == -1)
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









        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {

            base.OnActivityResult(requestCode, resultCode, data);
        }

        public bool IsVideoOnList(string link, List<string> listalinks)
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
                    var playlist = new PlayList();
                    playlist.Name = System.IO.Path.GetFileNameWithoutExtension(elementos);
                    var text = File.ReadAllText(elementos).Trim();
                    try
                    {
                        playlist.Portrait = text.Split('$')[1].Split(';')[0];
                    }
                    catch (Exception)
                    {
                        playlist.Portrait = "";
                    }
                    playlist.MediaElements = new List<PlaylistElement>();
                    var nombreses = text.Split('$')[0].Split(';').ToList();
                    var linkeses = text.Split('$')[1].Split(';').ToList();

                    for (int e = 0; e < nombreses.Count; e++)
                    {
                        if (nombreses[e].Trim() != "" && linkeses[e].Trim() != "")
                        {
                            playlist.MediaElements.Add(new PlaylistElement()
                            {
                                Name = nombreses[e].Trim(),
                                Link = linkeses[e].Trim()

                            });
                        }

                    }
                    listalocal.Add(playlist);
                }

            }
            JSONlistalocal = JsonConvert.SerializeObject(listalocal);
            Console.WriteLine(JSONlistalocal);
        }

        public void ListenStoredPlaylists()
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
                clieente = playlistListener.AcceptTcpClient();
                if (clieente.Connected == true)
                {



                    Thread procc = new Thread(new ThreadStart(ListenStoredPlaylists));
                    procc.Start();

                }
            }
            catch (Exception)
            {

            }
            while (detenedor == true && clieente.Connected)
            {


                if (JSONlistalocal.Trim() != "" && (!enviolistas || lenviejo != JSONlistalocal.Length))
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


                var listareproduccion = new PlayList();
                if (datacompleta.Trim().Length > 0 && datacompleta.Trim() != "")
                {
                    var querry = datacompleta.Split(new[] { "__==__==__" }, StringSplitOptions.None)[0];
                    jsoncompleto = datacompleta.Split(new[] { "__==__==__" }, StringSplitOptions.None)[1];

                    if (jsoncompleto.Trim() != "none")
                        listareproduccion = JsonConvert.DeserializeObject<PlayList>(jsoncompleto);


                    if (querry == "Fromremote")
                    {


                        RunOnUiThread(() =>
                    {

                        var alerta = new Android.Support.V7.App.AlertDialog.Builder(this)
                                 .SetTitle("Advertencia")
                                 .SetMessage("Desea reproducir la lista de reproduccion remota: " + listareproduccion.Name + "??")
                                 .SetPositiveButton("Si", (aa, aaa) =>
                                 {
                                     new Thread(() =>
                                     {
                                         reproducirlistaremota(listareproduccion);
                                     }).Start();
                                 })
                                 .SetNegativeButton("No", (aa, aaa) => { })

                                 .Create();
                        alerta.Window.SetType(WindowManagerTypes.Toast);
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
                                   .SetMessage("Desea reproducir la lista de reproduccion local: " + listareproduccion.Name + "??")
                                   .SetPositiveButton("Si", (aa, aaa) =>
                                   {
                                       new Thread(() =>
                                       {
                                           reproducirlalistalocal(listareproduccion.Name);
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
                                   .SetMessage("Desea guardar la lista de reproduccion remota: " + listareproduccion.Name + "??\n NOTA:\nsi hay una lista con este mismo nombre sera sustituida por esta")
                                   .SetPositiveButton("Si", (aa, aaa) =>
                                   {
                                       new Thread(() =>
                                       {
                                           var nombreses = string.Join(";", listareproduccion.MediaElements.Select(axx => axx.Name).ToArray()) + ";";
                                           var linkeses = string.Join(";", listareproduccion.MediaElements.Select(axx => axx.Link).ToArray()) + ";";
                                           var archi = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listareproduccion.Name);
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

                        var listilla = new List<PlaylistElement>();
                        var texto = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listareproduccion.Name);
                        var nombreses = texto.Split('$')[0].Split(';').ToList();
                        var links = texto.Split('$')[1].Split(';').ToList();

                        var listaelementos = new List<PlaylistElement>();
                        for (int i = 0; i < nombreses.Count; i++)
                        {

                            if (nombreses[i].Trim() != "" || links[i].Trim() != "")
                            {
                                var elemento = new PlaylistElement()
                                {
                                    Name = nombreses[i],
                                    Link = links[i]
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

        public void ListenQuerys()
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
                    clieente = queryListener.AcceptTcpClient();


                    if (clieente.Connected == true)
                    {

                        clienteses.Add(clieente);

                        Thread procc = new Thread(new ThreadStart(ListenQuerys));
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

                                if (spSlidingPanel.IsExpanded)
                                {
                                    spSlidingPanel.CollapsePane();
                                }
                                else
                                {
                                    spSlidingPanel.ExpandPane();
                                }
                                if (videoQuality > 0 && !videoon)
                                    imgToggleVideo.PerformClick();
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
                                    RunOnUiThread(() => imgPlay.SetBackgroundResource(Resource.Drawable.pausebutton2));
                                    Clouding_service.gettearinstancia().musicaplayer.Start();



                                }
                                else

                                {
                                    RunOnUiThread(() => imgPlay.SetBackgroundResource(Resource.Drawable.playbutton2));
                                    Clouding_service.gettearinstancia().musicaplayer.Pause();


                                }
                            }
                        }
                        else
                            if (capturado.Trim() == "recall()")
                        {
                            // capturado = "";
                            new Thread(() =>
                            {

                                SendAllToClients();
                            }).Start();
                        }

                        else
                            if (capturado.Trim() == "vol0()")
                        {
                            estadovolumen = "vol0()";
                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(0 * 0.01f, 0 * 0.01f);
                            RunOnUiThread(() => imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumelowrojo));
                            SendPortraitToClients();
                        }
                        else
                             if (capturado.Trim() == "vol50()")
                        {
                            estadovolumen = "vol50()";
                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(50 * 0.01f, 50 * 0.01f);
                            RunOnUiThread(() => imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumemediumrojo));
                            SendPortraitToClients();
                        }
                        else
                             if (capturado.Trim() == "vol100()")
                        {
                            estadovolumen = "vol100()";
                            Clouding_service.gettearinstancia().musicaplayer.SetVolume(100 * 0.01f, 100 * 0.01f);
                            RunOnUiThread(() => imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumehighrojo));
                            SendPortraitToClients();
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
                            RunOnUiThread(() => SbVolume.Progress = volumenactual);

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
                            RunOnUiThread(() => SbVolume.Progress = volumenactual);

                        }

                        else

                            if (capturado.Trim() == "actualizarlistaactual()")
                        {
                            //  capturado = "";
                            Thread proc = new Thread(new ThreadStart(SendPlayListToClients));
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
                                SendPortraitToClients();
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
                                    NextVideo();
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
                                PreviousVideo();
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
                                SettingsHelper.SaveSetting("comando", "adelantar()");
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
                                SettingsHelper.SaveSetting("comando", "adelantar()");
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
                                        var parcelable = lvPlayList.OnSaveInstanceState();
                                        lvPlayList.Adapter = adaptadol;

                                        lvPlayList.OnRestoreInstanceState(parcelable);
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

            queryListener.Stop();
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
                            RunOnUiThread(() => imgPortrait.SetImageBitmap(imageBitmap));
                            RunOnUiThread(() => FindViewById<ImageView>(Resource.Id.bgimg).SetImageBitmap(imageBitmap));
                        }

                    }

            }
            catch (Exception) { RunOnUiThread(() => imgPortrait.SetImageBitmap(null)); }
            return imageBitmap;
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

                if (SocketHelper.IsConnected(c) == true)
                {



                    c.Client.Send(System.Text.Encoding.Default.GetBytes("caratula()><;" + imgurl + ";" + tvTitle.Text.Replace('$', ' ') + ";" + "darkgray" + ";" + zelda));

                    Thread.Sleep(400);

                    c.Client.Send(System.Text.Encoding.Default.GetBytes("listar()><;" + listaenlinea));



                }
            }


        }

        public void loadbackupplaylist()
        {
            var back = JsonConvert.DeserializeObject<BackupPlaylist>(File.ReadAllText(Constants.CachePath + "/backupplaylist.json"));
            lapara.Clear();
            laparalinks.Clear();
            listacaratulas.Clear();
            lapara = back.Titles;
            laparalinks = back.Links;
            if (laparalinks[lapara.Count - 1].Trim() == "")
            {
                laparalinks.RemoveAt(lapara.Count - 1);
            }
            listacaratulas = new List<string>(lapara);
            listacaratulas[back.Index] = ">" + lapara[back.Index] + "<";
            locanterior = back.Index;
            indiceactual = back.Index;
            linkactual = laparalinks[back.Index];


            adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
            RunOnUiThread(() =>
            {
                var parcelable = lvPlayList.OnSaveInstanceState();

                lvPlayList.Adapter = adaptadol;

                lvPlayList.OnRestoreInstanceState(parcelable);

            });

            new Thread(() =>
            {
                buscarvidth(laparalinks[back.Index], true);
            }).Start();

        }
        public void listenplaylistchanges()
        {
            int currentcount = 0;
            int currentpos = 0;
            while (detenedor && instance != null)
            {
                if ((lapara.Count != currentcount || indiceactual != currentpos) && lapara.Count > 0 && backupprompted)
                {
                    currentcount = lapara.Count;
                    currentpos = indiceactual;
                    savebackupplaylist();
                }
                else
                if (lapara.Count == 0 && backupprompted)
                {

                    if (File.Exists(Constants.CachePath + "/backupplaylist.json"))
                        File.Delete(Constants.CachePath + "/backupplaylist.json");

                }
                Thread.Sleep(3000);

            }

        }
        public void savebackupplaylist()
        {


            BackupPlaylist back = new BackupPlaylist()
            {
                Titles = lapara,
                Links = laparalinks,
                Index = indiceactual,
                Portraits = listacaratulas
            };
            var archivo = File.CreateText(Constants.CachePath + "/backupplaylist.json");
            archivo.Write(JsonConvert.SerializeObject(back));
            archivo.Close();

        }

        public void reproducirlistaremota(PlayList elementos)
        {




            if (elementos.MediaElements.Count > 0)
            {
                lapara.Clear();
                laparalinks.Clear();
                listacaratulas.Clear();
                lapara = elementos.MediaElements.Select(x => x.Name).ToList();
                laparalinks = elementos.MediaElements.Select(x => x.Link).ToList();
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
                    var parcelable = lvPlayList.OnSaveInstanceState();

                    lvPlayList.Adapter = adaptadol;

                    lvPlayList.OnRestoreInstanceState(parcelable);

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
                try
                {
                    var parcelable = lvPlayList.OnSaveInstanceState();
                    lvPlayList.Adapter = adaptadol;

                    lvPlayList.OnRestoreInstanceState(parcelable);
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
                if (SocketHelper.IsConnected(cl) == true)
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

                        NotificationsHelper.ShowNotification(this, lapara[indice], " Eliminado de la cola!", laparalinks[indice], noticode);
                        laparalinks.RemoveAt(indice);
                        lapara.RemoveAt(indice);
                        listacaratulas.RemoveAt(indice);
                        indiceactual = laparalinks.FindIndex(x => x.Split('=')[1].ToLower() == linkactual.Split('=')[1].ToLower());
                        locanterior = indiceactual;

                        adaptadol = new adapterlistaremoto(this, listacaratulas, laparalinks, linkactual);
                        RunOnUiThread(() =>
                        {
                            var parcelable = lvPlayList.OnSaveInstanceState();
                            lvPlayList.Adapter = adaptadol;

                            lvPlayList.OnRestoreInstanceState(parcelable);
                        });

                        SendPlayListToClients();
                    }
                }
            }).Start();

        }


        protected override void OnDestroy()
        {
            detenedor = false;

            try
            {
                queryListener.Stop();
                playlistListener.Stop();
            }
            catch (Exception e)
            {
                var mensaje = e;

            }



            wake.Release();
            MultiHelper.ExecuteGarbageCollection();
            if (Clouding_service.gettearinstancia() != null)
            {
                Clouding_service.gettearinstancia().musicaplayer.Reset();
            }
            StopService(new Intent(this, typeof(Clouding_service)));
            //   

            SettingsHelper.SaveSetting("onlineactivo", "no");
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
                    DlnavigationDrawer.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
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
        public void loadsuggestionslistener()
        {
            var Innerlink = "";
            while (instance != null)
            {


                if (linkactual != Innerlink && linkactual.Trim() != "")
                {
                    Innerlink = linkactual;

                    sugerenciasdeelemento = SuggestionsHelper.GetSuggestions(linkactual, laparalinks);
                    RunOnUiThread(() =>
                    {
                        adaptadorsugerencias adap = new adaptadorsugerencias(this, sugerenciasdeelemento);
                        adap.ItemClick += (aa, aaa) =>
                        {
                            RunOnUiThread(() =>
                            {
                                Intent intentar = new Intent(this, typeof(customdialogact));
                                intentar.PutExtra("ipadress", ipAddress);
                                intentar.PutExtra("imagen", sugerenciasdeelemento[aaa.Position].Portrait);
                                intentar.PutExtra("url", sugerenciasdeelemento[aaa.Position].Link);
                                intentar.PutExtra("titulo", sugerenciasdeelemento[aaa.Position].Name);
                                intentar.PutExtra("color", "DarkGray");
                                StartActivity(intentar);

                            });
                        };
                        adap.ItemLongClick += (aa, aaa) =>
                        {

                            RunOnUiThread(() =>
                            {
                                Toast.MakeText(this, sugerenciasdeelemento[aaa.Position].Name, ToastLength.Long).Show();
                            });
                        };
                        rvSuggestions.SetAdapter(adap);
                        if (!headeradded)
                        {

                            headeradded = true;
                            layoutSuggestions.Visibility = ViewStates.Visible;

                        }
                    });
                }



                Thread.Sleep(2000);
            }


        }
        public void ipchangerlistener()
        {

            while (detenedor)
            {

                string currentip = SocketHelper.GetIp();
                if (ipAddress != currentip)
                {

                    ipAddress = currentip;

                    detenedor = false;
                    Thread.Sleep(1500);
                    detenedor = true;
                    storedPlaylistsThread.Abort();
                    querysThread.Abort();
                    clienteses.Clear();
                    queryListener.Stop();
                    playlistListener.Stop();
                    queryListener = new TcpListener(IPAddress.Parse(ipAddress), 1024);
                    queryListener.Start();
                    playlistListener = new TcpListener(IPAddress.Parse(ipAddress), 9856);
                    playlistListener.Start();

                    storedPlaylistsThread = new Thread(() =>
                    {
                        ListenStoredPlaylists();
                    });
                    querysThread = new Thread(() =>
                    {
                        ListenQuerys();
                    });
                    storedPlaylistsThread.Start();
                    querysThread.Start();
                    if (!MultiHelper.HasInternetConnection())
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
                    else
                    {
                        RunOnUiThread(() => Toast.MakeText(this, "Conexion reestablecida", ToastLength.Long).Show());
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
                var results = src.SearchQuery(termino, 1);
                if (results.Count > 0)
                {
                    var listatitulos = results.Select(ax => WebUtility.HtmlDecode(StringsHelper.RemoveIllegalPathCharacters(ax.Title.Replace("&quot;", "").Replace("&amp;", "")))).ToList();
                    var listalinks = results.Select(ax => ax.Url).ToList();
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
                           intentoo.PutExtra("ipadress", ipAddress);
                           intentoo.PutExtra("url", listalinks[posicion]);
                           intentoo.PutExtra("imagen", @"https://i.ytimg.com/vi/" + listalinks[posicion].Split('=')[1] + "/mqdefault.jpg");
                           StartActivity(intentoo);

                       };
                       adapterlistaremoto adapt = new adapterlistaremoto(this, listatitulos, listalinks);
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
                SetVideoSize();


            }
            else
            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape && videoon)
            {
                SetVideoSize();
                //   video.LayoutParameters = new RelativeLayout.LayoutParams(video.LayoutParameters.Width,sizevideonormal);
            }

        }


        public void Onresume()
        {
            if (videoon)
                svVideoLayout.Visibility = ViewStates.Visible;
            base.OnResume();
        }
        public void Onpause()
        {
            svVideoLayout.Visibility = ViewStates.Gone;
            base.OnPause();
        }

        public void SetVideoSize()
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
                Android.Views.ViewGroup.LayoutParams lp = svVideoLayout.LayoutParameters;
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
                svVideoLayout.LayoutParameters = relative;
            }
            catch (Exception) { }
        }



    }

}







