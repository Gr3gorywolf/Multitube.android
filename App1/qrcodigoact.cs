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
using System.Net;
using System.Net.Sockets;
using Android.Graphics;
using System.Threading;
using Android.Support.Design.Widget;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class qrcodigoact : Activity
    {
    
        public string imagem;
        public string ipadre = "";
       public ImageView imagenview;

        Thread tr;
        TextView textboxtitulo;
        public ImageView playpause;
        public AlertDialog alerta;
        ListView lista;
        int countold = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Qrdialog);

           ipadre= Intent.GetStringExtra("ipadre");
          
            imagenview = new ImageView(this);
            FloatingActionButton btn = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton1);

            ImageView volver = FindViewById<ImageView>(Resource.Id.imageView4);
            imagenview.SetImageBitmap(GetQRCode());
            LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            LinearLayout ll2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            playpause = FindViewById<ImageView>(Resource.Id.imageView5);
            textboxtitulo = FindViewById<TextView>(Resource.Id.textView2);
           lista = FindViewById<ListView>(Resource.Id.listView1);
           // ll.SetBackgroundColor(Android.Graphics.Color.DarkGray);
          //  ll2.SetBackgroundColor(Android.Graphics.Color.Black);
          tr = new Thread(new ThreadStart(cojerstream));
            tr.Start();
            textboxtitulo.Selected = true;
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay dispositivos conectados.." });
            RunOnUiThread(() => {
                var parcelable = lista.OnSaveInstanceState();
                lista.Adapter = adaptadol;
                lista.OnRestoreInstanceState(parcelable);
            });
            alerta = new AlertDialog.Builder(this).SetView(imagenview)
                    .SetTitle("Vincular nuevo dispositivo").SetMessage("Para conectarse entre al modo control remoto de la app desde el otro dispositivo\n")
                        .SetPositiveButton("Entendido!", (ax, ass) => { alerta.Dismiss(); })               
                        .SetCancelable(false)
                        .Create();
            //    animar2(ll2);

            clasesettings.animarfab(btn);
            clasesettings.ponerfondoyactualizar(this);
            lista.ItemClick += (se, del) =>
            {
                
            };
            btn.Click += delegate
            {

                alerta.Show();
            };
            volver.Click += delegate
             {
                 animar(volver);
             
                 tr.Abort();
                 this.Finish();

             };
            playpause.Click += delegate
            {
                animar(playpause);
                mainmenu_Offline.gettearinstancia().play.PerformClick();
             

            };

         //   ll2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));



        }
       
        public override void OnBackPressed()
        {
          
            tr.Abort();
            this.Finish();
            clasesettings.recogerbasura();
        }
       
        public void animar(Java.Lang.Object imagen)
        {
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();
        }
        public void animar2(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(700);
            animacion.Start();

        }
        private Bitmap GetQRCode()
        {

            var writer = new ZXing.Mobile.BarcodeWriter();
            writer.Format = ZXing.BarcodeFormat.QR_CODE;
            writer.Options.Margin = 1;
            writer.Options.Height = 300;
            writer.Options.Width = 300;



         
       
            return writer.Write(ipadre);
        }

        public void cojerstream()
        {

          
         
                while (!this.IsDestroyed)
                {
                    if (mainmenu_Offline.gettearinstancia() != null)
                    {

                    if (mainmenu_Offline.gettearinstancia().buscando != true)
                    {
                        if (mainmenu_Offline.gettearinstancia().label.Text != textboxtitulo.Text 
                            && mainmenu_Offline.gettearinstancia().label.Text.Trim()!="")
                        {
                            RunOnUiThread(() => textboxtitulo.Text = mainmenu_Offline.gettearinstancia().label.Text);

                        }
                    }
                    else {
                        RunOnUiThread(() => textboxtitulo.Text = "Buscando...");
                    }

                    if (textboxtitulo.Text.Trim() == "" && textboxtitulo.Text.Trim() != "No hay elementos en cola")
                    {

                        RunOnUiThread(() => { textboxtitulo.Text = "No hay elementos en cola"; });
                    }




                    if (mainmenu_Offline.gettearinstancia().clienteses.Count > 0 )
                    {
                        try
                        {
                            var dummynames = mainmenu_Offline.gettearinstancia()
                                .clienteses.Select(ax => ((IPEndPoint)ax.Client.RemoteEndPoint).Address.ToString())
                                .Where(aax => aax != mainmenu_Offline.gettearinstancia().ipadre.Trim()).Distinct()
                                .ToList();
                            var dummylinks = dummynames.Select(ax => ax + "=sdss").ToList();

                            RunOnUiThread(() =>
                            {
                                if (dummynames.Count != countold)
                                {
                                    countold = dummynames.Count;
                                    adapterlistaremoto ada = new adapterlistaremoto(this, dummynames, dummylinks, null, Resource.Drawable.remotecontrolbuttons);

                                    var parcelable = lista.OnSaveInstanceState();
                                    lista.Adapter = ada;
                                    lista.OnRestoreInstanceState(parcelable);
                                }


                            });
                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        RunOnUiThread(() => {
                    var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay dispositivos conectados.." });

                            var parcelable = lista.OnSaveInstanceState();
                            lista.Adapter = adaptadol;
                            lista.OnRestoreInstanceState(parcelable);
                        });
                    }
                    }
                    else
                    if (mainmenu.gettearinstancia() != null)
                    {
                        if (mainmenu.gettearinstancia().label.Text != textboxtitulo.Text)
                        {
                            RunOnUiThread(() => textboxtitulo.Text = mainmenu.gettearinstancia().label.Text);
                        }
                    }
                    else
                    {

                    }


                    Thread.Sleep(1000);
                }
         
    


    }
}
}