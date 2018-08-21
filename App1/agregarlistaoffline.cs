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

using System.Threading;
namespace App1
{
    [Activity(Label = "agregarlistaoffline", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class agregarlistaoffline : Activity
    {
        ListView listbox;
        EditText textbox;
        ImageView botonagregar;
        ImageView botonsalir;
        LinearLayout lineall;
        LinearLayout lineall2;
        ImageView botoneliminar;
        List<string> listalinks = new List<string>();

        List<string> listanombres= new List<string>();
        string elementossincortar;
        TcpClient cliente = new TcpClient();
        public bool eneliminacion = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Crearlistaoffline);
            cliente.Connect(Intent.GetStringExtra("ipadre"), 1024);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
            textbox= FindViewById<EditText>(Resource.Id.editText1);
            botonagregar = FindViewById<ImageView>(Resource.Id.imageView2);
            botoneliminar= FindViewById<ImageView>(Resource.Id.imageView3);
            botonsalir = FindViewById<ImageView>(Resource.Id.imageView1);
            elementossincortar = "";
            elementossincortar = clasesettings.gettearvalor("elementosactuales");
         
       
            lineall = FindViewById<LinearLayout>(Resource.Id.linearlayout0);
            lineall2 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => listbox.Adapter = adaptadolo);
            clasesettings.ponerfondoyactualizar(this);

            if (elementossincortar.Trim().Length> 5)
            {
            
                listanombres = elementossincortar.Split('$')[0].Split(';').ToList();
                listalinks = elementossincortar.Split('$')[1].Split(';').ToList();
            
            }
            if (listanombres.Count > 0)
            {
                if (listanombres[listanombres.Count - 1].Trim() == "")
                {
                    listanombres.RemoveAt(listanombres.Count - 1);

                }
                if (listalinks[listalinks.Count - 1].Trim() == "")
                {
                    listalinks.RemoveAt(listalinks.Count - 1);

                }
            }
           
            botoneliminar.SetBackgroundResource(Resource.Drawable.closecircularbuttonofacross);
            ArrayAdapter adaptador = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, listanombres);
            listbox.Adapter = adaptador;
           // lineall.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            animar2(lineall2);
            botoneliminar.Click += delegate
            {
                animar(botoneliminar);
                if (!eneliminacion)
                {
                   botoneliminar.SetBackgroundResource(Resource.Drawable.menucircularbutton);
                    adaptadorlista adalter = new adaptadorlista(this, listanombres,listalinks, textbox.Text, false, true);
                    listbox.Adapter = adalter;
                    eneliminacion = true;
                }
                else
                {
                    botoneliminar.SetBackgroundResource(Resource.Drawable.closecircularbuttonofacross);
                    adaptador = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, listanombres);
                    listbox.Adapter = adaptador;
                    eneliminacion = false;
                }
               

            };
            botonagregar.Click += delegate{
                
             
   animar(botonagregar);
                if (textbox.Text.Length >= 1)
                {
                   

                    var saas = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                    if(!saas.Contains(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + textbox.Text))
                    {
                        StreamWriter escritor;
                        string elementosfull = "";
                        escritor = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters( textbox.Text));

                        if (listanombres.Count - 1 > 0 && listanombres.Count - 1 > 0 && listanombres[0].Trim() != "" && listalinks[0].Trim() != "")
                        {
                            elementosfull = string.Join(";", listanombres) + ";" + "$" + string.Join(";", listalinks) + ";";
                        }
                        else
                        {
                            elementosfull = "  $  ";
                        }
                        escritor.Write(elementosfull);
                        escritor.Close();
                        cliente.Client.Send(Encoding.Default.GetBytes("actualizarplaylist()"));
                        cliente.Client.Disconnect(false);
                        Toast.MakeText(this, "Lista guardada satisfactoriamente", ToastLength.Long).Show();
                        Finish();
                        clasesettings.recogerbasura();
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
                    Toast.MakeText(this, "Por favor digite un nombre para la lista de reproduccion", ToastLength.Long).Show();
                }
            };
            botonsalir.Click += delegate
            {
              
                cliente.Client.Disconnect(false);
                animar(botonsalir);
                Finish();
                clasesettings.recogerbasura();
            };
        }

        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }

        public void ok(object sender, EventArgs e)
        {
            StreamWriter escritor;

            string elementosfull = "";
            escritor = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters(textbox.Text));
            if(listanombres.Count-1>0 && listanombres.Count - 1 > 0 && listanombres[0].Trim()!="" && listalinks[0].Trim() != "")
            {
                 elementosfull = string.Join(";", listanombres)+";" + "$" + string.Join(";", listalinks)+";";
            }
            else
            {
                elementosfull = "  $  ";
            }
            escritor.Write(elementosfull);
            escritor.Close();
            cliente.Client.Send(Encoding.Default.GetBytes("actualizarplaylist()"));
            cliente.Client.Disconnect(false);
            Toast.MakeText(this, "Lista guardada satisfactoriamente", ToastLength.Long).Show();
            Finish();
            clasesettings.recogerbasura();
        }
        public void no(object sender, EventArgs e)
        {

        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
            cliente.Client.Disconnect(false);
            Finish();
            clasesettings.recogerbasura();
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