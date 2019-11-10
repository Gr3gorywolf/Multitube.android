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
using Android.Webkit;
using System.Collections.Generic;
using Android.Graphics;
using System.Net;
using Android.Speech;
using System.Net.Http;
using Android.Renderscripts;
using System.IO;
using Android.Graphics.Drawables;
using System.Linq;
using Android.Media.Session;
using Android.Support.V7.App;
using Android.Support.V4;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Newtonsoft.Json;
using Android.Glide;
using YoutubeSearch;
using System.Text.RegularExpressions;
using App1.Models;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize,  Theme = "@style/Theme.DesignDemo", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState = true)]
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
    public class Mainmenu : Android.Support.V7.App.AppCompatActivity
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
    {
        public bool nocomprobar = false;
        public TcpClient clientela;
        public bool detenedor = true;
        public ListView lista;
       public adapterlistaremoto adaptadol;
        public ImageView caratula2;
        public TextView label;
        public RelativeLayout lineall;
        public string zelda;
        public string colol = "none";
        public bool agregando = false;
        public int voz = 9;
        public EditText textbox;
        public LinearLayout lineall2;
        public MediaSession mSession;
        public string ip = "";
      public  List<string> lista2;
     public   List<string> listalinks;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        Thread actualizarlista;
        ScrollView menuham;
        public static Mainmenu instancia;
        public ImageView fondo;
        public Bitmap fondoblurreado;
        public bool buscando = false;
        public bool reproduciendo = false;
        public  ImageView atras;
        public ImageView adelante;
        public  ImageView play;
        public  ImageView adelantar;
        public  ImageView atrazar;
        public  ImageView fullscreen;
        public  ImageView download;
        public  ImageView voldown;
        public  ImageView volup;
        public ProgressBar barrap;
        public IpData modeloip;
        DrawerLayout sidem;
        NavigationView itemsm;
        ImageView botonaccion;
        public string devicename = "";
        public string jsonlistacustom = "";
        public string jsonlistasremotas = "";
        public TcpClient clientelalistas;
        public bool playlistreceived = false;
        public Dictionary<string, PlaylistElement> listafavoritos = new Dictionary<string, PlaylistElement>();
        public bool compatible = true;
        public int volact = 0;
        Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout panel;
        public History objetohistorial = new History();
        ImageView botonlike;
        public List<YoutubeSearch.VideoInformation> sugerencias = new List<YoutubeSearch.VideoInformation>();
        PowerManager.WakeLock wake;
        public static Mainmenu gettearinstancia()
        {
            return instancia;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.perfectmain4);
            PowerManager manejador = (PowerManager)GetSystemService(PowerService);
             wake = manejador.NewWakeLock(WakeLockFlags.Partial, "MyApp::MyWakelockTag");
            wake.Acquire();



            clientela = new TcpClient();
            clientelalistas = new TcpClient();
            try
            {

                ip = Intent.GetStringExtra("MyData") ?? "Data not available";

                clientela.Client.Connect(ip.Trim(), 1024);
               
            }

            catch (SocketException)
            {

            }

          

             var resultado=   clientelalistas.Client.BeginConnect(ip.Trim(), 9856,null,null);
                resultado.AsyncWaitHandle.WaitOne(1000);
            if (!clientelalistas.Client.Connected)
                compatible = false;
          
            ///////////////////////////////#Botones#////////////////////////////////
            menuham = FindViewById<ScrollView>(Resource.Id.linearLayout9);
            ImageView botonabrirmenu = FindViewById<ImageView>(Resource.Id.imageView22);
            TextView estadomenu = FindViewById<TextView>(Resource.Id.textView9);
           botonaccion = FindViewById<ImageView>(Resource.Id.btnaccion);
            LinearLayout layoutvolumen = FindViewById<LinearLayout>(Resource.Id.linearLayout12);
            var botonbusqueda = FindViewById<ImageView>(Resource.Id.imageView20);
            ImageView botonsincronizar= FindViewById<ImageView>(Resource.Id.imageView17);
            ImageView listareprod = FindViewById<ImageView>(Resource.Id.imageView16);
           atras = FindViewById<ImageView>(Resource.Id.imageView2);
     adelante = FindViewById<ImageView>(Resource.Id.imageView4);
          play = FindViewById<ImageView>(Resource.Id.imageView3);
          adelantar = FindViewById<ImageView>(Resource.Id.imageView5);
          atrazar = FindViewById<ImageView>(Resource.Id.imageView7);
           fullscreen = FindViewById<ImageView>(Resource.Id.imageView6);
         download = FindViewById<ImageView>(Resource.Id.imageView8);
         voldown = FindViewById<ImageView>(Resource.Id.imageView9);
       volup = FindViewById<ImageView>(Resource.Id.imageView10);
            ImageView agregar = FindViewById<ImageView>(Resource.Id.imageView11);
            ImageView escuchar = FindViewById<ImageView>(Resource.Id.imageView15);
           lineall2=FindViewById< LinearLayout > (Resource.Id.linearLayout7);
            ImageView buscar = FindViewById<ImageView>(Resource.Id.imageView12);
           textbox = FindViewById<EditText>(Resource.Id.editText1);
            ImageView abrirbrowser = FindViewById<ImageView>(Resource.Id.imageView14);
            // lineall= FindViewById<RelativeLayout>(Resource.Id.linearLayout0);
             label = FindViewById < TextView>(Resource.Id.textView1);
            caratula2 = FindViewById<ImageView>(Resource.Id.imageView13);
            lista = FindViewById<ListView>(Resource.Id.listView1);
            var ll10 = FindViewById<LinearLayout>(Resource.Id.lldownload);
            var ll11 = FindViewById<LinearLayout>(Resource.Id.linearLayout11);
            var ll12 = FindViewById<LinearLayout>(Resource.Id.lllistas);          
            var ll14 = FindViewById<LinearLayout>(Resource.Id.linearLayout14);
            var ll16 = FindViewById<LinearLayout>(Resource.Id.linearLayout16);
            fondo = FindViewById<ImageView>(Resource.Id.imageView45);
            var barra = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            var barra2 = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            var barra4 = FindViewById<Android.Support.V7.Widget.CardView>(Resource.Id.linearLayout6);
            sidem = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
            itemsm = FindViewById<NavigationView>(Resource.Id.content_frame);
            barrap = FindViewById<ProgressBar>(Resource.Id.progresoind);
            var searchview = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);
            modeloip = SocketHelper.GetIps();
            botonlike = FindViewById<ImageView>(Resource.Id.imglike);
            panel = FindViewById<Cheesebaron.SlidingUpPanel.SlidingUpPanelLayout>(Resource.Id.sliding_layout);
            //  var barra3 = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            ////////////////////////////////////////////////////////////////////////

            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Control remoto";
           // SupportActionBar.SetBackgroundDrawable(new ColorDrawable(Color.ParseColor("#2b2e30")));
            lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
            animar2(lineall2);
            layoutvolumen.Visibility = ViewStates.Invisible;
            menuham.Visibility = ViewStates.Invisible;
            estadomenu.Text = "";
            botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
            instancia = this;
            label.Selected = true;


            panel.IsUsingDragViewTouchEvents = true;
            panel.DragView = FindViewById<RelativeLayout>(Resource.Id.scrollable);
            var solapa = FindViewById<Android.Support.V7.Widget.CardView>(Resource.Id.solapita);
            solapa.Click += delegate
            {

                if (panel.IsExpanded)
                    panel.CollapsePane();
                else
                    panel.ExpandPane();
            };

            botonaccion.SetBackgroundResource(Resource.Drawable.playpause2);
            // layoutvolumen.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            //  barra.SetBackgroundColor(Color.ParseColor("#2b2e30"));
            //  barra2.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            //  barra3.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            //   barra4.SetBackgroundColor(Color.ParseColor("#2b2e30"));
            //  menuham.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            layoutvolumen.BringToFront();
            layoutvolumen.BringToFront();
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => {
                var parcelable = lista.OnSaveInstanceState();
                lista.Adapter = adaptadol;
                lista.OnRestoreInstanceState(parcelable);
            });

        


            new Thread(() =>
            {
                iniciarservicio();
                Thread.Sleep(500);
                actualizarlista = new Thread(new ThreadStart(cojerstream));
                actualizarlista.Start();
            }).Start();
            if (compatible) {
                new Thread(() =>
                {
                    cojerlistas();

                }).Start();
            }
           
            WallpaperManager wm = WallpaperManager.GetInstance(this);
            Drawable d = wm.Drawable;
            fondoblurreado = clasesettings.CreateBlurredImageformbitmap(this, 20, ((BitmapDrawable)d).Bitmap);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // fondo.SetImageBitmap(fondoblurreado);
            RunOnUiThread(() => fondo.SetBackgroundColor(Color.ParseColor("#323538")));

         
                if (File.Exists(Constants.CachePath + "/history.json"))
                {

                    objetohistorial = JsonConvert.DeserializeObject<History>(File.ReadAllText(Constants.CachePath + "/history.json"));

            }
            else
            {

                objetohistorial = new History();
                objetohistorial.Videos = new List<PlaylistElement>();
                objetohistorial.Links = new Dictionary<string, int>();
            }
            if (File.Exists(Constants.CachePath + "/favourites.json"))
            {
                listafavoritos = JsonConvert.DeserializeObject<Dictionary<string, PlaylistElement>>(File.ReadAllText(Constants.CachePath + "/favourites.json"));
            }

            StartActivity(new Intent( this, typeof(actividadinicio)));
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

               
                if (zelda.Trim() != "")
                {
                    var link = zelda.Replace("https", "http");
                    var elemento = new PlaylistElement()
                    {
                        Link = link,
                        Name = label.Text
                    };
                    listafavoritos = clasesettings.agregarfavoritos(this, listafavoritos, elemento);

                    if (!listafavoritos.ContainsKey(link))
                        botonlike.SetBackgroundResource(Resource.Drawable.heartoutline);
                    else
                        botonlike.SetBackgroundResource(Resource.Drawable.heartcomplete);

                }
            };
            panel.PanelExpanded += delegate
            {

                if (volact == 0)
                botonaccion.SetBackgroundResource(Resource.Drawable.volumelow);
                else
                if(volact==50)
                    botonaccion.SetBackgroundResource(Resource.Drawable.volumemedium);
                else
                if(volact==100)
                    botonaccion.SetBackgroundResource(Resource.Drawable.volumehigh);
            };
            panel.PanelCollapsed += delegate
            {
                botonaccion.SetBackgroundResource(Resource.Drawable.playpause2);

            };
            searchview.QueryTextSubmit += delegate
            {

                if (searchview.Query.Trim().Length > 3)
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


            itemsm.NavigationItemSelected += (sender, e) => {
                //  e.MenuItem.SetChecked(true);

                //react to click here and swap fragments or navigate
                e.MenuItem.SetChecked(true);
                e.MenuItem.SetChecked(true);



                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Descargar")
                {
                    if (zelda != "" )
                    {

                        Intent internado = new Intent(this, typeof(actdownloadcenter));



                        internado.PutExtra("zelda", zelda);
                        internado.PutExtra("ip", ip);
                        internado.PutExtra("color", colol);
                        StartActivity(internado);

                        animarycerrar(menuham);
                        botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                        estadomenu.Text = "";




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
                    intento.PutExtra("ipadre", ip);
                    intento.PutExtra("color", colol);
                    StartActivity(intento);
                    animarycerrar(menuham);
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";
                }
                else
                    if (e.MenuItem.TitleFormatted.ToString().Trim() == "Listas de reproduccion")
                {
                    if (compatible)
                    {
                        if (jsonlistasremotas.Trim() != "")
                        {
                            Intent intentoo = new Intent(this, typeof(Reproducirlistaact));
                            intentoo.PutExtra("ip", ip);
                            StartActivity(intentoo);
                            animarycerrar(menuham);
                            botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                            estadomenu.Text = "";
                        }
                        else
                        {
                            Toast.MakeText(this, "Aun no se han recibido las listas de el servidor...", ToastLength.Long).Show();
                        }
                    }
                    else {
                        Toast.MakeText(this, "Su servidor no es compatible con esta version", ToastLength.Long).Show();
                    }

                }
          
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Sincronizar listas")
                {
                    Intent intento = new Intent(this, typeof(actividadsincronizacion));
                    intento.PutExtra("ipadre", ip);

                    StartActivity(intento);
                    animarycerrar(menuham);
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";

                }
                else
                if (e.MenuItem.TitleFormatted.ToString().Trim() == "Busqueda rapida")
                {

                    Intent intento = new Intent(this, typeof(actfastsearcher));
                    intento.PutExtra("ipadres", ip);
                    StartActivity(intento);
                    animarycerrar(menuham);
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
                    else
                    {

                        actividadinicio.gettearinstancia().Finish();
                        Intent intento = new Intent(this, typeof(actividadinicio));

                        StartActivity(intento);
                    }
                    OverridePendingTransition(0, 0);
                }
                e.MenuItem.SetChecked(false);
                e.MenuItem.SetChecked(false);
                sidem.CloseDrawers();

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
           
            ll14.Click += delegate
            {
                animar(ll14);
                botonsincronizar.PerformClick();
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
                animar(botonabrirmenu);  
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
            botonaccion.Click += delegate
            {

                /* if (layoutvolumen.Visibility == ViewStates.Visible)
                 {
                     layoutvolumen.Visibility = ViewStates.Invisible;
                 }
                 else
                 {

                     layoutvolumen.BringToFront();
                     animar2(layoutvolumen);
                     layoutvolumen.Visibility = ViewStates.Visible;
                 }*/
                animar(botonaccion);
                if (panel.IsExpanded)
                {
                    Intent intentoo = new Intent(this, typeof(actmenuvolumen));
                    intentoo.PutExtra("ipadre", ip);
                    StartActivity(intentoo);
                }
                else {
                    play.PerformClick();
                }

            };
            botonbusqueda.Click += delegate
            {
              
                Intent intento = new Intent(this, typeof(actfastsearcher));
                intento.PutExtra("ipadres",ip);
                StartActivity(intento);
                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";
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
                StartActivityForResult(voiceIntent,voz);
                }
                else
                {
                    Toast.MakeText(this, "ningun microfono detectado", ToastLength.Long).Show();
                };

            };

            botonsincronizar.Click += delegate
            {
                Intent intento = new Intent(this, typeof(actividadsincronizacion));
                intento.PutExtra("ipadre",ip);

                StartActivity(intento);
                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";
            };

            lista.ItemClick += (sender, easter) =>
            {
                if (lista.Count > 0 && listalinks.Count>0)
                {

                    Intent intento = new Intent(this, typeof(deletedialogact));
                    string perfectitulo = lista2[easter.Position];
                  perfectitulo=  perfectitulo.Replace('>', ' ');
                 perfectitulo=   perfectitulo.Replace('<', ' ');
                    intento.PutExtra("index", easter.Position.ToString());
                    intento.PutExtra("color", colol);
                    intento.PutExtra("titulo", perfectitulo);
                    intento.PutExtra("ipadress", ip);
                    intento.PutExtra("url", listalinks[easter.Position]);
                    intento.PutExtra("imagen", @"https://i.ytimg.com/vi/" + listalinks[easter.Position].Split('=')[1] + "/hqdefault.jpg");
                    StartActivity(intento);
                }
            };
            lista.Scroll += (sender, easter) =>
            {
               
            };


            atras.Click += (sender, easter) =>
        {
            animar(atras);
            clientela.Client.Send(Encoding.ASCII.GetBytes("back()"));
        };
            abrirbrowser.Click += (sender, easter) =>
            {
              
                Intent intento = new Intent(this, typeof(customsearcheract));
                intento.PutExtra("ipadre", ip);
                intento.PutExtra("color", colol);
                StartActivity(intento);
                animarycerrar(menuham);
                botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                estadomenu.Text = "";
            };
            listareprod.Click += delegate
                  {
                     
                      Intent intentoo = new Intent(this, typeof(Reproducirlistaact));
                      intentoo.PutExtra("ip", ip);
                      StartActivity(intentoo);
                      animarycerrar(menuham);
                      botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                      estadomenu.Text = "";


                  };
            adelante.Click += (sender, easter) =>
             {
                 animar(adelante);
                 clientela.Client.Send(Encoding.ASCII.GetBytes("next()"));
             };
            play.Click += (sender, easter) =>
              {
                  animar(play);
                  clientela.Client.Send(Encoding.ASCII.GetBytes("playpause()"));
              };
            adelantar.Click += (sender, easter) =>
              {
                  animar(adelantar);
                  clientela.Client.Send(Encoding.ASCII.GetBytes("actual+()"));
              };
            atrazar.Click += (sender, easter) =>
            {
                animar(atrazar);
                clientela.Client.Send(Encoding.ASCII.GetBytes("actual-()"));
            };
            fullscreen.Click += (sender, easter) =>
            {
                animar(fullscreen);
                clientela.Client.Send(Encoding.ASCII.GetBytes("fullscreen()"));
            };
            download.Click += (sender, easter) =>
            {
                animar(download);
                if (zelda != "" && colol!="none") {
                 
                    Intent internado = new Intent(this, typeof(actdownloadcenter));
                  
                  
                  
                    internado.PutExtra("zelda", zelda);
                internado.PutExtra("ip", ip);
                    internado.PutExtra("color", colol);
                    StartActivity(internado);

                    animarycerrar(menuham);
                    botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
                    estadomenu.Text = "";




                }
                else
                {
                    Toast.MakeText(this, "Reproduzca un video para descargar", ToastLength.Long).Show();
                }
            };
          
        voldown.Click += (sender, easter) =>
            {
                animar(voldown);
                clientela.Client.Send(Encoding.ASCII.GetBytes("vol-()"));
            };
            volup.Click += (sender, easter) =>
            {
                animar(volup);
                clientela.Client.Send(Encoding.ASCII.GetBytes("vol+()"));
            };
            agregar.Click += (sender, easter) =>
            {
                animar(agregar);
                if (agregando == false) {
                    agregando = true;

                    if(getearurl(textbox.Text.Trim() ) == "%%si%%" &&textbox.Text.Trim().Length>=3){



                clientela.Client.Send(Encoding.ASCII.GetBytes("agregar()"));
               
             
                clientela.Client.Send(Encoding.ASCII.GetBytes(textbox.Text.Trim()));
                textbox.Text = "";
                    }
                    else
                if (textbox.Text.Trim().Length < 3)
                    {
                        Toast.MakeText(this, "La busqueda debe contener almenos 3 caracteres", ToastLength.Long).Show();
                    }
                    agregando = false;
                }


            };
            buscar.Click += (sender, easter) =>
            {
                animar(buscar);
              //  detenedor = false;
                if (getearurl(textbox.Text.Trim()) == "%%si%%" && textbox.Text.Trim().Length > 3)
                {
                    clientela.Client.Send(Encoding.ASCII.GetBytes(textbox.Text.Trim()));
                    textbox.Text = "";
                }
                else
                if( textbox.Text.Trim().Length <= 3)
                {
                    Toast.MakeText(this, "La busqueda debe contener almenos 3 caracteres", ToastLength.Long).Show();
                }
              //  detenedor = true;
            };


        
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////

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
        public void iniciarservicio()
        {
            StopService(new Intent(this, typeof(cloudingserviceonline)));
            StopService(new Intent(this, typeof(Clouding_serviceoffline)));
            StopService(new Intent(this, typeof(Clouding_service)));
            StopService(new Intent(this, typeof(serviciodownload)));
         StartService(new Intent(this, typeof(cloudingserviceonline)));
           
           // StartService(new Intent(this, typeof(serviciodownload)));
        }

        public string getearurl(string titttt)
        {

            try
            {
                HttpClient cliente = new HttpClient(new ModernHttpClient.NativeMessageHandler());
                var retorno = cliente.GetStringAsync("https://decapi.me/youtube/videoid?search=" + titttt);
                string prra = retorno.Result;
                string linkkk = "http://www.youtube.com/watch?v=" + retorno.Result;

                if (retorno.Result.ToLower().Trim() == "invalid video id or search string.")
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
      public bool estasucio(string querry)
        {
            if (querry.Contains("caratula()"))
            {
                return true;
            }else
                  if (querry.Contains("links()"))
            {
                return true;
            }
            else
                  if (querry.Trim()=="")
            {
                return true;
            }
            else
                  if (querry.Contains("listar()"))
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }

        public void cojerlistas()
        {



            RunOnUiThread(() =>
            {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso.SetTitle("Sincronizando listas de reproduccion");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.Create();
                dialogoprogreso.Show();

            });


            byte[] bites = new byte[5000000];
            int byteslenght = 0;
    
            string datacompleta = "";
           /// clientelalistas.Client.Send(Encoding.UTF8.GetBytes("Getplaylist__==__==__none"));





            while (detenedor)
            { 
           

          


               
                var stream = clientelalistas.GetStream();
                while (stream.DataAvailable)
                {
                    byteslenght = stream.Read(bites, 0, bites.Length);
                    datacompleta += System.Text.Encoding.UTF8.GetString(bites, 0, byteslenght);
                }


            
             
                if (datacompleta.Trim().Length > 0 && datacompleta.Trim()!="" )
                {

                    if (!datacompleta.Trim().Contains("__==__==__")) {
                        RunOnUiThread(() =>
                        {
                            dialogoprogreso.Dismiss();
                        });
                        jsonlistasremotas = datacompleta;
                     
                     
                    }
                     
                    else {
                        dialogoprogreso.Dismiss();
                        jsonlistacustom = datacompleta.Split(new[] { "__==__==__" }, StringSplitOptions.None)[1];
                        playlistreceived = true;
                    }

                }







                datacompleta = "";
                Thread.Sleep(1000);
            }

        }
        public void cojerstream()
        {
            
           lista2 = new List<string>();
            listalinks= new List<string>();
            string JSONcaratula = "";
            string JSONlinks = "";
            string JSONtitles = "";

            bool cojioto = false;
            byte[] bites = new byte[1000000];
            bool enviomensaje = false;
            var stream = clientela.GetStream();

            string capturado = "";


            int o = 0;
            while (detenedor == true && clientela.Connected == true)
            {
                if (enviomensaje == false)
                {
                    clientela.Client.Send(Encoding.Default.GetBytes("notificar()"));
                    
                    clientela.Client.Send(Encoding.Default.GetBytes("recall()"));
                    enviomensaje = true;
               new Thread(() =>
                    {
                        testconnection();

                    }).Start();
                }
            

              

              

             

                while (stream.DataAvailable)
                {




                    o = stream.Read(bites, 0, bites.Length);


                    capturado += Encoding.UTF8.GetString(bites, 0, o);
                    cojioto = true;
                }
                if (cojioto && capturado.Trim() != "")
                {





                    JSONtitles = capturado.Split(new[] { "[%%TITLES%%]" },StringSplitOptions.None )[1];
                    JSONlinks= capturado.Split(new[] { "[%%LINKS%%]" }, StringSplitOptions.None)[1].Split(new[] { "[%%TITLES%%]" }, StringSplitOptions.None)[0];
                    JSONcaratula= capturado.Split(new[] { "[%%CARATULA%%]" }, StringSplitOptions.None)[1].Split(new[] { "[%%LINKS%%]" }, StringSplitOptions.None)[0];
                    //          listicalinks = divisor[1].Split(';');




                    if (JSONcaratula.Trim() != "")
                    {

                        var listaelementos = JsonConvert.DeserializeObject<string[]>(JSONcaratula).ToList();
                        zelda = listaelementos[2];

                        if (label.Text.Trim() != listaelementos[1]) 
                            RunOnUiThread(() => label.Text = listaelementos[1]);
                       
                       

                        buscando = bool.Parse(listaelementos[4]);
                        if (modeloip.Ips.ContainsKey(ip)) { 
                        if (modeloip.Ips[ip] != listaelementos[5]) {
                            modeloip.Ips[ip] = listaelementos[5];
                             
                                var jsons = JsonConvert.SerializeObject(modeloip);
                                var axc = File.CreateText(Constants.CachePath + "/ips.json");
                                axc.Write(jsons);
                                axc.Close();
                            }
                            


                        }
                        devicename = listaelementos[5];
                        RunOnUiThread(() =>
                        {
                            if (buscando)
                                barrap.Visibility = ViewStates.Visible;
                            else
                                barrap.Visibility = ViewStates.Gone;

                           
                        });


                      
                            if (listaelementos[3].Trim() == "vol0()")
                            {
                                if(panel.IsExpanded)
                                RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.volumelowrojo));

                                volact = 0;
                            }

                            else
                        if (listaelementos[3].Trim() == "vol50()")
                            {
                               if (panel.IsExpanded)
                                RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.volumemediumrojo));
                                volact = 50;
                            }

                            else
                        if (listaelementos[3].Trim() == "vol100()") {
                                if (panel.IsExpanded)
                                RunOnUiThread(() => botonaccion.SetBackgroundResource(Resource.Drawable.volumehighrojo));
                                volact = 100;
                            }
                            
                     
                        RunOnUiThread(() => {
                        if (!listafavoritos.ContainsKey(zelda.Replace("https", "http")))
                            botonlike.SetBackgroundResource(Resource.Drawable.heartoutline);
                        else
                            botonlike.SetBackgroundResource(Resource.Drawable.heartcomplete);
                        });
                        clasesettings.recogerbasura();
                        new Thread(() =>
                        {
                            if (listaelementos[0] != "")
                            {
                                ponerimagen(listaelementos[0], listaelementos[1]);

                            }

                        }).Start();



                    }
                    if (JSONlinks.Trim() != "" && JSONtitles.Trim()!="") {

                        listalinks.Clear();
                        listalinks = JsonConvert.DeserializeObject<string[]>(JSONlinks).ToList();
                       lista2.Clear();
                        lista2 = JsonConvert.DeserializeObject<string[]>(JSONtitles).ToList();

                        if ( listalinks.Count >= lista2.Count)
                        {
                            adaptadol = new adapterlistaremoto(this, lista2, listalinks, zelda);
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();
                                lista.Adapter = adaptadol;
                                lista.OnRestoreInstanceState(parcelable);
                            });
                        }
                        else {
                            RunOnUiThread(() =>
                            {
                                new Android.Support.V7.App.AlertDialog.Builder(this)
                                    .SetTitle("Se ha producido un error al cargar los elementos")
                                    .SetMessage("Desea recargarlos?")
                                    .SetPositiveButton("Si", (ax, sd) => { clientela.Client.Send(Encoding.UTF8.GetBytes("recall()")); })
                                    .SetNegativeButton("no", (sdf, dfd) => { })
                                    .Create()
                                    .Show();
                            });
                        }

                        clasesettings.recogerbasura();

                    }






                 
                }





                cojioto = false;

                bites = new byte[1000000];


              
                capturado = "";

                o = 0;
                Thread.Sleep(270);

            }
           
            this.Finish();
        }



        //  public override on
        protected override void OnDestroy()
        {
            nocomprobar = true;
            detenedor = false;
            clientela.Client.Disconnect(true);
            actualizarlista.Abort();
            clasesettings.recogerbasura();
            if (cloudingserviceonline.gettearinstancia() != null)
            {
                cloudingserviceonline.gettearinstancia().StopSelf();
            }
            wake.Release();
            base.OnDestroy();
        }
        public override void Finish()
        {
           
            base.Finish();
           
        }
       
        private void ponerimagen(string url,string titrr)
        {               
                            RunOnUiThread(() => {
                                fondo.SetBackgroundColor(Color.ParseColor("#323538"));

                               Glide.With(this).Load(url).Into(caratula2);
                             

                                Glide.With(this).Load(url).Into(FindViewById<ImageView>(Resource.Id.bgimg));




                            });
                          
                           
                            if (cloudingserviceonline.gettearinstancia() != null)
                            {
                            
                                cloudingserviceonline.gettearinstancia().ipactual = ip;
                                cloudingserviceonline.gettearinstancia().linkactual = url;
                                cloudingserviceonline.gettearinstancia().tituloactual = titrr; 
                                cloudingserviceonline.gettearinstancia().mostrarnotificacion();
                            }
            clasesettings.recogerbasura();

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

        public void testconnection()
        {
           
            while (true)
            {
                if (nocomprobar) {
                    break;
                }
                if (!SocketHelper.IsConnected(clientela) && !nocomprobar)
                {
                   
                    
                    this.Finish();
                    if (actividadinicio.gettearinstancia() != null)
                        actividadinicio.gettearinstancia().Finish();
                    StartActivity(typeof(actmenuprincipal));
                    break;
                }
                Thread.Sleep(2000);
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

                    ////   boton.Visibility = ViewStates.Visible;
                    menuham.Visibility = ViewStates.Invisible;

                };
            });
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
                    var listatitulos = results.Select(ax => WebUtility.HtmlDecode(RemoveIllegalPathCharacters(ax.Title.Replace("&quot;", "").Replace("&amp;", "")))).ToList();
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
                            intentoo.PutExtra("ipadress", ip);
                            intentoo.PutExtra("url", listalinks[posicion]);
                            intentoo.PutExtra("imagen", @"https://i.ytimg.com/vi/" + listalinks[posicion].Split('=')[1] + "/mqdefault.jpg");
                            StartActivity(intentoo);

                        };
                        adapterlistaremoto adapt = new adapterlistaremoto(this, listatitulos, listalinks);
                        lista.Adapter = adapt;

                        new Android.App.AlertDialog.Builder(this)
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



        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
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


    }









}
               


           
       
   

       



