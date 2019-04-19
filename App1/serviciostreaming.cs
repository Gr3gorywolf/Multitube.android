using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using DNS.Server;
using DNS.Client;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Token;
using Firebase.Xamarin.Database.Query;
using System.IO.Compression;
namespace App1
{
    [Service]
    class serviciostreaming : Service
    {
     
     static   SimpleHTTPServer servidor;
        string ipadre = "";


      static  Service instancia;

        public static Service gettearinstancia() {

            return instancia;
        }
        public bool CheckInternetConnection()
        {
            string CheckUrl = "https://gr3gorywolf.github.io/Multitubeweb/";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);

                iNetRequest.Timeout = 5000;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());
                iNetResponse.Close();

                return true;

            }
            catch (WebException )
            {

                // Console.WriteLine (".....no connection..." + ex.ToString ());

                return false;
            }
        }

        public override void OnCreate()
        {


            instancia = this;
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipadre = ip.ToString();

                }
            }
  
         
           
            new Thread(() =>
            {

                if (CheckInternetConnection())
                {


                    bool notienenada = false;

                    WebClient cliente = new WebClient();
                    cliente.DownloadDataAsync(new Uri("https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/Updates/version.gr3v"));
                    cliente.DownloadDataCompleted += (aaa, aaaa) =>
                    {
                    string versionsinparsear = Encoding.UTF8.GetString(aaaa.Result);               
                        if (!File.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/version.gr3v"))
                        {
                         notienenada = true;
                        }
                        if (notienenada)
                        {
                          clasesettings.updatejavelyn(versionsinparsear);
                        }
                        else
                        {
                            if (versionsinparsear != File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/version.gr3v")) 
                            {
                                clasesettings.updatejavelyn(versionsinparsear);
                            }
                        }
                    };
                    meterdata();
 
                }
                else {
               
                }
            }).Start();
            if (servidor != null)
                servidor.Stop();

            servidor = new SimpleHTTPServer(Android.OS.Environment.ExternalStorageDirectory.ToString(), 12345, ipadre, this);
            var pelo = new Thread(() =>
            {
                checkchange();
            });
            pelo.IsBackground = true;
            pelo.Start();



            var brandom = new Random();

            Intent internado2 = new Intent(this, typeof(serviciointerpreter23));
            Intent internado3 = new Intent(this, typeof(serviciointerpreter234));
            var pendingIntent3 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado3, 0);
            var pendingIntent2 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado2, 0);
            Notification.Action accion = new Notification.Action(Resource.Drawable.drwaable, "Parar", pendingIntent2);
            Notification.Action accion2 = new Notification.Action(Resource.Drawable.drwaable, "Conectarse", pendingIntent3);

            Notification.BigTextStyle textoo = new Notification.BigTextStyle();
            textoo.SetBigContentTitle("Stremeando en " + ipadre + ":12345");
            textoo.SetSummaryText("Toque para parar de stremear su media");
            textoo.BigText("Para conectarse introduzca " + ipadre + ":12345  " + "en su navegador o entre a multitubeweb.tk y toque conectar luego escanee el codigo que aparece al presionar el boton de + en multitubeweb.   al tocar parar se cierrar el servicio de streaming");


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            Notification.Builder nBuilder = new Notification.Builder(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            nBuilder.SetContentTitle("Stremeando en " + ipadre + ":12345");
            nBuilder.SetContentText("Toque para parar de stremear su media");

            nBuilder.SetOngoing(true);
          //  nBuilder.SetContentIntent(pendingIntent2);
            nBuilder.SetStyle(textoo);
            nBuilder.SetSmallIcon(Resource.Drawable.antena);
            nBuilder.SetColor(Android.Graphics.Color.ParseColor("#ce2c2b"));
            nBuilder.AddAction(accion);
            nBuilder.AddAction(accion2);

            Notification notification = nBuilder.Build();
            StartForeground(193423456, notification);
            base.OnCreate();
            //  descargar(intent.GetStringExtra("path"), intent.GetStringExtra("archivo"), intent.GetStringExtra("titulo"), intent.GetStringExtra("link"));

        }



        


        public void checkchange() {
            while (true) {


                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ip in localIPs)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {

                        if (ipadre != ip.ToString()) {

                           
                            servidor.Stop();
                            ipadre = ip.ToString();



                            servidor = new SimpleHTTPServer(Android.OS.Environment.ExternalStorageDirectory.ToString(), 12345, ipadre,this);
                            var brandom = new Random();

                            Intent internado2 = new Intent(this, typeof(serviciointerpreter23));
                            Intent internado3 = new Intent(this, typeof(serviciointerpreter234));
                            var pendingIntent3 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado3, 0);
                            var pendingIntent2 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado2, 0);
                            Notification.Action accion = new Notification.Action(Resource.Drawable.drwaable, "Parar", pendingIntent2);
                            Notification.Action accion2 = new Notification.Action(Resource.Drawable.drwaable, "Conectarse", pendingIntent3);
                            Notification.BigTextStyle textoo = new Notification.BigTextStyle();
                            textoo.SetBigContentTitle("Stremeando en " + ipadre + ":12345");
                            textoo.SetSummaryText("Toque para parar de stremear su media");
                            textoo.BigText("Para conectarse introduzca " + ipadre + ":12345  " + "en su navegador o entre a multitubeweb.tk y toque conectar luego escanee el codigo que aparece al presionar el boton de + en multitubeweb.   al tocar parar se cierrar el servicio de streaming");


#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                            Notification.Builder nBuilder = new Notification.Builder(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                            nBuilder.SetContentTitle("Stremeando en " + ipadre + ":12345");
                            nBuilder.SetContentText("Toque para parar de stremear su media");
                            nBuilder.SetStyle(textoo);
                            nBuilder.SetColor(Android.Graphics.Color.DarkRed.ToArgb());
                            nBuilder.SetOngoing(true);
                         //   nBuilder.SetContentIntent(pendingIntent2);
                            nBuilder.SetSmallIcon(Resource.Drawable.antena);
                            nBuilder.AddAction(accion);
                            nBuilder.AddAction(accion2);
                            Notification notification = nBuilder.Build();
                            StartForeground(193423456, notification);

                         

                            if (CheckInternetConnection())
                            {
                                meterdata();
                            
                            }

                            }
                        

                    }
                }
                clasesettings.recogerbasura();
                Thread.Sleep(5000);
            }

        }

        public override void OnTaskRemoved(Intent rootIntent)
        {


          //  base.OnTaskRemoved(rootIntent);


        }

        public async void meterdata() {


            try
            {
                var id = "";
                if (clasesettings.gettearid() != null)
                {
                    id = clasesettings.gettearid();
                }
                else
                {
                    Random rondom = new Random();
                    char[] array = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm' };
                    string serial = rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                        +
                        rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                         +
                        rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                         +
                        rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                         +
                        rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString();
                    var creador = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/uid");
                    creador.Write(serial);
                    creador.Close();
                    id = serial;

                }

                // Email/Password Auth
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("<your firebase auth key>"));

                var auth = await authProvider.SignInWithEmailAndPasswordAsync("<your firebase username>", "<your firebase password>");

                // The auth Object will contain auth.User and the Authentication Token from the request
                var token = auth.FirebaseToken;
                // System.Diagnostics.Debug.WriteLine();
                var firebase = new FirebaseClient("<your firebase proyect url>");


                // Console.WriteLine($"Key for the new item: {item.Key}");

                // add new item directly to the specified location (this will overwrite whatever data already exists at that location)

                var musicass = "";
                var videos = "";
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/downloaded.gr3d"))
                {
                    musicass = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/downloaded.gr3d");
                }
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/downloaded.gr3d2"))
                {
                    videos = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/downloaded.gr3d2");
                }





                var cantvideos = 0;
                var cantmusicas = 0;
                if (musicass != "")
                {
                    foreach (string perrito in musicass.Remove(musicass.Length - 1, 1).Split('¤'))
                    {
                        if (File.Exists(perrito.Split('²')[2]))
                        {
                            cantmusicas++;
                        }
                    }

                }
                if (videos != "")
                {
                    foreach (string perrito in videos.Remove(videos.Length - 1, 1).Split('¤'))
                    {
                        if (File.Exists(perrito.Split('²')[2]))
                        {
                            cantvideos++;
                        }
                    }
                }

                var mapita = new Dictionary<string, string>();
                mapita.Add("Musica", cantmusicas.ToString());
                mapita.Add("Nombre", Android.OS.Build.Model);
                mapita.Add("Tipo", "Telefono");
                mapita.Add("Videos", cantvideos.ToString());
                mapita.Add("ip", ipadre);
                await firebase.Child("Devices").Child(id).WithAuth(token).PutAsync<Dictionary<string, string>>(mapita); // <-- Add Auth token if required. Auth instructions further down in readme.
            }
            catch (Exception e) {
                Console.WriteLine("ha ocurrido una excepcion" + e.Message + e.TargetSite + e.Source + e.InnerException);
            };

            //   enviaratodos(token,firebase);
        }

        public override void OnDestroy()
        {
            StopForeground(true);
            servidor.Stop();
            clasesettings.recogerbasura();
            base.OnDestroy();



        }
        public async void enviaratodos(string token,FirebaseClient firebase) {
            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/webclients.gr3wc")) {
                var textotodo = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/webclients.gr3wc");
                textotodo = textotodo.Remove(textotodo.Length-1, 1);
            Random rondom = new Random();
            char[] array = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm' };
            string serial = rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                +
                rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                 +
                rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                 +
                rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString()
                 +
                rondom.Next(1, 9).ToString() + array[rondom.Next(0, 12)].ToString();

         
         
            var mapita = new Dictionary<string, Dictionary<string,string>>();
/*
                var items = await firebase
             .Child("yourentity")
  //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
            .OrderByKey()
            .OnceAsync<YourObject>();

    */

         
                foreach (var prro in textotodo.Split('¤')) {
                    var diccio = new Dictionary<string, string>();
                    diccio.Add(clasesettings.gettearvalor("uniqueid"), serial);
                  //  mapita.Add(prro, diccio);
                    await firebase.Child("WEB").Child(prro).WithAuth(token).PatchAsync(diccio);
                    
                }
            
           

           

            }
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}