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
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.Dialog.NoActionBar")]
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
          //  linea.SetBackgroundColor(Android.Graphics.Color.DarkGray);
         //   linea1.SetBackgroundColor(Android.Graphics.Color.Black);
            nombrevideo = Intent.GetStringExtra("nombrevid");
            nombrevideo = RemoveIllegalPathCharacters(nombrevideo);
            nombrevideo = nombrevideo.Replace('$', ' ').Replace(';',' ');
            linkvideo= Intent.GetStringExtra("linkvid");
           linkvideo = RemoveIllegalPathCharacters(linkvideo);
            linkvideo = linkvideo.Replace('$', ' ').Replace(';', ' ');
            fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            this.SetFinishOnTouchOutside(false);
            var adaptadolo = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, new List<string> { "No hay elementos para mostrar.." });
            RunOnUiThread(() => listbox.Adapter = adaptadolo);
            llenarlista();

            new Thread(() =>
            {
                try
                {


                    var imagen = clasesettings.CreateBlurredImageonline(this, 20, linkvideo);
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
            };

        //    linea1.SetBackgroundColor(Android.Graphics.Color.ParseColor(clasesettings.gettearvalor("color")));
            cerrar.Click += delegate
            {
                this.Finish();
            };
            // Create your application here
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
                this.Finish();
                clasesettings.recogerbasura();
            }
            else
            {
                this.Finish();
                clasesettings.recogerbasura();
                Toast.MakeText(this, "El elemento ya existe en la lista", ToastLength.Long).Show();
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
                elementoscompletos = items.ToList();

                for (int i = 0; i < items.Length; i++)

                {
                   elementos.Add(Path.GetFileNameWithoutExtension(items[i]));
                  
                }
                ArrayAdapter adaptador = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, elementos);
                RunOnUiThread(() => listbox.Adapter = adaptador);
            }
            catch (Exception)
            {
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist");
                llenarlista();
            }
        }
    }
}