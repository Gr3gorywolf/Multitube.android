using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YoutubeSearch;
using System.Drawing;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Android.Speech;
using System.Net.Sockets;
using Android.Graphics;
using Android.Glide;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo", WindowSoftInputMode = SoftInput.StateAlwaysHidden | SoftInput.AdjustResize)]
    public class customsearcheract : Activity
    {
        public string ipadresss;
        public bool buscando = false;
        public int index = 0;
        public List<Videos> viddeos;
        public bool parar = true;
        public List<Videosimage> listaimagen;
        public string termino;
        public ListView listbox;
        public List<Android.Graphics.Bitmap> imagelist;
        public List<string> nombreses;
        public List<string> linkeses;
        public List<string> autoreses;
        public List<string> duraciones;
        public EditText texto;
        public int voz = 7;
       
        string color = "none";
        public Thread procc;
        public TextView tv1;
        public Videosimage videoimagen;
        public LinearLayout lineaa;
      //  public ProgressBar progresooo;
        public LinearLayout lineall2;
        public List<Android.Graphics.Bitmap> imageneses = new List<Android.Graphics.Bitmap>();
        public List<Android.Graphics.Bitmap> imagenesesblur = new List<Android.Graphics.Bitmap>();
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        public ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Customsearcher);
            ///////////////////////////////////////declaraciones/////////////////////////////////
           
            string ip = Intent.GetStringExtra("ipadre").Trim();
           
            listaimagen = new List<Videosimage>();
            viddeos = new List<Videos>();
            imagelist = new List<Android.Graphics.Bitmap>();
         
            texto = FindViewById<EditText>(Resource.Id.editText1);
          //  ImageView botonbuscar = FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView botonreconocer= FindViewById<ImageView>(Resource.Id.imageView2);
            ImageView playpause = FindViewById<ImageView>(Resource.Id.imageView4);
           tv1 = FindViewById<TextView>(Resource.Id.textView1);
        lineaa = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            ImageView botonhome = FindViewById<ImageView>(Resource.Id.imageView3);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
          //  progresooo = FindViewById<ProgressBar>(Resource.Id.progresss);
          
            clasesettings.ponerfondoyactualizar(this);
            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => {

                var parcelable = listbox.OnSaveInstanceState();
                listbox.Adapter = adaptadol;
                listbox.OnRestoreInstanceState(parcelable);
            });
            ////////////////////////////////////////////////miselaneo///////////////////////////////////
            color = Intent.GetStringExtra("color");
            nombreses = new List<string>();
            linkeses = new List<string>();
            autoreses= new List<string>();
            duraciones = new List<string>();
        //   lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#4f5459"));
          //  animar2(lineall2);
            tv1.Selected = true;

         
            if (color.Trim() != "none")
            {
              //  lineaa.SetBackgroundColor(Android.Graphics.Color.ParseColor(color.Trim()));
            }

            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache"))
                {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache");
            }
           

            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser"))
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser");
            }
            else
            {
               
            }

            /////////////////////////////////////////////////threads///////////////////////////////////////////////
          //  lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));
            procc = new Thread(new ThreadStart(cojerstream));
            procc.Start();

          var  procc2 = new Thread(new ThreadStart(cargardesdecache));
            procc2.Start();
            ////////////////////////////////////////events//////////////////////////////////////////////////////
            texto.KeyPress += (aaxx, e) =>
            {
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    // Code executed when the enter key is pressed down

                    if (buscando == false)
                    {
                        termino = texto.Text;
                        //  Toast.MakeText(this, "Buscando.. por favor espere", ToastLength.Long);
                        foreach (string aa in Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser"))
                        {
                            File.Delete(aa);
                        }
                        clasesettings.recogerbasura();
                        Thread proc = new Thread(new ThreadStart(buscar));
                        proc.Start();
                    }

                }
                else
                {
                    e.Handled = false;
                }
            };
        
            botonhome.Click +=delegate {
                animar(botonhome);
              
                procc.Abort();
               
                clasesettings.recogerbasura();
                this.Finish();
               
            };
            
            playpause.Click += delegate
            {
                animar(playpause);
                if (mainmenu.gettearinstancia() != null)
                    mainmenu.gettearinstancia().play.PerformClick();
                else
                if (mainmenu_Offline.gettearinstancia() != null)
                   mainmenu_Offline.gettearinstancia().play.PerformClick();
             
            };

            botonreconocer.Click += delegate
             {
                 animar(botonreconocer);
                 try {
                     var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                     voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                     voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 500);
                     voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 500);
                     voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 10000);
                     voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                     voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                     StartActivityForResult(voiceIntent, voz);
                 } catch (Exception)
                 {

                 }

             };
          
                listbox.ItemClick += (easter, sender) =>
            {
                if (nombreses.Count > 0) { 
                    if (sender.Position >= 0) {

                    Intent intentar = new Intent(this, typeof(customdialogact));
                    string ipadree = Intent.GetStringExtra("ipadre");
                    intentar.PutExtra("ipadress", ipadree);
                    intentar.PutExtra("imagen", "http://i.ytimg.com/vi/" + linkeses[sender.Position].Split('=')[1] + "/mqdefault.jpg");
                    intentar.PutExtra("url", linkeses[sender.Position]);
                    intentar.PutExtra("titulo", nombreses[sender.Position]);
                    intentar.PutExtra("color", color);
                    StartActivity(intentar);
                }
                }

            };






            // Create your application here
        }

          
       
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == voz)
            {
                if (resultCode == Result.Ok)
                {


                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {

                        texto.Text =  matches[0];
                        if (buscando == false)
                        {
                            termino = texto.Text;
                            //  Toast.MakeText(this, "Buscando.. por favor espere", ToastLength.Long);
                            foreach (string aa in Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser"))
                            {
                                File.Delete(aa);
                            }
                            clasesettings.recogerbasura();
                            Thread proc = new Thread(new ThreadStart(buscar));
                            proc.Start();
                        }

                    }

                    else
                        Toast.MakeText(this, "No se pudo escuchar nada", ToastLength.Long).Show();
                }
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
        public void buscar()
        {
            buscando = true;
            parar = true;
         
             
            viddeos.Clear();
            listaimagen.Clear();
            nombreses.Clear();
            linkeses.Clear();
            autoreses.Clear();
            duraciones.Clear();
            VideoSearch buscavideos = new VideoSearch();
            //  RunOnUiThread(() => Toast.MakeText(this, "Espere mientras se buscan resultados...", ToastLength.Long).Show());



            RunOnUiThread(() =>
            {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Buscando resultados...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
            });
          


            index = 0;
         
            try {
                var aa = buscavideos.SearchQuery(termino, 3);
              

        
            foreach (var ec in aa )
            {
                   
             
                if (parar == true)
                {
                        nombreses.Add(WebUtility.HtmlDecode( RemoveIllegalPathCharacters( ec.Title.Replace("&quot;", "").Replace("&amp;", ""))));
                        autoreses.Add(ec.Url);
                        duraciones.Add(ec.Duration);
                        linkeses.Add(ec.Url);
                        index++;
             
             
                }                                      
            }
                dialogoprogreso.Dismiss();

             var   adaptadol = new adapterlistaremotobuscadores(this, nombreses,linkeses, autoreses, duraciones);
                RunOnUiThread(() => {
                    var parcelable = listbox.OnSaveInstanceState();
                    listbox.Adapter = adaptadol;
                    listbox.OnRestoreInstanceState(parcelable);
                });
                if (nombreses.Count == 0) {
                    var adaptadolss = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                    RunOnUiThread(() => {
                        var parcelable = listbox.OnSaveInstanceState();
                        listbox.Adapter = adaptadolss;
                        listbox.OnRestoreInstanceState(parcelable);
                    });
                }
                buscando = false;
                parar = false;
                Thread proc = new Thread(new ThreadStart(enthread));
                proc.Start();
          
          
            }
            catch (Exception)


            {
                dialogoprogreso.Dismiss();
                RunOnUiThread(() => Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                parar = false;
                buscando = false;
            }
        }
        public override void OnBackPressed()

        {
            procc.Abort();
        
            this.Finish();


      
    }
        public override void Finish()
        {

 
          ////  cliet.Client.Disconnect(true);
            base.Finish();
            //     this.Dispose();
        }
        public void cargardesdecache()
        {
            try
            {

                RunOnUiThread(() => Toast.MakeText(this, "Cargando datos desde cache", ToastLength.Long).Show());
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3"))
                {
                 //   listaimagen.Clear();
                    nombreses.Clear();
                    viddeos.Clear();
                    linkeses.Clear();
                    autoreses.Clear();
                    duraciones.Clear();
                  //  imageneses.Clear();
                   // imagenesesblur.Clear();
                    var asdsa = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");

                    nombreses = asdsa.Split('²')[0].Split('¹').ToList();
                    linkeses = asdsa.Split('²')[1].Split('¹').ToList();
                    autoreses= asdsa.Split('²')[2].Split('¹').ToList();
                    duraciones = asdsa.Split('²')[3].Split('¹').ToList();
                    if (nombreses[0].Trim() == "" || linkeses[0].Trim()=="") {
                        nombreses.Clear();
                        viddeos.Clear();
                        linkeses.Clear();
                        autoreses.Clear();
                        duraciones.Clear();
                    }
                    adapterlistaremotobuscadores adaltel = new adapterlistaremotobuscadores(this, nombreses, linkeses, autoreses, duraciones);
                    RunOnUiThread(() => {
                        var parcelable = listbox.OnSaveInstanceState();
                        listbox.Adapter = adaltel;
                        listbox.OnRestoreInstanceState(parcelable);
                    });

                    if (nombreses.Count == 0)
                    {
                        var adaptadolss = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                        RunOnUiThread(() => listbox.Adapter = adaptadolss);
                    }
                  

                    /*  if (listaimagen.Count == 0)
                      {
                          var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> {"No se encontraron datos en cache"});
                          RunOnUiThread(() => listbox.Adapter = adaptadol);

                      }*/
                }

            }
            catch (Exception)
            {

            }
        }
      

        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
     

       
      

        public void enthread()
        {
            imagenesesblur.Clear();
            imageneses.Clear();
            try
           {

                


                                                                         
                                    if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3")) {
                                        File.Delete(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");
                                    }
                                    var ee = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");

                                    string todosnombres = String.Join("¹", nombreses.ToArray());
                                    string todoslinks = string.Join("¹", linkeses.ToArray());
                                    string autores = string.Join("¹", autoreses.ToArray());
                                    string duracioness = string.Join("¹", duraciones.ToArray());
                                    ee.Write(todosnombres + "²" + todoslinks + "²" +autores+ "²" +duracioness);
                                    ee.Close();
                             

                                    clasesettings.recogerbasura();
            }


            catch (Exception)
            {
              /*  var asdd = listbox.FirstVisiblePosition;
                var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, nombreses);
                RunOnUiThread(() => listbox.Adapter = adaptadol);
                RunOnUiThread(() => listbox.SetSelection(asdd));*/
            }
        }



        public Bitmap getRoundedShape(Bitmap scaleBitmapImage)
        {
            int targetWidth = 110;
            int targetHeight = 110;
            Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                targetHeight, Bitmap.Config.Argb4444);

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

        public bool tienemicrofono()
        {
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {


                return false;
            }
            else
            {
                return true;
            }
        }
        public void cojerstream()
        {
            while (!this.IsDestroyed)
            {
                if (mainmenu_Offline.gettearinstancia() != null)
                {

                    if (mainmenu_Offline.gettearinstancia().buscando != true)
                    {
                        if (mainmenu_Offline.gettearinstancia().label.Text != tv1.Text
                            && mainmenu_Offline.gettearinstancia().label.Text.Trim() != "")
                        {


                            RunOnUiThread(() => tv1.Text = mainmenu_Offline.gettearinstancia().label.Text);
                        }
                    }
                    else {
                        RunOnUiThread(() => tv1.Text ="Buscando...");
                    }


                }
                else
                if (mainmenu.gettearinstancia() != null)
                {

                    if (mainmenu.gettearinstancia().buscando != true)
                    {
                        if (mainmenu.gettearinstancia().label.Text != tv1.Text
                        && mainmenu.gettearinstancia().label.Text.Trim() != "")
                        {
                            RunOnUiThread(() => tv1.Text = mainmenu.gettearinstancia().label.Text);
                        }
                    }
                    else {
                        RunOnUiThread(() => tv1.Text = "Buscando...");
                    }





                }
               

                if (tv1.Text.Trim() == "" && tv1.Text.Trim() != "No hay elementos en cola")
                {

                    RunOnUiThread(() => { tv1.Text = "No hay elementos en cola"; });
                }

                Thread.Sleep(1000);
            }
        }
        protected override void OnDestroy()
        {
            listaimagen = new List<Videosimage>();
            nombreses = new List<string>();
            viddeos = new List<Videos>();
            linkeses = new List<string>();
            imageneses = new List<Bitmap>();
            imagenesesblur = new List<Bitmap>();
           
            listaimagen.Clear();
            try
            {
                Glide.Get(this).ClearMemory();
            }
            catch (Exception)
            {
            }
            clasesettings.recogerbasura();
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
        public void animar2(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleX", 0.5f, 1f);
            animacion.SetDuration(700);
            animacion.Start();

        }
    }
           

    }
    
