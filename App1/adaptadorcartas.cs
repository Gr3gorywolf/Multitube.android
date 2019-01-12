using System;
using Android.Glide;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Content;
namespace App1
{
    class adaptadorcartas : RecyclerView.Adapter
    {
        public event EventHandler<adaptadorcartasClickEventArgs> ItemClick;
        public event EventHandler<adaptadorcartasClickEventArgs> ItemLongClick;
       List<playlistelements> items;
        Context contexto;
        public adaptadorcartas(List<playlistelements> data,Context cont)
        {
            items = data;
            contexto = cont;
        }

     
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

       
            View itemView = null;
            
            itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.layoutcartas, parent, false);

            var vh = new adaptadorcartasViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

          
            var holder = viewHolder as adaptadorcartasViewHolder;
            holder.texto.Text = items[position].nombre;
            Glide.With(contexto)
                .Load("http://i.ytimg.com/vi/" + items[position].link.Split('=')[1] + "/mqdefault.jpg")
                .Into(holder.imagen);
        }

        public override int ItemCount => items.Count;

        void OnClick(adaptadorcartasClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(adaptadorcartasClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class adaptadorcartasViewHolder : RecyclerView.ViewHolder
    {
      
        public TextView texto { get; set; }
        public ImageView imagen { get; set; }

        public adaptadorcartasViewHolder(View itemView, Action<adaptadorcartasClickEventArgs> clickListener,
                            Action<adaptadorcartasClickEventArgs> longClickListener) : base(itemView)
        {
            texto = itemView.FindViewById<TextView>(Resource.Id.textView) ;
            imagen= itemView.FindViewById<ImageView>(Resource.Id.imageView);
            itemView.Click += (sender, e) => clickListener(new adaptadorcartasClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new adaptadorcartasClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class adaptadorcartasClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}