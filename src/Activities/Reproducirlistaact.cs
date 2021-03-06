using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using App1.Models;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
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
        public List<PlayList> listaslocales = new List<PlayList>();
        public List<PlayList> listasremotas = new List<PlayList>();
        Android.Support.Design.Widget.TabLayout tl;
      public  int playlistidx = 0;
        //fromlocal es el remoto de aqui y el local de el server
        string query = "Fromremote";
      static  Reproducirlistaact inst;
        public static Reproducirlistaact gettearinstancia() {
            return inst;

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Reproducirlista);
            inst = this;
            //////////////////////////////declaraciones//////////////////////////////////
           
            parador = true;
            playpause = FindViewById<ImageView>(Resource.Id.imageView2);
            lista = FindViewById<ListView>(Resource.Id.listView1);
            volverhome = FindViewById<ImageView>(Resource.Id.imageView1);
            textbox = FindViewById<TextView>(Resource.Id.textView1);
            background = FindViewById<LinearLayout>(Resource.Id.LinearLayout0);
            barra = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            tl = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tabs);

            tl.AddTab(tl.NewTab().SetText("Local"));
            tl.AddTab(tl.NewTab().SetText("Remoto"));
            //////////////////////////////////////////////////////////////////////////////
            /////////////////////////////miselaneo+conexion//////////////////////////////


            //  barra.SetBackgroundColor(Android.Graphics.Color.Black);
            

            new Thread(() => cojerstream()).Start();
            new Thread(() => llenarlistas()).Start();
            textbox.Selected = true;
          /*  var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => lista.Adapter = adaptadolo);*/
            //////////////////////////////////////////////////////////////////////////////
            ////////////////////////////Eventos//////////////////////////////////////////
           // barra.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));

            playpause.Click += delegate
            {
           
              Mainmenu.gettearinstancia().clientela.Client.Send(Encoding.Default.GetBytes("playpause()"));
            };
            volverhome.Click += delegate
            {
       
     
                Finish();
                MultiHelper.ExecuteGarbageCollection();
            };
            tl.TabSelected += (aa, sss) =>
            {
                new Thread(() =>
                {

                    if (sss.Tab.Text == "Local")
                    {



                        query = "Fromremote";



                        if (listaslocales.Count > 0)
                        {
                            var adap = new adapterlistaremoto(this, listaslocales.Select(asd => asd.Name).ToList(), listaslocales.Select(asd => asd.Portrait).ToList());
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();
                                lista.Adapter = adap;
                                lista.OnRestoreInstanceState(parcelable);
                            });
                        }
                        else
                        {
                            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();
                                lista.Adapter = adaptadol;
                                lista.OnRestoreInstanceState(parcelable);
                            });

                        }





                    }
                    else {




                        query = "Fromlocal";
                        if (listasremotas.Count > 0)
                        {
                            var adap = new adapterlistaremoto(this, listasremotas.Select(asd => asd.Name).ToList(), listasremotas.Select(asd => asd.Portrait).ToList());
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();
                                lista.Adapter = adap;
                                lista.OnRestoreInstanceState(parcelable);
                            });
                        }
                        else
                        {
                            var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                            RunOnUiThread(() =>
                            {
                                var parcelable = lista.OnSaveInstanceState();
                                lista.Adapter = adaptadol;
                                lista.OnRestoreInstanceState(parcelable);
                            });

                        }







                    }


                }).Start();
            };

            lista.ItemClick += (sender, args)=>
            {

             
                    playlistidx = args.Position;
             
                Intent intento = new Intent(this, typeof(dialogolistasact));
                if (query == "Fromremote" && listaslocales.Count>0)
                {
                    if (listaslocales[playlistidx].Portrait.Trim() != "") {
                        intento.PutExtra("nombrelista", listaslocales[playlistidx].Name);
                        intento.PutExtra("portada", listaslocales[playlistidx].Portrait);
                        intento.PutExtra("querry", query);

                        StartActivity(intento);
                    }
                    else {
                        Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                        }
                }
                else
                
                {
                    if (listasremotas.Count > 0) {
                        if (listasremotas[playlistidx].MediaElements.Count > 0)
                        {
                            intento.PutExtra("nombrelista", listasremotas[playlistidx].Name);
                            intento.PutExtra("portada", listasremotas[playlistidx].Portrait);
                            intento.PutExtra("querry", query);
                            StartActivity(intento);
                        }
                        else {
                            Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                        }
                    }
                    /*    mainmenu.gettearinstancia()
                       .clientelalistas
                      .Client
                      .Send(Encoding
                      .UTF8
                       .GetBytes("Sendplaylist__==__==__" + JsonConvert.SerializeObject(listasremotas[playlistidx])));*/
                }
              

             /*   AlertDialog.Builder ad = new AlertDialog.Builder(this);
                ad.SetCancelable(false);
                ad.SetTitle("Advertencia");
                ad.SetMessage("Desea reproducir esta lista de reproduccio en el dispositivo servidor??");
                 ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                posicion = args.Position;
                ad.SetPositiveButton("Si", ok);
                ad.SetNegativeButton("No", no);
                ad.Create();
                ad.Show();*/



            
            };




    
            // Create your application here
        }
        void no(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Operacion cancelada", ToastLength.Long).Show();
        }
        void ok(object sender, EventArgs e)
        {
            if (query == "Fromremote")
            {

                if (listaslocales.Count > 0)
                {


                    var listilla = new List<PlaylistElement>();
                    var texto = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + listaslocales[playlistidx].Name);
                    var nombreses = texto.Split('$')[0].Split(';').ToList();
                    var links= texto.Split('$')[1].Split(';').ToList();
             
                    var listaelementos = new List<PlaylistElement>();
                    for (int i = 0; i < nombreses.Count; i++) {

                        if (nombreses[i].Trim() != "" || links[i].Trim() != "") {
                        var elemento = new PlaylistElement()
                        {
                            Name = nombreses[i],
                            Link = links[i]
                        };
                        listaelementos.Add(elemento);
                        }
                    }
                    listaslocales[playlistidx].MediaElements = listaelementos;
                    Mainmenu.gettearinstancia()
                         .clientelalistas
                         .Client
                         .Send(Encoding.UTF8.GetBytes(query + "__==__==__" + JsonConvert.SerializeObject(listaslocales[playlistidx])));
                }
                else
                {
                    Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                }
            }
            else {

                if (listasremotas.Count > 0)
                {
                    Mainmenu.gettearinstancia()
                        .clientelalistas
                        .Client
                        .Send(Encoding.UTF8.GetBytes(query + "__==__==__" + JsonConvert.SerializeObject(listasremotas[playlistidx])));
                }
                else
                {
                    Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                }

            }
        }
        public override void OnBackPressed()
        {
           
          
            Finish();
            MultiHelper.ExecuteGarbageCollection();
        }
        public void cojerstream()
        {

          
          

            while ( detenedor )
            {
                

                if (Mainmenu.gettearinstancia() != null)
                {

                    if (Mainmenu.gettearinstancia().buscando == false)
                    {
                        if (Mainmenu.gettearinstancia().label.Text != textbox.Text
                         && Mainmenu.gettearinstancia().label.Text.Trim() != "")
                        {
                            RunOnUiThread(() => textbox.Text = Mainmenu.gettearinstancia().label.Text);
                        }
                    }
                    else {

                        RunOnUiThread(() => textbox.Text = "Buscando...");
                    }
                }
                else
               if (Mainmenu.gettearinstancia() != null)
                {
                    if (Mainmenu.gettearinstancia().buscando == false)
                    {
                        if (Mainmenu.gettearinstancia().label.Text.Trim() != ""
                        && textbox.Text != Mainmenu.gettearinstancia().label.Text)
                        {
                            RunOnUiThread(() => textbox.Text = Mainmenu.gettearinstancia().label.Text);
                        }
                    }else
                        RunOnUiThread(() => textbox.Text = "Buscando...");

                }


                if (textbox.Text.Trim() == "" && textbox.Text.Trim() != "No hay elementos en cola")
                {

                    RunOnUiThread(() => { textbox.Text = "No hay elementos en cola"; });
                }
                Thread.Sleep(500);
            }
            
        }

        public void llenarlistas() {


            var jsonremoto = Mainmenu.gettearinstancia().jsonlistasremotas;
            if (jsonremoto.Trim().Length > 0)
                listasremotas = JsonConvert.DeserializeObject<List<PlayList>>(jsonremoto);




            if (System.IO.Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/"))
            {
                string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");
              
                foreach (var elementos in items)
                {
                    var elementolista = new PlayList();
                    elementolista.Name = System.IO.Path.GetFileNameWithoutExtension(elementos);
                    var text = File.ReadAllText(elementos).Trim();
                    try
                    {
                        elementolista.Portrait = text.Split('$')[1].Split(';')[0];
                    }
                    catch (Exception)
                    {
                        elementolista.Portrait = "";
                    }
                    elementolista.MediaElements = new List<PlaylistElement>();
                    listaslocales.Add(elementolista);

                }

            }

          
                if (listaslocales.Count > 0)
                {
                    var adap = new adapterlistaremoto(this, listaslocales.Select(asd => asd.Name).ToList(), listaslocales.Select(asd => asd.Portrait).ToList());
                RunOnUiThread(() => {
                    var parcelable = lista.OnSaveInstanceState();
                    lista.Adapter = adap;
                    lista.OnRestoreInstanceState(parcelable);
                });
            }
                else {
                    var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                    RunOnUiThread(() => {
                        var parcelable = lista.OnSaveInstanceState();
                        lista.Adapter = adaptadol;
                        lista.OnRestoreInstanceState(parcelable);
                    });

                }

   


        }

        protected override void OnDestroy()
        {

          
            detenedor = false;
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