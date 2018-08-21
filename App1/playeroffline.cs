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
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState = true)]
    public class playeroffline : Activity,ISurfaceHolderCallback
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
        public List<string> portadases = new List<string>();
        public List<string> patheses = new List<string>();
        public List<Android.Graphics.Bitmap> imageneses = new List<Bitmap>();
        public Dictionary<string ,int > diccionario = new Dictionary<string , int>();
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
        ProgressDialog dialogoprogreso;
        public ImageView fondo;
        public long millis = 0;
        ImageView entrarenmodovideo;
        public string postcachepath = "";
        broadcast_receiver br;
        public int postcachepos = 0;
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
            ////////////////////////////////////////////////miselaneo////////////////////////////////
            imagenvideomusica.SetBackgroundResource(Resource.Drawable.videoplayer);
            barraariba.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            barraabajo.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            sidemenu.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
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
            new Thread(() =>
            {
                counterenzero();
            }).Start();

         
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
            entrarenmodovideo.Click += delegate
            {
                animar(entrarenmodovideo);
                if (video.Visibility == ViewStates.Visible)
                {
                    lista.Visibility = ViewStates.Visible;
                    video.Visibility = ViewStates.Gone;
                    entrarenmodovideo.SetBackgroundResource(Resource.Drawable.videoplayer);


                }
                else
                {
                    lista.Visibility = ViewStates.Invisible;
                    video.Visibility = ViewStates.Visible;
                    if (!videoenholder)
                    {
                        holder.AddCallback(this);
                    }
                    entrarenmodovideo.SetBackgroundResource(Resource.Drawable.list);
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
                filter.Text = "";
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
                        barraariba.Visibility = ViewStates.Invisible;
                        barraabajo.Visibility = ViewStates.Invisible;
                        sidemenu.Visibility = ViewStates.Invisible;
                        abrirmenu.Visibility = ViewStates.Visible;
                        videofullscreen = true;
                    }
                    else
                    {
                        video.Visibility = ViewStates.Visible;
                        barraariba.Visibility = ViewStates.Visible;
                        barraabajo.Visibility = ViewStates.Visible;
                        videofullscreen = false ;
                    }
                    

                }
            };
            portada.Click += delegate
            {
                animar(portada);
                filter.Text = "";
                var adaptadol2 = new adapterlistaoffline(this, nombreses.Where(a => a.ToLower().Contains(filter.Text.ToLower())).ToList(), portadases, "", nombreses, diccionario, patheses);

                RunOnUiThread(() => lista.Adapter = adaptadol2);
                if (filter.Text.Length >= 1)
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
                entrarenmodovideo.SetBackgroundResource(Resource.Drawable.videoplayer);
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
                {

                    filter.Text = "";
                    if (!showingvideosresults)
                {
                        var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                        RunOnUiThread(() => lista.Adapter = adaptadolo);
                        animar2(lista);
                    imagenvideomusica.SetBackgroundResource(Resource.Drawable.musicalnote);
                        textovideomusica.Text = "Musica";
                    showingvideosresults = true;
                    indiceactual = -1;
                    cargarvideos();
 
                }else
                {
                        var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                        RunOnUiThread(() => lista.Adapter = adaptadolo);
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

              var vii=  aa.View.FindViewById<TextView>(Resource.Id.textView1);
               
                reproducir(nombreses.IndexOf(vii.Text),false);
            };
            filter.TextChanged += delegate
            {

                var adaptadol2 = new adapterlistaoffline(this, nombreses.Where(a => a.ToLower().Contains(filter.Text.ToLower())).ToList(), portadases, "",nombreses,diccionario,patheses);
               
                    RunOnUiThread(() => lista.Adapter = adaptadol2);
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
            var adaptadol2 = new adapterlistaoffline(this, nombreses.Where(a => a.ToLower().Contains(filter.Text.ToLower())).ToList(), portadases, "", nombreses, diccionario, patheses);
          
            RunOnUiThread(() => lista.Adapter = adaptadol2);
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
            portadases.Clear();
            patheses.Clear();
            var listaordenado = new List<string>();
            string todomezclado = "";
       if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d")) { 
             todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
       
             listaordenado = todomezclado.Split('¤').ToList();
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
                    nombreses.Add(ax.Split('²')[0]);
                    portadases.Add(ax.Split('²')[1]);
                    patheses.Add(ax.Split('²')[2]);
                }

            }
            }
            if (!sincronized)
            {
                RunOnUiThread(() =>
                {
                    dialogoprogreso = new ProgressDialog(this);
                    dialogoprogreso.SetCanceledOnTouchOutside(false);
                    dialogoprogreso.SetCancelable(false);
                    dialogoprogreso.SetTitle("Cargando...");
                    dialogoprogreso.SetMessage("Por favor espere");
                    dialogoprogreso.Show();
                });
                sync();
            }      
            comprobarpatheses();
          
           
            var adaptadol = new adapterlistaoffline(this, nombreses, portadases, "", nombreses,diccionario,patheses);
            RunOnUiThread(() => lista.Adapter = adaptadol);
            if (nombreses.Count == 0)
            {
                var adaptadolllll = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                RunOnUiThread(() => lista.Adapter = adaptadolllll);
            }
            listaordenado = new List<string>();
            todomezclado = "";
            clasesettings.recogerbasura();
        }
        public void cargarvideos()
        {
            nombreses.Clear();
            portadases.Clear();
            patheses.Clear();
            string todomezclado = "";
            var listaordenado = new List<string>();
            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
            {
                 todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");


                listaordenado = todomezclado.Split('¤').ToList();
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
                        nombreses.Add(ax.Split('²')[0]);
                        portadases.Add(ax.Split('²')[1]);
                        patheses.Add(ax.Split('²')[2]);
                    }

                }
            }
            if (!sincronized)
            {
                RunOnUiThread(() =>
                {
                    dialogoprogreso = new ProgressDialog(this);
                    dialogoprogreso.SetCanceledOnTouchOutside(false);
                    dialogoprogreso.SetCancelable(false);
                    dialogoprogreso.SetTitle("Cargando...");
                    dialogoprogreso.SetMessage("Por favor espere");
                    dialogoprogreso.Show();
                });
                sync();
            }
            comprobarpatheses();
          
            var adaptadol = new adapterlistaoffline(this, nombreses, portadases, "", nombreses,diccionario,patheses);
            RunOnUiThread(() => lista.Adapter = adaptadol);
            if (nombreses.Count == 0)
            {
                var adaptadolllll = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                RunOnUiThread(() => lista.Adapter = adaptadolllll);
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
           
            clasesettings.preguntarsimenuosalir(this);
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
            UnregisterReceiver(br);
            instancia = null;
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

                }
                }
                // clasesettings.guardarsetting("tapnumber", "0");
                Thread.Sleep(1000);
            }

        }
        public void counterenzero()
        {
           /*while (!detenedor)
            {
               counter = 0;
                Thread.Sleep(800);
            }*/
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
                    listanewlink.Add(portadases[i]);
                  
                }
                i++;
            }
            nombreses = new List<string>(listanewname);
            portadases = new List<string>(listanewlink);
            patheses = new List<string>(listanewpath);
         

           
            var adaptadol = new adapterlistaoffline(this, nombreses, portadases, "",nombreses,diccionario,patheses);
            RunOnUiThread(() => lista.Adapter = adaptadol);
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
            imageneses.Clear();

            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d"))
            {
                string todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");

                var listaordenado = todomezclado.Split('¤').ToList();
                if (listaordenado[listaordenado.Count - 1].Trim() == "")
                {
                    listaordenado.RemoveAt(listaordenado.Count - 1);
                }


                foreach (string ax in listaordenado)
                {
                    if (ax.Trim().Length > 1)
                    {
                        fullnombre.Add(ax.Split('²')[0]);
                        fullportadas.Add(ax.Split('²')[1]);
                        fullpaths.Add(ax.Split('²')[2]);
                    }

                }
                listaordenado = new List<string>();
                todomezclado = "";

            }
            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
            {
                string todomezclado = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");

                var listaordenado = todomezclado.Split('¤').ToList();
                if (listaordenado[listaordenado.Count - 1].Trim() == "")
                {
                    listaordenado.RemoveAt(listaordenado.Count - 1);
                }


                foreach (string ax in listaordenado)
                {
                    if (ax.Trim().Length > 1)
                    {
                        fullnombre.Add(ax.Split('²')[0]);
                        fullportadas.Add(ax.Split('²')[1]);
                        fullpaths.Add(ax.Split('²')[2]);
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
                        using (var webbrowser = new WebClient())
                        {
                            webbrowser.DownloadFile(new Uri("https://i.ytimg.com/vi/" + fullportadas[i].Split('=')[1] + "/mqdefault.jpg"), Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + fullportadas[i].Split('=')[1]);
                        }
                    }
                }
                catch (Exception)
                {

                }
                using (Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + fullportadas[i].Split('=')[1]))
                {


                    imageneses.Add(getRoundedShape(imagen, i));


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
        public static Bitmap scaleDown(Bitmap realImage)
        {

            Bitmap newBitmap = Bitmap.CreateScaledBitmap(realImage, 250, 250, false);
                  
            return newBitmap;
        }


        public Bitmap getRoundedShape(Bitmap scaleBitmapImage,int pos)
        {

            try
            {
                int targetWidth = 90;
                int targetHeight = 90;
                Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                            targetHeight, Bitmap.Config.Argb4444);


                using (Canvas canvas = new Canvas(targetBitmap))
                {


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
                }
                if (!diccionario.ContainsKey(fullnombre[pos]))
                {
                    diccionario.Add(fullnombre[pos], targetBitmap.GenerationId);
                }

             
                return targetBitmap;

              
            }
            catch (Exception e)
            {
              
                var sddd = e;
                if (!diccionario.ContainsKey(fullnombre[pos]))
                {
                    diccionario.Add(fullnombre[pos],0);
                }
              
                return scaleBitmapImage;
            }
          
        }









        public Bitmap getRoundedShape2(Bitmap scaleBitmapImage, int pos)
        {

            try
            {
                int targetWidth = 90;
                int targetHeight = 90;
                Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                            targetHeight, Bitmap.Config.Argb4444);


                using (Canvas canvas = new Canvas(targetBitmap))
                {


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
                }
             


                return targetBitmap;


            }
            catch (Exception e)
            {

                var sddd = e;
               

                return scaleBitmapImage;
            }

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
                        RunOnUiThread(() => entrarenmodovideo.SetBackgroundResource(Resource.Drawable.videoplayer));
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
                    Clouding_serviceoffline.gettearinstancia().linkactual = Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + portadases[indice].Split('=')[1];
                    RunOnUiThread(() => {
                        try
                        {
                            portada.SetImageBitmap(imageneses.First(info => info.GenerationId == diccionario[nombreses[indice]]));
                        }
                        catch (Exception) {

                        }
                      
                        
                        });
                    animar4(portada);

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {
                        RunOnUiThread(() => fondo.DestroyDrawingCache());
                        //  RunOnUiThread(() => fondo = FindViewById<ImageView>(Resource.Id.fondo1));
                        RunOnUiThread(() => fondo.SetImageBitmap(clasesettings.CreateBlurredImageoffline(this, 20, portadases[indiceactual])));
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
                Toast.MakeText(this, "error al reproducir", ToastLength.Long).Show();
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
                catch (Exception) {
                }
            }
             
        

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