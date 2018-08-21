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
using System.Threading;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class Reproducirlistaact : Activity
    {
        public bool parador;
        public ImageView playpause;
        public int posicion = 0;
        public ListView lista;
        public ImageView volverhome;
        public TextView textbox;
       public TcpClient clientee;
        public LinearLayout background;
        public LinearLayout barra;
        public Thread teee;
        bool detenedor = true;
        public List<string> listastring = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Reproducirlista);
            //////////////////////////////declaraciones//////////////////////////////////
            clientee = new TcpClient();
            string ipa = Intent.GetStringExtra("ip").Trim();
            clientee.Client.Connect(ipa, 1024);
            parador = true;
            playpause = FindViewById<ImageView>(Resource.Id.imageView2);
            lista = FindViewById<ListView>(Resource.Id.listView1);
            volverhome = FindViewById<ImageView>(Resource.Id.imageView1);
            textbox = FindViewById<TextView>(Resource.Id.textView1);
            background = FindViewById<LinearLayout>(Resource.Id.LinearLayout0);
            barra = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            //////////////////////////////////////////////////////////////////////////////
            /////////////////////////////miselaneo+conexion//////////////////////////////
          

            barra.SetBackgroundColor(Android.Graphics.Color.Black);
            animar2(barra);
         
            Thread teee = new Thread(new ThreadStart(cojerstream));
            teee.Start();
            textbox.Selected = true;
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => lista.Adapter = adaptadolo);
            //////////////////////////////////////////////////////////////////////////////
            ////////////////////////////Eventos//////////////////////////////////////////
            barra.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
           
            playpause.Click += delegate
            {
                animar(playpause);
                clientee.Client.Send(Encoding.Default.GetBytes("playpause()"));
            };
            volverhome.Click += delegate
            {
                animar(volverhome);
                clientee.Client.Disconnect(false);
                teee.Abort();
                Finish();
                clasesettings.recogerbasura();
            };
            lista.ItemClick += (sender, args)=>
            {
                AlertDialog.Builder ad = new AlertDialog.Builder(this);
                ad.SetCancelable(false);
                ad.SetTitle("Advertencia");
                ad.SetMessage("Desea reproducir la lista de reproduccion " + listastring[args.Position] + " en el dispositivo servidor??");
                 ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                posicion = args.Position;
                ad.SetPositiveButton("Si", ok);
                ad.SetNegativeButton("No", no);
                ad.Create();
                ad.Show();
            };




    
            // Create your application here
        }
        void no(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Operacion cancelada", ToastLength.Long).Show();
        }
        void ok(object sender, EventArgs e)
        {
            if (lista.Count > 0)
            {
                clientee.Client.Send(Encoding.Default.GetBytes("pedirlista()"));
                Thread.Sleep(100);

                clientee.Client.Send(Encoding.Default.GetBytes(posicion.ToString()));
                clientee.Client.Disconnect(false);
                teee.Abort();


                Finish();
                clasesettings.recogerbasura();
            }
            else
            {
                Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
            }
        }
        public override void OnBackPressed()
        {
            clientee.Client.Disconnect(false);
            teee.Abort();
            Finish();
            clasesettings.recogerbasura();
        }
        public void cojerstream()
        {

            List<string> lista2 = new List<string>();
            string[] listica = null;
          
            byte[] bites = new byte[120000];
            string capturado = "";

            bool enviomensaje = false;
            int o;


            while (clientee.Connected == true && detenedor )
            {
                var stream = clientee.GetStream();
                if (!enviomensaje)
                {
                    enviomensaje = true;
                    clientee.Client.Send(Encoding.Default.GetBytes("actualizarlalista()"));
                }



                while (stream.DataAvailable)
                {




                    o = stream.Read(bites, 0, bites.Length);


                    capturado += Encoding.ASCII.GetString(bites, 0, o);
                 
                }




                if (capturado.Trim().Length > 5) 
                {

                



                 
               
                    listica = capturado.Split(';');

                    if (capturado.Trim() != "" && listica[0].Trim() == "caratula()><" && listica[0].Trim() != "listar()><")
                    {
                        capturado = "";
                        if (mainmenu_Offline.gettearinstancia() != null)
                        {
                            RunOnUiThread(() => textbox.Text = mainmenu_Offline.gettearinstancia().label.Text);
                        }
                        else
                 if (mainmenu.gettearinstancia() != null)
                        {
                            RunOnUiThread(() => textbox.Text = mainmenu.gettearinstancia().label.Text);
                        }
                        else
                        {

                        }

                    }

                    else
                      if (capturado.Trim() != "" && listica[0].Trim() == "listar()><")
                    {
                        listastring.Clear();
                        capturado = "";
                        for(int i = 1; i < listica.Length; i++)
                        {
                            listastring.Add(listica[i]);

                        }
                        ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listastring);
                       RunOnUiThread(()=> lista.Adapter = adaptador);
                    }

                    lista2.Clear();
                    listica.ToList().Clear();
                    bites = new byte[120000];
                    capturado = "";
                    o = 0;
                }
             
                Thread.Sleep(100);
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