using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Utils
{
    public static class ImageHelper
    {
        public static Bitmap GetRoundedShape(Bitmap scaleBitmapImage)
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

        public static Bitmap CreateBlurredImageFromBitmap(Context context, int radius, Bitmap image)
        {

            // Load a clean bitmap and work from that.
            Bitmap originalBitmap = image;


            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
            {
                try
                {
                    Bitmap blurredBitmap;
                    blurredBitmap = Bitmap.CreateBitmap(originalBitmap);                 
                    RenderScript rs = RenderScript.Create(context);               
                    Allocation input = Allocation.CreateFromBitmap(rs, originalBitmap, Allocation.MipmapControl.MipmapFull, AllocationUsage.Script);
                    Allocation output = Allocation.CreateTyped(rs, input.Type);              
                    ScriptIntrinsicBlur script = ScriptIntrinsicBlur.Create(rs, Element.U8_4(rs));
                    script.SetInput(input);                 
                    script.SetRadius(radius);            
                    script.ForEach(output);                  
                    output.CopyTo(blurredBitmap);
                    output.Dispose();                
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
        public static Bitmap CreateBlurredImageFromPortrait(Context context, int radius, string link)
        {
            Bitmap originalBitmap;
            if (link.ToLower().Contains("youtube.com"))
            {
                originalBitmap = BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]);
            }
            else
            {
                originalBitmap = BitmapFactory.DecodeFile(link);
            }

            return CreateBlurredImageFromBitmap(context, radius, originalBitmap);
        }

        public static Bitmap CreateBlurredImageFromUrl(Context context, int radius, string url)
        {
            WebClient client = new WebClient();
            var buffer = new byte[0];
            if (!url.StartsWith("https://i.ytimg.com/vi/"))
            {
                buffer = client.DownloadData("https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg");
            }
            else
            {
                buffer = client.DownloadData(url);
            }

            Bitmap originalBitmap = BitmapFactory.DecodeByteArray(buffer, 0, buffer.Length);

            return CreateBlurredImageFromBitmap(context, radius, originalBitmap);
        }
    }
}