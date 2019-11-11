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
using System.Threading;
using App1.Utils;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.UserDialog")]
    public class actividadagregarlistahecha : Activity
    {
        public string nombrevideo = "";
        public string linkvideo = "";
        ListView listbox;
        List<string> elementos = new List<string>();
        List<string> elementoscompletos = new List<string>();
        ImageView fondo;
        public int posicion = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.agregaralistahecha);
            listbox = FindViewById<ListView>(Resource.Id.listView1);
            ImageView cerrar = FindViewById<ImageView>(Resource.Id.imageView1);
            LinearLayout linea = FindViewById<LinearLayout>(Resource.Id.root);
            LinearLayout linea1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            Button nueva = FindViewById<Button>(Resource.Id.imageView3);
            //  linea.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            //   linea1.SetBackgroundColor(Android.Graphics.Color.Black);
            nombrevideo = Intent.GetStringExtra("nombrevid");
            nombrevideo = RemoveIllegalPathCharacters(nombrevideo);
            nombrevideo = nombrevideo.Replace('$', ' ').Replace(';',' ');
            linkvideo= Intent.GetStringExtra("linkvid");
           linkvideo = RemoveIllegalPathCharacters(linkvideo);
            linkvideo = linkvideo.Replace('$', ' ').Replace(';', ' ');
            fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            this.SetFinishOnTouchOutside(true);
        /*    var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => listbox.Adapter = adaptadolo);*/
            llenarlista();

            new Thread(() =>
            {
                try
                {


                    var imagen = ImageHelper.GetImageBitmapFromUrl("http://i.ytimg.com/vi/" + linkvideo.Split('=')[1] + "/mqdefault.jpg");
                    RunOnUiThread(() =>
                    {

                        fondo.SetImageBitmap(imagen);
                        animar4(fondo);
                    });
                }
                catch (Exception)
                {

                }
            }).Start();
                listbox.ItemClick += (aasd, adsf) =>
            {


                if (elementos.Count > 0) { 
                posicion = adsf.Position;
                AlertDialog.Builder ad = new AlertDialog.Builder(this);
                ad.SetCancelable(false);
                ad.SetMessage("Esta seguro que desea agregar "+nombrevideo+" A la lista de reproduccion "+elementos[adsf.Position]+"??" );
                ad.SetTitle("Advertencia");
                ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                ad.SetPositiveButton("Si", alertaok);
                ad.SetNegativeButton("No", alertano);
                ad.Create();
                ad.Show();
                }
            };

            //    linea1.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            nueva.Click += delegate
            {






                EditText texto = new EditText(this);
                texto.Hint = "Nombre de la lista";

                new AlertDialog.Builder(this)
                   .SetTitle("Introduzca el nombre de la nueva lista de reproduccion")
                   .SetView(texto)
                   .SetCancelable(false)
                   .SetNegativeButton("Cancelar", (ax, ax100) => { })
                   .SetPositiveButton("Crear", (xd, xdd) => {

                       if (texto.Text.Length < 3)
                       {
                           Toast.MakeText(this, "El nombre debe tener almenos 3 caracteres", ToastLength.Long).Show();
                       }
                       else
                       {
                           var saas = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                           if (!saas.Contains(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters(texto.Text)))
                           {
                               crearlista(texto.Text);
                               llenarlista();
                               if (menulistaoffline.gettearinstancia() != null)
                               {
                                   menulistaoffline.gettearinstancia().llenarlista();
                               }
                           }
                           else
                           {
                               AlertDialog.Builder ad = new AlertDialog.Builder(this);
                               ad.SetTitle("Advertencia");
                               ad.SetMessage("El elemento " + texto.Text + " ya existe desea reemplazarlo??");
                               ad.SetCancelable(false);
                               ad.SetIcon(Resource.Drawable.warningsignonatriangularbackground);
                               ad.SetPositiveButton("Si", (axx, axxx) => {
                                   crearlista(texto.Text);
                                   llenarlista();
                                   if (menulistaoffline.gettearinstancia() != null)
                                   {
                                       menulistaoffline.gettearinstancia().llenarlista();
                                   }

                                   new Thread(() =>
                                   {

                                       MainmenuOffline.gettearinstancia().llenarplaylist();
                                   }).Start();
                               });
                               ad.SetNegativeButton("No", (ux, uxdd) => {
                                   Toast.MakeText(this, "Operacion cancelada", ToastLength.Long).Show();
                               });
                               ad.Create();
                               ad.Show();
                           }




                       }




                   })
                   .Create().Show();






            };
            cerrar.Click += delegate
            {
                this.Finish();
            };
            // Create your application here
        }
        public void crearlista(string playlistname)
        {
            StreamWriter escritor;
            escritor = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + RemoveIllegalPathCharacters(playlistname));
            escritor.Write("  $  ");
            escritor.Close();
        
            Toast.MakeText(this, "Lista guardada satisfactoriamente", ToastLength.Long).Show();
            MultiHelper.ExecuteGarbageCollection();
        }
        private static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
        public bool encontroparecido(string link, List<string> listalinks)
        {
            bool encontro = false;

           
            foreach (string ee in listalinks)
            {
                if (ee.Length > 7)
                {

           
                if (ee.Split('=')[1] == link.Split('=')[1])
                {
                    encontro = true;
                }
                }
            }
            if (encontro)
            {

                return true;
            }

            else
            {

                return false;
            }
        }

        public void alertaok(object sender, EventArgs e)
        {
           
            string archivoentexto = File.ReadAllText(elementoscompletos[posicion]);
            if (!encontroparecido(linkvideo, archivoentexto.Split('$')[1].Split(';').ToList())){

        
            string nombreentero = archivoentexto.Split('$')[0] + nombrevideo;
            string linkentero = archivoentexto.Split('$')[1] + linkvideo;
         
            File.Delete(elementoscompletos[posicion]);
            var sd = File.CreateText(elementoscompletos[posicion]);
            sd.Write(nombreentero + ";$" + linkentero+";");
            sd.Close();
            Toast.MakeText(this, "Elemento agregado exitosamente", ToastLength.Long).Show();
              
                if (menulistaoffline.gettearinstancia() != null) {
                    menulistaoffline.gettearinstancia().llenarlista();
                }
                MultiHelper.ExecuteGarbageCollection();
                if (MainmenuOffline.gettearinstancia() != null) { 
                new Thread(() =>
                {

                    MainmenuOffline.gettearinstancia().llenarplaylist();
                }).Start();
                }
                this.Finish();



            }
            else
            {
                Toast.MakeText(this, "El elemento ya existe en la lista", ToastLength.Long).Show();
                if (menulistaoffline.gettearinstancia() != null)
                {
                    menulistaoffline.gettearinstancia().llenarlista();
                }
                MultiHelper.ExecuteGarbageCollection();
               
                this.Finish();

            }

        }
        public void animar4(Java.Lang.Object imagen)
        {

            Android.Animation.ObjectAnimator animacion = Android.Animation.ObjectAnimator.OfFloat(imagen, "translationY", 1000, 0);
            animacion.SetDuration(500);
            animacion.Start();

        }

        public void alertano(object sender, EventArgs e)
        {

    
        }
        public void llenarlista()
        {
            elementos.Clear();
            try
            {
                string[] items = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/");
                List<string> imagenames = new List<string>();
                elementoscompletos = items.ToList();

                for (int i = 0; i < items.Length; i++)

                {


                   elementos.Add(Path.GetFileNameWithoutExtension(items[i]));
                    var textofile = File.ReadAllText(items[i]).Trim();
                    if (textofile.Trim().Length > 4)
                    {
                        imagenames.Add(textofile.Split('$')[1].Split(';')[0]);
                    }
                    else {
                        imagenames.Add("");
                    }
                    textofile = "";      
                }
             
                if (elementos.Count == 0)
                {
                    var adaptadol = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
                    RunOnUiThread(() =>
                    {
                        var parcelable = listbox.OnSaveInstanceState();
                        listbox.Adapter = adaptadol;
                        listbox.OnRestoreInstanceState(parcelable);
                    }
                        );
                }
                else {
                    adapterlistaremoto adaptador = new adapterlistaremoto(this, elementos.ToList(), imagenames);
                    RunOnUiThread(() => {
                        var parcelable = listbox.OnSaveInstanceState();
                        listbox.Adapter = adaptador;
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
    }
}