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
using Android.Graphics;
using System.Threading;
namespace App1
{
    [Activity(Label = "actvolumen", Icon = "@drawable/icon", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.UserDialogTraslucent")]
    public class actmenuvolumen : Activity
    {

        public ImageView bajo;
        public ImageView medio;
        public ImageView alto;
      
        public LinearLayout fondo;
        protected override void OnCreate(Bundle savedInstanceState)
        {      
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.menuvolumen);
            bajo = FindViewById<ImageView>(Resource.Id.imageView1);
            medio = FindViewById<ImageView>(Resource.Id.imageView2);
            alto = FindViewById<ImageView>(Resource.Id.imageView3);
            fondo = FindViewById<LinearLayout>(Resource.Id.fondo1);
            fondo.SetBackgroundColor(Color.ParseColor("#2b2e30"));
            string ip = Intent.GetStringExtra("ipadre");
            this.SetFinishOnTouchOutside(true);
            
            ////////////////////////////////////////clicks//////////////
            bajo.Click += delegate
            {
                animar(bajo);

                if (Mainmenu.gettearinstancia() != null)
                    Mainmenu.gettearinstancia().clientela.Client.Send(Encoding.UTF8.GetBytes("vol0()"));
                else
                 if (Clouding_service.gettearinstancia() != null)
                {
                    YoutubePlayerServerActivity.gettearinstancia().volumenactual = 0;
                    Clouding_service.gettearinstancia().musicaplayer.SetVolume(0f, 0f);
                    YoutubePlayerServerActivity.gettearinstancia(). imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumelowrojo);
                }
               
                this.Finish();
                ;
          };
            medio.Click += delegate
            {
                animar(medio);
                if (Mainmenu.gettearinstancia() != null)
                    Mainmenu.gettearinstancia().clientela.Client.Send(Encoding.UTF8.GetBytes("vol50()"));
                else
                  if (Clouding_service.gettearinstancia() != null) {
                    YoutubePlayerServerActivity.gettearinstancia().volumenactual = 50;
                    Clouding_service.gettearinstancia().musicaplayer.SetVolume(0.5f, 0.5f);
                    YoutubePlayerServerActivity.gettearinstancia(). imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumemediumrojo);

                }
                this.Finish();
            };
            alto.Click += delegate
            {
                animar(alto);
                if (Mainmenu.gettearinstancia() != null)
                    Mainmenu.gettearinstancia().clientela.Client.Send(Encoding.UTF8.GetBytes("vol100()"));
                else
                      if (Clouding_service.gettearinstancia() != null) {
                    YoutubePlayerServerActivity.gettearinstancia().volumenactual = 100;
                    Clouding_service.gettearinstancia().musicaplayer.SetVolume(1f, 1f);
                    YoutubePlayerServerActivity.gettearinstancia(). imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumehighrojo);
                }

                this.Finish();
            };


        }

        protected override void OnDestroy()
        {



            if (YoutubePlayerServerActivity.gettearinstancia() != null) {

                if (YoutubePlayerServerActivity.gettearinstancia().spSlidingPanel.IsExpanded) {
                    var inst = YoutubePlayerServerActivity.gettearinstancia();
                    if (inst.volumenactual == 0)
                        inst.imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumelow);
                    else
         if (inst.volumenactual == 50)
                        inst.imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumemedium);
                    else
         if (inst.volumenactual == 100)
                        inst.imgSlideDownAction.SetBackgroundResource(Resource.Drawable.volumehigh);

                }
            }

            Thread.Sleep(200);
         
            base.OnDestroy();
        }
        public void animar(Java.Lang.Object imagen)
        {
            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(300);
            animacion.Start();
            Android.Animation.ObjectAnimator animacion2 = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleY", 0.5f, 1f);
            animacion2.SetDuration(300);
            animacion2.Start();
        }
    }
}