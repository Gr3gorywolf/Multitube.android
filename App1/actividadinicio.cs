using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using System.Collections.Generic;
using System.Net;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Support.Design.Widget;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState = true)]
    public class actividadinicio :AppCompatActivity
    {

        RecyclerView listasugerencias;
        DrawerLayout sidem;
        Android.App.AlertDialog alerta;
        NavigationView itemsm;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (mainmenu_Offline.gettearinstancia() != null)
                SetContentView(Resource.Layout.layoutinicio);
            else {

                SetContentView(Resource.Layout.layoutinicioremote);
            }
            RecyclerView test = FindViewById<RecyclerView>(Resource.Id.listamas);
            RecyclerView listaultimos = FindViewById<RecyclerView>(Resource.Id.listaultimos);
            RecyclerView listafavoritos = FindViewById<RecyclerView>(Resource.Id.listafavoritos);
            listasugerencias = FindViewById<RecyclerView>(Resource.Id.listasugerencias);
            List<playlistelements> listafake = new List<playlistelements>();
            SetSupportActionBar(FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar));
            SupportActionBar.Title = "Inicio";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            itemsm = FindViewById<NavigationView>(Resource.Id.content_frame);
            sidem = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);

            new System.Threading.Thread(() =>
            {
                buscar();
            }).Start();
            for (int i = 0; i < 20; i++)
            {

                listafake.Add(new playlistelements
                {
                    nombre = "klk wawawa",
                    link = "https://www.youtube.com/watch?v=4G40N0G6lzE"
                });
            }


 
                 

            LinearLayoutManager enlinea = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            test.SetLayoutManager(enlinea);
            LinearLayoutManager enlinea2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listaultimos.SetLayoutManager(enlinea2);
            LinearLayoutManager enlinea3 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listafavoritos.SetLayoutManager(enlinea3);
            LinearLayoutManager enlinea4 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listasugerencias.SetLayoutManager(enlinea4);

            adaptadorcartas adap = new adaptadorcartas(listafake, this);
            test.SetAdapter(adap);
            listaultimos.SetAdapter(adap);
            listafavoritos.SetAdapter(adap);




            itemsm.NavigationItemSelected += (sender, e) =>
            {
                switch (e.MenuItem.TitleFormatted.ToString()) {

                    case "Reproductor":
                        this.Finish();
                        break;
                    case "Navegador personalizado":
                        Intent intento = new Intent(this, typeof(customsearcheract));
                        intento.PutExtra("ipadre","localhost");
                        intento.PutExtra("color", "black");
                        StartActivity(intento);                     
                        break;
                    case "Conectar clientes":
                       
                           
                            Intent intetno = new Intent(this, typeof(qrcodigoact));
                            intetno.PutExtra("ipadre", "localhost");
                            StartActivity(intetno);

                     
                            break;
                    case "Listas de reproduccion":

                        if (mainmenu_Offline.gettearinstancia() != null)
                        {
                            Intent elint = new Intent(this, typeof(menulistaoffline));
                            elint.PutExtra("ipadre", "localhost");
                            StartActivity(elint);
                        }
                        else {


                            var con = mainmenu.gettearinstancia();
                            if (con.compatible)
                            {
                                if (con.jsonlistasremotas.Trim() != "")
                                {
                                    Intent intentoo = new Intent(this, typeof(Reproducirlistaact));
                                    intentoo.PutExtra("ip", "localhost");
                                    StartActivity(intentoo);
                                  
                                }
                                else
                                {
                                    Toast.MakeText(this, "Aun no se han recibido las listas de el servidor...", ToastLength.Long).Show();
                                }
                            }
                            else
                            {
                                Toast.MakeText(this, "Su servidor no es compatible con esta version", ToastLength.Long).Show();
                            }




                        }

                        break;


                }
                e.MenuItem.SetChecked(false);
                e.MenuItem.SetChecked(false);
                sidem.CloseDrawers();
            };


            }

        public void buscar()
        {

            RunOnUiThread(() =>
            {

                ProgressBar progreso = new ProgressBar(this);
                alerta = new Android.App.AlertDialog.Builder(this).SetTitle("buscando resultados")
                .SetView(progreso)
                .SetCancelable(false)
                .SetMessage("Por favor espere")
                .Create();
                alerta.Show();

            });

            YoutubeSearch.VideoSearch buscar = new YoutubeSearch.VideoSearch();
            var results = buscar.SearchQuery("", 1);
            List<playlistelements> listafake = new List<playlistelements>();
            foreach (var data in results)
            {

                listafake.Add(new playlistelements
                {
                    nombre = WebUtility.HtmlDecode(data.Title),
                    link = data.Url
                });


            }
            RunOnUiThread(() =>
            {
                adaptadorcartas adap = new adaptadorcartas(listafake, this);
                adap.ItemClick += (aa, aaa) =>
                {
                    RunOnUiThread(() =>
                    {
                        var elemento = listafake[aaa.Position];
                        Intent intento = new Intent(this, typeof(customdialogact));

                        intento.PutExtra("url", elemento.link);
                        intento.PutExtra("titulo", elemento.nombre);
                        intento.PutExtra("imagen", "http://i.ytimg.com/vi/" + elemento.link.Split('=')[1] + "/mqdefault.jpg");
                        StartActivity(intento);
                    });
                };
                listasugerencias.SetAdapter(adap);
                alerta.Dismiss();
            });

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    sidem.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        public override void OnBackPressed()
        {
           
            if (sidem.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                RunOnUiThread(() => { sidem.CloseDrawers(); });
            else
            {
               
                    clasesettings.preguntarsimenuosalir(this);
        
            }
            base.OnBackPressed();

        }


    }




}