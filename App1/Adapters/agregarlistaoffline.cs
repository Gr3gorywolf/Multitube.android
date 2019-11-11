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
using System.Text.RegularExpressions;
using System.Net.Sockets;
using Android.Support.Design;
using System.Threading;
using App1.Utils;

namespace App1
{
    [Activity(Label = "agregarlistaoffline", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo")]
    public class agregarlistaoffline : Activity
    {
        ListView listbox;
        EditText textbox;
        Android.Support.Design.Widget.FloatingActionButton botonagregar;
        ImageView botonsalir;
        LinearLayout lineall;
        LinearLayout lineall2;
        Android.Support.Design.Widget.FloatingActionButton botoneliminar;
        List<string> listalinks = new List<string>();

        List<string> listanombres= new List<string>();
       
        TcpClient cliente = new TcpClient();
        public bool eneliminacion = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Crearlistaoffline);
            cliente.Connect(Intent.GetStringExtra("ipadre"), 1024);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
            textbox= FindViewById<EditText>(Resource.Id.editText1);
            botonagregar = FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.imageView2);
            botoneliminar= FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.imageView3);
            botonsalir = FindViewById<ImageView>(Resource.Id.imageView1);
          
          //  elementossincortar = clasesettings.gettearvalor("elementosactuales");      
            lineall = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => {
                var parcelable = listbox.OnSaveInstanceState();
                listbox.Adapter = adaptadolo;
                listbox.OnRestoreInstanceState(parcelable);
            });
            UiHelper.SetBackgroundAndRefresh(this); 
            AlertDialog.Builder adx = new AlertDialog.Builder(this);
            adx.SetCancelable(false);
            adx.SetTitle("Cargar elementos en reproduccion");
            adx.SetIcon(Resource.Drawable.alert);
            adx.SetMessage("Desea cargar todos los elementos que estan en reproduccion actualmente?");
            adx.SetNegativeButton("No",nox);
            adx.SetPositiveButton("Si", six);
            adx.Create();
            adx.Show();

           
            eneliminacion = true;
           /* adapterlistaremoto adaptador = new adapterlistaremoto(this, listanombres,listalinks);
            listbox.Adapter = adaptador;*/
            lineall2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#2b2e30"));
           // animar2(lineall2);
            botoneliminar.Click += delegate
            {
               // animar(botoneliminar);
                if (!eneliminacion)
                {
                   //botoneliminar.SetBackgroundResource(Resource.Drawable.playlistcheck);
                    adaptadorlista adalter = new adaptadorlista(this, listanombres,listalinks, textbox.Text, false, true);
                    var parcelable = listbox.OnSaveInstanceState();

                    listbox.Adapter = adalter;
                    listbox.OnRestoreInstanceState(parcelable);
                    eneliminacion = true;
                }
                else
                {
                  //  botoneliminar.SetBackgroundResource(Resource.Drawable.playlistedit);
                  /*  adaptadorzz = new adapterlistaremoto(this, listanombres,listalinks);
                    listbox.Adapter = adaptadorzz;
                    eneliminacion = false;*/
                }
               

            };
            botonagregar.Click += delegate{
                
             
  // animar(botonagregar);
                if (textbox.Text.Length >= 3)
                {
                   

                    var saas = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                    if(!saas.Contains(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + textbox.Text))
                    {
                        StreamWriter escritor;
                        string elementosfull = "";
                        escritor = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters( textbox.Text));

                        if (listanombres.Count  > 0 && listalinks.Count  > 0 && listanombres[0].Trim() != "" && listalinks[0].Trim() != "")
                        {
                            elementosfull = string.Join(";", listanombres) + ";" + "$" + string.Join(";", listalinks) + ";";
                        }
                        else
                        {
                            elementosfull = "  $  ";
                        }
                        escritor.Write(elementosfull);
                        escritor.Close();
                        menulistaoffline.gettearinstancia().llenarlista();
                        cliente.Client.Disconnect(false);
                        Toast.MakeText(this, "Lista guardada satisfactoriamente", ToastLength.Long).Show();
                        Finish();
                        MultiHelper.ExecuteGarbageCollection();
                    }
                    else
                    {
                        AlertDialog.Builder ad = new AlertDialog.Builder(this);
                        ad.SetTitle("Advertencia");
                        ad.SetMessage("El elemento " + textbox.Text + " ya existe desea reemplazarlo??");
                        ad.SetCancelable(false);
                        ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                        ad.SetPositiveButton("Si", ok);
                        ad.SetNegativeButton("No", no);
                        ad.Create();
                        ad.Show();
                    }

                   
                    

                }
                else{
                    Toast.MakeText(this, "El nombre de la lista de reproduccion debe tener almenos 3 caracteres", ToastLength.Long).Show();
                }
            };
            botonsalir.Click += delegate
            {
              
                cliente.Client.Disconnect(false);
                animar(botonsalir);
                Finish();
                MultiHelper.ExecuteGarbageCollection();
            };
        }

        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
        public  void six(object sender, EventArgs e)
        {
            listanombres = MainmenuOffline.gettearinstancia().lapara;
            listalinks = MainmenuOffline.gettearinstancia().laparalinks;



            // botoneliminar.SetBackgroundResource(Resource.Drawable.playlistedit);
            if (listanombres.Count > 0)
            {
                adapterlistaremotoconeliminar adalter22 = new adapterlistaremotoconeliminar(this, listanombres, listalinks, textbox.Text, false, true);
                var parcelable = listbox.OnSaveInstanceState();
                listbox.Adapter = adalter22;
                listbox.OnRestoreInstanceState(parcelable);
            }


        }
        public  void nox(object sender, EventArgs e)
        {
           

        }
        public void ok(object sender, EventArgs e)
        {
            StreamWriter escritor;

            string elementosfull = "";
            escritor = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters(textbox.Text));
            if(listanombres.Count>0 && listalinks.Count > 0 && listanombres[0].Trim()!="" && listalinks[0].Trim() != "")
            {
                 elementosfull = string.Join(";", listanombres)+";" + "$" + string.Join(";", listalinks)+";";
            }
            else
            {
                elementosfull = "  $  ";
            }
            escritor.Write(elementosfull);
            escritor.Close();
            //  cliente.Client.Send(Encoding.Default.GetBytes("actualizarplaylist()"));
            menulistaoffline.gettearinstancia().llenarlista();
            cliente.Client.Disconnect(false);
            Toast.MakeText(this, "Lista guardada satisfactoriamente", ToastLength.Long).Show();
            Finish();
            MultiHelper.ExecuteGarbageCollection();
        }
        public void no(object sender, EventArgs e)
        {

        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
            cliente.Client.Disconnect(false);
            Finish();
            MultiHelper.ExecuteGarbageCollection();
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