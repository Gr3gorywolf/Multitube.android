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
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class qrcodigoact : Activity
    {
    
        public string imagem;
        public string ipadre = "";
       public ImageView imagenview;
        public TcpClient cliet;
        Thread tr;
        TextView textboxtitulo;
        public ImageView playpause;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Qrdialog);

           ipadre= Intent.GetStringExtra("ipadre");
            cliet = new TcpClient(ipadre, 1024);
            imagenview = FindViewById<ImageView>(Resource.Id.imageView1);    
            ImageView volver = FindViewById<ImageView>(Resource.Id.imageView4);
            imagenview.SetImageBitmap(GetQRCode());
            LinearLayout ll = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            LinearLayout ll2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            playpause = FindViewById<ImageView>(Resource.Id.imageView5);
            textboxtitulo = FindViewById<TextView>(Resource.Id.textView2);
           // ll.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            ll2.SetBackgroundColor(Android.Graphics.Color.Black);
          tr = new Thread(new ThreadStart(cojerstream));
            tr.Start();
            textboxtitulo.Selected = true;
            animar2(ll2);


            clasesettings.ponerfondoyactualizar(this);
            volver.Click += delegate
             {
                 animar(volver);
                 cliet.Client.Disconnect(false);
                 tr.Abort();
                 this.Finish();

             };
            playpause.Click += delegate
            {
                animar(playpause);

                cliet.Client.Send(Encoding.Default.GetBytes("playpause()"));

            };

            ll2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
        


        }
        public override void OnBackPressed()
        {
            cliet.Client.Disconnect(false);
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

          
         
                while (cliet.Client.Connected)
                {
                    if (mainmenu_Offline.gettearinstancia() != null)
                    {
                        if (mainmenu_Offline.gettearinstancia().label.Text != textboxtitulo.Text)
                        {
                            RunOnUiThread(() => textboxtitulo.Text = mainmenu_Offline.gettearinstancia().label.Text);
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