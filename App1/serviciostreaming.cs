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

namespace App1
{
    [Service(Exported = true, Enabled = true)]
    class serviciostreaming : Service
    {
        DnsServer servidordns;
        SimpleHTTPServer servidor;
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
            MasterFile archivomaster = new MasterFile();
            archivomaster.AddIPAddressResourceRecord("www.multitube.io", ipadre);
            servidordns = new DnsServer(archivomaster, "8.8.8.8");
            servidordns.Listen();
           
            new Thread(() =>
            {

                if (CheckInternetConnection())
                {


                    bool notienenada = false;

                    WebClient cliente = new WebClient();
                    cliente.DownloadDataAsync(new Uri("https://gr3gorywolf.github.io/Multitubeweb/version.gr3v"));
                    cliente.DownloadDataCompleted += (aaa, aaaa) =>
                    {
                        string versionsinparsear = Encoding.UTF8.GetString(aaaa.Result);


                        for (int i = 1; i < versionsinparsear.Split(';').Length; i++)
                        {
                            if (!File.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/" + versionsinparsear.Split(';')[i]))
                            {
                                notienenada = true;
                            }


                        }

                        //   

                        if (notienenada)
                        {

                            var archivo = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/version.gr3v");
                            archivo.Write(versionsinparsear);
                            archivo.Close();
                            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/js"))
                            {
                                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/js");
                            }
                            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/css"))
                            {
                                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/css");
                            }
                            for (int i = 1; i < versionsinparsear.Split(';').Length; i++)
                            {
                                cliente.DownloadFile(new Uri("https://gr3gorywolf.github.io/Multitubeweb/" + versionsinparsear.Split(';')[i]), Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/" + versionsinparsear.Split(';')[i]);
                            }




                        }
                        else
                        {
                            if (versionsinparsear.Split(';')[0] != File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/version.gr3v").Split(';')[0])
                            {

                                var archivo = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/version.gr3v");
                                archivo.Write(versionsinparsear);
                                archivo.Close();

                                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/js"))
                                {
                                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/js");
                                }
                                if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/css"))
                                {
                                    Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/css");
                                }
                                for (int i = 1; i < versionsinparsear.Split(';').Length; i++)
                                {
                                    cliente.DownloadFile(new Uri("https://gr3gorywolf.github.io/Multitubeweb/" + versionsinparsear.Split(';')[i]), Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/" + versionsinparsear.Split(';')[i]);
                                }



                            }

                        }




                    };
                }
                else {
                    StopSelf();
                }
            }).Start();
            servidor = new SimpleHTTPServer(Android.OS.Environment.ExternalStorageDirectory.ToString(), 12345, ipadre, this);
            var pelo = new Thread(() =>
            {
                checkchange();
            });
            pelo.IsBackground = true;
            pelo.Start();



            var brandom = new Random();

            Intent internado2 = new Intent(this, typeof(serviciointerpreter23));

            var pendingIntent2 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado2, 0);


            Notification.Builder nBuilder = new Notification.Builder(this);
            nBuilder.SetContentTitle("Stremeando en " + ipadre + ":12345");
            nBuilder.SetContentText("Toque para parar de stremear su media");

            nBuilder.SetOngoing(true);
            nBuilder.SetContentIntent(pendingIntent2);
            nBuilder.SetSmallIcon(Resource.Drawable.antena);

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

                            servidordns.Dispose();
                            servidor.Stop();
                            ipadre = ip.ToString();



                            servidor = new SimpleHTTPServer(Android.OS.Environment.ExternalStorageDirectory.ToString(), 12345, ipadre,this);
                            var brandom = new Random();

                            Intent internado2 = new Intent(this, typeof(serviciointerpreter23));
                      
                            var pendingIntent2 = PendingIntent.GetService(ApplicationContext, brandom.Next(2000, 50000) + brandom.Next(2000, 50000), internado2, 0);
                           


                            Notification.Builder nBuilder = new Notification.Builder(this);
                            nBuilder.SetContentTitle("Stremeando en " + ipadre + ":12345");
                            nBuilder.SetContentText("Toque para parar de stremear su media");
                            nBuilder.SetOngoing(true);
                            nBuilder.SetContentIntent(pendingIntent2);
                            nBuilder.SetSmallIcon(Resource.Drawable.antena);

                            Notification notification = nBuilder.Build();
                            StartForeground(193423456, notification);

                            MasterFile archivomaster = new MasterFile();
                            archivomaster.AddIPAddressResourceRecord("www.multitube.io", ipadre);
                            servidordns = new DnsServer(archivomaster, "8.8.8.8");
                            servidordns.Listen();
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
        public override void OnDestroy()
        {
            StopForeground(true);
            servidor.Stop();
            clasesettings.recogerbasura();
            base.OnDestroy();



        }
     
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}