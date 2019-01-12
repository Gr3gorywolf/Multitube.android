using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.OS;
using Android.App;
using Android.Content;
using System.IO;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Threading;
using Android.Media;
using Android.Renderscripts;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.UserDialog")]
    public class actividadadinfooffline : Activity
    {
        MediaPlayer musicaplayer = new MediaPlayer();
        SeekBar barra;
        ImageView playpause;
        bool detenedor = true;
        public bool playerseteado = false;
        public bool estabareproduciendo = false;
        ImageView fondo;
       LinearLayout link;
        string linkvid = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutinfooffline);
            //////////////////////////////////////mapeo
            var nombre = FindViewById<TextView>(Resource.Id.textView3);
           link = FindViewById<LinearLayout>(Resource.Id.boton2);
            var foto = FindViewById<ImageView>(Resource.Id.imageView2);
            fondo = FindViewById<ImageView>(Resource.Id.imageView10);
            var cerrar = FindViewById<ImageView>(Resource.Id.imageView1);
            playpause = FindViewById<ImageView>(Resource.Id.imageView3);
            barra= FindViewById<SeekBar>(Resource.Id.seekBar1);
            var botoncarpeta = FindViewById<LinearLayout>(Resource.Id.boton1);
          //  var reproducirext = FindViewById<ImageView>(Resource.Id.imageView11);

            ///////////////////////////////////////////////////////
            this.SetFinishOnTouchOutside(true);
            nombre.Text = Intent.GetStringExtra("nombre");
          linkvid = Intent.GetStringExtra("link");
            nombre.Selected = true;
            link.Selected = true;


            
                if (Clouding_service.gettearinstancia() != null)
                {
                    estabareproduciendo = Clouding_service.gettearinstancia().musicaplayer.IsPlaying;

                }
                else
                if (Clouding_serviceoffline.gettearinstancia() != null)
                {
                    estabareproduciendo = Clouding_serviceoffline.gettearinstancia().musicaplayer.IsPlaying;
                }

                /*
                            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                            {

                                fondo.SetImageBitmap(CreateBlurredImage(20, link.Text));
                            }

                    */


                playpause.SetBackgroundResource(Resource.Drawable.playbutton2);
                using (var imagen = BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + linkvid.Split('=')[1]))
                {
                    foto.SetImageBitmap(getRoundedShape(imagen));
                }

                animar5(foto);
                new Thread(() =>
                {
                    ponerporciento();
                }).Start();

                //////////////////////////////////////clicks
                barra.ProgressChanged += (aass, asda) =>
                 {
                     if (asda.FromUser)
                     {
                         musicaplayer.SeekTo(asda.Progress);
                     }
                 };
            link.Click += delegate
            {
             
                var uri = Android.Net.Uri.Parse(linkvid);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

            };
                botoncarpeta.Click += delegate
                {
                    string path = Intent.GetStringExtra("path");

                // Verify the intent will resolve to at least one activity if (intent.resolveActivity(getPackageManager()) != null) { startActivity(chooser); } 



                Intent intentssdd = new Intent(Intent.ActionView);
                    intentssdd.SetDataAndType(Android.Net.Uri.Parse(path), "resource/folder");
                    Intent chooser = Intent.CreateChooser(intentssdd, "tumama");

                    if (intentssdd.ResolveActivityInfo(PackageManager, 0) != null)
                    {
                        StartActivity(chooser);
                    }
                    else
                    {

                        Toast.MakeText(this, "No hay explorador de archivos valido en su sistema android", ToastLength.Long).Show();
                    }

                };

                cerrar.Click += delegate
                {
                    this.Finish();
                };
                playpause.Click += delegate
                {
                    if (playerseteado)
                    {
                        if (musicaplayer.IsPlaying)
                        {
                            playpause.SetBackgroundResource(Resource.Drawable.playbutton2);
                            musicaplayer.Pause();
                            if (estabareproduciendo)
                            {

                                if (Clouding_service.gettearinstancia() != null)
                                {
                                    Clouding_service.gettearinstancia().musicaplayer.Start();

                                }
                                else
                               if (Clouding_serviceoffline.gettearinstancia() != null)
                                {
                                    Clouding_serviceoffline.gettearinstancia().musicaplayer.Start();

                                }


                            }

                        }
                        else
                        {
                            playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
                            musicaplayer.Start();

                            if (estabareproduciendo)
                            {

                                if (Clouding_service.gettearinstancia() != null)
                                {
                                    Clouding_service.gettearinstancia().musicaplayer.Pause();

                                }
                                else
                               if (Clouding_serviceoffline.gettearinstancia() != null)
                                {
                                    Clouding_serviceoffline.gettearinstancia().musicaplayer.Pause();

                                }


                            }




                        }

                    }
                    else
                    {
                        reproducir(Intent.GetStringExtra("path"));
                    }

                };

                // Create your application here
            }
                


        public void animar5(View imagen)
        {
            RunOnUiThread(() =>
            {
                imagen.SetLayerType(LayerType.Hardware, null);
                Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
                animacion.SetDuration(500);
                animacion.Start();
                animacion.AnimationEnd += delegate
                {
                    imagen.SetLayerType(LayerType.None, null);
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1)
                    {

                        fondo.SetImageBitmap(CreateBlurredImage(20, linkvid));
                    }


                };
            });
        }



        public override void Finish()
        {
            base.Finish();
            musicaplayer.Reset();
            detenedor = false;

            if (estabareproduciendo)
            {

                if (Clouding_service.gettearinstancia() != null)
                {
                    Clouding_service.gettearinstancia().musicaplayer.Start();

                }
                else
               if (Clouding_serviceoffline.gettearinstancia() != null)
                {
                    Clouding_serviceoffline.gettearinstancia().musicaplayer.Start();

                }


            }


            clasesettings.recogerbasura();
        }
        public void ponerporciento()
        {
            while (detenedor)
            {
                if (musicaplayer.IsPlaying)
                {
                    RunOnUiThread(() => barra.Max = musicaplayer.Duration);
                    RunOnUiThread(() => barra.Progress = musicaplayer.CurrentPosition);

                }

                Thread.Sleep(1000);
            }
        }


        private Bitmap CreateBlurredImage(int radius, string link)
        {
          
            // Load a clean bitmap and work from that.
            Bitmap originalBitmap = BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/portraits/" + link.Split('=')[1]);

        

            return originalBitmap;
        }

        public void reproducir(string downloadurl)
        {
            musicaplayer.Reset();


            musicaplayer = Android.Media.MediaPlayer.Create(this, Android.Net.Uri.Parse(downloadurl));

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            musicaplayer.SetAudioStreamType(Android.Media.Stream.Music);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            musicaplayer.Start();
            ///  musicaplayer.Start();
            ///  
            if (estabareproduciendo)
            {

                if (Clouding_service.gettearinstancia() != null)
                {
                    Clouding_service.gettearinstancia().musicaplayer.Pause();

                }
                else
               if (Clouding_serviceoffline.gettearinstancia() != null)
                {
                    Clouding_serviceoffline.gettearinstancia().musicaplayer.Pause();

                }


            }
            playpause.SetBackgroundResource(Resource.Drawable.pausebutton2);
            musicaplayer.Completion += delegate
            {
                playpause.SetBackgroundResource(Resource.Drawable.playbutton2);
                barra.Progress = 0;
            };
            playerseteado = true;

        }
        public Bitmap getRoundedShape(Bitmap scaleBitmapImage)
        {
            try
            {
                int targetWidth = 480;
                int targetHeight =480;
                Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                        targetHeight, Bitmap.Config.Argb8888);

                
                Canvas canvas = new Canvas(targetBitmap);
                Android.Graphics.Path path = new Android.Graphics.Path();
                path.AddCircle(((float)targetWidth - 1) / 2,
                    ((float)targetHeight - 1) / 2,
                    (Math.Min(((float)targetWidth),
                        ((float)targetHeight)) / 2),
                    Android.Graphics.Path.Direction.Ccw);

                canvas.ClipPath(path);
                Bitmap sourceBitmap = scaleBitmapImage;
                canvas.DrawBitmap(sourceBitmap,
                    new Rect(0, 0, sourceBitmap.Width,
                        sourceBitmap.Height),
                    new Rect(0, 0, targetWidth, targetHeight), null);
          

                return targetBitmap;


            }
            catch (Exception )
            {
               
             
                return scaleBitmapImage;
            }

        }
    }
}