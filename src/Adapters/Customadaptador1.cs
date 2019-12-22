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
using System.Net;
using System.IO;
using App1.Models;

namespace App1
{
    class Customadaptador1 : BaseAdapter
    {

        Context context;
        List<VideoImage> listaimagenes;
        bool cargarimageness = false;
        bool modomini = false;
      public  Customadaptador1ViewHolder holder;
        WebClient cliente = new WebClient();
        List<Android.Graphics.Bitmap> imageneses = new List<Bitmap>();
        public List<Android.Graphics.Bitmap> imagensblurr = new List<Bitmap>();
     
        List<Customadaptador1ViewHolder> losholders = new List<Customadaptador1ViewHolder>();

        public void animar5(View imagen, int pos, ImageView fondo, Bitmap img)
        {

            imagen.SetLayerType(LayerType.Hardware, null);
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(250);
            animacion.Start();
            animacion.AnimationEnd += delegate
            {
                imagen.SetLayerType(LayerType.None, null);
               


            };

        }
        public void animar6(View imagen)
        {

            imagen.SetLayerType(LayerType.Hardware, null);
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", 1000,0);
            animacion.SetDuration(150);
            animacion.Start();
            animacion.AnimationEnd += delegate
            {
                imagen.SetLayerType(LayerType.None, null);



            };

        }

        public Bitmap getRoundedShape(Bitmap scaleBitmapImage)
        {
            int targetWidth = 110;
            int targetHeight = 110;
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
        public Customadaptador1(Context context,List<VideoImage>imagenes, bool cargarimagenes,bool modominii, List<Android.Graphics.Bitmap> imagens, List<Android.Graphics.Bitmap> blurimgs)
        {
            cargarimageness = cargarimagenes;
            this.context = context;
            this.listaimagenes = imagenes;
            this.modomini = modominii;
            imageneses = imagens;
            imagensblurr = blurimgs;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            
            return null;
        }
        public void dentrodelthread()
        {

           
          

        }
        public override long GetItemId(int position)
        {
         
            return position;
        }
       
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            try
            {
                 holder = new Customadaptador1ViewHolder();

                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
             if (!modomini)
              {
                  view = inflater.Inflate(Resource.Layout.layout1, parent, false);
                    holder.fondo = view.FindViewById<ImageView>(Resource.Id.fondo1);
               }
             else
            {
                    view = inflater.Inflate(Resource.Layout.layout2, parent, false);
           }

                //replace with your item and your holder items
                //comment back in

                //holder.Title = 
                holder.imagen = view.FindViewById<ImageView>(Resource.Id.imageView1);
                holder.titulillo = view.FindViewById<TextView>(Resource.Id.textView1);
             //   holder.imagen = view.FindViewById<ImageView>(Resource.Id.imageView1);


                if (listaimagenes.Count > 1 && position <= listaimagenes.Count - 1)
                {


                    /*
                if(!File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + position))
                    {
                        Byte[] biteimagen = cliente.DownloadData(listaimagenes[position].imagen);

                 

                        Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeByteArray(biteimagen,0,biteimagen.Length);
                        var aa = File.Create(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + position);
                        aa.Write(biteimagen, 0, biteimagen.Length);
                        aa.Close();
                        holder.imagen.SetImageBitmap(imagen);
                        holder.titulillo.Text = listaimagenes[position].nombre;
                        biteimagen = new byte[0];
                    }
                    else
                    {
                        Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + position);
                        holder.imagen.SetImageBitmap(imagen);
                        holder.titulillo.Text = listaimagenes[position].nombre;
                    }*/
                    holder.titulillo.Text = listaimagenes[position].Name;
                 
      
                    if (cargarimageness)
                    {
                        holder.imagen.SetImageBitmap(imageneses[position]);

                    }
                  if (!modomini)
                    {
                        if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                        {

                            holder.fondo.SetImageBitmap(imagensblurr[position]);
                        }
                        animar5(holder.imagen, position, holder.fondo, imagensblurr[position]);
                    }else
                    {
                        animar6(holder.imagen);
                    }
                  
                }
             
             
                //fill in your items
                //holder.Title.Text = "new text here";


               
            }
            catch (Exception) { }
            return view;
        }

        //Fill in cound here, currently 0
        
        public override int Count
        {
            get { return listaimagenes.Count; }
        }

    }


    
    class Customadaptador1ViewHolder : Java.Lang.Object
    {
        public TextView titulillo;
        public ImageView imagen;
        public ImageView fondo;
        //Your adapter views to re-use
        //public TextView Title { get; set; }


    }
}