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
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]


    public class actividadsync : Android.Support.V7.App.AppCompatActivity


    {
        int progreso = 0;
        public int cantbytesreceiving;
        public string selectedtab = "Audios";
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
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        ListView listaelementos;
        ImageView qr;
        Button botonescaneo;
        ImageView enviardisco;
        Android.Support.V7.Widget.SearchView textoedicion;
        Android.Support.Design.Widget.TabLayout tl;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.actsyncoffline);
          botonescaneo = FindViewById<Button>(Resource.Id.imageView6);
            qr = FindViewById<ImageView>(Resource.Id.imageView1);
            enviardisco = FindViewById<ImageView>(Resource.Id.imageView3);
           listaelementos = FindViewById<ListView>(Resource.Id.listView1);
            mipuertoarchivo = rondon.Next(1024, 9999);
           mipuertoquerry = rondon.Next(1024, 9999);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            dialogoprogreso.SetCanceledOnTouchOutside(false);
            dialogoprogreso.SetCancelable(false);

           
            textoedicion = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);                           
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
            SetSupportActionBar(FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar));
            SupportActionBar.Title = "Sicronización de medios";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
             tl = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tabs);
            tl.Visibility = ViewStates.Gone;
            tl.AddTab(tl.NewTab().SetText("Audios"));
            tl.AddTab(tl.NewTab().SetText("Videos"));
            textoedicion.Visibility = ViewStates.Gone;
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => {
                var parcelable = listaelementos.OnSaveInstanceState();
                listaelementos.Adapter = adaptadolo;
                listaelementos.OnRestoreInstanceState(parcelable);
            });
            new Thread(() =>
            {
                servidorftp.Run();
            }).Start();

      
            if (playeroffline.gettearinstancia().showingvideosresults)
            {
              
                enviardisco.SetBackgroundResource(Resource.Drawable.musicalnote);
            }
            else
            {
               enviardisco.SetBackgroundResource(Resource.Drawable.videoplayer);
              
               
            }

            if (!SettingsHelper.HasKey("notificosync?")) {
                SettingsHelper.SaveSetting("notificosync?", "Si");
                new AlertDialog.Builder(this).SetTitle("Como se usa?")
                    .SetMessage("Para sincronizar con otro dispositivo uno de los dispositivos debe escanear el código qr de el otro.\n luego le aparecerá un menú con toda la media que usted tenga descargada.\n al tocar un elemento se le enviara a la persona\nNOTA:Si se cierra el menú de sinconización mientras se envía un elemento puede que se cancelen las descargas locales.")
                    .SetPositiveButton("Entendido!",(aa,aaa)=> { }).Create().Show();

            }


            clientequerry = new TcpClient();
        
          
            var adaptadol2 = new adapterlistaoffline(this, playeroffline.gettearinstancia().nombreses.ToList(), playeroffline.gettearinstancia().linkeses, "", playeroffline.gettearinstancia().nombreses, playeroffline.gettearinstancia().diccionario, playeroffline.gettearinstancia().patheses);
            RunOnUiThread(() => listaelementos.Adapter = adaptadol2);
            qr.SetImageBitmap(GetQRCode());
          






            textoedicion.QueryTextChange += delegate
            {
                adaptadol2 = new adapterlistaoffline(this, playeroffline.gettearinstancia().nombreses.Where(a => a.ToLower().Contains(textoedicion.Query.ToLower())).ToList(), playeroffline.gettearinstancia().linkeses, "", playeroffline.gettearinstancia().nombreses, playeroffline.gettearinstancia().diccionario, playeroffline.gettearinstancia().patheses);

                RunOnUiThread(() => {
                    var parcelable = listaelementos.OnSaveInstanceState();

                    listaelementos.Adapter = adaptadol2;
                    listaelementos.OnRestoreInstanceState(parcelable);

                });
               

            };

            tl.TabSelected += (aa, sss) =>
            {
                new Thread(() =>
                {

                    if (sss.Tab.Text == "Audios")
                    {
                        playeroffline.gettearinstancia().cargarmp3();
                        selectedtab = "Audios";
                    }
                    else { 
                        playeroffline.gettearinstancia().cargarvideos();
                        selectedtab = "Videos";
                    }
                    adaptadol2 = new adapterlistaoffline(this, playeroffline.gettearinstancia().nombreses.ToList(), playeroffline.gettearinstancia().linkeses, "", playeroffline.gettearinstancia().nombreses, playeroffline.gettearinstancia().diccionario, playeroffline.gettearinstancia().patheses);

                    RunOnUiThread(() =>
                    {
                        var parcelable = listaelementos.OnSaveInstanceState();
                        listaelementos.Adapter = adaptadol2;
                        listaelementos.OnRestoreInstanceState(parcelable);
                    });
                }).Start();
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
                    textoedicion.Visibility = ViewStates.Visible;
                    tl.Visibility = ViewStates.Visible;
                /*    tl.AddTab(tl.NewTab().SetText("Audios"));
                    tl.AddTab(tl.NewTab().SetText("Videos"));*/
                    //  enviardisco.Visibility = ViewStates.Visible;
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
                Android.Animation.ObjectAnimator animacion2 = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleY", 0.5f, 1f);
                animacion2.SetDuration(300);
                animacion2.Start();
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
            writer.Options.Height = 500;
            writer.Options.Width = 500;





            return writer.Write(miip+ "¤" + mipuertoquerry+ "¤" + mipuertoarchivo+ "¤" + SettingsHelper.GetSetting("rutadescarga") + "¤" + Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/");
        }
        public void sincronizar()
        {

           clientequerry.Client.Send(Encoding.UTF8.GetBytes("dataserver()¤" + miip + "¤" + mipuertoquerry + "¤" + mipuertoarchivo+ "¤" + SettingsHelper.GetSetting("rutadescarga")+ "¤" +mipathimagenes));
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
                            
                                                   
                            textoedicion.Visibility = ViewStates.Visible;
                            tl.Visibility = ViewStates.Visible;
                          
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
                Thread.Sleep(100);
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

            ///cliente.Encoding = Encoding.UTF8;
            // cliente.Credentials = new NetworkCredential("anonymous", "");




            string titulo = "Descargando localmente: " + Path.GetFileNameWithoutExtension(nombrearchivo);
            string tituloo = "";

            NotificationManager notificationManager =
     GetSystemService(Context.NotificationService) as NotificationManager;
            if (nombrearchivo.Trim().Length > 40)
            {
                titulo = titulo.Remove(40);
            }
            tituloo = titulo;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            var builder = new Notification.Builder(ApplicationContext);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            builder.SetContentTitle(titulo);
            builder.SetContentText("Espere por favor");
            builder.SetSmallIcon(Resource.Drawable.downloadbutton);
            builder.SetProgress(100, progreso,true);
            notificationManager.Notify(notiide, builder.Build());






            RunOnUiThread(() => Toast.MakeText(this, "Descargando localmente: " + Path.GetFileNameWithoutExtension(nombrearchivo) + " se le notificara de su progreso", ToastLength.Long).Show());

          
       

            cliente.DownloadFileCompleted += (aa, aaa) =>
                {
                    progreso = 100;
                    WebClient cliente2 = new WebClient();
                    cliente2.Encoding = Encoding.UTF8;
                    cliente2.Credentials = new NetworkCredential("anonymous", "");
                
                    cliente2.DownloadFileCompleted += delegate
                    {
                        
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
                  
                      

         
                    escribirenregistro(Path.Combine(SettingsHelper.GetSetting("rutadescarga"), nombrearchivo), linkarchivo);
                    Intent intentssdd = new Intent(this, typeof(actividadadinfooffline));
                    intentssdd.PutExtra("nombre", Path.GetFileName(nombrearchivo));
                    intentssdd.PutExtra("link", linkarchivo);
                    intentssdd.PutExtra("path", Path.Combine(SettingsHelper.GetSetting("rutadescarga"), nombrearchivo));





                    PendingIntent intentosd = PendingIntent.GetActivity(this,rondon.Next(24,100000), intentssdd, PendingIntentFlags.UpdateCurrent);
                    string titulo2 = "Descarga local completada de : " + Path.GetFileNameWithoutExtension(nombrearchivo);
                    string tituloo2 = "";
               
                    if (nombrearchivo.Trim().Length > 40)
                    {
                        titulo2 = titulo2.Remove(40);
                    }
                    tituloo2 = titulo2;

                    builder.SetContentTitle(titulo2);
                    builder.SetContentText("Toque para abrir");
                    builder.SetSmallIcon(Resource.Drawable.downloadbutton);
                    builder.SetContentIntent(intentosd);
                    builder.SetProgress(1, 1, false);
                    notificationManager.Notify(notiide, builder.Build());
                    RunOnUiThread(() => Toast.MakeText(this, "Descarga local de: " +Path.GetFileNameWithoutExtension( nombrearchivo) + " completada", ToastLength.Long).Show());
                    new Thread(() => {

                       
                        if (selectedtab=="Audios")
                            playeroffline.gettearinstancia().cargarmp3();
                        else
                            playeroffline.gettearinstancia().cargarvideos();


                        var adaptadol2 = new adapterlistaoffline(this, playeroffline.gettearinstancia().nombreses.ToList(), playeroffline.gettearinstancia().linkeses, "", playeroffline.gettearinstancia().nombreses, playeroffline.gettearinstancia().diccionario, playeroffline.gettearinstancia().patheses);
                        RunOnUiThread(() => listaelementos.Adapter = adaptadol2);

                    }).Start();
                };
            if (patharchivo.StartsWith("/"))
            {
                cliente.DownloadFileAsync(new Uri(@"ftp://" + ipdelotro + ":" + puertoarchivosdelotro + patharchivo), Path.Combine(SettingsHelper.GetSetting("rutadescarga"), nombrearchivo));
            }else
            {
                cliente.DownloadFileAsync(new Uri(@"ftp://" + ipdelotro + ":" + puertoarchivosdelotro + "/" + getteardisco( patharchivo)), Path.Combine(SettingsHelper.GetSetting("rutadescarga"), nombrearchivo));
            }
                 
             
       






           
        }
        public override void OnBackPressed()
        {

            this.Finish();
            base.OnBackPressed();
        }
        public string getteardisco(string pathcompletito)
        {
            char literal = patharchivo[0];
            return pathcompletito.Replace(literal + ":", literal.ToString());
        }

        protected override void OnDestroy()
        {
            progreso = 100;
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
            new Thread(() =>
            {
                playeroffline.gettearinstancia().sincronized = false;
                playeroffline.gettearinstancia().desdethread = true;
             

                if (!playeroffline.gettearinstancia().showingvideosresults)
                    playeroffline.gettearinstancia().cargarmp3();
                else
                    playeroffline.gettearinstancia().cargarvideos();

             
                

            
            }).Start();
            base.OnDestroy();
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
            enviararchivo(Path.GetFileName(playeroffline.gettearinstancia().patheses[posx]), playeroffline.gettearinstancia().linkeses[posx], playeroffline.gettearinstancia().patheses[posx]);
        }
        public  void no(object sender, EventArgs e)
        {
          
        }









        /// <summary>
        /// //////////////////////////////////////////////////////////ignore2
        /// </summary>



      
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                   
                    this.Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }




    }


    }
