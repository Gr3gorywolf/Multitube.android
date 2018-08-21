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
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.Dialog.NoActionBar")]
    public class actfastsearcher : Activity
    {
        string termino = "";
        string ip = "";
        ProgressBar progreso;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutfastsearcher);
            EditText textobuscar = FindViewById<EditText>(Resource.Id.edittext1);
            ImageView botonbuscarr = FindViewById<ImageView>(Resource.Id.ricochet);
            ImageView botoncerrar = FindViewById<ImageView>(Resource.Id.imageView2);
            ip = Intent.GetStringExtra("ipadres");
            progreso = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progreso.Max = 100;
            this.SetFinishOnTouchOutside(false);
            botonbuscarr.Click += delegate
            {
             
                    termino = textobuscar.Text;
                    new Thread(() => { buscaryabrir(); }).Start();
                    Toast.MakeText(this, "Buscando resultados...", ToastLength.Long).Show();
                   progreso.Progress = 10;
               
            };
            botoncerrar.Click += delegate
            {
                this.Finish();
            };
           
            // Create your application here
        }



        public void buscaryabrir()
        {
            try
            {
                string url = getearurl(termino);
            RunOnUiThread(() => progreso.Progress = 50);
           if (url != "%%nulo%%")
            {


            var a=clasesettings.gettearvideoid(url, true);
                RunOnUiThread(() => progreso.Progress = 100);
                Intent intentar = new Intent(this, typeof(customdialogact));
            
                RunOnUiThread(() => {

                
                    intentar.PutExtra("ipadress", ip);
                    intentar.PutExtra("imagen", "https://i.ytimg.com/vi/" + url.Split('=')[1] + "/mqdefault.jpg") ;
                    intentar.PutExtra("url", url);
                    intentar.PutExtra("titulo", a.titulo);
                    intentar.PutExtra("color", "DarkGray");
                    StartActivity(intentar);
                  


                });
                }
            }
            catch (Exception)
            {
                RunOnUiThread(() => Toast.MakeText(this, "No se encotraron resultados", ToastLength.Long).Show());
                RunOnUiThread(() => progreso.Progress = 0);
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
                    var retorno = cliente.GetStringAsync("https://decapi.me/youtube/videoid?search=" + titttt);
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