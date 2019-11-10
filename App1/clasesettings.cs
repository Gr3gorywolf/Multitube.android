using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using VideoLibrary;
using Android.Renderscripts;
using System.Net;
using Android.Net.Wifi;
using Android.Net;
using System.IO;
using Android.Glide;
using Android.Glide.Request;
using Android.Support.V7.Palette;
using System.Drawing;
using Android.Support.V7.Graphics;
using System.IO.Compression;
using Newtonsoft.Json;
using HtmlAgilityPack;
using Bitmap = Android.Graphics.Bitmap;
using App1.Models;
using App1.Utils;

namespace App1
{



   
   
 


   

    
  
  
   
    class clasesettings
    {


        public static Activity context = null;
        public static Client<YouTubeVideo> youtubeclient = null;
       
        public static bool updateschecked = false;

    

       
     
        public static string settearipsp()
        {
            string ipa = "";
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipa = ip.ToString();

                }
            }
            return ipa;

        }




        public static void preguntarsimenuosalir(Context ctx)
        {

            context = (Activity)ctx;
            AlertDialog.Builder ad = new AlertDialog.Builder(ctx);
            ad.SetCancelable(false);
            ad.SetTitle("Advertencia");
            ad.SetIcon(Resource.Drawable.alert);
            ad.SetMessage("Que desea hacer");
            ad.SetNegativeButton("Ir a la pantalla de selección de modos", si);
            ad.SetNeutralButton("Salir de la aplicacion", no);
            ad.SetPositiveButton("Cancelar", no2);
            ad.Create();
            ad.Show();



        }



        public static string serializarmedia(List<MediaElement> elementos)
        {
            string serialized = "";
            foreach (var elem in elementos)
                serialized += elem.Name + "²" + elem.Link + "²" + elem.Path + "¤";

            return serialized;
        }
        public static bool tieneconexion()
        {
            string CheckUrl = "https://www.youtube.com/";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);

                iNetRequest.Timeout = 10000;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());
                iNetResponse.Close();

                return true;

            }
            catch (WebException)
            {

                // Console.WriteLine (".....no connection..." + ex.ToString ());

                return false;
            }
        }
        public static List<MediaElement> obtenermedia(string path)
        {

            List<MediaElement> intmedia = new List<MediaElement>();
            if (File.Exists(path))
            {
                foreach (var parts in File.ReadAllText(path).Split('¤'))
                {
                    if (parts.Trim().Length > 0)
                    {
                        intmedia.Add(new MediaElement()
                        {
                            Name = parts.Split('²')[0],
                            Link = parts.Split('²')[1],
                            Path = parts.Split('²')[2]
                        });

                    }


                }
                return intmedia;
            }
            else
                return new List<MediaElement>();





        }






        public static bool tieneelementos()
        {
            var elementlist = obtenermedia(Constants.CachePath + "/downloaded.gr3d")
                .Concat(obtenermedia(Constants.CachePath + "/downloaded.gr3d2")).ToList();
            var counter = 0;
            foreach (var elem in elementlist)
            {
                if (File.Exists(elem.Path))
                    counter++;
            }
            if (counter > 0)
                return true;
            else
                return false;

        }



        public static string gettearid()
        {



            if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/uid"))
            {
                return File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/uid").Trim();
            }
            else
            {


                return null;
            }


        }
        public static void si(object sender, EventArgs e)
        {
            context.StartActivity(new Intent(context, typeof(actsplashscreen)));
            recogerbasura();
            context.Finish();
            if (Mainmenu.gettearinstancia() != null)
                Mainmenu.gettearinstancia().Finish();
            if (MainmenuOffline.gettearinstancia() != null)
                MainmenuOffline.gettearinstancia().Finish();

            if (actividadinicio.gettearinstancia() != null)
                actividadinicio.gettearinstancia().Finish();

        }
        public static void no(object sender, EventArgs e)
        {
            context.Finish();
            if (Mainmenu.gettearinstancia() != null)
                Mainmenu.gettearinstancia().Finish();
            if (MainmenuOffline.gettearinstancia() != null)
                MainmenuOffline.gettearinstancia().Finish();

            if (actividadinicio.gettearinstancia() != null)
                actividadinicio.gettearinstancia().Finish();

        }
        public static void no2(object sender, EventArgs e)
        {

        }
        public static void agregaracoladescarga()
        {

        }



        public static void updatejavelyn(string version)
        {
            var cliente = new WebClient();
            var archivo = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/version.gr3v");
            archivo.Write(version);
            archivo.Close();
            cliente.DownloadFile(new System.Uri("https://gr3gorywolf.github.io/Multitubeweb/update.zip"), Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/update.zip");

            ZipArchive ar = new ZipArchive(File.Open(Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/update.zip", FileMode.Open, FileAccess.Read, FileShare.Read));
            var extpath = Android.OS.Environment.ExternalStorageDirectory.ToString() + "/.gr3cache/";

            foreach (var entry in ar.Entries)
            {

                if (entry.FullName.EndsWith("/"))
                {
                    if (!Directory.Exists(extpath + entry.FullName))
                        Directory.CreateDirectory(extpath + entry.FullName);
                }
                else
                {
                    using (var xd = File.Create(extpath + entry.FullName))
                    {
                        entry.Open().CopyTo(xd);
                        xd.Close();
                    }
                }
            }
            File.Delete(extpath + "update.zip");










        }

        public static Dictionary<string, PlaylistElement> agregarfavoritos(Activity contexto, Dictionary<string, PlaylistElement> lista, PlaylistElement elemento)
        {

            if (lista.ContainsKey(elemento.Link))
            {
                lista.Remove(elemento.Link);
                contexto.RunOnUiThread(() => { Toast.MakeText(contexto, "Elemento eliminado de favoritos", ToastLength.Long).Show(); });
            }
            else
            {
                lista.Add(elemento.Link, elemento);
                contexto.RunOnUiThread(() => { Toast.MakeText(contexto, "Elemento agregado a favoritos. Ahora aparecera en su pantalla de inicio", ToastLength.Long).Show(); });
            }
            var arch = File.CreateText(Constants.CachePath + "/favourites.json");
            arch.Write(JsonConvert.SerializeObject(lista));
            arch.Close();




            return lista;
        }

        public static int gettearcolorprominente(Android.Graphics.Bitmap bmp)
        {
            var p = Palette.From(bmp).Generate();
            return p.MutedSwatch.Rgb;
        }

        public static void ponerfondoyactualizar(Activity instancia)
        {

            if (MainmenuOffline.gettearinstancia() != null)
            {
                var prro = new Thread(() =>
                {
                    iractualizandofondo("sadasdasd", instancia);
                });
                prro.IsBackground = true;
                prro.Start();
            }
            else
            if (Mainmenu.gettearinstancia() != null)

            {
                var prro = new Thread(() =>
                  {

                      iractualizandofondo("online", instancia);
                  });
                prro.IsBackground = true;
                prro.Start();

            }

        }

        public static void iractualizandofondo(string onlineoofline, Activity instancia)
        {
            ImageView fondin = instancia.FindViewById<ImageView>(Resource.Id.fondo1);
            if (onlineoofline == "online")
            {
                /*   while (instancia!=null)
                   {*/
                instancia.RunOnUiThread(() =>
                {
                        //  if (mainmenu.gettearinstancia().fondoblurreado != null)
                        //  {


                        // fondin.SetBackgroundColor(new Android.Graphics.Color(gettearcolorprominente(mainmenu.gettearinstancia().fondoblurreado)));
                        fondin.SetBackgroundColor(Android.Graphics.Color.ParseColor("#353535"));

                        //    }


                    });
                recogerbasura();
                /*      if (instancia.IsFinishing)
                      {
                          break;
                      }
                     // Thread.Sleep(5000);
                      recogerbasura();

                  }*/

            }
            else
            {

                //    while (instancia!=null)
                //  {

                instancia.RunOnUiThread(() =>
                {
                        //  if (mainmenu_Offline.gettearinstancia().fondoblurreado != null)
                        //  {

                        // fondin.SetBackgroundColor(new Android.Graphics.Color(gettearcolorprominente(mainmenu_Offline.gettearinstancia().fondoblurreado)));
                        fondin.SetBackgroundColor(Android.Graphics.Color.ParseColor("#353535"));
                        //   }

                    });
                //    Thread.Sleep(5000);
                recogerbasura();
                /*  if (instancia.IsFinishing)
                  {
                      break;
                  }
              }*/

            }
        }









        public static Bitmap CreateBlurredImageoffline(Context contexto, int radius, string link)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap;
            if (link.ToLower().Contains("youtube.com"))
            {
                originalBitmap = BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]);
            }
            else
            {
                originalBitmap = BitmapFactory.DecodeFile(link);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {

                // Create another bitmap that will hold the results of the filter.
                try
                {
                    Bitmap blurredBitmap;
                    blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

                    // Create the Renderscript instance that will do the work.
                    RenderScript rs = RenderScript.Create(contexto);

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
                    output.Dispose();
                    //originalBitmap.Dispose();
                    script.Dispose();
                    input.Dispose();
                    rs.Dispose();
                    return blurredBitmap;
                }
                catch (Exception)
                {
                    return originalBitmap;
                }
            }
            else
            {
                return originalBitmap;
            }
        }



        public static void modoinmersivo(Window ventana, bool visible)
        {
            int uiOptions = 0;
            uiOptions = (int)ventana.DecorView.SystemUiVisibility;
            if (!visible)
            {


                uiOptions |= (int)SystemUiFlags.LowProfile;
                uiOptions |= (int)SystemUiFlags.Fullscreen;
                uiOptions |= (int)SystemUiFlags.HideNavigation;
                uiOptions |= (int)SystemUiFlags.ImmersiveSticky;
            }
            else
            {
                uiOptions &= (int)SystemUiFlags.LowProfile;
                uiOptions &= (int)SystemUiFlags.Fullscreen;
                uiOptions &= (int)SystemUiFlags.HideNavigation;
                uiOptions &= (int)SystemUiFlags.ImmersiveSticky;

            }
            ventana.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        }
        public static Bitmap CreateBlurredImageofflineadapters(Context contexto, int radius, string link)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap;
            if (link.ToLower().Contains("youtube.com"))
            {
                originalBitmap = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]), 150, 150, false);
            }
            else
            {
                originalBitmap = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeFile(link), 150, 150, false);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {

                // Create another bitmap that will hold the results of the filter.
                try
                {
                    Bitmap blurredBitmap;
                    blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

                    // Create the Renderscript instance that will do the work.
                    RenderScript rs = RenderScript.Create(contexto);

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
                    output.Dispose();
                    //originalBitmap.Dispose();
                    script.Dispose();
                    input.Dispose();
                    rs.Dispose();
                    return blurredBitmap;
                }
                catch (Exception)
                {
                    return originalBitmap;
                }
            }
            else
            {
                return originalBitmap;
            }
        }


        public static Bitmap CreateBlurredImageformbitmap(Context contexto, int radius, Bitmap imagen)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap = imagen;


            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {

                // Create another bitmap that will hold the results of the filter.
                try
                {
                    Bitmap blurredBitmap;
                    blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

                    // Create the Renderscript instance that will do the work.
                    RenderScript rs = RenderScript.Create(contexto);

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
                    output.Dispose();
                    //originalBitmap.Dispose();
                    script.Dispose();
                    input.Dispose();
                    rs.Dispose();
                    return blurredBitmap;
                }
                catch (Exception)
                {
                    return originalBitmap;
                }
            }
            else
            {
                return originalBitmap;
            }
        }




        public static Bitmap GetImageBitmapFromUrl(string url)
        {

            Bitmap imageBitmap = null;
            try
            {


                if (url.Trim() != "")
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(url);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);


                        }

                    }

            }
            catch (Exception) { }




            return imageBitmap;
        }


        public static Bitmap CreateBlurredImageonline(Context contexto, int radius, string link)
        {

            // Load a clean bitmap and work from that.
            WebClient cliente = new WebClient();
            var aa = new byte[0];



            if (!link.StartsWith("https://i.ytimg.com/vi/"))
            {
                aa = cliente.DownloadData("https://i.ytimg.com/vi/" + link.Split('=')[1] + "/mqdefault.jpg");
            }
            else
            {
                aa = cliente.DownloadData(link);
            }

            Bitmap originalBitmap = BitmapFactory.DecodeByteArray(aa, 0, aa.Length);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {
                // Create another bitmap that will hold the results of the filter.
                try
                {
                    Bitmap blurredBitmap;
                    blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

                    // Create the Renderscript instance that will do the work.
                    RenderScript rs = RenderScript.Create(contexto);

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
                    output.Destroy();

                    // originalBitmap.Dispose();
                    script.Destroy();
                    input.Destroy();
                    rs.Destroy();
                    aa = new byte[0];
                    return blurredBitmap;
                }
                catch (Exception)
                {
                    return originalBitmap;
                }

            }
            else
            {

                return originalBitmap;
            }
        }

    
       
       

        public static void recogerbasura()
        {
            try
            {


                if (true)
                {


                    GC.Collect();
                }
            }
            catch (Exception)
            {

            }

        }


       
        public static void deciralbroadcast(Context contexto, string mensaje)
        {
            Intent intento = new Intent(contexto, typeof(broadcast_receiver));
            Bundle bdl = new Bundle();
            bdl.PutString("klk", mensaje);
            intento.PutExtra("prro", bdl);
            contexto.SendBroadcast(intento);
        }
       

        public static void mostrarnotificacion(Activity contexto, string titulo, string mensaje, string link, int codigo)
        {

            contexto.RunOnUiThread(() =>
            {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                var nBuilder = new Notification.Builder(contexto);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos


                nBuilder.SetLargeIcon(clasesettings.GetImageBitmapFromUrl("http://i.ytimg.com/vi/" + link.Split('=')[1] + "/mqdefault.jpg"));
                nBuilder.SetContentTitle(titulo);
                nBuilder.SetContentText(mensaje);
                nBuilder.SetColor(Android.Graphics.Color.ParseColor("#ce2c2b"));
                nBuilder.SetSmallIcon(Resource.Drawable.list);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                nBuilder.SetSound(Android.Media.RingtoneManager.GetDefaultUri(Android.Media.RingtoneType.Notification));
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                Notification notification = nBuilder.Build();
                NotificationManager notificationManager =
                  (NotificationManager)contexto.GetSystemService(Context.NotificationService);
                notificationManager.Notify(5126768, notification);
            });


        }
       
       
        public static VideoShortInfo gettearvideoid(string elink, bool videoabierto, int calidad)
        {
            string video2 = "";
            string title = "";
            try
            {
                if (youtubeclient == null)
                    youtubeclient = Client.For(YouTube.Default);
                var video = youtubeclient.GetAllVideosAsync(elink);
                var resultados = video.Result;
                title = resultados.First().Title.Replace("- YouTube", "");
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                {


                    var results = resultados.Where(info => info.Resolution == calidad && info.AudioFormat == AudioFormat.Aac);

                    if (results.Count() == 0)
                        while (results.Count() == 0)
                        {
                            if (calidad == 360 && results.Count() == 0)
                            {
                                results = resultados.Where(info => info.Resolution == 240 && info.AudioFormat == AudioFormat.Aac);
                            }
                            else
                            if (calidad == 720 && results.Count() == 0)
                            {
                                results = resultados.Where(info => info.Resolution == 360 && info.AudioFormat == AudioFormat.Aac);
                            }
                            if (calidad == 240 && results.Count() == 0)
                            {
                                results = resultados.Where(info => info.Resolution == -1 && info.AudioFormat == AudioFormat.Aac);
                            }

                        }
                    video2 = results.First().GetUriAsync().Result;


                }
                else
                {
                    video2 = resultados.First(info => info.Resolution == 240 && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
                }

                if (!videoabierto)
                {

                    VideoShortInfo papu = new VideoShortInfo();
                    papu.downloadurl = video2;
                    papu.titulo = title;
                    return papu;
                }
                else
                {



                    VideoShortInfo papu = new VideoShortInfo();
                    papu.downloadurl = "";
                    papu.titulo = title;
                    return papu;
                }
            }
            catch (Exception)
            {

                return null;
            }


        }


    }
}