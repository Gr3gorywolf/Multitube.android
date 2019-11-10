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
using System.Net.Sockets;
using System.Threading;
using Android.Graphics;

using System.Net;
using System.Net.Http;
using System.IO;
using App1.Utils;

namespace App1
{
   public class adaptadorlista : BaseAdapter
    {

        Context context;
        List<string> listilla = new List<string>();
        List<string> linkeses = new List<string>();
        public string nombresito = "pelo";
        public bool listacompletai;
        public bool solomodifyelarray;
        string listaactual = "";
      
        public int pos = 0;
        public void ok(object sender, EventArgs e)
        {
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listaactual);
            listilla.RemoveAt(pos);
            linkeses.RemoveAt(pos);
            string parte1 = "";
      
            string parte2 = "";
            foreach (string aagg in listilla)
            {
                parte1+= aagg + ";";
            }
            foreach(string sss in linkeses)
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
        
            listilla.RemoveAt(pos);
            linkeses.RemoveAt(pos);
        

    

         
            this.NotifyDataSetChanged();

            Toast.MakeText(context, "Elemento eliminado satisfactoriamente", ToastLength.Long).Show();
        }
        public void ok2(object sender, EventArgs e)
        {
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listilla[pos]);
            listilla.RemoveAt(pos);
            linkeses.RemoveAt(pos);
          
            this.NotifyDataSetChanged();

            Toast.MakeText(context, "Elemento eliminado satisfactoriamente", ToastLength.Long).Show();
        }

        public  void no(object sender, EventArgs e)
        {
          
        }

        public adaptadorlista(Context context,List<string>listaelementos, List<string> listaliks,string nombredeladlista,bool listacompleta,bool solomodifyelarrayy)
        {
      
            this.context = context;

            listilla = listaelementos;
            linkeses = listaliks;
            listaactual = nombredeladlista;
            solomodifyelarray = solomodifyelarrayy;
            this.listacompletai = listacompleta;
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
            adaptadorlistaViewHolder holder = null;

            if (view != null)
                holder = view.Tag as adaptadorlistaViewHolder;

            if (holder == null)
            {
                holder = new adaptadorlistaViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.layoutlistilla, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.textView1);
                holder.botoneliminar= view.FindViewById<ImageView>(Resource.Id.imageView1);
              

                holder.botoneliminar.Click += (aadfd, aaaa) =>
                {


                    pos = (int)(((ImageView)aadfd).GetTag(Resource.Id.imageView1));

                    AlertDialog.Builder ad = new AlertDialog.Builder(context);
                    ad.SetCancelable(false);
                    if (listacompletai && !solomodifyelarray)
                    {
                        ad.SetMessage("Esta seguro que desea eliminar la lista de reproduccion " + listilla[pos] + "??");
                    }
                    else
                      if (!listacompletai && !solomodifyelarray)
                    {
                        ad.SetMessage("Esta seguro que desea eliminar " + listilla[pos] + " de la lista de reproduccion " + listaactual + "??");
                    }
                    else
                    if ( solomodifyelarray)                 
                    {
                        ad.SetMessage("Esta seguro que desea remover el elemento? " + listilla[pos]);
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
                        if ( solomodifyelarray)
                    {
                        ad.SetPositiveButton("Si", ok3);
                    }
                 
                    ad.SetNegativeButton("No", no);
                 
                    ad.Create();
                    ad.Show();


                };

                view.Tag = holder;
            }
            else
            {
             
                //   holder.botoneliminar = view.FindViewById<ImageView>(Resource.Id.imageView1);
            

            }
            //fill in your items
            holder.botoneliminar.SetTag(Resource.Id.imageView1, position);
            holder.Title.Text = listilla[position];

          //  holder.botoneliminar.SetTag(Resource.Id.imageView1, position);
            return view;
        }

        //Fill in cound here, currently 0
      
        public override int Count
        {
            get
            {
                return listilla.Count;
              
            }
        }

    }
    

    class adaptadorlistaViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
       
        public TextView Title { get; set; }
        public ImageView botoneliminar { get; set; }
    }
}