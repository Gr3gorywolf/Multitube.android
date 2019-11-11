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
using Android.Graphics;
using Android.Animation;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class actbio :Android.Support.V7.App.AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutbio);
            this.SetFinishOnTouchOutside(true);
            var github = FindViewById<ImageView>(Resource.Id.github);
            var webpage = FindViewById<ImageView>(Resource.Id.webpage);
            animacionzoonin(github, "scaleX");
            animacionzoonin(github, "scaleY");
            animacionzoonin(webpage, "scaleX");
            animacionzoonin(webpage, "scaleY");
            animacionfadein(FindViewById<ImageView>(Resource.Id.portada2), 1500);
            var action = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar);
            SetSupportActionBar(action);
            SupportActionBar.Title = "Acerca de";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            github.Click += delegate
            {
                var uri = Android.Net.Uri.Parse("https://github.com/Gr3gorywolf/Multitube.android");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

            };
            webpage.Click += delegate
            {
                var uri = Android.Net.Uri.Parse("https://gr3gorywolf.github.io/multitubepage.github.io/");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

            };
            
        }
        public void animacionzoonin(Java.Lang.Object objeto, string propiedad)
        {
            var anim = ObjectAnimator.OfFloat(objeto, propiedad, 0.3f, 1f);
            anim.SetDuration(2000);
        
            anim.Start();
        }
        public void animacionrepetitiva(Java.Lang.Object objeto, string propiedad)
        {
            var anim = ObjectAnimator.OfFloat(objeto, propiedad, 0.8f, 1f);
            anim.SetDuration(1000);
            anim.RepeatCount = int.MaxValue - 200;
            anim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
            anim.Start();
        }
        public void animacionfadein(Java.Lang.Object objeto, int duracion)
        {
            var anim = ObjectAnimator.OfFloat(objeto, "alpha", 0f, 1f);
            anim.SetDuration(duracion);
            anim.Start();
        }
        public override void OnBackPressed()
        {
      
            base.OnBackPressed();
        }
        public void animar3(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this.Finish();
                    StartActivity(new Intent(this, typeof(actsplashscreen)));
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}