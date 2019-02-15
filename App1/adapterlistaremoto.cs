using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Graphics;
using System.Net;
using System.Threading;
//using Square.Picasso;
using Android.Graphics.Drawables;
using Android.Glide;
using Android.Glide.Request;

namespace App1
{
    public class adapterlistaremoto : BaseAdapter
    {

        Context context;
        List<string> nombres = new List<string>();
        List<string> links = new List<string>();
        //   List<Android.Graphics.Bitmap> imagenes = new List<Bitmap>();
        List<string> todoslosnombres = new List<string>();
        Dictionary<string, int> diccionario = new Dictionary<string, int>();
        public List<string> patheses = new List<string>();
        string linkactual = null;
        int newplaceholder = 0;
        // int pos = 0;

        public void animar(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();



        }
        public adapterlistaremoto(Context context, List<string> nombreses, List<string> linkeses, string linkactuall = null, int placeholderid = 0)
        {
            //List<Android.Graphics.Bitmap> imageneses

            this.context = context;
            nombres = nombreses;
            links = linkeses;
            linkactual = linkactuall;
            newplaceholder = placeholderid;
            try {
                Glide.Get(context).ClearMemory();
            }
            catch (Exception) { 
                }
          
          //  imagenes = imageneses;

        }
        protected override void Dispose(bool disposing)
        {

           
            base.Dispose(disposing);
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            int placeholder = 0;
         
            adaptadorlistaremotoViewHolder holder = null;
            if (newplaceholder != 0) {

                placeholder = newplaceholder;
            }
            else {
                placeholder = Resource.Drawable.image;


                }
            if (convertView != null)
                holder = convertView.Tag as adaptadorlistaremotoViewHolder;

            if (holder == null)
            {
                holder = new adaptadorlistaremotoViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
               convertView = inflater.Inflate(Resource.Layout.layoutlistaplayerindependiente, parent, false);
               holder.Title = convertView.FindViewById<TextView>(Resource.Id.textView1);
               holder.portrait = convertView.FindViewById<ImageView>(Resource.Id.imageView1);

              //  view.SetBackgroundResource(Resource.Drawable.drwaablegris);
               convertView.Tag = holder;
                /*   if (links.Contains(""))
                   {
                       links.Remove("");
                   }*/

                holder.portrait.SetTag(Resource.Id.imageView1, links[position]);

        
                    if (linkactual == null || linkactual.Trim()=="")
                {
                    if (links[position].Trim() != "")
                    {
                        Glide.With(context)
                      .Load("http://i.ytimg.com/vi/" + links[ position].Split('=')[1] + "/mqdefault.jpg")
                      .Apply(RequestOptions.CircleCropTransform().Placeholder(placeholder))
                       .Into(holder.portrait);
                    }
                    else {
                        Glide.With(context)
                      .Load("")
                      .Apply(RequestOptions.CircleCropTransform().Placeholder(placeholder))
                       .Into(holder.portrait);
                    }
                }
                else {

                    if (linkactual.Split('=')[1] == links[position].Split('=')[1])
                    {
                        
                        Glide.With(context)
                         
                   .Load("https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/Updates/playinganimation.gif")
                  
                     .Apply(RequestOptions.NoTransformation().Placeholder(placeholder))
                  .Into(holder.portrait);
                    }
                    else {
                        Glide.With(context)
                       .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                       .Apply(RequestOptions.CircleCropTransform().Placeholder(placeholder))
                       .Into(holder.portrait);
                    }
                    

                }




            }

          //  Console.WriteLine(linkactual + ">>>" + links[position]);
            if (holder.portrait.GetTag(Resource.Id.imageView1).ToString() != links[position])
            {
                try
                {
                    if (linkactual == null || linkactual.Trim() == "")
                    {

                        if (links[position].Trim() != "")
                        {
                            Glide.With(context)
                          .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                          .Apply(RequestOptions.CircleCropTransform().Placeholder(placeholder))
                           .Into(holder.portrait);
                        }
                        else
                        {
                            Glide.With(context)
                          .Load("")
                          .Apply(RequestOptions.CircleCropTransform().Placeholder(placeholder))
                           .Into(holder.portrait);
                        }
                    }
                    else
                    {

                        if (linkactual.Split('=')[1] == links[position].Split('=')[1])
                        {
                         
                            Glide.With(context)
                       .Load("https://raw.githubusercontent.com/Gr3gorywolf/Multitube.android/master/Updates/playinganimation.gif")
                        .Apply(RequestOptions.NoTransformation().Placeholder(placeholder))
                      .Into(holder.portrait);

                        }
                        else
                        {
                            Glide.With(context)
                           .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                           .Apply(RequestOptions.CircleCropTransform().Placeholder(placeholder))
                           .Into(holder.portrait);
                        }


                    }
                }
                catch (Exception) { }

            }




           
            holder.Title.Text = nombres[position].Replace(">", "").Replace("<", "");

          //  holder.animar3((View)convertView);
            holder.portrait.SetTag(Resource.Id.imageView1, links[position]);


            //fill in your items
            //holder.Title.Text = "new text here";



           /// clasesettings.recogerbasura();
            return convertView;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return nombres.Count;
            }
        }

    }
   
    class adaptadorlistaremotoViewHolder : Java.Lang.Object
    {
        public TextView Title { get; set; }
        public ImageView portrait { get; set; }

        public void animar3(View imagen)
        {
            imagen.SetLayerType(LayerType.Hardware, null);
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "alpha", 0f, 1f);
            animacion.SetDuration(220);
            animacion.Start();
            animacion.AnimationEnd += delegate
            {
                imagen.SetLayerType(LayerType.None, null);
            };
            //Your adapter views to re-use
            //public TextView Title { get; set; }
        }
    }
}