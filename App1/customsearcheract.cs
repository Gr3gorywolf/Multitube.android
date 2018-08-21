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
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
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
        public EditText texto;
        public int voz = 7;
        public TcpClient cliet;
        string color = "none";
        public Thread procc;
        public TextView tv1;
        public Videosimage videoimagen;
        public LinearLayout lineaa;
        public ProgressBar progresooo;
        public LinearLayout lineall2;
        public List<Android.Graphics.Bitmap> imageneses = new List<Android.Graphics.Bitmap>();
        public List<Android.Graphics.Bitmap> imagenesesblur = new List<Android.Graphics.Bitmap>();

       
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Customsearcher);
            ///////////////////////////////////////declaraciones/////////////////////////////////
            cliet = new TcpClient();
            string ip = Intent.GetStringExtra("ipadre").Trim();
            cliet.Client.Connect(ip, 1024);
            listaimagen = new List<Videosimage>();
            viddeos = new List<Videos>();
            imagelist = new List<Android.Graphics.Bitmap>();
          
            texto = FindViewById<EditText>(Resource.Id.editText1);
            ImageView botonbuscar = FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView botonreconocer= FindViewById<ImageView>(Resource.Id.imageView2);
            ImageView playpause = FindViewById<ImageView>(Resource.Id.imageView4);
           tv1 = FindViewById<TextView>(Resource.Id.textView1);
        lineaa = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            ImageView botonhome = FindViewById<ImageView>(Resource.Id.imageView3);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
            progresooo = FindViewById<ProgressBar>(Resource.Id.progresss);
          
            clasesettings.ponerfondoyactualizar(this);
            ////////////////////////////////////////////////miselaneo///////////////////////////////////
            color = Intent.GetStringExtra("color");
            nombreses = new List<string>();
            linkeses = new List<string>();
            lineall2.SetBackgroundColor(Android.Graphics.Color.Black);
            animar2(lineall2);
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
            lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            procc = new Thread(new ThreadStart(cojerstream));
            procc.Start();

          var  procc2 = new Thread(new ThreadStart(cargardesdecache));
            procc2.Start();
            ////////////////////////////////////////events//////////////////////////////////////////////////////
            botonbuscar.Click += delegate
            {
                animar(botonbuscar);
                if (buscando == false) { 
                termino = texto.Text;
                    Toast.MakeText(this, "Buscando.. por favor espere", ToastLength.Long);
                    foreach (string aa in Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser"))
                    {
                        File.Delete(aa);
                    }
                    clasesettings.recogerbasura();
                    Thread proc = new Thread(new ThreadStart(buscar));
                proc.Start();
                }
            };
            botonhome.Click +=delegate {
                animar(botonhome);
              
                procc.Abort();
                cliet.Client.Disconnect(true);
                clasesettings.recogerbasura();
                this.Finish();
               
            };
            
            playpause.Click += delegate
            {
                animar(playpause);
                cliet.Client.Send(Encoding.ASCII.GetBytes("playpause()"));
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
              
                    if (sender.Position >= 0) {

                    Intent intentar = new Intent(this, typeof(customdialogact));
                    string ipadree = Intent.GetStringExtra("ipadre");
                    intentar.PutExtra("ipadress", ipadree);
                    intentar.PutExtra("imagen", viddeos[sender.Position].imgurl);
                    intentar.PutExtra("url", viddeos[sender.Position].url);
                    intentar.PutExtra("titulo", viddeos[sender.Position].nombre);
                    intentar.PutExtra("color", color);
                    StartActivity(intentar);
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

                        texto.Text = " " + matches[0];
              

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
            VideoSearch buscavideos = new VideoSearch();
            RunOnUiThread(() => Toast.MakeText(this, "Espere mientras se buscan resultados...", ToastLength.Long).Show());





            index = 0;
         
            try {
                var aa = buscavideos.SearchQuery(termino, 2);
              

            RunOnUiThread(() => progresooo.Max = aa.Count);
            foreach (var ec in aa )
            {
             
                if (parar == true)
                {
                
                   Videos video = new Videos();
                    video.nombre = RemoveIllegalPathCharacters(ec.Title.Replace("&quot;", "").Replace("&amp;", ""));
                    video.url = ec.Url;
                    video.tiempo = RemoveIllegalPathCharacters(ec.Title.Replace("&quot;", "").Replace("&amp;", ""));
                    video.imgurl = "https://i.ytimg.com/vi/" + ec.Url.Split('=')[1] + "/mqdefault.jpg";
                   videoimagen = new Videosimage();
                    videoimagen.nombre = RemoveIllegalPathCharacters(ec.Title.Replace("&quot;", "").Replace("&amp;", ""));
                    videoimagen.imagen = "https://i.ytimg.com/vi/" + ec.Url.Split('=')[1] + "/mqdefault.jpg"; ;
                        nombreses.Add(RemoveIllegalPathCharacters( ec.Title.Replace("&quot;", "").Replace("&amp;", "")));
                        /*
                          Byte[] biteimagen =  clienteee.DownloadData(ec.Thumbnail);

                          using (MemoryStream memoria = new MemoryStream(biteimagen))
                          {
                              Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeStream(memoria);
                              video.imagen = imagen;
                              videoimagen.imagen = imagen;
                          }
                          */


                        //  Byte[] biteimagen = clienteee.DownloadData(ec.Thumbnail);



                        //   Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeByteArray(biteimagen, 0, biteimagen.Length);
                        //   var aah = File.Create(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/" + index);
                        //   aah.Write(biteimagen, 0, biteimagen.Length);
                        //    aah.Close();

                        // biteimagen = new byte[0];

                        linkeses.Add(ec.Url);
                   listaimagen.Add(videoimagen);
                    viddeos.Add(video);
                   imagelist.Add(video.imagen);








                 
                    index++;
                    RunOnUiThread(() => progresooo.Progress = index);
             
                }
                //  ArrayAdapter<string> adaptadorvids = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listaimagen);
             
              


            }
             var   adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, nombreses);
                RunOnUiThread(() => listbox.Adapter = adaptadol);
                buscando = false;
                parar = false;
                Thread proc = new Thread(new ThreadStart(enthread));
                proc.Start();
          
          
            }
            catch (Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                parar = false;
                buscando = false;
            }
        }
        public override void OnBackPressed()

        {
            procc.Abort();
          cliet.Client.Disconnect(true);
            this.Finish();


      
    }
        public override void Finish()
        {

            listaimagen = new List<Videosimage>();
            nombreses = new List<string>();
            viddeos = new List<Videos>();
            linkeses = new List<string>();
            imageneses = new List<Bitmap>();
            imagenesesblur = new List<Bitmap>();
            clasesettings.recogerbasura();
            listaimagen.Clear();
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
                    listaimagen.Clear();
                    nombreses.Clear();
                    viddeos.Clear();
                    linkeses.Clear();
                    imageneses.Clear();
                    imagenesesblur.Clear();
                    var asdsa = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");

                    nombreses = asdsa.Split('²')[0].Split('¹').ToList();
                    linkeses = asdsa.Split('²')[1].Split('¹').ToList();
                    for (int i = 0; i < nombreses.Count - 1; i++)
                    {

                        videoimagen = new Videosimage();
                        videoimagen.nombre = RemoveIllegalPathCharacters(nombreses[i]);
                        videoimagen.imagen = "http://i.ytimg.com/vi/" + linkeses[i].Split('=')[1] + "/mqdefault.jpg";
                        Videos losvids = new Videos();

                        losvids.imgurl = "http://i.ytimg.com/vi/" + linkeses[i].Split('=')[1] + "/mqdefault.jpg";
                        losvids.nombre = videoimagen.nombre;
                        losvids.url = linkeses[i];
                        viddeos.Add(losvids);
                        listaimagen.Add(videoimagen);
                        using (Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/" + listaimagen[i].imagen.Split('/')[4]))
                        {
                            imageneses.Add(getRoundedShape(imagen));
                            imagenesesblur.Add(clasesettings.CreateBlurredImageofflineadapters(this, 20, Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/" + listaimagen[i].imagen.Split('/')[4]));
                        }





                    }
                   
                    Customadaptador1 adaltel = new Customadaptador1(this, listaimagen, true, false, imageneses, imagenesesblur);
                    RunOnUiThread(() => listbox.Adapter = adaltel);

                    if (listaimagen.Count == 0)
                    {
                        var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> {"No se encontraron datos en cache"});
                        RunOnUiThread(() => listbox.Adapter = adaptadol);

                    }
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

                RunOnUiThread(() => progresooo.Max = listaimagen.Count - 1);
                RunOnUiThread(() => progresooo.Progress = 0);
              
               
                for (int i = 0; i < listaimagen.Count; i++)
                {

                    if (!buscando)
                    {



                        /*   new Thread(() =>
                           {
                           */

                        using (var clientuuuu = new System.Net.WebClient()) { 
                            clientuuuu.Credentials = new NetworkCredential();


                            int pos = i;
                            //   listaimagen[i].imagen.Split('/')[4]
                            string url = listaimagen[pos].imagen;
                            string imagename = listaimagen[pos].imagen.Split('/')[4];
                                clientuuuu.DownloadFile(new Uri(url), Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/" + imagename);
                            RunOnUiThread(() => progresooo.Progress++);


                          
                        }
                        using (Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeFile(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/" + listaimagen[i].imagen.Split('/')[4]))
                        {
                            imageneses.Add(getRoundedShape(imagen));
                            imagenesesblur.Add(clasesettings.CreateBlurredImageofflineadapters(this, 20, Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/" + listaimagen[i].imagen.Split('/')[4]));
                        }

                        // Android.Graphics.Bitmap imagen = Android.Graphics.BitmapFactory.DecodeByteArray(ss, 0, ss.Length);

                        //holder.imagen.SetImageBitmap(imagen);

                        // }).Start();
                    }



                  
                       



                }


               
                                    if (!buscando)
                                {

                                    var asdd = listbox.FirstVisiblePosition;
                                    Customadaptador1 adaltel = new Customadaptador1(this, listaimagen, true,false,imageneses,imagenesesblur);
                                    RunOnUiThread(() => listbox.Adapter = adaltel);
                                    RunOnUiThread(() => listbox.SetSelection(asdd));
                                    if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3")) {
                                        File.Delete(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");
                                    }
                                    var ee = File.CreateText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/webbrowser/cachesito.gr3");

                                    string todosnombres = String.Join("¹", nombreses.ToArray());
                                    string todoslinks = string.Join("¹", linkeses.ToArray());
                                    ee.Write(todosnombres + "²" + todoslinks);
                                    ee.Close();
                                }

                clasesettings.recogerbasura();
            }


            catch (Exception)
            {
                var asdd = listbox.FirstVisiblePosition;
                var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, nombreses);
                RunOnUiThread(() => listbox.Adapter = adaptadol);
                RunOnUiThread(() => listbox.SetSelection(asdd));
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
            while (cliet.Client.Connected)
            {
                if (mainmenu_Offline.gettearinstancia() != null)
                {
                    if (mainmenu_Offline.gettearinstancia().label.Text != tv1.Text)
                    {
                        RunOnUiThread(() => tv1.Text = mainmenu_Offline.gettearinstancia().label.Text);
                    }

                }
                else
                if (mainmenu.gettearinstancia() != null)
                {
                    if (mainmenu.gettearinstancia().label.Text != tv1.Text)
                    {
                        RunOnUiThread(() => tv1.Text = mainmenu.gettearinstancia().label.Text);
                    }





                }
                else
                {

                }


                Thread.Sleep(1000);
            }
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
    }
           

    }
    
