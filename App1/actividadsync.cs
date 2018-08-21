using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentFTP;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using mooftpserv;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class actividadsync : Activity
    {
        public int cantbytesreceiving;
        Random rondon = new Random();
        public bool ensubida = false;
        public bool enbajada = false;
        bool detenedor = true;
        public Server servidorftp;
        public TcpListener servidorquerry;
        public TcpClient clientequerry;
        FtpClient clienteftp = new FtpClient();
        public string nombrearchivo;
        public string linkarchivo;
        public string supath = "";
        int mipuertoarchivo;
        int mipuertoquerry;
        string ipdelotro = "";
        string miip = "";
        int posx = 0;
        string pathimagenesdelotro = "";
        string mipathimagenes = Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/";
        int puertoquerrydelotro = 0;
        int puertoarchivosdelotro = 0;
        public int cantidadbytesarchivoarecibir = 0;
        string patharchivo = "";
        ProgressDialog dialogoprogreso;
        ListView listaelementos;
        ImageView qr;
             ImageView botonescaneo;
        ImageView enviardisco;
        LinearLayout layoutfiltro;
        ImageView fondo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.actsyncoffline);
          botonescaneo = FindViewById<ImageView>(Resource.Id.imageView6);
            qr = FindViewById<ImageView>(Resource.Id.imageView1);
            enviardisco = FindViewById<ImageView>(Resource.Id.imageView3);
           listaelementos = FindViewById<ListView>(Resource.Id.listView1);
            mipuertoarchivo = rondon.Next(200, 5000) + rondon.Next(200, 5000);
           mipuertoquerry = rondon.Next(200, 5000) + rondon.Next(200, 5000);
            dialogoprogreso = new ProgressDialog(this);
            dialogoprogreso.SetCanceledOnTouchOutside(false);
            dialogoprogreso.SetCancelable(false);
            layoutfiltro = FindViewById<LinearLayout>(Resource.Id.linearLayout3);
            EditText textoedicion = FindViewById<EditText>(Resource.Id.editText1);
            ImageView botonborrar = FindViewById<ImageView>(Resource.Id.imageView7);
            var botoncerrar = FindViewById<ImageView>(Resource.Id.imageView5);
            fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            layoutfiltro.Visibility = ViewStates.Gone;
            listaelementos.Visibility = ViewStates.Gone;
            enviardisco.Visibility = ViewStates.Gone;
            servidorquerry = new TcpListener(IPAddress.Any, mipuertoquerry);
            servidorquerry.Start();
            miip =gettearip();
            servidorftp = new Server();
            servidorftp.LocalPort = mipuertoarchivo;
            servidorftp.LocalAddress = IPAddress.Parse(miip);
            servidorftp.LogHandler = new DefaultLogHandler(false);
            servidorftp.AuthHandler = new DefaultAuthHandler();
            servidorftp.FileSystemHandler = new DefaultFileSystemHandler();
            servidorftp.BufferSize = 1000000;
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => listaelementos.Adapter = adaptadolo);
            new Thread(() =>
            {
                servidorftp.Run();
            }).Start();

            botoncerrar.Click += delegate
            {
                dialogoprogreso = new ProgressDialog(this);
                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Recargando datos...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
                this.Finish();
            };
            if (playeroffline.gettearinstancia().showingvideosresults)
            {
              
                enviardisco.SetBackgroundResource(Resource.Drawable.musicalnote);
            }
            else
            {
               enviardisco.SetBackgroundResource(Resource.Drawable.videoplayer);
              
               
            }
            clientequerry = new TcpClient();
        
          
            var adaptadol2 = new adapterlistaoffline(this, playeroffline.gettearinstancia().nombreses.ToList(), playeroffline.gettearinstancia().portadases, "", playeroffline.gettearinstancia().nombreses, playeroffline.gettearinstancia().diccionario, playeroffline.gettearinstancia().patheses);
            RunOnUiThread(() => listaelementos.Adapter = adaptadol2);
            qr.SetImageBitmap(GetQRCode());
            botonborrar.Click += delegate
            {
                textoedicion.Text = "";
            };
            textoedicion.TextChanged += delegate
            {
                adaptadol2 = new adapterlistaoffline(this, playeroffline.gettearinstancia().nombreses.Where(a => a.ToLower().Contains(textoedicion.Text.ToLower())).ToList(), playeroffline.gettearinstancia().portadases, "", playeroffline.gettearinstancia().nombreses, playeroffline.gettearinstancia().diccionario, playeroffline.gettearinstancia().patheses);

                RunOnUiThread(() => listaelementos.Adapter = adaptadol2);
                if (textoedicion.Text.Length >= 1)
                {
                    botonborrar.Visibility = ViewStates.Visible;
                }
                else
                {
                    botonborrar.Visibility = ViewStates.Gone;
                }

            };
           
            listaelementos.ItemClick += (aa, aaa) =>
            {
                var vii = aaa.View.FindViewById<TextView>(Resource.Id.textView1);
            int posicion=    playeroffline.gettearinstancia().nombreses.IndexOf(vii.Text);
              
                new Thread(() =>
                {

                    preguntarsienviarorecibir(true, playeroffline.gettearinstancia().nombreses[posicion], posicion);
                }).Start() ;
              
            };
            var sv1 = new Thread(() =>
            {
                servidorhabladera(false);
            });
            sv1.IsBackground = true;
            sv1.Start();
           
            enviardisco.Click += delegate
            {
               if(playeroffline.gettearinstancia().showingvideosresults)
                {
                    animar2(listaelementos);
                    enviardisco.SetBackgroundResource(Resource.Drawable.videoplayer);
                }
                else
                {
                    animar2(listaelementos);
                    enviardisco.SetBackgroundResource(Resource.Drawable.musicalnote);
                }
              playeroffline.gettearinstancia().RunOnUiThread(() => {
                playeroffline.gettearinstancia().showvideos.PerformClick();
                      });
                 adaptadol2 = new adapterlistaoffline(this, playeroffline.gettearinstancia().nombreses.ToList(), playeroffline.gettearinstancia().portadases, "", playeroffline.gettearinstancia().nombreses, playeroffline.gettearinstancia().diccionario, playeroffline.gettearinstancia().patheses);

                RunOnUiThread(() => listaelementos.Adapter = adaptadol2);

            };
            botonescaneo.Click +=async delegate
            {

                sv1.Abort();  
                ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var resultado = await scanner.Scan();
                if (resultado != null)
                {
                    sv1.Abort();
                 
                    ipdelotro = resultado.Text.Split('¤')[0];
                    puertoquerrydelotro =int.Parse( resultado.Text.Split('¤')[1]);
                    puertoarchivosdelotro =int.Parse( resultado.Text.Split('¤')[2]);
                   supath = resultado.Text.Split('¤')[3];
                    pathimagenesdelotro = resultado.Text.Split('¤')[4];
                    clientequerry.Client.Connect(ipdelotro, puertoquerrydelotro);
                    
               
                    sv1 = new Thread(() =>
                    {
                        servidorhabladera(true);
                    });
                    sv1.IsBackground = true;
                    sv1.Start();
                    listaelementos.Visibility = ViewStates.Visible;
                    botonescaneo.Visibility = ViewStates.Gone;
                    qr.Visibility = ViewStates.Gone;
                    layoutfiltro.Visibility = ViewStates.Visible;
                    enviardisco.Visibility = ViewStates.Visible;
                    sincronizar();

                }
            };
            // Create your application here
        }
        public void animar(Java.Lang.Object imagen)
        {

            RunOnUiThread(() =>
            {
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
                animacion.SetDuration(300);
                animacion.Start();
            });



        }
        public void animar2(Java.Lang.Object imagen)
        {
            RunOnUiThread(() =>
            {
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
                animacion.SetDuration(700);
                animacion.Start();
            });

        }
        public string gettearip()
        {
            string klk = "";
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {

                    klk = ip.ToString();
                }
            }
            return klk;
        }
        private Android.Graphics.Bitmap GetQRCode()
        {

            var writer = new ZXing.Mobile.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options.Margin = 1;
            writer.Options.Height = 300;
            writer.Options.Width = 300;





            return writer.Write(miip+ "¤" + mipuertoquerry+ "¤" + mipuertoarchivo+ "¤" + clasesettings.gettearvalor("rutadescarga") + "¤" + Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/");
        }
        public void sincronizar()
        {

           clientequerry.Client.Send(Encoding.UTF8.GetBytes("dataserver()¤" + miip + "¤" + mipuertoquerry + "¤" + mipuertoarchivo+ "¤" + clasesettings.gettearvalor("rutadescarga")+ "¤" +mipathimagenes));
        }
        public void enviararchivo(string nombrearchivo,string linkarchivo,string patharchivo)
        {
      
            clientequerry.Client.Send(Encoding.UTF8.GetBytes("mediadata()¤" + nombrearchivo + "¤" + linkarchivo + "¤" + patharchivo));
       
         


            ////////////////////////////////////////////////////////////////


           
          
          //  clientearchivo.Client.Send(archivoenbytes);
         
              




        }

      
        public void servidorhabladera(bool tenemostodo)
        {
            if (!tenemostodo)
            {
                try
                {
                    clientequerry = servidorquerry.AcceptTcpClient();
                }
                catch (Exception)
                {

                }
                 
            }
            if (clientequerry.Connected) { 
            int cantidadbytes = 0;
            var stream = clientequerry.GetStream();
            byte[] losbits = new byte[2000000];
            string querrystring = "";
            while (detenedor)
            {
                if (clientequerry.Connected) { 
                while (stream.DataAvailable)
                {
                    cantidadbytes = stream.Read(losbits, 0, losbits.Length);
                    querrystring += Encoding.UTF8.GetString(losbits, 0, cantidadbytes);
                }

                if (querrystring.StartsWith("mediadata()"))
                {
                    nombrearchivo = querrystring.Split('¤')[1];
                    linkarchivo = querrystring.Split('¤')[2];
                     patharchivo = querrystring.Split('¤')[3];
                            new Thread(() =>
                        {
                            preguntarsienviarorecibir(false, nombrearchivo, 0);
                           
                        }).Start();
                }
                else
                 if (querrystring.StartsWith("dataserver()"))
                {
                    ipdelotro = querrystring.Split('¤')[1];
                    puertoquerrydelotro = int.Parse(querrystring.Split('¤')[2]);
                    puertoarchivosdelotro = int.Parse(querrystring.Split('¤')[3]);

                  ///  RunOnUiThread(() => Toast.MakeText(this, "me conecte", ToastLength.Long).Show());                    
                        supath = querrystring.Split('¤')[4];
                        pathimagenesdelotro= querrystring.Split('¤')[5];
                        RunOnUiThread(() =>
                        {
                            listaelementos.Visibility = ViewStates.Visible;
                            botonescaneo.Visibility = ViewStates.Gone;
                            qr.Visibility = ViewStates.Gone;
                            layoutfiltro.Visibility = ViewStates.Visible;
                            enviardisco.Visibility = ViewStates.Visible;
                        });
                        //   clientearchivo.Client.Connect(ipdelotro, puertoarchivosdelotro);



                    }
                else              
               if (querrystring.StartsWith("acabamos"))
                {
                            this.Finish();
                 }
                    querrystring = "";
                    cantidadbytes = 0;
                stream = clientequerry.GetStream();
                losbits = new byte[2000000];

                }
                querrystring = "";
                Thread.Sleep(40);
            }
        }
        }
        /// <summary>
        /// //////////////////////////////////////////////////ignore
        /// </summary>
        /// <param name="path"></param>
        /// <param name="linkarchivo"></param>


        /// 
        public void descargame(string nombrearchivo, string linkarchivo,string patharchivo)
        {

            int notiide = rondon.Next(24, 100000);
            WebClient cliente = new WebClient();

            cliente.Encoding = Encoding.UTF8;
            cliente.Credentials = new NetworkCredential("anonymous", "");

            RunOnUiThread(() => Toast.MakeText(this, "Descargando localmente: " + Path.GetFileNameWithoutExtension(nombrearchivo) + " se le notificara de su progreso", ToastLength.Long).Show());
            cliente.DownloadProgressChanged += (aa, aaa) =>
            {
                string titulo = "Descargando localmente: " + Path.GetFileNameWithoutExtension(nombrearchivo);
                string tituloo = "";
                NotificationManager notificationManager =
           GetSystemService(Context.NotificationService) as NotificationManager;
                if (nombrearchivo.Trim().Length > 40)
                {
                    titulo = titulo.Remove(40);
                }
                tituloo = titulo;
                var builder = new Notification.Builder(ApplicationContext);
                builder.SetContentTitle(titulo);
                builder.SetContentText("Espere por favor");
                builder.SetProgress(100, aaa.ProgressPercentage, false);
                builder.SetSmallIcon(Resource.Drawable.downloadbutton);
                if (aaa.ProgressPercentage == 20|| aaa.ProgressPercentage == 40 || aaa.ProgressPercentage == 60 || aaa.ProgressPercentage == 80 || aaa.ProgressPercentage == 100){
                    notificationManager.Notify(notiide, builder.Build());
                }

                 
                

                };


         
                cliente.DownloadFileCompleted += (aa, aaa) =>
                {
                    WebClient cliente2 = new WebClient();
                    cliente2.Encoding = Encoding.UTF8;
                    cliente2.Credentials = new NetworkCredential("anonymous", "");
                    /*    try
                        {*/
                    cliente2.DownloadFileCompleted += delegate
                    {
                        RunOnUiThread(() => fondo.SetImageBitmap(clasesettings.CreateBlurredImageoffline(this, 20, linkarchivo)));
                    };
                    if (pathimagenesdelotro.StartsWith("/"))
                        {
                            cliente2.DownloadFile(new Uri(@"ftp://" + ipdelotro + ":" + puertoarchivosdelotro + pathimagenesdelotro + linkarchivo.Split('=')[1]), Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + linkarchivo.Split('=')[1]);
                        }
                        else
                        {

                        try { 

                     cliente2.DownloadFile(new Uri(@"ftp://" + ipdelotro + ":" + puertoarchivosdelotro + "/"+ getteardisco(pathimagenesdelotro) + linkarchivo.Split('=')[1] + ".jpg"), Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + linkarchivo.Split('=')[1]);
                        }
                        catch (Exception)
                        {

                            try {
                            cliente2.DownloadFile("http://i.ytimg.com/vi/"+linkarchivo.Split('=')[1]+"/mqdefault.jpg", Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + linkarchivo.Split('=')[1]);

                            }
                            catch (Exception)
                            {

                            }
                            }

                        }
                       // string perruris = "ftp://" + ipdelotro + ":" + puertoarchivosdelotro + "/" + getteardisco(pathimagenesdelotro) + linkarchivo.Split('=')[1] + ".jpg";

                     //   string MMG = perruris;
                      

                  //  }
                  /*  catch (Exception)
                    {

                    }*/
                    escribirenregistro(Path.Combine(clasesettings.gettearvalor("rutadescarga"), nombrearchivo), linkarchivo);
                    Intent intentssdd = new Intent(this, typeof(actividadadinfooffline));
                    intentssdd.PutExtra("nombre", Path.GetFileName(nombrearchivo));
                    intentssdd.PutExtra("link", linkarchivo);
                    intentssdd.PutExtra("path", Path.Combine(clasesettings.gettearvalor("rutadescarga"), nombrearchivo));





                    PendingIntent intentosd = PendingIntent.GetActivity(this,rondon.Next(24,100000), intentssdd, PendingIntentFlags.UpdateCurrent);
                    string titulo = "Descarga local completada de : " + Path.GetFileNameWithoutExtension(nombrearchivo);
                    string tituloo = "";
                    NotificationManager notificationManager =
               GetSystemService(Context.NotificationService) as NotificationManager;
                    if (nombrearchivo.Trim().Length > 40)
                    {
                        titulo = titulo.Remove(40);
                    }
                    tituloo = titulo;
                    var builder = new Notification.Builder(ApplicationContext);
                    builder.SetContentTitle(titulo);
                    builder.SetContentText("Toque para abrir");
                    builder.SetSmallIcon(Resource.Drawable.downloadbutton);
                    builder.SetContentIntent(intentosd);
                    notificationManager.Notify(notiide, builder.Build());
                    RunOnUiThread(() => Toast.MakeText(this, "Descarga local de: " +Path.GetFileNameWithoutExtension( nombrearchivo) + " completada", ToastLength.Long).Show());
                };
            if (patharchivo.StartsWith("/"))
            {
                cliente.DownloadFileAsync(new Uri(@"ftp://" + ipdelotro + ":" + puertoarchivosdelotro + patharchivo), Path.Combine(clasesettings.gettearvalor("rutadescarga"), nombrearchivo));
            }else
            {
                cliente.DownloadFileAsync(new Uri(@"ftp://" + ipdelotro + ":" + puertoarchivosdelotro + "/" + getteardisco( patharchivo)), Path.Combine(clasesettings.gettearvalor("rutadescarga"), nombrearchivo));
            }
                 
             
       






           
        }
        public override void OnBackPressed()
        {

            dialogoprogreso = new ProgressDialog(this);
            dialogoprogreso.SetCanceledOnTouchOutside(false);
            dialogoprogreso.SetCancelable(false);
            dialogoprogreso.SetTitle("Recargando datos...");
            dialogoprogreso.SetMessage("Por favor espere");
            dialogoprogreso.Show();
            this.Finish();
            base.OnBackPressed();
        }
        public string getteardisco(string pathcompletito)
        {
            char literal = patharchivo[0];
            return pathcompletito.Replace(literal + ":", literal.ToString());
        }
        public override void Finish()
        {
         
          
            try
            {
               
               
                servidorquerry.Stop();
          
            servidorftp.Stop();
                detenedor = false;
                //clientequerry.Client.Disconnect(false);

                if (clientequerry.Connected)
            {
                        clientequerry.Client.Send(Encoding.UTF8.GetBytes("acabamos"));
                        clientequerry.Client.Disconnect(false);
            }
            }
            catch (Exception)
            {

            };
        
            playeroffline.gettearinstancia().sincronized = false;
            playeroffline.gettearinstancia().desdethread = true;
            playeroffline.gettearinstancia().showingvideosresults = true;
            playeroffline.gettearinstancia().cargarmp3();
            playeroffline.gettearinstancia().RunOnUiThread(() => playeroffline.gettearinstancia().showvideos.PerformClick());
           clasesettings.recogerbasura();
            dialogoprogreso.Dismiss();
            //  this.Dispose();
            base.Finish();
        }
        public void escribirenregistro(string path,string linkarchivo)
        {
            string datosviejos = "";


            if (Path.GetFileName(path).EndsWith(".mp3"))
            {
                ///////////////////////////si es mp3

                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d"))
                {
                    datosviejos = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
                }
                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits");
                }

                if (!datosviejos.Contains(Path.GetFileNameWithoutExtension(path) + "²" + linkarchivo + "²" + path + "¤"))
                {
                    var aafff = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d");
                    aafff.Write(datosviejos + Path.GetFileNameWithoutExtension(path) + "²" + linkarchivo + "²" + path + "¤");
                    aafff.Close();

                }


            }
            //////////////////////////////////////////////si es mp4
            else
            {
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2"))
                {
                    datosviejos = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");
                }
                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits"))
                {
                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits");
                }

                if (!datosviejos.Contains(Path.GetFileNameWithoutExtension(path) + "²" + linkarchivo + "²" + path + "¤"))
                {
                    var aafff = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + "downloaded.gr3d2");
                    aafff.Write(datosviejos + Path.GetFileNameWithoutExtension(path) + "²" + linkarchivo + "²" + path + "¤");
                    aafff.Close();

                }
            }



        }


        public void preguntarsienviarorecibir(bool envio,string nombre,int posicion)
        {

            RunOnUiThread(() =>
            {
                AlertDialog.Builder ad = new AlertDialog.Builder(this);
                ad.SetCancelable(false);
                ad.SetTitle("Advertencia");
                ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                posx = posicion;
                if (envio)
                {
                    ad.SetMessage("Desea enviar: " + Path.GetFileNameWithoutExtension(nombre) + " al cliente solicitado");
                    ad.SetPositiveButton("Si", si);

                }
                else
                {
                    ad.SetMessage("Desea recibir: " + Path.GetFileNameWithoutExtension(nombre) + " del cliente solicidato");
                    ad.SetPositiveButton("Si", si2);
                }


                ad.SetNegativeButton("No", no);


                ad.Create();
                ad.Show();
            });

        }
        public void si2(object sender, EventArgs e)
        {
            descargame(nombrearchivo, linkarchivo, patharchivo);
        }
        public  void si(object sender, EventArgs e)
        {
            enviararchivo(Path.GetFileName(playeroffline.gettearinstancia().patheses[posx]), playeroffline.gettearinstancia().portadases[posx], playeroffline.gettearinstancia().patheses[posx]);
            RunOnUiThread(() => fondo.SetImageBitmap(clasesettings.CreateBlurredImageoffline(this, 20, playeroffline.gettearinstancia().portadases[posx])));

        }
        public  void no(object sender, EventArgs e)
        {
          
        }








        /// <summary>
        /// //////////////////////////////////////////////////////////ignore2
        /// </summary>



    }


    }
