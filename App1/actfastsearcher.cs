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
using System.Threading;
using System.Net;
using System.Net.Http;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.UserDialog")]
    public class actfastsearcher : Activity
    {
        string termino = "";
        string ip = "";
       
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        public ProgressDialog dialogoprogreso;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutfastsearcher);
            EditText textobuscar = FindViewById<EditText>(Resource.Id.editText1);
          //  ImageView botonbuscarr = FindViewById<ImageView>(Resource.Id.ricochet);
            ImageView botoncerrar = FindViewById<ImageView>(Resource.Id.imageView2);
            ip = Intent.GetStringExtra("ipadres");
          
            this.SetFinishOnTouchOutside(false);


            textobuscar.KeyPress += (aaxx, e) =>
            {
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    // Code executed when the enter key is pressed down

                    if (textobuscar.Text.Trim().Length>3)
                    {
                        termino = textobuscar.Text;
                        new Thread(() => { buscaryabrir(); }).Start();
                    }
                    else {
                        Toast.MakeText(this, "La busqueda debe contener almenos 3 caracteres", ToastLength.Long).Show() ;
                        }

                }
                else
                {
                    e.Handled = false;
                }
            };
         
            botoncerrar.Click += delegate
            {
                this.Finish();
            };
           
            // Create your application here
        }



        public void buscaryabrir()
        {


            RunOnUiThread(() =>
            {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                dialogoprogreso.SetCanceledOnTouchOutside(false);
                dialogoprogreso.SetCancelable(false);
                dialogoprogreso.SetTitle("Buscando resultados...");
                dialogoprogreso.SetMessage("Por favor espere");
                dialogoprogreso.Show();
            });
            try
            {
                string url = getearurl(termino);
                //  RunOnUiThread(() => progreso.Progress = 50);
                if (url != "%%nulo%%")
                {


                    var a = clasesettings.gettearvideoid(url, true,-1);
                    //    RunOnUiThread(() => progreso.Progress = 100);
                    if (a != null) { 
                    Intent intentar = new Intent(this, typeof(customdialogact));

                    RunOnUiThread(() =>
                    {
                        dialogoprogreso.Dismiss();

                        intentar.PutExtra("ipadress", ip);
                        intentar.PutExtra("imagen", "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg");
                        intentar.PutExtra("url", url);
                        intentar.PutExtra("titulo", a.titulo);
                        intentar.PutExtra("color", "DarkGray");
                        StartActivity(intentar);



                    });
                    }
                    else
                    {
                        RunOnUiThread(() =>
                        {
                            dialogoprogreso.Dismiss();
                            Toast.MakeText(this, "Error al extraer el video posiblemente los servidores esten en mantenimiento", ToastLength.Long).Show();
                        });
                    }
                }
                else {
                    RunOnUiThread(() =>
                    {
                        dialogoprogreso.Dismiss();
                    });
                }
            }
            catch (Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "No se encotraron resultados", ToastLength.Long).Show());
                dialogoprogreso.Dismiss();
            }
        }
        
         
              
    
        public string getearurl(string titttt)
        {

            try
            {

                string linkkk = "";
                string prra = "";
                bool esunlink = false;
                try
                {
                    var dfasdsf = titttt.Split('=')[1];
                    esunlink = true;
                }
                catch (Exception)
                {

                }
                if (!esunlink)
                {


                    HttpClient cliente = new HttpClient(new ModernHttpClient.NativeMessageHandler());
                    var retorno = cliente.GetStringAsync("http://decapi.me/youtube/videoid?search=" + titttt);
                    prra = retorno.Result;


                    linkkk = "http://www.youtube.com/watch?v=" + retorno.Result;
                }
                else
                {
                    linkkk = "http://www.youtube.com/watch?v=" + titttt.Split('=')[1];
                }


                if (prra.ToLower().Trim() == "invalid video id or search string." && !esunlink)
                {
                    RunOnUiThread(() => Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                    return "%%nulo%%";
                }
                else
                {
                    return linkkk;
                }

            }
            catch (Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "No se encontro el termino", ToastLength.Long).Show());
                return "%%nulo%%";
            }

        }

    }
  
}