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
using System.IO;
using System.Net.Sockets;
using System.Threading;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class menulistaoffline : Activity
    {
        TextView textboxtitulo;
        TcpClient cliet;
        LinearLayout lineall2;
        LinearLayout lineaa;
        public Thread tree;
        public ListView listbox;
        public List<string> listareprod;
        public List<string> listareprod2;
        public bool eneliminacion = false;
        string listaenlinea;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Menulistaoffline);
           string ipadress= Intent.GetStringExtra("ipadre");
            cliet = new TcpClient(ipadress, 1024);
            listareprod = new List<string>();
            listareprod2 = new List<string>();
            textboxtitulo = FindViewById<TextView>(Resource.Id.textView1);
            ImageView playpause = FindViewById<ImageView>(Resource.Id.imageView5);
            ImageView botonelimiar= FindViewById<ImageView>(Resource.Id.imageView7);
            ImageView botonagregar = FindViewById<ImageView>(Resource.Id.imageView6);
            ImageView botonhome = FindViewById<ImageView>(Resource.Id.imageView4);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
            ImageView botonreproducir = FindViewById<ImageView>(Resource.Id.imageView3);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
           listaenlinea = Intent.GetStringExtra("listaenlinea");
            lineaa = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            animar2(lineall2);
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => listbox.Adapter = adaptadolo);
            llenarlista();

           
            // lineaa.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            tree = new Thread(new ThreadStart(cojerstream));
            tree.Start();
            textboxtitulo.Selected = true;
            botonelimiar.SetBackgroundResource(Resource.Drawable.closecircularbuttonofacross);
            clasesettings.ponerfondoyactualizar(this);
            lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
           
            listbox.ItemClick += (aaa, aaaa) =>
            {

                if(File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/"+listareprod[aaaa.Position]).Split('$')[0].Split(';').ToList().Count>=1 && File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listareprod[aaaa.Position]).Split('$')[0].Split(';')[0].Trim() != "") { 
                Intent internado = new Intent(this, typeof(Reproducirlistadialog));
                internado.PutExtra("ip", Intent.GetStringExtra("ipadre"));
                internado.PutExtra("nombrelista", listareprod[aaaa.Position]);
                internado.PutExtra("index", aaaa.Position.ToString());
                StartActivity(internado);
                }
                else
                {
                    Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                }
            };
            botonelimiar.Click += delegate
            {
                animar(botonelimiar);
                if (eneliminacion)
                {
                    
                    eneliminacion = false;
                    botonelimiar.SetBackgroundResource(Resource.Drawable.closecircularbuttonofacross);
                    ArrayAdapter adaptador = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, listareprod);
                    listbox.Adapter = adaptador;
                }
              
                else
                {
                    botonelimiar.SetBackgroundResource(Resource.Drawable.menucircularbutton);
                    adaptadorlista adaptador = new adaptadorlista(this, listareprod, listareprod2, "noser", true, false);
                    listbox.Adapter = adaptador;
                    eneliminacion = true;

                }
            };
            botonagregar.Click += delegate
            {
              
                    Intent intento = new Intent(this,typeof(agregarlistaoffline));               
                    intento.PutExtra("ipadre", ipadress);
                    StartActivity(intento);


             
            };
       
            playpause.Click += delegate
                 {
                     animar(playpause);

                     cliet.Client.Send(Encoding.ASCII.GetBytes("playpause()"));
                 };
            botonhome.Click += delegate
            {

                animar(botonhome);
                cliet.Client.Disconnect(false);
                tree.Abort();
                this.Finish();

            };
 




            // Create your application here
        }
        public override void OnBackPressed()
        {
            cliet.Client.Disconnect(false);
            tree.Abort();
            this.Finish();
        }
        public void cojerstream()
        {
          
          
            List<string> lista2 = new List<string>();
            string[] listica = new string[3324234];
            var stream = cliet.GetStream();
       
            byte[] bites = new byte[120000];
            string capturado = "";
         

            int o = 0;

            while (cliet.Connected == true)
            {

              
               





                try {
                    cliet.Client.Send(Encoding.Default.GetBytes("caratula()"));
                    while ((o = stream.Read(bites, 0, bites.Length)) != 0 )
                    {
                        capturado = Encoding.Default.GetString(bites, 0, o);
                    listica = capturado.Split(';');
                   
                    if (capturado.Trim() != "" && listica[0].Trim() == "caratula()><")
                    {

                            if (mainmenu_Offline.gettearinstancia() != null)
                            {
                                RunOnUiThread(() => textboxtitulo.Text = mainmenu_Offline.gettearinstancia().label.Text);
                            }
                            else
                 if (mainmenu.gettearinstancia() != null)
                            {
                                RunOnUiThread(() => textboxtitulo.Text = mainmenu.gettearinstancia().label.Text);
                            }
                            else
                            {

                            }
                            capturado = "";


                        }
                    else
                    if (capturado.Trim() != "" && listica[0].Trim() == "listar()><")
                    {

                    }
                    else
                     if (capturado.Trim() != "" && listica[0].Trim() == "links()><")
                    {
                         



                    }
                    else
                            if (capturado.Trim() != "" && listica[0].Trim() == "actualizaa()")
                    {

                        llenarlista();

                    }
                    else
                    if (capturado != " " && capturado.Trim() != "caratula()" && listica[0].Trim() != "caratula()><")
                    {
                       

                    }

                   
                   
                    
                       

                    }
                   
                   
                }
                catch (Exception)
                {

                }
               
           
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
        public void llenarlista()
        {
            listareprod.Clear();
            try
            {
                string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");
             
          
                    for(int i=0;i<items.Length;i++)

                {
                    listareprod2 = items.ToList();
                listareprod.Add(Path.GetFileNameWithoutExtension(items[i]));
                }
                ArrayAdapter adaptador = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,listareprod);
           RunOnUiThread(()=>listbox.Adapter = adaptador);
            }
            catch (Exception)
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                llenarlista();
            }
        }
    }
   
}