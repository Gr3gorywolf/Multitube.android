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
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://"+ipadress+":" + _port.ToString() + "/");
            _listener.Start();
            while (true)
            {
             try
                {
                    HttpListenerContext context = _listener.GetContext();
                  
                        Process(context);



               }
                 catch (Exception)
                  {

                  }
            }
        }

        private void Process(HttpListenerContext context)
        {









            ///////////////////////////////////////////////quiere interactuar conmigo
            if (context.Request.Url.ToString().Contains("&&querry&&"))
            {

                if (context.Request.Url.ToString().Contains("meconecte"))
                {
                  
                    Notification.Builder nBuilder = new Notification.Builder(conteto);
                    nBuilder.SetContentTitle("Nuevo dispositivo conectado");
                    nBuilder.SetContentText("Un nuevo dispositivo está stremeando su media");
                    nBuilder.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));


                    nBuilder.SetSmallIcon(Resource.Drawable.antena);

                    Notification notification = nBuilder.Build();
                    NotificationManager notificationManager =
              (NotificationManager)conteto.GetSystemService(Context.NotificationService);
                    notificationManager.Notify(5126768, notification);


                }
               









            }
            else {

          

            bool esindice = false;
            string filename = context.Request.Url.AbsolutePath;
            Console.WriteLine(filename);
            filename = filename.Substring(1);
            filename = System.Net.WebUtility.UrlDecode(filename);
            
            if (string.IsNullOrEmpty(filename))
            {
                foreach (string indexFile in _indexFiles)
                {
                    if (File.Exists(Path.Combine(_rootDirectory+"/.gr3cache", indexFile)))
                    {
                        filename = indexFile;
                        esindice = true;
                        break;
                    }
                }
            }
            var nomesinpath = filename;
            if (!esindice) {

                if (!filename.Contains(".mp3") && !filename.Contains(".mp4"))
                {
                    filename = Path.Combine(_rootDirectory, filename);
                }
               
             
             
            }
            else {




               
                filename = Path.Combine(_rootDirectory + "/.gr3cache", filename);

            }
          

            if (File.Exists(filename))
            {
            new Thread(() =>
            {
               try
                {
                    using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
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
               

                            context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : mimo;
                            context.Response.ContentLength64 = stream.Length;


                            context.Response.KeepAlive = true;
                    
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.AddHeader("Accept-Ranges","bytes");
                            context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                            context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));
                            context.Response.AddHeader("Content-Range",string.Format("bytes {0}-{1}/{2}", 0,Convert.ToInt32( stream.Length) - 1, Convert.ToInt32(stream.Length)));
                            context.Response.ContentLength64 = stream.Length;
                        
                              stream.CopyTo(context.Response.OutputStream);
                            stream.Flush();

                              context.Response.OutputStream.Flush();
                            context.Response.OutputStream.Close();

                            /*  context.Response.OutputStream.Write(byteses,0,byteses.Length);


                              byteses = new byte[0];*/


                        }


                        else {

                            var reader = new StreamReader(stream);
                            var segmentosfull = "";
                            var teto = reader.ReadToEnd();
                            foreach (string bloques in teto.Split('¤'))
                            {
                                if (bloques != ""  &&  bloques != null)
                                {
                                    if (File.Exists(bloques.Split('²')[2]))
                                    {
                                        segmentosfull += bloques + '¤';

                                    }

                                }


                            }
                        reader.DiscardBufferedData();
                        reader.Dispose();

                        var bytesitosdestring = System.Text.Encoding.UTF8.GetBytes(segmentosfull);
                        using (MemoryStream streamm = new MemoryStream(bytesitosdestring, 0, bytesitosdestring.Length)) {


                            context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : mimo;
                            context.Response.ContentLength64 = streamm.Length;
                            context.Response.SendChunked = true;


                            context.Response.StatusCode = (int)HttpStatusCode.OK;

                            context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                            context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                            streamm.CopyTo(context.Response.OutputStream);
                            streamm.Flush();
                            context.Response.OutputStream.Flush();
                            context.Response.OutputStream.Close();

                        }

                        clasesettings.recogerbasura();

                    }










                   
                      
                   // stream.Close();
                    }

              }
                catch (Exception)
                {
                    // context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

            }).Start();







            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

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