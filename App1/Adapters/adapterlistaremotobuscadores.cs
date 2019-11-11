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
using Newtonsoft.Json;

namespace App1
{
    public class adapterlistaremotobuscadores : BaseAdapter
    {

        Context context;
        List<string> nombres = new List<string>();
        List<string> links = new List<string>();
        List<string> autoreses = new List<string>();
        List<string> duracioneses = new List<string>();
        //   List<Android.Graphics.Bitmap> imagenes = new List<Bitmap>();
        List<string> todoslosnombres = new List<string>();
        Dictionary<string, int> diccionario = new Dictionary<string, int>();
        public List<string> patheses = new List<string>();
        string linkactual = null;
        // int pos = 0;

        public void animar(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();



        }
        public adapterlistaremotobuscadores(Context context, List<string> nombreses, List<string> linkeses, List<string> autores, List<string> duracion, string linkactuall = null)
        {
            //List<Android.Graphics.Bitmap> imageneses

            this.context = context;
            nombres = nombreses;
            links = linkeses;
            linkactual = linkactuall;
            autoreses = autores;
            duracioneses = duracion;
           
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
            var view = convertView;
            adapterlistaremotobuscadoresViewHolder holder = null;

            if (view != null)
                holder = view.Tag as adapterlistaremotobuscadoresViewHolder;

            if (holder == null)
            {
                holder = new adapterlistaremotobuscadoresViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
               view = inflater.Inflate(Resource.Layout.layoutbuscadorconcarta, parent, false);
               holder.Title = view.FindViewById<TextView>(Resource.Id.textView);
                holder.Title2 = view.FindViewById<TextView>(Resource.Id.textView2);
                holder.Title3 = view.FindViewById<TextView>(Resource.Id.textView3);
                holder.portrait = view.FindViewById<ImageView>(Resource.Id.imageView);
              //  view.SetBackgroundResource(Resource.Drawable.drwaablegris);
               view.Tag = holder;
                /*   if (links.Contains(""))
                   {
                       links.Remove("");
                   }*/

                holder.portrait.SetTag(Resource.Id.imageView, links[position]);


                if (linkactual == null && linkactual != links[position])
                {

                    Glide.With(context)
                  .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                   .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                   .Into(holder.portrait);
                }
                else {

                    if (linkactual == links[position])
                    {
                        Glide.With(context)
                   .Load("http://gr3gorywolf.github.io/Multitubeweb/playcircle.png")
                    .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                  .Into(holder.portrait);
                    }
                    else {
                        Glide.With(context)
                       .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                        .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                       .Into(holder.portrait);
                    }
                    

                }




            }


            if (holder.portrait.GetTag(Resource.Id.imageView).ToString() != links[position])
            {
                try
                {
                    if (linkactual == null && linkactual != links[position])
                    {

                        Glide.With(context)
                      .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                       .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                       .Into(holder.portrait);
                    }
                    else
                    {

                        if (linkactual == links[position])
                        {
                            Glide.With(context)
                       .Load("http://gr3gorywolf.github.io/Multitubeweb/playcircle.png")
                        .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                      .Into(holder.portrait);
                        }
                        else
                        {
                            Glide.With(context)
                           .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                            .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                           .Into(holder.portrait);
                        }


                    }
                }
                catch (Exception) { }

            }




            holder.Title2.Text = autoreses[position];
            holder.Title3.Text = duracioneses[position];
            holder.Title.Text = nombres[position];

         
            holder.animar3(view);
            holder.portrait.SetTag(Resource.Id.imageView, links[position]);


            //fill in your items
            //holder.Title.Text = "new text here";



           /// clasesettings.recogerbasura();
            return view;
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
 
    class adapterlistaremotobuscadoresViewHolder : Java.Lang.Object
    {
        public TextView Title { get; set; }
        public TextView Title2 { get; set; }
        public TextView Title3 { get; set; }
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
        }
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}