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
namespace App1
{
    class adapterlistaoffline : BaseAdapter
    {

        Context context;
        List<string> nombres = new List<string>();
        List<string> links = new List<string>();
     //   List<Android.Graphics.Bitmap> imagenes = new List<Bitmap>();
        List<string> todoslosnombres = new List<string>();
        Dictionary<string, int> diccionario = new Dictionary<string, int>();
        public List<string> patheses = new List<string>();
        int pos = 0;
       
        public void animar(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();



        }
        public adapterlistaoffline(Context context,List<string>nombreses,List<string>linkeses,string filtroo,List<string> todosnombres,Dictionary<string,int>dicci,List<string>pathesess)
        {
            //List<Android.Graphics.Bitmap> imageneses
            this.context = context;
            nombres = nombreses;
            links = linkeses;
          //  imagenes = imageneses;
            todoslosnombres = todosnombres;
            diccionario = dicci;
            patheses = pathesess;
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
            adapterlistaofflineViewHolder holder = null;

            if (view != null)
                holder = view.Tag as adapterlistaofflineViewHolder;

            if (holder == null)
            {
                holder = new adapterlistaofflineViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
               view = inflater.Inflate(Resource.Layout.layoutlistaplayerindependiente, parent, false);
               holder.Title = view.FindViewById<TextView>(Resource.Id.textView1);
               holder.portrait = view.FindViewById<ImageView>(Resource.Id.imageView1);
              //  view.SetBackgroundResource(Resource.Drawable.drwaablegris);
               view.Tag = holder;
                holder.portrait.Click += (aaas, adfas) => {
                    animar(holder.portrait);
                    pos = (int)(((ImageView)aaas).GetTag(Resource.Id.imageView1));
                    using (Intent intento = new Intent(context,typeof(actividadadinfooffline)))
                    {
                        intento.PutExtra("nombre", nombres[pos]);
                        intento.PutExtra("link", links[todoslosnombres.IndexOf(nombres[pos])]);
                        intento.PutExtra("path", patheses[todoslosnombres.IndexOf(nombres[pos])]);
                        Thread.Sleep(50);
                        context.StartActivity(intento);

                    }

                };

            }
            holder.Title.Text = nombres[position];
            try
            {
            
                    holder.portrait.SetImageBitmap(playeroffline.gettearinstancia().imageneses.First(info => info.GenerationId == diccionario[nombres[position]]));
                  

               
                      
                 }
                catch (Exception e)
            {
                var eo = e;
                
                    holder.portrait.SetImageResource(Resource.Drawable.musicalnote);
                   
               
                eo = null;
            }
          holder.animar3(holder.portrait);
            holder.portrait.SetTag(Resource.Id.imageView1, position);


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
   
    class adapterlistaofflineViewHolder : Java.Lang.Object
    {
        public TextView Title { get; set; }
        public ImageView portrait { get; set; }

        public void animar3(View imagen)
        {
            imagen.SetLayerType(LayerType.Hardware, null);
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationX", 1000, 0);
            animacion.SetDuration(250);
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