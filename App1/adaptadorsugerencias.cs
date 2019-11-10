using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Glide;
using Android.Content;
using Android.Glide.Request;
using App1.Models;

namespace App1
{
    class adaptadorsugerencias : RecyclerView.Adapter
    {
        public event EventHandler<adaptadorsugerenciasClickEventArgs> ItemClick;
        public event EventHandler<adaptadorsugerenciasClickEventArgs> ItemLongClick;
        List<Suggestion> items;
        Context contexto;
        public adaptadorsugerencias(Context con,List<Suggestion> data)
        {
            items = data;
            contexto = con;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.layoutadaptadorsugerencias;
            itemView = LayoutInflater.From(parent.Context).
                  Inflate(id, parent, false);

            var vh = new adaptadorsugerenciasViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as adaptadorsugerenciasViewHolder;
            Glide.With(contexto)
                   .Load(item.Portrait)
                   .Apply(RequestOptions.CircleCropTransform().SkipMemoryCache(true).Override(60, 60).Placeholder(Resource.Drawable.image))
                   .Into(holder.imagen);
            holder.texto.Text = item.Name;
        }

        public override int ItemCount => items.Count;

        void OnClick(adaptadorsugerenciasClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(adaptadorsugerenciasClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class adaptadorsugerenciasViewHolder : RecyclerView.ViewHolder
    {
       public ImageView imagen { get; set; }
        public TextView texto { get; set; }

        public adaptadorsugerenciasViewHolder(View itemView, Action<adaptadorsugerenciasClickEventArgs> clickListener,
                            Action<adaptadorsugerenciasClickEventArgs> longClickListener) : base(itemView)
        {
            imagen = ItemView.FindViewById<ImageView>(Resource.Id.imageView) ;
            texto = ItemView.FindViewById<TextView>(Resource.Id.textview);
            itemView.Click += (sender, e) => clickListener(new adaptadorsugerenciasClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new adaptadorsugerenciasClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class adaptadorsugerenciasClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}