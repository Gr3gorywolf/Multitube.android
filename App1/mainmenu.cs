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
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize,  Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState = true)]
    
    public class mainmenu : Activity
    {
      
        public TcpClient clientela;
        public bool detenedor = true;
        public ListView lista;
       public ArrayAdapter<string> adaptadol;
        public ImageView caratula2;
        public TextView label;
        public RelativeLayout lineall;
        public string zelda;
        public string colol = "none";
        public bool agregando = false;
        public int voz = 9;
        public EditText textbox;
        public LinearLayout lineall2;
      
        public string ip = "";
        List<string> lista2;
        List<string> listalinks;
        Thread actualizarlista;
        ScrollView menuham;
        public static mainmenu instancia;
        public ImageView fondo;
        public Bitmap fondoblurreado;

        public  ImageView atras;
        public ImageView adelante;
        public  ImageView play;
        public  ImageView adelantar;
        public  ImageView atrazar;
        public  ImageView fullscreen;
        public  ImageView download;
        public  ImageView voldown;
        public  ImageView volup;
        public static mainmenu gettearinstancia()
        {
            return instancia;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.perfectmain4);

        


            clientela = new TcpClient();
            try
            {

                ip = Intent.GetStringExtra("MyData") ?? "Data not available";

                clientela.Client.Connect(ip.Trim(), 1024);
            }

            catch (SocketException)
            {

            }
         actualizarlista = new Thread(new ThreadStart(cojerstream));
            actualizarlista.Start();
          
            ///////////////////////////////#Botones#////////////////////////////////
            menuham = FindViewById<ScrollView>(Resource.Id.linearLayout9);
            ImageView botonabrirmenu = FindViewById<ImageView>(Resource.Id.imageView22);
            TextView estadomenu = FindViewById<TextView>(Resource.Id.textView9);
            ImageView botonmostrarvolumen = FindViewById<ImageView>(Resource.Id.imageView21);
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
            var barra4 = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
          //  var barra3 = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            ////////////////////////////////////////////////////////////////////////
            lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
            animar2(lineall2);
            layoutvolumen.Visibility = ViewStates.Invisible;
            menuham.Visibility = ViewStates.Invisible;
            estadomenu.Text = "";
            botonabrirmenu.SetBackgroundResource(Resource.Drawable.menu);
            instancia = this;
            label.Selected = true;
            layoutvolumen.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            barra.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            barra2.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
          //  barra3.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            barra4.SetBackgroundColor(Color.ParseColor(clasesettings.gettearvalor("color")));
            menuham.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            layoutvolumen.BringToFront();
            layoutvolumen.BringToFront();
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => lista.Adapter = adaptadol);

            new Thread(() =>
            {
                iniciarservicio();
            }).Start();
            bool estacontenido = false;
            string todaslasip = clasesettings.gettearvalor("ips").Trim();
          
                try
                {
                
                    foreach(string e in todaslasip.Split('>'))
                    {
                        if(e.Trim()== Intent.GetStringExtra("MyData").Trim() )
                        {
                            estacontenido = true;
                        }
                    }


                }
                catch (Exception)
                {

                }
            if (!estacontenido)
            {
                clasesettings.guardarsetting("ips", todaslasip +  Intent.GetStringExtra("MyData").Trim()+">" );
            }




            ///////////////////////////////#clicks#/////////////////////////////////

            WallpaperManager wm = WallpaperManager.GetInstance(this);
            Drawable d = wm.Drawable;
            fondoblurreado = clasesettings.CreateBlurredImageformbitmap(this, 20, ((BitmapDrawable)d).Bitmap);
            fondo.SetImageBitmap(fondoblurreado);
           
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
            botonmostrarvolumen.Click += delegate
            {
              
                if (layoutvolumen.Visibility == ViewStates.Visible)
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
                if(lista.Count>0)
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
            StartService(new Intent(this, typeof(serviciodownload)));
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
                    return "%%si%%";
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
        public void cojerstream()
        {
            
           lista2 = new List<string>();
            listalinks= new List<string>();
            string[] listica = new string [0];

            bool cojioto = false;
            byte[] bites = new byte[50000];
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




              
                    listica = capturado.Split(';');
                    //          listicalinks = divisor[1].Split(';');


                    if (listica[0].Trim() == "listar()><")
                    {
                        clasesettings.recogerbasura();
                    }
                    else
                      if (listica[0].Trim() == "links()><")
                    {

                        listalinks.Clear();
                    
                        for (int i = 1; i < listica.Length; i++)
                        {
                            if (listica[i].Contains("cerrar()")){
                                detenedor = false;
                                this.Finish();
                            }
                            listalinks.Add(listica[i]);
                        };
                        clasesettings.recogerbasura();

                    }
                    else

                  if (capturado.Trim() != "caratula()" && !estasucio(listica[0].Trim()))
                    {
                      
                            lista2.Clear();
                        
                      
                     
                      
                        for (int i = 0; i < listica.Length; i++)
                        {
                            if (listica[i] != "" && !estasucio(listica[i]))
                            {
                                if (listica[i].Contains("cerrar()"))
                                {
                                    detenedor = false;
                                    this.Finish();
                                }
                                lista2.Add(listica[i]);

                               
                                //  adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lista2);
                             
                            }

                        }
                      //  RunOnUiThread(() => Toast.MakeText(this, "llego amiguito??", ToastLength.Short).Show());
                      capturado = "";


                        int ecrolx = 0;
                        RunOnUiThread(() => ecrolx = lista.FirstVisiblePosition);
                      
                        adaptadol = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,lista2);
                        RunOnUiThread(() => lista.Adapter = adaptadol);
                        RunOnUiThread(() => lista.SetSelection(ecrolx));


                        clasesettings.recogerbasura();



                    }
                    else
                   
                        if ( listica[0].Trim()=="caratula()><")
                    {
                        capturado = "";

                       
                        zelda = listica[4];
                     
                      RunOnUiThread(() => label.Text = listica[2]);
                        new Thread(() =>
                        {
                            if (listica[1] != "")
                            {
                                GetImageBitmapFromUrl(listica[1]);
                              
                            }

                        }).Start();
                        colol = listica[3];
                     
                        clasesettings.guardarsetting("cquerry", "data()>" + listica[2].Replace('>', ' ') + ">" + listica[1].Replace('>', ' ') + ">" + ip.Replace('>', ' '));
                        clasesettings.recogerbasura();

                        
                    }
                    else
                       if (listica[0].Contains("cerrar()"))
                        {
                        this.Finish();
                        detenedor = false;

                        clasesettings.recogerbasura();
                    }
                   

                }
                cojioto = false;

                bites = new byte[50000];


                listica.ToList().Clear();
                capturado = "";

                o = 0;
                Thread.Sleep(10);

            }
           
            this.Finish();
        }



        //  public override on
        protected override void OnDestroy()
        {
            detenedor = false;
            clientela.Client.Disconnect(true);
            actualizarlista.Abort();
            clasesettings.recogerbasura();
            if (cloudingserviceonline.gettearinstancia() != null)
            {
                cloudingserviceonline.gettearinstancia().StopSelf();
            }

            base.OnDestroy();
        }
        public override void Finish()
        {
           
            base.Finish();
           
        }
       
        private Bitmap GetImageBitmapFromUrl(string url)
        {
         
            Bitmap imageBitmap = null;
            try
            {
              

                if (url != " ")
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                            var sinblur = imageBitmap;
                            Bitmap conblur = null;
                            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                            {
                                conblur = CreateBlurredImage(20, BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length));
                            
                            }
                            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                            {

                                RunOnUiThread(() => fondo.SetImageBitmap(conblur));
                                fondoblurreado = conblur;
                            }
                            RunOnUiThread(() => caratula2.SetImageBitmap(sinblur));
                            Thread.Sleep(25);
                            clasesettings.recogerbasura();
                            if (cloudingserviceonline.gettearinstancia() != null)
                            {
                            
                                cloudingserviceonline.gettearinstancia().ipactual = ip;
                                cloudingserviceonline.gettearinstancia().linkactual = url;
                                cloudingserviceonline.gettearinstancia().tituloactual = label.Text;
                                cloudingserviceonline.gettearinstancia().mostrarnotificacion();
                            }

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

        public override void OnBackPressed()
        {

            clasesettings.preguntarsimenuosalir(this);
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
            bool auto = true;
            while (auto)
            {
               if(!prueba_de_lista_generica.SocketExtensions.IsConnected(clientela))
                {
                    auto = false;
                    this.Finish();
                    StartActivity(typeof(actmenuprincipal));
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


    }









}
               


           
       
   

       



