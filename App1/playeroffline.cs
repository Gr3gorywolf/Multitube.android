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
using System.IO;
using Android.Media;
using System.Net;
using System.Threading;
using Android.Support.V4;
using Android.Renderscripts;
using Android.Media.Session;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Support.V7.AppCompat;
using Android.Support.V7.View;
using Android.Support.V7.Util;
using Android.Support.V7.Content;
using Android.Glide;
using Android.Glide.Request;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState = true)]
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
    public class playeroffline : Android.Support.V7.App.AppCompatActivity, ISurfaceHolderCallback
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
    {
        public int posactual;
        public bool videofullscreen = false;
        List<string> fullnombre = new List<string>();
        List<string> fullportadas = new List<string>();
        List<string> fullpaths = new List<string>();
        public bool videoenholder = false;
        public bool desdethread = false;
        MediaPlayer musicaplayer = new MediaPlayer();
        public List<string> nombreses = new List<string>();
        public List<string> linkeses = new List<string>();
        public List<string> patheses = new List<string>();

        public Dictionary<string, int> diccionario = new Dictionary<string, int>();
        public int counter = 0;
        public bool sincronized = false;
        public int indiceactual = 0;
        public TextView titulo;
        public ImageView adelantar;
        public ImageView atrazar;
        public ImageView siguiente;
        public EditText filter;
        public ImageView anterior;
        public ImageView playpause;
        public ListView lista;
        public ImageView portada;
        public SeekBar porcientoreproduccion;
        public bool detenedor = true;
        public string playerstatus = "";
        public static playeroffline instancia;
        public bool showingvideosresults = false;
        public ImageView abrirmenu;
        public LinearLayout cerrarmenu;
        public LinearLayout showvideos;
        public LinearLayout sidemenu;
        public SurfaceView video;
        public ISurfaceHolder holder;
        TextView textovideomusica;
        ImageView imagenvideomusica;
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
        public ImageView fondo;
        public long millis = 0;
        ImageView entrarenmodovideo;
        public string postcachepath = "";
        broadcast_receiver br;
        public int postcachepos = 0;
        public MediaSession mSession;
        DrawerLayout sidem;
        NavigationView itemsm;
        ImageView botonaccion;
        Android.Support.V7.Widget.SearchView searchview;
        Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout panel;



        public static playeroffline gettearinstancia()
        {
            return instancia;
        }
        public void SurfaceChanged(ISurfaceHolder holder,[GeneratedEnum]Format formato,int klk,int klk2)
        {

        }
        public void SurfaceCreated(ISurfaceHolder holder)
        {

            Clouding_serviceoffline.gettearinstancia().musicaplayer.SetDisplay(holder);
            videoenholder = true;
        }
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Clouding_serviceoffline.gettearinstancia().musicaplayer.SetDisplay(null);
        }




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutplayeroffline);

            ////////////////////////////////////////////////mapeos////////////////////////////
            lista = FindViewById<ListView>(Resource.Id.listView1);
            titulo = FindViewById<TextView>(Resource.Id.textView1);
            portada = FindViewById<ImageView>(Resource.Id.imageView9);
            adelantar = FindViewById<ImageView>(Resource.Id.imageView14);
            atrazar = FindViewById<ImageView>(Resource.Id.imageView11);
            siguiente = FindViewById<ImageView>(Resource.Id.imageView13);
            anterior = FindViewById<ImageView>(Resource.Id.imageView10);
            playpause = FindViewById<ImageView>(Resource.Id.imageView12);
            filter = FindViewById<EditText>(Resource.Id.editText1);
            porcientoreproduccion = FindViewById<SeekBar>(Resource.Id.seekBar1);
            abrirmenu = FindViewById<ImageView>(Resource.Id.imageView15);
            video = FindViewById<SurfaceView>(Resource.Id.videoView1);
            cerrarmenu = FindViewById<LinearLayout>(Resource.Id.linearLayout9);
            showvideos = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            sidemenu = FindViewById<LinearLayout>(Resource.Id.linearLayout7);
            imagenvideomusica = FindViewById<ImageView>(Resource.Id.imageView8);
            textovideomusica = FindViewById<TextView>(Resource.Id.textView2);
            fondo = FindViewById<ImageView>(Resource.Id.imageView45);
            var linealsyn = FindViewById<LinearLayout>(Resource.Id.linearLayout20);
            var barraariba = FindViewById<LinearLayout>(Resource.Id.linearLayout8);
            var barraabajo = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            var botonborrar = FindViewById<ImageView>(Resource.Id.imageView46);
            entrarenmodovideo = FindViewById<ImageView>(Resource.Id.imageView47);
            sidem = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            itemsm = FindViewById<NavigationView>(Resource.Id.content_frame);
            botonaccion = FindViewById<ImageView>(Resource.Id.btnaccion);
            searchview = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);
       
            ////////////////////////////////////////////////miselaneo////////////////////////////////

            panel = FindViewById<Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout>(Resource.Id.sliding_layout);
         
            
            panel.IsUsingDragViewTouchEvents = true;
            panel.DragView = FindViewById<RelativeLayout>(Resource.Id.scrollable);
            var solapa = FindViewById<LinearLayout>(Resource.Id.solapita);
            solapa.Click += delegate
            {

                if (panel.IsExpanded)
                    panel.CollapsePane();
                else
                    panel.ExpandPane();
            };
            //  panel.Clickable = false;
            //   panel.IsUsingDragViewTouchEvents = true;

            imagenvideomusica.SetBackgroundResource(Resource.Drawable.videoplayer);
        //    barraariba.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
          //  barraabajo.SetBackgroundColor(Color.ParseColor("#2b2e30"));
          //  sidemenu.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            clasesettings.guardarsetting("tapnumber", "0");
            clasesettings.guardarsetting("offlineactivo", "si");
            holder = video.Holder;
          sidemenu.BringToFront();
            sidemenu.BringToFront();
            sidemenu.BringToFront();
          sidemenu.Visibility = ViewStates.Gone;
            WallpaperManager wm = WallpaperManager.GetInstance(this);
            Drawable d = wm.Drawable;
          var  fondoblurreado = clasesettings.CreateBlurredImageformbitmap(this, 20, ((BitmapDrawable)d).Bitmap);
            fondo.SetImageBitmap(fondoblurreado);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
            SetSupportActionBar(toolbar);
         
           
            //Enable support action bar to display hamburger
     
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);
           SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "";
            //SupportActionBar.SetBackgroundDrawable( new ColorDrawable(Color.ParseColor("#2b2e30")) );
         //   SupportActionBar.SetHomeButtonEnabled(true);
        

            if (clasesettings.gettearvalor("mediacache").Trim() != "")
            {

                postcachepath = clasesettings.gettearvalor("mediacache");
               
                if (System.IO.Path.GetFileName(postcachepath).EndsWith(".mp3"))
                {
                    new Thread(() => { cargarmp3(); }).Start();
                }else
                {
                    imagenvideomusica.SetBackgroundResource(Resource.Drawable.musicalnote);
                    textovideomusica.Text = "Musica";
                    showingvideosresults = true;
                    new Thread(() => { cargarvideos(); }).Start();
                }


            }
            else
            {
                new Thread(() => { cargarmp3(); }).Start();
            }
            new Thread(() =>
            {
                actualizarmediasesion();
            }).Start();
            new Thread(() =>
            {
                ponerpostcache();
            }).Start();
            Random brom = new Random();
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
      
         
            botonborrar.Visibility = ViewStates.Gone;
            /***********************************************************************
             * 
             * 
             * 
             * 
             * */


            // Start listening for button presses


            titulo.Selected = true;
          /****************************************************************************
           
             
             
             */
            clasesettings.guardarsetting("videoactivo", "no");
            playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
            instancia = this;
            video.Visibility = ViewStates.Gone;
            /////////////////////////////////////////////////////////clicks////////////////////////////////////////
            panel.PanelExpanded += delegate
            {
                botonaccion.SetBackgroundResource(Resource.Drawable.folder);

            };
            panel.PanelCollapsed += delegate
            {
                if (Clouding_serviceoffline.gettearinstancia() != null)
                {
                    if (Clouding_serviceoffline.gettearinstancia().musicaplayer.IsPlaying)                 
                            botonaccion.SetBackgroundResource(Resource.Drawable.pausebutton2);                  
                    else                            
                            botonaccion.SetBackgroundResource(Resource.Drawable.playbutton2);
                  }

                  
              
            };

        

            searchview.QueryTextChange += (aa, aaa) =>
            {
             
                var adaptadol2 = new adapterlistaoffline(this, nombreses.Where(a => a.ToLower().Contains(aaa.NewText.ToLower())).ToList(), linkeses, "", nombreses, diccionario, patheses);

                RunOnUiThread(() => {

                    var parcelable = lista.OnSaveInstanceState();
                    lista.Adapter = adaptadol2;
                    lista.OnRestoreInstanceState(parcelable);

                });
            };



            botonaccion.Click += delegate
            {



                if (panel.IsExpanded)
                {
                    Android.Net.Uri selectedUri= Android.Net.Uri.Parse(System.IO.Path.GetPathRoot( patheses[indiceactual]));
                    Intent intent = new Intent(Intent.ActionView);
                    intent.SetDataAndType(selectedUri, "resource/folder");
                    StartActivity(Intent.CreateChooser(intent,"Seleccione un explorador"));
                }
                else {
                    playpause.PerformClick();
                }

            };


            itemsm.NavigationItemSelected += (sender, e) => {
                //  e.MenuItem.SetChecked(true);

                //react to click here and swap fragments or navigate
                e.MenuItem.SetChecked(true);
                e.MenuItem.SetChecked(true);
        
             
               
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Audios") {
                    if (showingvideosresults)
                    {
                        SupportActionBar.Show();
                        searchview.SetQuery("", false);
                        showingvideosresults = false;
                        indiceactual = -1;
                        cargarmp3();
                    }
                }
                else
                  if (e.MenuItem.TitleFormatted.ToString().Trim() == "Videos")
                {
                    if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
                    {

                       
                        if (!showingvideosresults)
                        {
                            SupportActionBar.Show();
                            searchview.SetQuery("", false);
                            showingvideosresults = true;
                            indiceactual = -1;
                            cargarvideos();
                        }
                      
                    }
                    else
                    {
                        Toast.MakeText(this, "No tiene videos descargados actualmente", ToastLength.Long).Show();

                    }
                }
                else
                  if (e.MenuItem.TitleFormatted.ToString().Trim() == "Sync")
                {
                    StartActivity(typeof(actividadsync));
                }
                e.MenuItem.SetChecked(false);
                e.MenuItem.SetChecked(false);
                sidem.CloseDrawers();
              
            };
            entrarenmodovideo.Click += delegate
            {
                panel.CollapsePane();
                SupportActionBar.Show();
                animar(entrarenmodovideo);
                if (video.Visibility == ViewStates.Visible)
                {
                    lista.Visibility = ViewStates.Visible;
                    video.Visibility = ViewStates.Gone;
                    entrarenmodovideo.SetBackgroundResource(Resource.Drawable.videorojo);
                    video.KeepScreenOn =false;
                    Window.ClearFlags(WindowManagerFlags.KeepScreenOn);

                }
                else
                {
                    lista.Visibility = ViewStates.Invisible;
                    video.Visibility = ViewStates.Visible;
                    if (!videoenholder)
                    {
                        holder.AddCallback(this);
                    }
                    entrarenmodovideo.SetBackgroundResource(Resource.Drawable.listaroja);
                    video.KeepScreenOn = true;
                    Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                }

            };
            linealsyn.Click += delegate
            {
                animar(linealsyn);
                animarycerrar(sidemenu);
                StartActivity(typeof(actividadsync));
            };
            botonborrar.Click += delegate
            {
                searchview.SetQuery("", false);
                botonborrar.Visibility = ViewStates.Gone;
            };
            barraariba.Click += delegate
            {

            };
            barraabajo.Click += delegate
            {

            };
            video.Touch += (aa, aaaa) =>
            {
                if (aaaa.Event.Action == MotionEventActions.Up)
                {
                    if (!videofullscreen)
                    {
                        video.Visibility = ViewStates.Visible;
                       // barraariba.Visibility = ViewStates.Invisible;
                       // barraabajo.Visibility = ViewStates.Invisible;
                        sidemenu.Visibility = ViewStates.Invisible;
                        FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility = ViewStates.Gone;
                        abrirmenu.Visibility = ViewStates.Visible;
                        SupportActionBar.Hide();
                       videofullscreen = true;
                    }
                    else
                    {
                        SupportActionBar.Show();
                        video.Visibility = ViewStates.Visible;
                        FindViewById<RelativeLayout>(Resource.Id.scrollable).Visibility = ViewStates.Visible;
                        ///   barraariba.Visibility = ViewStates.Visible;
                        //   barraabajo.Visibility = ViewStates.Visible;
                        videofullscreen = false ;
                    }
                    

                }
            };
            portada.Click += delegate
            {
                animar(portada);
                searchview.SetQuery("", false);
                var adaptadol2 = new adapterlistaoffline(this, nombreses.Where(a => a.ToLower().Contains(searchview.Query.ToLower())).ToList(), linkeses, "", nombreses, diccionario, patheses);

                RunOnUiThread(() => {
                    var parcelable = lista.OnSaveInstanceState();
                    lista.Adapter = adaptadol2;
                    lista.OnRestoreInstanceState(parcelable);
                });
                if (searchview.Query.Length >= 1)
                {
                    botonborrar.Visibility = ViewStates.Visible;
                }
                else
                {
                    botonborrar.Visibility = ViewStates.Gone;
                }
                try
                {
                    lista.SetSelection(indiceactual);
                }
                catch (Exception)
                {

                }


            };
            abrirmenu.Click += delegate
            {
                animar(abrirmenu);
             
                abrirmenu.Visibility = ViewStates.Invisible;
                sidemenu.Visibility = ViewStates.Visible;
              
                animar6(sidemenu);
            };
            cerrarmenu.Click += delegate
            {
               
                animarycerrar(sidemenu);
               
            };
            sidemenu.Click += delegate
            {

            };
            showvideos.Click += delegate
            {
                
                animar(showvideos);
                lista.Visibility = ViewStates.Visible;
                video.Visibility = ViewStates.Gone;
               
                entrarenmodovideo.SetBackgroundResource(Resource.Drawable.videorojo);
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
                {

                    searchview.SetQuery("", false);
                    if (!showingvideosresults)
                {
                        var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                        RunOnUiThread(() => {
                            var parcelable = lista.OnSaveInstanceState();
                            lista.Adapter = adaptadolo;
                            lista.OnRestoreInstanceState(parcelable);
                        });
                        animar2(lista);
                    imagenvideomusica.SetBackgroundResource(Resource.Drawable.musicalnote);
                        textovideomusica.Text = "Musica";
                    showingvideosresults = true;
                    indiceactual = -1;
                    cargarvideos();
 
                }else
                {
                        var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                        RunOnUiThread(() => {
                            var parcelable = lista.OnSaveInstanceState();
                            lista.Adapter = adaptadolo;
                            lista.OnRestoreInstanceState(parcelable);

                        }
                        );
                        animar2(lista);
                        imagenvideomusica.SetBackgroundResource(Resource.Drawable.videoplayer);
                        textovideomusica.Text = "Videos";
                        showingvideosresults = false;
                    indiceactual = -1;
                    cargarmp3();
                }
                }else
                {
                    Toast.MakeText(this, "No tiene videos descargados actualmente", ToastLength.Long).Show();
                }
            };
            porcientoreproduccion.ProgressChanged += (aa, aaa) =>
            {

               
              if (aaa.FromUser)
                {
                    Clouding_serviceoffline.gettearinstancia().musicaplayer.SeekTo(porcientoreproduccion.Progress);
                }
            };
            lista.ItemClick += (a, aa) => {
                if (nombreses.Count > 0) {
                    var vii = aa.View.FindViewById<TextView>(Resource.Id.textView1);
                  

                  

                    reproducir(nombreses.IndexOf(vii.Text), false);
                }
                  
                
            };
            filter.TextChanged += delegate
            {

               
                if (filter.Text.Length >= 1)
                {
                    botonborrar.Visibility = ViewStates.Visible;
                }
                else
                {
                    botonborrar.Visibility = ViewStates.Gone;
                }
            
               /* newlistal.Clear();
                newlistan.Clear();
                newlistap.Clear();*/

            };
            adelantar.Click += delegate
            {
                animar(adelantar);
                Clouding_serviceoffline.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_serviceoffline.gettearinstancia().musicaplayer.CurrentPosition + Clouding_serviceoffline.gettearinstancia().musicaplayer.Duration * 0.10));
                // clasesettings.deciralbroadcast(this, "adelantar()");
                // clasesettings.guardarsetting("cquerry", "adelantar()");
            };
            atrazar.Click += delegate
            {
                animar(atrazar);
                Clouding_serviceoffline.gettearinstancia().musicaplayer.SeekTo(Convert.ToInt32(Clouding_serviceoffline.gettearinstancia().musicaplayer.CurrentPosition - Clouding_serviceoffline.gettearinstancia().musicaplayer.Duration * 0.10));
                //   clasesettings.guardarsetting("cquerry", "atrazar()");
            };
            siguiente.Click += delegate
            {
                animar(siguiente);
                reproducir(indiceactual + 1,false);
               // clasesettings.guardarsetting("cquerry", "siguiente()");
            };
            anterior.Click += delegate
            {
                animar(anterior);
                if (indiceactual - 1 < 0)
                {
                    indiceactual = 0;
                }
                else
                {
                    reproducir(indiceactual - 1,false);
                }
              
                //   clasesettings.guardarsetting("cquerry", "anterior()");
            };
            playpause.Click += delegate
            {


                animar(playpause);
                var estado = Clouding_serviceoffline.gettearinstancia().musicaplayer.IsPlaying;
                if (estado)
                {
                    RunOnUiThread(() => playpause.SetBackgroundResource(Resource.Drawable.playbutton2));
                    Clouding_serviceoffline.gettearinstancia().musicaplayer.Pause();
                }
                else
                {
                    RunOnUiThread(() => playpause.SetBackgroundResource(Resource.Drawable.pausebutton2));
                    Clouding_serviceoffline.gettearinstancia().musicaplayer.Start();
                }
             ///   clasesettings.deciralbroadcast(this, "playpause()");
               // clasesettings.guardarsetting("cquerry", "playpause()");
            };

            // Create your application here
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {

         


            // Android.Views.con
            /*
             switch (keyCode)
             {
                 case Android.Views.Keycode.Headsethook:

                     if (playeroffline.gettearinstancia() != null)
                     {
                         playeroffline.gettearinstancia().RunOnUiThread(() => {
                             playeroffline.gettearinstancia().playpause.CallOnClick();

                         });
                     }


                     break;
                 case Keycode.MediaPlayPause:
                     if (playeroffline.gettearinstancia() != null)
                     {
                         playeroffline.gettearinstancia().RunOnUiThread(() => {
                             playeroffline.gettearinstancia().playpause.CallOnClick();

                         });
                     }
                     break;
                 case Keycode.MediaNext:
                     if (playeroffline.gettearinstancia() != null)
                     {
                         playeroffline.gettearinstancia().RunOnUiThread(() => {
                             playeroffline.gettearinstancia().siguiente.CallOnClick();

                         });
                     }
                     break;
                 case Keycode.MediaPrevious:
                     if (playeroffline.gettearinstancia() != null)
                     {
                         playeroffline.gettearinstancia().RunOnUiThread(() => {
                             playeroffline.gettearinstancia().anterior.CallOnClick();

                         });
                     }
                     break;
             }

     */

            return base.OnKeyDown(keyCode, e);

        }

        public  void Onresume()
        {
        
            lista = FindViewById<ListView>(Resource.Id.listView1);
            var adaptadol2 = new adapterlistaoffline(this, nombreses.Where(a => a.ToLower().Contains(searchview.Query.ToLower())).ToList(), linkeses, "", nombreses, diccionario, patheses);
          
            RunOnUiThread(() => {
                var parcelable = lista.OnSaveInstanceState();
                lista.Adapter = adaptadol2;
                lista.OnRestoreInstanceState(parcelable);
            });
            lista.SetSelection(posactual);
          
            base.OnResume();
        }

        public void Onpause()
        {
          
          
            video.Visibility = ViewStates.Gone;
            lista.Visibility = ViewStates.Visible;
          
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string>());
           lista.Adapter = adaptadol;
            lista.Dispose();
            posactual = lista.FirstVisiblePosition;
         
            base.OnPause();
        }
        private Bitmap rCreateBlurredImage(int radius, string link)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap = BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]);

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

        public void cargarmp3()
        {
            nombreses.Clear();
            linkeses.Clear();
            patheses.Clear();
            var listaordenado = new List<string>();
            string todomezclado = "";
       if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d")) { 
             todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
       
             listaordenado = todomezclado.Split('�').ToList();
            if (listaordenado[listaordenado.Count - 1].Trim() == "")
            {
                listaordenado.RemoveAt(listaordenado.Count - 1);
            }

                if (clasesettings.gettearvalor("ordenalfabeto") == "si")
                {
                    listaordenado.Sort();
                }

         
            foreach (string ax in listaordenado)
            {
                if (ax.Trim().Length > 1)
                {
                    nombreses.Add(ax.Split('�')[0]);
                    linkeses.Add(ax.Split('�')[1]);
                    patheses.Add(ax.Split('�')[2]);
                }

            }
            }
            if (!sincronized)
            {
                RunOnUiThread(() =>
                {
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
                    dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
                    dialogoprogreso.SetCanceledOnTouchOutside(false);
                    dialogoprogreso.SetCancelable(false);
                    dialogoprogreso.SetTitle("Cargando...");
                    dialogoprogreso.SetMessage("Por favor espere");
                    
                    dialogoprogreso.Show();
                });
                sync();
            }      
            comprobarpatheses();
          
           
            var adaptadol = new adapterlistaoffline(this, nombreses, linkeses, "", nombreses,diccionario,patheses);
            RunOnUiThread(() => {
                var parcelable = lista.OnSaveInstanceState();
                lista.Adapter = adaptadol;
                lista.OnRestoreInstanceState(parcelable);
            });
            if (nombreses.Count == 0)
            {
                var adaptadolllll = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                RunOnUiThread(() => {
                    var parcelable = lista.OnSaveInstanceState();
                    lista.Adapter = adaptadolllll;
                    lista.OnRestoreInstanceState(parcelable);
                });
            }
            listaordenado = new List<string>();
            todomezclado = "";
            clasesettings.recogerbasura();
        }
        public void cargarvideos()
        {
            nombreses.Clear();
            linkeses.Clear();
            patheses.Clear();
            string todomezclado = "";
            var listaordenado = new List<string>();
            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
            {
                 todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");


                listaordenado = todomezclado.Split('�').ToList();
                if (listaordenado[listaordenado.Count - 1].Trim() == "")
                {
                    listaordenado.RemoveAt(listaordenado.Count - 1);
                }

                if (clasesettings.gettearvalor("ordenalfabeto") == "si")
                {
                    listaordenado.Sort();
                }
                foreach (string ax in listaordenado)
                {
                    if (ax.Trim().Length > 1)
                    {
                        nombreses.Add(ax.Split('�')[0]);
                        linkeses.Add(ax.Split('�')[1]);
                        patheses.Add(ax.Split('�')[2]);
                    }

                }
            }
            if (!sincronized)
            {
                RunOnUiThread(() =>
                {
#pragma warning disable CS0618 // El tipo o el miembro est�n obsoletos
                    dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro est�n obsoletos
                    dialogoprogreso.SetCanceledOnTouchOutside(false);
                    dialogoprogreso.SetCancelable(false);
                    dialogoprogreso.SetTitle("Cargando...");
                    dialogoprogreso.SetMessage("Por favor espere");
                    dialogoprogreso.Show();
                });
                sync();
            }
            comprobarpatheses();
          
            var adaptadol = new adapterlistaoffline(this, nombreses, linkeses, "", nombreses,diccionario,patheses);
            RunOnUiThread(() => {
                var parcelable = lista.OnSaveInstanceState();
                lista.Adapter = adaptadol;
                lista.OnRestoreInstanceState(parcelable);
            });
            if (nombreses.Count == 0)
            {
                var adaptadolllll = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                RunOnUiThread(() => {
                    var parcelable = lista.OnSaveInstanceState();
                    lista.Adapter = adaptadolllll ;
                    lista.OnRestoreInstanceState(parcelable);
                });
            }
            listaordenado = new List<string>();
            todomezclado = "";
            clasesettings.recogerbasura();
        }

        public void ponerpostcache()
        {
          
        }

        public void iniciarservidor()
        {

            StopService(new Intent(this, typeof(cloudingserviceonline)));
            StopService(new Intent(this, typeof(Clouding_serviceoffline)));
            StopService(new Intent(this, typeof(Clouding_service)));
            StartService(new Intent(this, typeof(Clouding_serviceoffline)));
            RunOnUiThread(() => holder.AddCallback(this));
          ///  Thread.Sleep(2000);
            new Thread(() => { receiver(); }).Start();
           
        }

        public override void OnBackPressed()
        {
            if (sidem.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                RunOnUiThread(() => { sidem.CloseDrawers(); });
            else { 
            if (!panel.IsExpanded)
                clasesettings.preguntarsimenuosalir(this);
            else
                RunOnUiThread(() => panel.CollapsePane());
            }
            // base.OnBackPressed();
        }


        protected override void OnDestroy()
        {


            if (Clouding_serviceoffline.gettearinstancia() != null)
            {
                Clouding_serviceoffline.gettearinstancia().musicaplayer.Reset();
            }


            StopService(new Intent(this, typeof(Clouding_serviceoffline)));
            clasesettings.recogerbasura();
          //  UnregisterReceiver(br);
         //   instancia = null;
            clasesettings.guardarsetting("offlineactivo", "no");
            base.OnDestroy();
        }
       
        
           
       
       



        public void receiver()
        {

        
            while (detenedor)
            {
                if (Clouding_serviceoffline.gettearinstancia() != null) {
                    if (Clouding_serviceoffline.gettearinstancia().musicaplayer.IsPlaying)
                    {
                       
                        RunOnUiThread(() => porcientoreproduccion.Max = Clouding_serviceoffline.gettearinstancia().musicaplayer.Duration);
                        RunOnUiThread(() => porcientoreproduccion.Progress = Clouding_serviceoffline.gettearinstancia().musicaplayer.CurrentPosition);
                        if ( !panel.IsExpanded) {
                           
                           RunOnUiThread(()=> botonaccion.SetBackgroundResource(Resource.Drawable.pausebutton2));
                        }
                    }
                    else {
                        if (!panel.IsExpanded)
                        {

                            RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.playbutton2));
                        }

                    }
                }
               

                // clasesettings.guardarsetting("tapnumber", "0");
                Thread.Sleep(1000);
            }

        }
  
        public void comprobarpatheses()
        {
            var listanewpath = new List<string>();
            var listanewname = new List<string>();
            var listanewlink = new List<string>();
          
            int i = 0;
            foreach( string ph in patheses){
                if (File.Exists(ph))
                {
                    listanewpath.Add(patheses[i]);
                    listanewname.Add(nombreses[i]);
                    listanewlink.Add(linkeses[i]);
                  
                }
                i++;
            }
            nombreses = new List<string>(listanewname);
            linkeses = new List<string>(listanewlink);
            patheses = new List<string>(listanewpath);
         

           
            var adaptadol = new adapterlistaoffline(this, nombreses, linkeses, "",nombreses,diccionario,patheses);
            RunOnUiThread(() =>
            {

                var parcelable = lista.OnSaveInstanceState();

                lista.Adapter = adaptadol;
                lista.OnRestoreInstanceState(parcelable);
            }
            );
            listanewpath = new List<string>();
            listanewname = new List<string>();
            listanewlink = new List<string>();
            if (postcachepath.Trim() != "")
            {
                
                reproducir(patheses.IndexOf(postcachepath),true);
                try
                {
                    RunOnUiThread(() => dialogoprogreso.Dismiss());
                }
                catch (Exception)
                {

                };
                postcachepath = "";
              //  postcachepos = 0;
            }
            // imageneses
        }
        public void sync()
        {

            diccionario.Clear();
         

            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d"))
            {
                string todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");

                var listaordenado = todomezclado.Split('�').ToList();
                if (listaordenado[listaordenado.Count - 1].Trim() == "")
                {
                    listaordenado.RemoveAt(listaordenado.Count - 1);
                }


                foreach (string ax in listaordenado)
                {
                    if (ax.Trim().Length > 1)
                    {
                        fullnombre.Add(ax.Split('�')[0]);
                        fullportadas.Add(ax.Split('�')[1]);
                        fullpaths.Add(ax.Split('�')[2]);
                    }

                }
                listaordenado = new List<string>();
                todomezclado = "";

            }
            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
            {
                string todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");

                var listaordenado = todomezclado.Split('�').ToList();
                if (listaordenado[listaordenado.Count - 1].Trim() == "")
                {
                    listaordenado.RemoveAt(listaordenado.Count - 1);
                }


                foreach (string ax in listaordenado)
                {
                    if (ax.Trim().Length > 1)
                    {
                        fullnombre.Add(ax.Split('�')[0]);
                        fullportadas.Add(ax.Split('�')[1]);
                        fullpaths.Add(ax.Split('�')[2]);
                    }

                }
                listaordenado = new List<string>();
                todomezclado = "";
            }
            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits"))
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits");
            }
            for (int i = 0; i < fullportadas.Count; i++)
            {
                try
                {


                    if (!File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + fullportadas[i].Split('=')[1]))
                    {
                        new Thread(() =>
                        {
                            using (var webbrowser = new WebClient())
                            {
                                webbrowser.DownloadFile(new Uri("https://i.ytimg.com/vi/" + fullportadas[i].Split('=')[1] + "/mqdefault.jpg"), Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + fullportadas[i].Split('=')[1]);
                            }
                        }).Start();
                    }
                }
                catch (Exception)
                {

                }
                if (!diccionario.ContainsKey(fullnombre[i]))
                {
                    diccionario.Add(fullnombre[i],new Random().Next(999999999));
                }
            }

            fullnombre = new List<string>();
            fullpaths = new List<string>();
            fullportadas = new List<string>();
            sincronized = true;

            if (postcachepath.Trim() == "")
            {
                try
                {
                    RunOnUiThread(() => dialogoprogreso.Dismiss());
                }
                catch (Exception)
                {

                };
            }
          

            ///  clasesettings.recogerbasura();
            /// 

            if (!desdethread)
            {

                new Thread(() => { iniciarservidor(); }).Start();
               
            }
            desdethread = false;
        }
        




        public void animar5(View imagen)
        {
            RunOnUiThread(() =>
            {
                imagen.SetLayerType(LayerType.Hardware, null);
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(250);
            animacion.Start();
            animacion.AnimationEnd += delegate
            {
                imagen.SetLayerType(LayerType.None, null);
              
            };
            });
        }

        public void animar4(View imagen)
        {
            RunOnUiThread(() =>
            {


                imagen.SetLayerType(LayerType.Hardware, null);
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", 1000, 0);
                animacion.SetDuration(250);
                animacion.Start();
                animacion.AnimationEnd += delegate
                {
                    imagen.SetLayerType(LayerType.None, null);
                };
            });
        }

        public void cambiarprogreso()
        {
             RunOnUiThread(()=>    porcientoreproduccion.Max = 10);
            RunOnUiThread(() => porcientoreproduccion.Progress = 5);
        }
        public void reproducir(int indice,bool desdecache)
        {
            try
            {
                if (indice <= nombreses.Count - 1 && indice >= 0)
                {

                    RunOnUiThread(() =>
                    {
                        lista.Visibility = ViewStates.Visible;
                        video.Visibility = ViewStates.Gone;
                    });


                    if (System.IO.Path.GetFileName(patheses[indice]).EndsWith(".mp4"))
                    {
                        RunOnUiThread(() => entrarenmodovideo.SetBackgroundResource(Resource.Drawable.videorojo));
                        RunOnUiThread(() => entrarenmodovideo.Visibility = ViewStates.Visible);
                    }
                    else
                    {

                        RunOnUiThread(() => entrarenmodovideo.Visibility = ViewStates.Gone);
                    }
                    indiceactual = indice;
                    RunOnUiThread(() => playpause.SetBackgroundResource(Resource.Drawable.pausebutton2));
                    RunOnUiThread(() => titulo.Text = nombreses[indice]);
                    Clouding_serviceoffline.gettearinstancia().tituloactual = nombreses[indice];
                    Clouding_serviceoffline.gettearinstancia().linkactual = Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + linkeses[indice].Split('=')[1];
                    RunOnUiThread(() => {
                        try
                        {

                            Glide.With(this)
                            .Load(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + linkeses[indice].Split('=')[1])
                            .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                            .Into(portada);
                            Glide.With(this)
                                 .Load(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + linkeses[indice].Split('=')[1])
                                 .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                                 .Into(FindViewById<ImageView>(Resource.Id.bgimg));

                        }
                        catch (Exception) {

                        }
                      
                        
                        });
                    animar4(portada);

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        RunOnUiThread(() => fondo.DestroyDrawingCache());
                        //  RunOnUiThread(() => fondo = FindViewById<ImageView>(Resource.Id.fondo1));
                        RunOnUiThread(() => fondo.SetImageBitmap(clasesettings.CreateBlurredImageoffline(this, 20, linkeses[indiceactual])));
                        animar5(fondo);
                    }
                    if (desdecache)
                    {
                        RunOnUiThread(() => playpause.SetBackgroundResource(Resource.Drawable.playbutton2));
                    }
                    Clouding_serviceoffline.gettearinstancia().reproducir(patheses[indice], desdecache);

                    if (Clouding_serviceoffline.gettearinstancia() != null)
                    {

                        if (Clouding_serviceoffline.gettearinstancia().musicaplayer != null)
                        {
                            if (Clouding_serviceoffline.gettearinstancia().musicaplayer.IsPlaying)
                            {
                                clasesettings.guardarsetting("mediacache", patheses[indiceactual]);

                            }


                        }
                    }


                }
                else
                {
                    if (indice < 0)
                    {
                        indice = 0;
                    }
                    else
                    if (indice > nombreses.Count - 1)
                    {
                        indice = nombreses.Count - 1;
                        if (!Clouding_service.gettearinstancia().musicaplayer.IsPlaying)
                        {
                            RunOnUiThread(() => playpause.SetBackgroundResource(Resource.Drawable.playbutton2));
                        }

                    }
                    indiceactual = indice;


                }
                /// clasesettings.deciralbroadcast(this, "reproducir()>" + patheses[indice]);
                //  clasesettings.guardarsetting("musica", patheses[indice]);
                //  clasesettings.guardarsetting("servicio", "musica");
                //   clasesettings.guardarsetting("cquerry", "data()>" + nombreses[indice] + ">" + Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + portadases[indice].Split('=')[1]);

                // musicaplayer.SetDataSource(downloadurl);



                //    musicaplayer.Reset();


                //     musicaplayer = Android.Media.MediaPlayer.Create(this, Android.Net.Uri.Parse(downloadurl));

                //  musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
                //   musicaplayer.Start();


                clasesettings.recogerbasura();
            }
            catch (Exception) {
              RunOnUiThread(()=> Toast.MakeText(this, "Error al reproducir", ToastLength.Long).Show());
            }
        }

       


        public void animar(View imagen)
        {

            RunOnUiThread(() =>
            {
                imagen.SetLayerType(LayerType.Hardware, null);
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
                animacion.SetDuration(300);
                animacion.Start();
                Android.Animation.ObjectAnimator animacion2 = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleY", 0.5f, 1f);
                animacion2.SetDuration(300);
                animacion2.Start();

                animacion.AnimationEnd += delegate
                {
                    imagen.SetLayerType(LayerType.None, null);
                };
            });

          

        }
        public void animar2(View imagen)
        {
            RunOnUiThread(() =>
            {
                imagen.SetLayerType(LayerType.Hardware, null);
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(700);
            animacion.Start();
                animacion.AnimationEnd += delegate
                {
                    imagen.SetLayerType(LayerType.None, null);
                };
            });

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
        public void actualizarmediasesion() {

            while (true) {
                try
                {
                    br = new broadcast_receiver();
                    IntentFilter filtro = new IntentFilter(Intent.ActionMediaButton);
                    filtro.Priority = 1000;
                    if (instancia != null)
                    {

                        if (br != null) {
                            UnregisterReceiver(br);
                        }
                          
                  

                        RegisterReceiver(br, filtro);
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
                catch (Exception) {
                }
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
        public void animarycerrar(Java.Lang.Object imagen)
        {
            RunOnUiThread(() =>
            {
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", 0, -1000);
            animacion.SetDuration(300);
            animacion.Start();
            animacion.AnimationEnd += delegate
            {
              
                abrirmenu.Visibility = ViewStates.Visible;
                sidemenu.Visibility = ViewStates.Invisible;
               
            };
            });
        }
    }
}