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
using System.Linq;
using System;

namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.DesignDemo", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, AlwaysRetainTaskState = true)]
    public class actividadinicio :AppCompatActivity
    {
        bool isserver = false;
        RecyclerView listasugerencias;
        DrawerLayout sidem;
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Android.App.ProgressDialog alerta;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        NavigationView itemsm;
        historial objetohistorial;
        RecyclerView listaultimos;
        RecyclerView listamas;
       public static actividadinicio ins;
        TextView textoserver;
        TextView textolista;
        TextView textonombreelemento;
        RecyclerView listafavoritos;
        bool detenedor = true;
        public List<playlistelements> favoritos = new List<playlistelements>();
        public Android.Support.V7.App.AlertDialog alertareproducirvideo = null;
        public Dictionary<string,playlistelements>  Diccfavoritos = new Dictionary<string,playlistelements>();
        /*
         0-mas reproducidos
         1-reproducidos ultimamente
         2-videos favoritos
         3-sugerencias
             
             */
        public List<LinearLayout> secciones = new List<LinearLayout>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
          
            if (mainmenu_Offline.gettearinstancia() != null)
                SetContentView(Resource.Layout.layoutinicio);
            else {

                SetContentView(Resource.Layout.layoutinicioremote);
            }
            ins = this;
            listamas = FindViewById<RecyclerView>(Resource.Id.listamas);
            listaultimos = FindViewById<RecyclerView>(Resource.Id.listaultimos);
         listafavoritos = FindViewById<RecyclerView>(Resource.Id.listafavoritos);
            listasugerencias = FindViewById<RecyclerView>(Resource.Id.listasugerencias);
            List<playlistelements> listafake = new List<playlistelements>();
            var imagentipoconex = FindViewById<ImageView>(Resource.Id.tipoconexion);
            textoserver = FindViewById<TextView>(Resource.Id.textoconexion);
            textonombreelemento = FindViewById<TextView>(Resource.Id.textoreprodactual);
           textolista = FindViewById<TextView>(Resource.Id.textoencola);
            SetSupportActionBar(FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.my_toolbar));
            secciones = new List<LinearLayout>()
            {
                FindViewById<LinearLayout>(Resource.Id.secion1),
                FindViewById<LinearLayout>(Resource.Id.secion2),
                 FindViewById<LinearLayout>(Resource.Id.secion3),
                  FindViewById<LinearLayout>(Resource.Id.secion4)

            };

            foreach (var ax in secciones)
                ax.Visibility = ViewStates.Gone;

           
            SupportActionBar.Title = "Inicio";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            itemsm = FindViewById<NavigationView>(Resource.Id.content_frame);
            sidem = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.hambur);
            textolista.Selected = true;
            textoserver.Selected = true;
            textonombreelemento.Selected = true;
            if (mainmenu_Offline.gettearinstancia() != null)
            {
                
                isserver = true;
                Diccfavoritos = mainmenu_Offline.gettearinstancia().listafavoritos;
                favoritos = Diccfavoritos.Values.ToList();
               
                objetohistorial = mainmenu_Offline.gettearinstancia().objetohistorial;
                imagentipoconex.SetBackgroundResource(Resource.Drawable.remotecontrolbuttons);
            }
            else {
                isserver = false;
                Diccfavoritos = mainmenu.gettearinstancia().listafavoritos;
                favoritos = Diccfavoritos.Values.ToList();
                imagentipoconex.SetBackgroundResource(Resource.Drawable.antena);
                objetohistorial = mainmenu.gettearinstancia().objetohistorial;
            }

            if (objetohistorial == null) 
                objetohistorial = new historial()
                {
                    videos = new List<playlistelements>(),
                    links = new Dictionary<string, int>()

                };

            if (favoritos == null)
                 favoritos = new List<playlistelements>();

            if (Diccfavoritos == null)
                Diccfavoritos = new Dictionary<string, playlistelements>();
            
            

            favoritos.Reverse();
            recargarhistorial();
          
            new System.Threading.Thread(() =>
            {

                seteartexto();
            }).Start();
             

         

        /*    for (int i = 0; i < 20; i++)
            {

                listafake.Add(new playlistelements
                {
                    nombre = "klk wawawa",
                    link = "https://www.youtube.com/watch?v=4G40N0G6lzE"
                });
            }
            */

 
                 

            LinearLayoutManager enlinea = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listamas.SetLayoutManager(enlinea);
            LinearLayoutManager enlinea2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listaultimos.SetLayoutManager(enlinea2);
            LinearLayoutManager enlinea3 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listafavoritos.SetLayoutManager(enlinea3);
            LinearLayoutManager enlinea4 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            listasugerencias.SetLayoutManager(enlinea4);

            adaptadorcartas adap = new adaptadorcartas(listafake, this);

            //   listafavoritos.SetAdapter(adap);


            FindViewById<CardView>(Resource.Id.cartainicio).Click += delegate
            {

                this.Finish();
            };

            itemsm.NavigationItemSelected += (sender, e) =>
            {
                switch (e.MenuItem.TitleFormatted.ToString()) {

                    case "Reproductor":
                        this.Finish();
                        break;
                    case "Control remoto":
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









        public static actividadinicio gettearinstancia() {

            return ins;
        }
        public void cargarmasvistos()
        {
            try
            {
                var linksorted = objetohistorial.links.OrderBy(x => x.Value).ToList();
                linksorted.Reverse();
                var elemsorted = new List<playlistelements>();

                foreach (var elem in linksorted)
                {
                    elemsorted.Add(objetohistorial.videos.FirstOrDefault(x => x.link == elem.Key));

                }
                RunOnUiThread(() =>
                {
                    adaptadorcartas adap = new adaptadorcartas(elemsorted.Take(30).ToList(), this);
                    adap.ItemClick += (aa, aaa) =>
                    {
                        RunOnUiThread(() =>
                        {
                            var elemento = elemsorted[aaa.Position];
                            Intent intento = new Intent(this, typeof(customdialogact));

                            intento.PutExtra("url", elemento.link);
                            intento.PutExtra("titulo", elemento.nombre);
                            intento.PutExtra("imagen", "http://i.ytimg.com/vi/" + elemento.link.Split('=')[1] + "/mqdefault.jpg");
                            StartActivity(intento);
                        });
                    };
                    listamas.SetAdapter(adap);
                    if (elemsorted.Count > 0)
                        secciones[0].Visibility = ViewStates.Visible;
                    else
                        secciones[0].Visibility = ViewStates.Gone;
                });
            }
            catch (Exception) {

            }
        }


        public void cargarhistorial() {
            try { 
            var elementos = objetohistorial.videos;
            elementos.Reverse();
            var historyelements = elementos.Take(30).ToList();
       
            adaptadorcartas adap = new adaptadorcartas(historyelements, this);
            adap.ItemClick += (aa, aaa) =>
            {
                RunOnUiThread(() =>
                {
                    var elemento = historyelements[aaa.Position];
                    Intent intento = new Intent(this, typeof(customdialogact));

                    intento.PutExtra("url", elemento.link);
                    intento.PutExtra("titulo", elemento.nombre);
                    intento.PutExtra("imagen", "http://i.ytimg.com/vi/" + elemento.link.Split('=')[1] + "/mqdefault.jpg");
                    StartActivity(intento);
                });
            };
            listaultimos.SetAdapter(adap);
            if (historyelements.Count > 0)
                secciones[1].Visibility = ViewStates.Visible;
            else
                secciones[1].Visibility = ViewStates.Gone;


        }
            catch (Exception) {

            }
}




        public void cargarfavoritos()
        {


            try
            {
                adaptadorcartas adap = new adaptadorcartas(favoritos, this);
                adap.ItemClick += (aa, aaa) =>
                {
                    RunOnUiThread(() =>
                    {
                        var elemento = favoritos[aaa.Position];
                        Intent intento = new Intent(this, typeof(customdialogact));

                        intento.PutExtra("url", elemento.link);
                        intento.PutExtra("titulo", elemento.nombre);
                        intento.PutExtra("imagen", "http://i.ytimg.com/vi/" + elemento.link.Split('=')[1] + "/mqdefault.jpg");
                        StartActivity(intento);
                    });
                };
                adap.ItemLongClick += (aa, aaa) =>
                {
                    RunOnUiThread(() =>
                    {
                        var elemento = favoritos[aaa.Position];
                        new Android.App.AlertDialog.Builder(this)
                        .SetTitle("Advertencia")
                        .SetMessage("Desea eliminar este elemento de la lista de favoritos?")
                        .SetPositiveButton("Si", (aax, asd) =>
                        {
                            Diccfavoritos = clasesettings.agregarfavoritos(this, Diccfavoritos, elemento);
                            favoritos = Diccfavoritos.Values.ToList();
                            favoritos.Reverse();
                            cargarfavoritos();
                        })
                        .SetNegativeButton("No", (asdd, ffff) => { })
                        .Create()
                        .Show();
                    });
                };
                RunOnUiThread(() =>
                {
                    listafavoritos.SetAdapter(adap);
                    if (favoritos.Count > 0)
                    {
                        secciones[2].Visibility = ViewStates.Visible;
                        if (!clasesettings.probarsetting("dialogofavoritos"))
                        {
                            RunOnUiThread(() =>
                            {
                                new Android.Support.V7.App.AlertDialog.Builder(this)
                                 .SetTitle("Informacion")
                                 .SetMessage("Al dejar presionado un elemento de favoritos usted podra eliminarlo de la lista")
                                 .SetCancelable(false)
                                 .SetPositiveButton("Entendido", (aa, fddggfd) => { })
                                 .Create()
                                 .Show();
                                clasesettings.guardarsetting("dialogofavoritos", "si");
                            });

                        }





                    }
                    else
                    {
                        secciones[2].Visibility = ViewStates.Gone;
                    }

                });

            }
            catch (Exception) {
            }

        }
        public void cargarresults()
        {
            try
            {
                RunOnUiThread(() =>
                {



#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    alerta = new ProgressDialog(this);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    alerta.SetTitle("Buscando videos sugeridos");
                    alerta.SetMessage("Por favor espere...");
                    alerta.SetCancelable(false);
                    alerta.Create();
                    alerta.Show();


                });

                YoutubeSearch.VideoSearch buscar = new YoutubeSearch.VideoSearch();
                List<YoutubeSearch.VideoInformation> results = new List<YoutubeSearch.VideoInformation>();
                if (mainmenu_Offline.gettearinstancia() != null)
                    if (mainmenu_Offline.gettearinstancia().sugerencias.Count > 0)
                        results = mainmenu_Offline.gettearinstancia().sugerencias;
                    else
                    {
                        results = buscar.SearchQuery("", 1);
                        mainmenu_Offline.gettearinstancia().sugerencias = results;
                    }
                else
                if (mainmenu.gettearinstancia() != null)
                    if (mainmenu.gettearinstancia().sugerencias.Count > 0)
                        results = mainmenu.gettearinstancia().sugerencias;
                    else
                    {
                        results = buscar.SearchQuery("", 1);
                        mainmenu.gettearinstancia().sugerencias = results;
                    }




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

                    if (listafake.Count > 0)
                        secciones[3].Visibility = ViewStates.Visible;
                    else
                        secciones[3].Visibility = ViewStates.Gone;

                });
            }
            catch (Exception) {
               RunOnUiThread(()=>alerta.Dismiss());
            }
        }
        public void recargarhistorial() {


            new System.Threading.Thread(() =>
            {
                cargarfavoritos();

            }).Start();
           
            if (objetohistorial.videos.Count > 0)
            {

                new System.Threading.Thread(() =>
                {
                    cargarmasvistos();

                }).Start();
                RunOnUiThread(() =>
                {
                    cargarhistorial();
                });
            }
            new System.Threading.Thread(() =>
            {
                cargarresults();

            }).Start();
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
        public void seteartexto() {
            
            while (detenedor) {
                if (mainmenu.gettearinstancia() != null || mainmenu_Offline.gettearinstancia() != null) { 
                if (isserver) {

                    RunOnUiThread(() =>
                    {
                        textolista.Text = mainmenu_Offline.gettearinstancia().lapara.Count + " Elementos en cola";
                        textoserver.Text = mainmenu_Offline.gettearinstancia().clienteses.Count + " Clientes conectados";
                        if (mainmenu_Offline.gettearinstancia().label.Text.Trim() != "") { 

                            if(textonombreelemento.Text != mainmenu_Offline.gettearinstancia().label.Text)
                                   textonombreelemento.Text = mainmenu_Offline.gettearinstancia().label.Text;
                        }
                        else
                            textonombreelemento.Text = "Ningún elemento el reproducción";
                    });
                }
                else

                {
                    RunOnUiThread(() =>
                    {
                        textolista.Text = mainmenu.gettearinstancia().lista.Count + " Elementos en cola";
                        textoserver.Text ="Conectado a "+  mainmenu.gettearinstancia().devicename;
                        if (mainmenu.gettearinstancia().label.Text.Trim() != "") {
                            if (textonombreelemento.Text != mainmenu.gettearinstancia().label.Text)
                                textonombreelemento.Text = mainmenu.gettearinstancia().label.Text;
                        }                           
                        else
                            textonombreelemento.Text = "Ningún elemento el reproducción";
                    });

                }
              


           
                }
                System.Threading.Thread.Sleep(3000);
            }
        }

        protected override void OnDestroy()
        {
            ins = null;
            detenedor = false;
            base.OnDestroy();
        }
        public override void Finish()
        {
            ins = null;
        
            base.Finish();
            OverridePendingTransition(0, 0);
        }
        public override void OnBackPressed()
        {
           
            if (sidem.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                RunOnUiThread(() => { sidem.CloseDrawers(); });
            else
            {
               
                    clasesettings.preguntarsimenuosalir(this);
        
            }
          

        }


    }




}