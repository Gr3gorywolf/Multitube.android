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
using ZXing;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class actividadsincronizacion : Activity
    {
        string ipadres = "";
       int puerto = 0;
        ImageView botonabrirqr;
        bool conectado = false;
        TcpListener servidor;
        public List<string> listasreprod = new List<string>();
        TcpClient cliente;
        public bool reiniciar = false;
        TcpClient clientelocal;
        string nombrelistaarecibir = "";
        string nombreserver = "";
        LinearLayout ll;
        ImageView homeb;
        ImageView playpause;
        public List<string> listalinks = new List<string>();
        public List<string> listanombres = new List<string>();
        TextView tvnombrecancion;
        TcpClient cliente2;
        TextView nombreservidor;
        Thread conocerset;
        Thread vero;
        LinearLayout root;
        public bool parador = true;
        Thread cojert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.menusincronizacion);
            /////////////////////////////Mappings//////////////////////////
            botonabrirqr = FindViewById<ImageView>(Resource.Id.imageView1);
            nombreservidor = FindViewById<TextView>(Resource.Id.textView2);
            playpause = FindViewById<ImageView>(Resource.Id.imageView3);
            homeb = FindViewById<ImageView>(Resource.Id.imageView2);
            ll = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            tvnombrecancion = FindViewById<TextView>(Resource.Id.textView3);
            root = FindViewById<LinearLayout>(Resource.Id.rooteeooo);
            ////////////////////////////////////////////////////////////////
            ll.SetBackgroundColor(Android.Graphics.Color.Black);
         //   root.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            cliente = new TcpClient();
            cliente2 = new TcpClient();
            clientelocal = new TcpClient();
            clientelocal.Client.Connect(Intent.GetStringExtra("ipadre"), 1024);
            //    ll.SetBackgroundColor(Android.Graphics.Color.Black);
            // animar2(ll);
            UiHelper.SetBackgroundAndRefresh(this);
        //    ll.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));
            servidor = new TcpListener(IPAddress.Any, 1060);
            botonabrirqr.SetBackgroundResource(Resource.Drawable.synchalf);
          vero = new Thread(new ThreadStart(cojerstreamlocal));
            vero.IsBackground = true;
            vero.Start();
            tvnombrecancion.Selected = true;
            homeb.Click += delegate
            {
                this.Finish();
            };
            playpause.Click += delegate
            {
                animar(playpause);
                clientelocal.Client.Send(Encoding.UTF8.GetBytes("playpause()"));
            };
            botonabrirqr.Click += async (ss,sss)=>
            {
                if (conectado == false) { 
                animar(botonabrirqr);
                ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
             
                var resultado = await scanner.Scan();
                string captured = resultado.Text.Trim() ;
                ipadres = captured.Split(';')[0];
                puerto = int.Parse(captured.Split(';')[1]);
                cliente.Client.Connect(ipadres, puerto);
               // botonabrirqr.Visibility = ViewStates.Invisible;
              
                Toast.MakeText(this, "conectado..", ToastLength.Long).Show();
            
                conocerset = new Thread(new ThreadStart(conocerse));
                conocerset.IsBackground = true;
                conocerset.Start();
                cojert = new Thread(new ThreadStart(cojerstream));
                cojert.IsBackground = true;
                cojert.Start();
                conectado = true;
                }
                else
                {

                    AlertDialog.Builder ad = new AlertDialog.Builder(this);
                    ad.SetCancelable(false);
                    ad.SetMessage("Desea desconectarse??");
                    ad.SetTitle("Advertencia");
                    ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                    ad.SetPositiveButton("Si", alertaok);
                    ad.SetNegativeButton("No", alertano);
                    ad.Create();
                    ad.Show();
                }
            };
          

     



        }

        public void alertaok(object sender, EventArgs e)
        {
            reiniciar = true;
            this.Finish();
        }
        public void alertano(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Operacion cancelada", ToastLength.Short).Show();
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
            Android.Animation.ObjectAnimator animacion2 = Android.Animation.ObjectAnimator.OfFloat(imagen, "scaleY", 0.5f, 1f);
            animacion2.SetDuration(300);
            animacion2.Start();

        }
        public override void Finish()
        {
            
            base.Finish();
            parador = false;
            if (SocketHelper.IsConnected(cliente) && !reiniciar)
            {
                cliente.Client.Send(Encoding.UTF8.GetBytes("desconectarse$"));
                cliente.Client.Disconnect(false);
            }
            else
             if (SocketHelper.IsConnected(cliente) && reiniciar)
            {
                cliente.Client.Send(Encoding.UTF8.GetBytes("desconectarse$"));
                cliente.Client.Disconnect(false);
                StartActivity(typeof(actividadsincronizacion));
            }
            clientelocal.Client.Disconnect(false);
            MultiHelper.ExecuteGarbageCollection();
        }
       
        public void cojerstreamlocal()
        {
          
            bool cojioto = false;
            string[] listica;
            var strim = clientelocal.GetStream();
            byte[] elbuffer = new byte[120000];
            int o = 0;
            bool enviomensaje = false;
            string lalistacompletita = "";
          
            while (parador) {
                if (!enviomensaje)
                {
                    clientelocal.Client.Send(Encoding.UTF8.GetBytes("recall()"));
                    enviomensaje = true;
                }
            while (strim.DataAvailable)
            {
                o = strim.Read(elbuffer,0, elbuffer.Length);
                lalistacompletita+= Encoding.UTF8.GetString(elbuffer, 0, o);
                    cojioto = true;
            }
                if (cojioto == true && lalistacompletita.Trim()!="")
                {  
                  listica = lalistacompletita.Split(';');
                    if (listica[0]== "caratula()><"){
                    
                        lalistacompletita = "";
                    }
                    else
                    if(listica[0] == "links()><")
                    {
                        listalinks = listica.ToList();
                        listalinks.RemoveAt(0);
                        lalistacompletita = "";
                        Thread proc = new Thread(new ThreadStart(enviarlistitaaa));
                        proc.IsBackground = true;
                        proc.Start();
                    }
                    else
                    if(lalistacompletita.Length>0)
                    {
                        listanombres = listica.ToList();
                        lalistacompletita = "";

                    }

                    cojioto = false;
                    elbuffer = new byte[120000];
                    o = 0;
             
                }


                if (MainmenuOffline.gettearinstancia() != null)
                {
                    if (MainmenuOffline.gettearinstancia().buscando == false)
                    {
                        if (MainmenuOffline.gettearinstancia().label.Text.Trim() != ""
                        && tvnombrecancion.Text != MainmenuOffline.gettearinstancia().label.Text)
                            RunOnUiThread(() => tvnombrecancion.Text = MainmenuOffline.gettearinstancia().label.Text);


                    }
                    else
                        RunOnUiThread(() => tvnombrecancion.Text = "Buscando...");
                }
                else
              if (Mainmenu.gettearinstancia() != null)
                {
                    if (Mainmenu.gettearinstancia().buscando == false)
                    {
                        if (Mainmenu.gettearinstancia().label.Text.Trim() != ""
                       && tvnombrecancion.Text != Mainmenu.gettearinstancia().label.Text)
                            RunOnUiThread(() => tvnombrecancion.Text = Mainmenu.gettearinstancia().label.Text);

                    }
                    else {
                        RunOnUiThread(() => tvnombrecancion.Text = "Buscando...");
                    }
                }


                if (tvnombrecancion.Text.Trim() == "" && tvnombrecancion.Text.Trim() != "No hay elementos en cola")
                {

                    RunOnUiThread(() => { tvnombrecancion.Text = "No hay elementos en cola"; });
                }


                Thread.Sleep(200);
            }




        }




        public void cojerarchivo()
        {

          

            bool cojio = false;
            byte[] buffer = new byte[120000];
            var strim = cliente2.GetStream();
            int o = 0;
            string lalistacompletita = "";

            while (parador)
            {

                try
                {

             
                while (strim.DataAvailable)
                {
                    o = strim.Read(buffer, o, buffer.Length);
                    lalistacompletita += Encoding.UTF8.GetString(buffer, 0, o);

                    cojio = true;
                }
                if (cojio == true)
                {
                 
                    StreamWriter sw = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + nombrelistaarecibir);
                    sw.Write(lalistacompletita);
                    sw.Close();
                        cojio = false;
                        buffer = new byte[120000];
                        strim = cliente2.GetStream();
                        o = 0;
                        lalistacompletita = "";
                   RunOnUiThread(()=>     Toast.MakeText(this, "Lista: " + nombrelistaarecibir + " Creada exitosamente!!", ToastLength.Long).Show());
                        conocerse();
                    
                      
                    }
                }
                catch(Exception e) {

                    Thread.Sleep(1000);
                    RunOnUiThread(() => Toast.MakeText(this, e.Message+e.HResult+e.Source, ToastLength.Long).Show());

                }
                Thread.Sleep(200);
            }

        }


        public void cojerstream()
        {

            while (parador)
            {
                NetworkStream strum = cliente.GetStream();
                byte[] bitesaresibir = new byte[120000];
                int o = 0;
                try { 
                while ((o = strum.Read(bitesaresibir, 0, bitesaresibir.Length)) != 0 && cliente.Client.Connected==true)
                {

                    string capturado = Encoding.Default.GetString(bitesaresibir, 0, o);
                    string tipo = capturado.Split('$')[0];
                    if (tipo == "minombre")
                    {
                        nombreserver = capturado.Split('$')[1];
                      RunOnUiThread(()=>  Toast.MakeText(this, "Conexion establecida con " + nombreserver, ToastLength.Short).Show());
                        RunOnUiThread(() => botonabrirqr.SetBackgroundResource(Resource.Drawable.syncfull));
                        RunOnUiThread(() => nombreservidor.Text = "Conectado con: " + nombreserver);
                        capturado="";
                    }
                    else
                    if (tipo == "gettearelementos")
                    {
                        string sr = File.ReadAllText(listasreprod[Convert.ToInt32(capturado.Split('$')[1])]);
                        int nolista = sr.Split('$')[0].Split(';').Length;

                        cliente.Client.Send(Encoding.Default.GetBytes("Elementos$" + nolista));

                        capturado = "";
                    }
                  
                     else
                    if (tipo == "desconectarse")
                        {
                            RunOnUiThread(()=> this.Finish());
                        }
                        else
                    if (tipo=="conectarservidor")
                    {
                        try
                        {
                            cliente2.Client.Connect(capturado.Split('$')[1].Split(';')[1], Convert.ToInt32(capturado.Split('$')[1].Split(';')[0]));
                            Thread prooo = new Thread(new ThreadStart(cojerarchivo));
                            prooo.IsBackground = true;
                            prooo.Start();
                                capturado = "";
                            }
                        catch (Exception )
                        {

                        

                        }
                       
                        
                    }
                    else
                        if(tipo== "recibirlista")
                    {
                        string str = File.ReadAllText(listasreprod[Convert.ToInt32(capturado.Split('$')[1])]);
                        cliente2.Client.Send(Encoding.UTF8.GetBytes(str));
                            capturado = "";
                    }
                    else
                        if (tipo == "nombrelista")
                    {
                        nombrelistaarecibir = capturado.Split('$')[1];
                        capturado = "";
                      
                    }
                        else
                        if (tipo == "listareprodactual")
                        {
                            string str = "";
                            string str2 = "";
                            if(listanombres.Count>0 && listalinks.Count > 0) {
                            foreach (string prr2 in listanombres)
                            {

                                str += prr2 + ";";
                            
                            }
                                str = str.Replace('$', ' ');
                                str = str.Remove(str.Length - 1, 1);
                             
                            foreach (string prr in listalinks)
                            {
                               
                                str2 += prr + ";";
                              
                            }
                         str2=str2.Remove(str2.Length - 1, 1);


                               
                             
                            cliente.Client.Send(Encoding.UTF8.GetBytes("listaactual$"+str+"$"+str2));
                            }
                        }



                    }

                }
                catch (Exception)
                {

                }

                }



        }
     public void enviarlistitaaa()
        {
            string str = "";
            string str2 = "";
            if (listanombres.Count > 0 && listalinks.Count > 0 && SocketHelper.IsConnected(cliente))
            {
                foreach (string prr2 in listanombres)
                {

                    str += prr2 + ";";

                }
                str = str.Replace('$', ' ');
                str = str.Remove(str.Length - 1, 1);

                foreach (string prr in listalinks)
                {

                    str2 += prr + ";";

                }
                str2 = str2.Remove(str2.Length - 1, 1);




                cliente.Client.Send(Encoding.UTF8.GetBytes("listaactual$" + str + "$" + str2));
            }
        }

        public void conocerse()
        {
            string todoslositemes = "";
            cliente.Client.Send(Encoding.UTF8.GetBytes("Data$"+Android.OS.Build.Device+"$ $ $"));
            Thread.Sleep(100);
            listasreprod.Clear();
            string[] directorio = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
            if (directorio.Length > 0)
            {

           
            foreach (string estring in directorio) 
            {
                string completita = Path.GetFileNameWithoutExtension(estring);
                string estring2 = completita.Replace(';', ' ');
                estring2=estring2.Replace('$', ' ');
                listasreprod.Add(estring);
                todoslositemes += estring2 + ";";
            }
                cliente.Client.Send(Encoding.UTF8.GetBytes("Listas$" + todoslositemes));
            }
            else
            {
                cliente.Client.Send(Encoding.UTF8.GetBytes("Listas$"));
            }
        
         
        }

    }
}