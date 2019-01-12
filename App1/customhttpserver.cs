using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace App1
{
    class SimpleHTTPServer
    {
        private readonly string[] _indexFiles = {
        "index.html",
        "index.htm",
        "default.html",
        "default.htm"
    };

        private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        #region extension to MIME type list
        {".asf", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".avi", "video/x-msvideo"},
        {".bin", "application/octet-stream"},
        {".cco", "application/x-cocoa"},
        {".crt", "application/x-x509-ca-cert"},
        {".css", "text/css"},
        {".deb", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dll", "application/octet-stream"},
        {".dmg", "application/octet-stream"},
        {".ear", "application/java-archive"},
        {".eot", "application/octet-stream"},
        {".exe", "application/octet-stream"},
        {".flv", "video/x-flv"},
        {".gif", "image/gif"},
        {".hqx", "application/mac-binhex40"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".ico", "image/x-icon"},
        {".img", "application/octet-stream"},
        {".iso", "application/octet-stream"},
        {".jar", "application/java-archive"},
        {".jardiff", "application/x-java-archive-diff"},
        {".jng", "image/x-jng"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".mml", "text/mathml"},
        {".mng", "video/x-mng"},
        {".mov", "video/quicktime"},
        {".mp3", "audio/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpg", "video/mpeg"},
        {".msi", "application/octet-stream"},
        {".msm", "application/octet-stream"},
        {".msp", "application/octet-stream"},
        {".pdb", "application/x-pilot"},
        {".pdf", "application/pdf"},
        {".pem", "application/x-x509-ca-cert"},
        {".pl", "application/x-perl"},
        {".pm", "application/x-perl"},
        {".png", "image/png"},
        {".prc", "application/x-pilot"},
        {".ra", "audio/x-realaudio"},
        {".rar", "application/x-rar-compressed"},
        {".rpm", "application/x-redhat-package-manager"},
        {".rss", "text/xml"},
        {".run", "application/x-makeself"},
        {".sea", "application/x-sea"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".swf", "application/x-shockwave-flash"},
        {".tcl", "application/x-tcl"},
        {".tk", "application/x-tcl"},
        {".txt", "text/plain"},
        {".war", "application/java-archive"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wmv", "video/x-ms-wmv"},
        {".xml", "text/xml"},
        {".xpi", "application/x-xpinstall"},
        {".zip", "application/zip"},
        {".mp4", "video/mpeg"}, 
        #endregion
    };
        private Thread _serverThread;
        private string _rootDirectory;
        private HttpListener _listener;
        private int _port;
        public Context conteto;
        public string ipadress;
        public int clientesconectados = 0;
        public string verifiedmp3 = "none";
        public string verifiedmp4 = "none";
        public string pathsito;
        public int puelto;
        public int mp4lenght = 0;
         public int mp3lenght = 0;
        public int Port
        {
            get { return _port; }
            private set { }
        }

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        public SimpleHTTPServer(string path, int port, string ipadre, Context contexto)

        {
            pathsito = path;
            puelto = port;
            conteto = contexto;
            ipadress = ipadre;
            this.Initialize(path, port);
        }

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        public SimpleHTTPServer(string path)
        {
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize(path, port);
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        private void Listen()
        {









            refreshregistry();

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://"+ipadress+":" + _port.ToString() + "/");
            _listener.UnsafeConnectionNtlmAuthentication = false;
            _listener.IgnoreWriteExceptions = true;
            _listener.AuthenticationSchemes = AuthenticationSchemes.None;
            _listener.Start();
            while (true)
            {

                clasesettings.recogerbasura();
                HttpListenerContext context = _listener.GetContext();
                    new Thread(() =>
                    {

                        
                            Process(context);
                 
                    }).Start();
                       
                



              
            }
        }

        private void Process(HttpListenerContext context)
        {







            ///////////////////////////////////////////////quiere interactuar conmigo
            ///   context.Response.AddHeader("Access-Control-Allow-Origin", "*");


          
       
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            context.Response.AddHeader("Access-Control-Allow-Methods", "*");
            context.Response.AddHeader("Access-Control-Allow-Headers", "*");
            if (context.Request.Url.ToString().Contains("&&querry&&"))
                {

                    if (context.Request.Url.ToString().Contains("meconecte"))
                    {

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                        Notification.Builder nBuilder = new Notification.Builder(conteto);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                        nBuilder.SetContentTitle("Nuevo dispositivo conectado");
                        nBuilder.SetContentText("Un nuevo dispositivo está stremeando su media");
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                        nBuilder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos


                        nBuilder.SetSmallIcon(Resource.Drawable.antena);

                        Notification notification = nBuilder.Build();
                        NotificationManager notificationManager =
                  (NotificationManager)conteto.GetSystemService(Context.NotificationService);
                        notificationManager.Notify(5126768, notification);


                    }










                }
                else
                {



                    bool esindice = false;
                    string filename = context.Request.Url.AbsolutePath;
                    Console.WriteLine(filename);
                    filename = filename.Substring(1);
                    filename = System.Net.WebUtility.UrlDecode(filename);

                    if (string.IsNullOrEmpty(filename))
                    {
                        foreach (string indexFile in _indexFiles)
                        {
                            if (File.Exists(Path.Combine(_rootDirectory + "/.gr3cache", indexFile)))
                            {
                                filename = indexFile;
                                esindice = true;
                                break;
                            }
                        }
                    }
                    var nomesinpath = filename;
                    if (!esindice)
                    {

                        if (!filename.Contains(".mp3") && !filename.Contains(".mp4"))
                        {
                            filename = Path.Combine(_rootDirectory, filename);
                        }



                    }
                    else
                    {





                        filename = Path.Combine(_rootDirectory + "/.gr3cache", filename);

                    }


                    if (File.Exists(filename))
                    {
                 
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    //Adding permanent http response headers

                    string mime;
                            string mimo = "";
                            if (filename.EndsWith("mp3"))
                            {
                                mimo = "audio/*";
                            }
                            else
                             if (filename.EndsWith("mp4"))
                            {
                                mimo = "video/mp4";
                            }







                            if (!filename.Contains("downloaded.gr3d"))
                            {





                       

                        using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {





                                context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : mimo;
                                context.Response.ContentLength64 = stream.Length;

                           
                                //context.Response.KeepAlive = true;
                                
                            
                                context.Response.AddHeader("Accept-Ranges", "bytes");
                                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                                context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));
                                context.Response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", 0, Convert.ToInt32(stream.Length) - 1, Convert.ToInt32(stream.Length)));
                                context.Response.ContentLength64 = stream.Length;

                             stream.CopyTo(context.Response.OutputStream);
                       

                                stream.Flush();

                              





                                /*  context.Response.OutputStream.Write(byteses,0,byteses.Length);


                                  byteses = new byte[0];*/


                            }
                            //    
                     
                            }


                            else
                            {

 
                                refreshregistry();
                                byte[] bytesitosdestring;
                                if (filename.EndsWith("2"))
                                    bytesitosdestring = System.Text.Encoding.UTF8.GetBytes(verifiedmp4);
                                else
                                    bytesitosdestring = System.Text.Encoding.UTF8.GetBytes(verifiedmp3);

                 

                        using (MemoryStream streamm = new MemoryStream(bytesitosdestring, 0, bytesitosdestring.Length))
                                {


                                    context.Response.ContentType = _mimeTypeMappings.TryGetValue(filename.Split('.')[1], out mime) ? mime : mimo;
                                    context.Response.ContentLength64 = streamm.Length;
                                    context.Response.SendChunked = true;

                   


                            context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                                    context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                                    streamm.CopyTo(context.Response.OutputStream);
                                    streamm.Flush();
                                   
                                   
                                }
                                clasesettings.recogerbasura();
                         
                               

                            }

















                    context.Response.OutputStream.Flush();
                    context.Response.OutputStream.Close();
                    context.Response.Close();





                }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }

                }
          
 }

        public void refreshregistry() {
            
            string teto = "";
            string teto2 = "";

            if (File.Exists(_rootDirectory + "/.gr3cache/downloaded.gr3d"))
            {
                teto = File.ReadAllText(_rootDirectory + "/.gr3cache/downloaded.gr3d");






                if (mp3lenght != teto.Length)
                {
                    verifiedmp3 = "";
                    foreach (string bloques in teto.Split('¤'))
                    {
                        if (bloques != "" && bloques != null)
                        {
                            if (File.Exists(bloques.Split('²')[2]))
                            {
                                verifiedmp3 += bloques + '¤';

                            }

                        }


                    }
                    mp3lenght = teto.Length;
                }
            }
            else {
                var ax = File.CreateText(clasesettings.rutacache + "/downloaded.gr3d");
                ax.Write("");
                ax.Close();
                verifiedmp3 = "none";
            }

            if (File.Exists(_rootDirectory + "/.gr3cache/downloaded.gr3d2"))
            {

                teto2 = File.ReadAllText(_rootDirectory + "/.gr3cache/downloaded.gr3d2");


                if (mp4lenght != teto2.Length)
                {
                    verifiedmp4 = "";
                    foreach (string bloques in teto2.Split('¤'))
                    {
                        if (bloques != "" && bloques != null)
                        {
                            if (File.Exists(bloques.Split('²')[2]))
                            {
                                verifiedmp4 += bloques + '¤';

                            }

                        }


                    }
                    mp4lenght = teto2.Length;
                
                }
            }
            else {
                var ax = File.CreateText(clasesettings.rutacache + "/downloaded.gr3d2");
                ax.Write("");
                ax.Close();
                verifiedmp4 = "none";
               
            }
        }
        private void Initialize(string path, int port)
        {
            this._rootDirectory = path;
            this._port = port;
            _serverThread = new Thread(this.Listen);
            _serverThread.IsBackground = true;
            _serverThread.Start();
        }























    }

   
}