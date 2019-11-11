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
using System.Text.RegularExpressions;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class menulistaoffline : Activity
    {
        TextView textboxtitulo;
      
        LinearLayout lineall2;
        LinearLayout lineaa;
        public Thread tree;
        public ListView listbox;
        public List<string> listareprod;
        public List<string> listareprod2;
       
        public bool eneliminacion = false;
        string listaenlinea;
       public static menulistaoffline instance;
        public static menulistaoffline gettearinstancia() {

            return instance;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Menulistaoffline);
           string ipadress= Intent.GetStringExtra("ipadre");
         
            listareprod = new List<string>();
            listareprod2 = new List<string>();
            textboxtitulo = FindViewById<TextView>(Resource.Id.textView1);
            ImageView playpause = FindViewById<ImageView>(Resource.Id.imageView5);
           var botonelimiar= FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.imageView7);
            var botonagregar = FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.imageView6);
            ImageView botonhome = FindViewById<ImageView>(Resource.Id.imageView4);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
            ImageView botonreproducir = FindViewById<ImageView>(Resource.Id.imageView3);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
           listaenlinea = Intent.GetStringExtra("listaenlinea");
            lineaa = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            instance = this;

            botonagregar.Enabled = true;
            //  animar2(lineall2);
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => {
                var parcelable = listbox.OnSaveInstanceState();
                listbox.Adapter = adaptadolo;
                listbox.OnRestoreInstanceState(parcelable);
            });
            llenarlista();
            AnimationHelper.AnimateFAB(botonagregar);        
            // lineaa.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            tree = new Thread(new ThreadStart(cojerstream));
            tree.Start();
            textboxtitulo.Selected = true;
            // botonelimiar.SetBackgroundResource(Resource.Drawable.playlistedit);
            UiHelper.SetBackgroundAndRefresh(this);
          //  lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));

            listbox.ItemClick += (aaa, aaaa) =>
            {
                if (listareprod.Count > 0) {
                
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
                }
            };
            botonelimiar.Click += delegate
            {
                animar(botonelimiar);
                if (eneliminacion)
                {
                    
                    eneliminacion = false;
                  //  botonelimiar.SetBackgroundResource(Resource.Drawable.playlistedit);
                    ArrayAdapter adaptador = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, listareprod);
                    var parcelable = listbox.OnSaveInstanceState();

                    listbox.Adapter = adaptador;
                    listbox.OnRestoreInstanceState(parcelable);
                }
              
                else
                {
                 //   botonelimiar.SetBackgroundResource(Resource.Drawable.playlistcheck);
                    adapterlistaremotoconeliminar adaptador = new adapterlistaremotoconeliminar(this, listareprod, listareprod2, "noser", true, false);
                    var parcelable = listbox.OnSaveInstanceState();
                    listbox.Adapter = adaptador;
                    listbox.OnRestoreInstanceState(parcelable);
                    eneliminacion = true;

                }
            };
            botonagregar.Click += delegate
            {

                /*  Intent intento = new Intent(this,typeof(agregarlistaoffline));               
                  intento.PutExtra("ipadre", ipadress);
                  StartActivity(intento);
                  */
                EditText texto = new EditText(this);
                texto.Hint = "Nombre de la lista";
                
                new AlertDialog.Builder(this)
                   .SetTitle("Introduzca el nombre de la nueva lista de reproduccion")
                   .SetView(texto)
                   .SetCancelable(false)
                   .SetNegativeButton("Cancelar", (ax, ax100) => { })
                   .SetPositiveButton("Crear", (xd, xdd) => {

                       if (texto.Text.Length <3)
                       {
                           Toast.MakeText(this, "El nombre debe tener almenos 3 caracteres", ToastLength.Long).Show();
                       }
                       else {
                           var saas = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                           if (!saas.Contains(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters(texto.Text)))
                           {
                               crearlista(texto.Text);
                           }
                           else
                           {
                               AlertDialog.Builder ad = new AlertDialog.Builder(this);
                               ad.SetTitle("Advertencia");
                               ad.SetMessage("El elemento " + texto.Text + " ya existe desea reemplazarlo??");
                               ad.SetCancelable(false);
                               ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                               ad.SetPositiveButton("Si", (axx,axxx)=> {
                                   crearlista(texto.Text);
                               });
                               ad.SetNegativeButton("No", (ux,uxdd)=> {
                                   Toast.MakeText(this, "Operacion cancelada", ToastLength.Long).Show();
                               });
                               ad.Create();
                               ad.Show();
                           }




                       }
              

                  

                   })
                   .Create().Show();

             
            };
       
            playpause.Click += delegate
                 {
                     animar(playpause);
                     MainmenuOffline.gettearinstancia().play.PerformClick();
                  
                 };
            botonhome.Click += delegate
            {

                animar(botonhome);
              
                tree.Abort();
                this.Finish();

            };
 




            // Create your application here
        }
        public override void OnBackPressed()
        {
          
            tree.Abort();
            this.Finish();
        }
        public void cojerstream()
        {
          
          
          
                    while (!this.IsDestroyed)
                    {
                  
                            if (MainmenuOffline.gettearinstancia() != null)
                            {

                    if (MainmenuOffline.gettearinstancia().buscando != true)
                    {
                        if (MainmenuOffline.gettearinstancia().label.Text != textboxtitulo.Text
                          && MainmenuOffline.gettearinstancia().label.Text.Trim() != "")                    
                            RunOnUiThread(() => textboxtitulo.Text = MainmenuOffline.gettearinstancia().label.Text);
                    }
                    else {
                        RunOnUiThread(() => textboxtitulo.Text = "Buscando...");
                    }
                            }
                            else
                                  if (Mainmenu.gettearinstancia() != null)
                            {
                                 
                                   RunOnUiThread(() => textboxtitulo.Text = Mainmenu.gettearinstancia().label.Text);
                            }



                if (textboxtitulo.Text.Trim() == "" && textboxtitulo.Text.Trim()!= "No hay elementos en cola") {

                    RunOnUiThread(() => { textboxtitulo.Text = "No hay elementos en cola"; });
                }


                Thread.Sleep(1000);
                        }
                  
               
          
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
        public  void llenarlista()
        {
            var linkesess = new List<string>();
            listareprod.Clear();
            try
            {
                string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");
             
          
                    for(int i=0;i<items.Length;i++)

                {
                    listareprod2 = items.ToList();
                listareprod.Add(Path.GetFileNameWithoutExtension(items[i]));
                    if (File.ReadAllText(items[i]).Trim().Length > 4)
                    {
                        linkesess.Add(File.ReadAllText(items[i]).Trim().Split('$')[1].Split(';')[0]);
                    }
                    else {
                        linkesess.Add(items[i]);
                    }
                }
                adapterlistaremotoconeliminar adaptador2 = new adapterlistaremotoconeliminar(this, listareprod, linkesess, "noser", true, false);
                RunOnUiThread(() =>
                {
                    var parcelable = listbox.OnSaveInstanceState();
                    listbox.Adapter = adaptador2;
                    listbox.OnRestoreInstanceState(parcelable);

                });
                if (listareprod.Count == 0) {

                 
                        var adaptadolss = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                        RunOnUiThread(() => {
                            var parcelable = listbox.OnSaveInstanceState();
                            listbox.Adapter = adaptadolss;
                            listbox.OnRestoreInstanceState(parcelable);

                        });
                 
                }

            }
            catch (Exception)
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                llenarlista();
            }
        }
        public void crearlista(string playlistname) {
            StreamWriter escritor;
            escritor = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters(playlistname));
            escritor.Write("  $  ");
            escritor.Close();
            llenarlista();
            Toast.MakeText(this, "Lista guardada satisfactoriamente", ToastLength.Long).Show();
            MultiHelper.ExecuteGarbageCollection();
            new Thread(() =>
            {
                MainmenuOffline.gettearinstancia().llenarplaylist();
            }).Start();
        }
        private static string RemoveIllegalPathCharacters(string path)
        {
         string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
         var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
         return r.Replace(path, "");
        }
    }

}