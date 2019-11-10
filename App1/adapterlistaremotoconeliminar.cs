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
using App1.Utils;

namespace App1
{
    public class adapterlistaremotoconeliminar : BaseAdapter
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
       
        public string nombresito = "pelo";
        public bool listacompletai;
        public bool solomodifyelarray;
        string listaactual = "";
        // int pos = 0;
        public int pos = 0;
        public void ok(object sender, EventArgs e)
        {
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listaactual);
            nombres.RemoveAt(pos);
           links.RemoveAt(pos);
            string parte1 = "";

            string parte2 = "";
            foreach (string aagg in nombres)
            {
                parte1 += aagg + ";";
            }
            foreach (string sss in links)
            {
                parte2 += sss + ";";
            }


            var aa = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listaactual);
            aa.Write(parte1 + "$" + parte2);
            aa.Close();


            SettingsHelper.SaveSetting("refrescarlistadatos", "ok");
            Toast.MakeText(context, "Elemento eliminado satisfactoriamente", ToastLength.Long).Show();
        }
        public void ok3(object sender, EventArgs e)
        {

            nombres.RemoveAt(pos);
            links.RemoveAt(pos);





            this.NotifyDataSetChanged();

            Toast.MakeText(context, "Elemento eliminado satisfactoriamente", ToastLength.Long).Show();
        }
        public void ok2(object sender, EventArgs e)
        {
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + nombres[pos]);
           nombres.RemoveAt(pos);
            links.RemoveAt(pos);

            this.NotifyDataSetChanged();

            Toast.MakeText(context, "Elemento eliminado satisfactoriamente", ToastLength.Long).Show();
        }

        public void no(object sender, EventArgs e)
        {

        }


        public void animar(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();



        }
        public adapterlistaremotoconeliminar(Context context, List<string> nombreses, List<string> linkeses, string nombredeladlista, bool listacompleta, bool solomodifyelarrayy)
        {
            //List<Android.Graphics.Bitmap> imageneses

            this.context = context;
            nombres = nombreses;
            links = linkeses;
          
            listaactual = nombredeladlista;
            solomodifyelarray = solomodifyelarrayy;
            this.listacompletai = listacompleta;
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
            adapterlistaremotoconeliminarViewHolder holder = null;

            if (view != null)
                holder = view.Tag as adapterlistaremotoconeliminarViewHolder;

            if (holder == null)
            {
                holder = new adapterlistaremotoconeliminarViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
               view = inflater.Inflate(Resource.Layout.layoutlistaplayerindependienteconmenu, parent, false);
               holder.Title = view.FindViewById<TextView>(Resource.Id.textView1);
              holder.boton = view.FindViewById<ImageView>(Resource.Id.imageView2);
                holder.portrait = view.FindViewById<ImageView>(Resource.Id.imageView1);
                holder.boton.Click += (aadfd, aaaa) =>
                {




                    PopupMenu menu = new PopupMenu(context, holder.boton);
                menu.Inflate(Resource.Drawable.menupopup);

                    menu.MenuItemClick += (s1, arg1) => {


                        pos = (int)(((ImageView)aadfd).GetTag(Resource.Id.imageView2));

                        AlertDialog.Builder ad = new AlertDialog.Builder(context);
                        ad.SetCancelable(false);
                        if (listacompletai && !solomodifyelarray)
                        {
                            ad.SetMessage("Esta seguro que desea eliminar la lista de reproduccion " + nombres[pos] + "??");
                        }
                        else
                          if (!listacompletai && !solomodifyelarray)
                        {
                            ad.SetMessage("Esta seguro que desea eliminar " + nombres[pos] + " de la lista de reproduccion " + listaactual + "??");
                        }
                        else
                        if (solomodifyelarray)
                        {
                            ad.SetMessage("Esta seguro que desea remover el elemento? " + nombres[pos]);
                        }
                        ad.SetTitle("Advertencia");
                        ad.SetIcon(Resource.Drawable.alert);
                        if (listacompletai && !solomodifyelarray)
                        {
                            ad.SetPositiveButton("Si", ok2);
                        }
                        else
                          if (!listacompletai && !solomodifyelarray)
                        {
                            ad.SetPositiveButton("Si", ok);
                        }
                        else
                            if (solomodifyelarray)
                        {
                            ad.SetPositiveButton("Si", ok3);
                        }

                        ad.SetNegativeButton("No", no);

                        ad.Create();
                        ad.Show();










                    };

                  
                    menu.Show();





                  


                };

                //  view.SetBackgroundResource(Resource.Drawable.drwaablegris);
                view.Tag = holder;
                /*   if (links.Contains(""))
                   {
                       links.Remove("");
                   }*/

                holder.portrait.SetTag(Resource.Id.imageView1, links[position]);


                try
                {
                    Glide.With(context)
               .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                .Apply(RequestOptions.CircleCropTransform().Placeholder(Resource.Drawable.image))
                .Into(holder.portrait);
                }
                catch (Exception) {
                    Glide.With(context)
               .Load("http://gr3gorywolf.github.io/Multitubeweb/lista.png")
                .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
                .Into(holder.portrait);
                }
                 
           




            }


            if (holder.portrait.GetTag(Resource.Id.imageView1).ToString() != links[position])
            {
                try
                {
                   
                        Glide.With(context)
                      .Load("http://i.ytimg.com/vi/" + links[position].Split('=')[1] + "/mqdefault.jpg")
                       .Apply(RequestOptions.CircleCropTransform().Placeholder(Resource.Drawable.image))
                       .Into(holder.portrait);
                   
                }
                catch (Exception) {
                    Glide.With(context)
              .Load("http://gr3gorywolf.github.io/Multitubeweb/lista.png")
               .Apply(RequestOptions.NoTransformation().Placeholder(Resource.Drawable.image))
               .Into(holder.portrait);
                }

            }



           
            holder.Title.Text = nombres[position];

         
            holder.animar3(view);
            holder.portrait.SetTag(Resource.Id.imageView1, links[position]);
            holder.boton.SetTag(Resource.Id.imageView2, position);

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
   
    class adapterlistaremotoconeliminarViewHolder : Java.Lang.Object
    {
        public TextView Title { get; set; }
        public ImageView boton { get; set; }

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