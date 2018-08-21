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
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.Dialog.NoActionBar")]
    public class actbio : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutbio);
            var textoanimar1 = FindViewById<TextView>(Resource.Id.textView9);
            var textoanimar2 = FindViewById<TextView>(Resource.Id.textView2);
            var textoanimar3 = FindViewById<TextView>(Resource.Id.textView4);
            var textoanimar4 = FindViewById<TextView>(Resource.Id.textView6);
            var textoanimar5= FindViewById<TextView>(Resource.Id.textView8);
            var layoutanimar1 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            var layoutanimar2 = FindViewById<LinearLayout>(Resource.Id.linearLayout5);
            var layoutanimar3 = FindViewById<LinearLayout>(Resource.Id.linearLayout6);
            var layoutanimar4 = FindViewById<LinearLayout>(Resource.Id.linearLayout7);
            var imageview1 = FindViewById<ImageView>(Resource.Id.imageView2);
            var botoncerrar = FindViewById<ImageView>(Resource.Id.imageView1);
            var fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            //////////////////////////////////////////////////////////////////////
            this.SetFinishOnTouchOutside(false);
            fondo.SetImageBitmap(clasesettings.CreateBlurredImageformbitmap(this,20,BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icona)));
            imageview1.SetImageBitmap(clasesettings.getRoundedShape(BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icona)));
            imageview1.Visibility = ViewStates.Gone;
            botoncerrar.Click += delegate
            {
                clasesettings.recogerbasura();
                this.Finish();
            };

            textoanimar2.Selected = true;
            textoanimar3.Selected = true;
            textoanimar4.Selected = true;
            textoanimar5.Selected = true;
            animar3(layoutanimar1);
            animar3(layoutanimar2);
            animar3(layoutanimar3);
            animar3(layoutanimar4);
            animar3(imageview1);
            animar3(fondo);

            // Create your application here
        }
        public override void OnBackPressed()
        {
            clasesettings.preguntarsimenuosalir(this);
            base.OnBackPressed();
        }
        public void animar3(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }

    }
}