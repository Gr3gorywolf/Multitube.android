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

namespace App1
{
    class tituloydownloadurl
    {
      public  string titulo { get; set; }
       public string downloadurl { get; set; }
    //    public string titulo { get; set; }
    }

    class clasesettings
    {


        public static Activity context = null;

        public static void preguntarsimenuosalir(Context ctx)
        {

            context =(Activity) ctx;
            AlertDialog.Builder ad = new AlertDialog.Builder(ctx);
            ad.SetCancelable(false);
            ad.SetTitle("Advertencia");
            ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
            ad.SetMessage("Que desea hacer");
            ad.SetNegativeButton("Ir al menu principal", si);
            ad.SetNeutralButton("Salir de la aplicacion", no);
            ad.SetPositiveButton("Cancelar", no2);
            ad.Create();
            ad.Show();



        }

        public static void si(object sender, EventArgs e)
        {
            recogerbasura();
            context.Finish();
            context.StartActivity(new Intent(context,typeof(actmenuprincipal)));

        }
        public static void no(object sender, EventArgs e)
        {
            context.Finish();
        }
        public static void no2(object sender, EventArgs e)
        {
            
        }
        public static void agregaracoladescarga()
        {
          
        }
        public static void ponerfondoyactualizar(Activity instancia)
        {

            if (mainmenu_Offline.gettearinstancia() != null)
            {
                var prro = new Thread(() =>
                {
                    iractualizandofondo("sadasdasd", instancia);
                });
                prro.IsBackground = true;
                prro.Start();
            }
            else
            if (mainmenu.gettearinstancia() != null)
           
            {
                var prro = new Thread(() =>
                  {
                      iractualizandofondo("online", instancia);
                  });
                prro.IsBackground = true;
                prro.Start();
              
            }

        }

        public static void iractualizandofondo(string onlineoofline,Activity instancia)
        {
            ImageView fondin = instancia.FindViewById<ImageView>(Resource.Id.fondo1);
            if (onlineoofline == "online")
            {
                while (instancia!=null)
                {
                    instancia.RunOnUiThread(() =>
                    {
                        if (mainmenu.gettearinstancia().fondoblurreado != null)
                        {
                        
                          
                            fondin.SetImageBitmap(mainmenu.gettearinstancia().fondoblurreado);
                        }
                       

                    });
                    if (instancia.IsFinishing)
                    {
                        break;
                    }
                    Thread.Sleep(5000);
                    recogerbasura();

                }
               
            }
            else
            {

                while (instancia!=null)
                {
                    instancia.RunOnUiThread(() =>
                    {
                        if (mainmenu_Offline.gettearinstancia().fondoblurreado != null)
                        {
                          
                            fondin.SetImageBitmap(mainmenu_Offline.gettearinstancia().fondoblurreado);
                        }
                       
                    });
                    Thread.Sleep(5000);
                    recogerbasura();
                    if (instancia.IsFinishing)
                    {
                        break;
                    }
                }
            
            }
        }




        

   


        public static Bitmap CreateBlurredImageoffline(Context contexto,int radius, string link)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap;
            if (link.ToLower().Contains("youtube.com"))
            {
                originalBitmap = BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/"+link.Split('=')[1]);
            }
            else
            {
                originalBitmap = BitmapFactory.DecodeFile(link);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {

                // Create another bitmap that will hold the results of the filter.
                try { 
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




        public static Bitmap CreateBlurredImageofflineadapters(Context contexto, int radius, string link)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap;
            if (link.ToLower().Contains("youtube.com"))
            {
                originalBitmap =Bitmap.CreateScaledBitmap( BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]),150,150,false);
            }
            else
            {
                originalBitmap = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeFile(link),150,150,false);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {

                // Create another bitmap that will hold the results of the filter.
                try { 
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
                try { 
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
                try { 
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

        public static bool probarsetting(string nombrekey)
        {
            try
            {
                var prro = gettearvalor(nombrekey);
                if (prro.Trim() != "")
                {
                    prro = null;
                    return true;
                }else
                {
                    prro = null;
                    return true;
                }
              
            }
            catch(Exception)
            {
                return false;
            }
        }
        public static void guardarsetting(string nombrekey,string valor)
        {

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            ISharedPreferencesEditor prefEditor = prefs.Edit();
            prefEditor.PutString(nombrekey, valor);
            prefEditor.Commit();

        }
        public static string gettearvalor(string nombrekey)
        {
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
          return  prefs.GetString(nombrekey, null);
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


        public static Bitmap getRoundedShape(Bitmap scaleBitmapImage)
        {
            int targetWidth = 480;
            int targetHeight = 360;
            Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                targetHeight, Bitmap.Config.Argb8888);

            Canvas canvas = new Canvas(targetBitmap);
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
            return targetBitmap;
        }

        public static void deciralbroadcast(Context contexto,string mensaje)
        {
            Intent intento = new Intent(contexto, typeof(broadcast_receiver));
            Bundle bdl = new Bundle();
            bdl.PutString("klk", mensaje);
            intento.PutExtra("prro", bdl);
            contexto.SendBroadcast(intento);
        }

        public static tituloydownloadurl gettearvideoid(string elink,bool videoabierto)
        {
            string video2 = "";
            string title = "";
            using (var videito = Client.For(YouTube.Default))
            {
                var video = videito.GetAllVideosAsync(elink);
                var resultados = video.Result;
                title = resultados.First().Title.Replace("- YouTube", "");
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                {
                    video2 = resultados.First(info => info.Resolution == -1 && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
                }
                else
                {
                    video2 = resultados.First(info => info.Resolution == 240 && info.AudioFormat == AudioFormat.Aac).GetUriAsync().Result;
                }

         
            }
        
        




            if (!videoabierto) {
                  
                tituloydownloadurl papu = new tituloydownloadurl();
                papu.downloadurl = video2;
                papu.titulo = title; 
                return papu;
            }
            else
            {
               
              
             
                tituloydownloadurl papu = new tituloydownloadurl();
                papu.downloadurl = "";
                papu.titulo =title;
                return papu;
            }
         
        }


    }
}