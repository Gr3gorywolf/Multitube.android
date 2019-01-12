using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Glide;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
namespace App1
{
    [Activity(Label = "Multitube", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, Theme = "@style/Theme.UserDialog")]
    public class dialogolistasact : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dialogolistas);
            var inst = Reproducirlistaact.gettearinstancia();
            ImageView fondo = FindViewById<ImageView>(Resource.Id.fondo1);
            ImageView imgaccion = FindViewById<ImageView>(Resource.Id.imagenaccion);
            TextView textoaccion= FindViewById<TextView>(Resource.Id.textView3);
            TextView textoheader = FindViewById<TextView>(Resource.Id.textView1);
            LinearLayout botonreproducir = FindViewById<LinearLayout>(Resource.Id.imageView3);
            LinearLayout botonaccion = FindViewById<LinearLayout>(Resource.Id.imageView2);
            LinearLayout botoninfo = FindViewById<LinearLayout>(Resource.Id.imageView5);
            this.SetFinishOnTouchOutside(true);
            textoheader.Text = Intent.GetStringExtra("nombrelista");
                Glide.With(this)
                .Load("http://i.ytimg.com/vi/"+Intent.GetStringExtra("portada").Split('=')[1] +"/mqdefault.jpg")
                .Into(fondo);
            var query= Intent.GetStringExtra("querry");
            if (query == "Fromlocal")
            {
                imgaccion.SetBackgroundResource(Resource.Drawable.downloadbutton);
                textoaccion.Text = "Guardar lista";
            }
            else {
                imgaccion.SetBackgroundResource(Resource.Drawable.upload);
                textoaccion.Text = "Enviar lista";

            }


            botonaccion.Click += delegate
            {
                new Thread(() =>
                {

                    if (query == "Fromlocal")
                    {
                      
                            mainmenu.gettearinstancia().playlistreceived = false;
                        RunOnUiThread(() =>
                        {
                            new Android.Support.V7.App.AlertDialog.Builder(this)
                                .SetTitle("Advertencia")
                                .SetMessage("Desea guardar la lista de reproduccion remota: " + Intent.GetStringExtra("nombrelista") + "??\n NOTA:\nSi hay una lista con este mismo nombre sera sustituida por esta")
                                .SetPositiveButton("Si", (aa, aaa) =>
                                {
                                    new Thread(() =>
                                    {

                                        var elementos = inst.listasremotas[inst.playlistidx];
                                        var nombreses = string.Join(";", elementos.elementos.Select(axx => axx.nombre).ToArray()) + ";";
                                        var linkeses = string.Join(";", elementos.elementos.Select(axx => axx.link).ToArray()) + ";";
                                        var archi = File.CreateText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + Intent.GetStringExtra("nombrelista"));
                                        archi.Write(nombreses + "$" + linkeses);
                                        archi.Close();
                                        RunOnUiThread(() => Toast.MakeText(this, "Lista guardada exitosamente", ToastLength.Long).Show());
                                    }).Start();
                                })
                                .SetNegativeButton("No", (aa, ff) => { })
                                .Create()
                                .Show();
                        });


                    }
                    else {


                       
                        var listilla = new List<playlistelements>();
                        var texto = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + inst.listaslocales[inst.playlistidx].nombre);
                        var nombreses = texto.Split('$')[0].Split(';').ToList();
                        var links = texto.Split('$')[1].Split(';').ToList();

                        var listaelementos = new List<playlistelements>();
                        for (int i = 0; i < nombreses.Count; i++)
                        {

                            if (nombreses[i].Trim() != "" && links[i].Trim() != "")
                            {
                                var elemento = new playlistelements()
                                {
                                    nombre = nombreses[i],
                                    link = links[i]
                                };
                                listaelementos.Add(elemento);
                            }
                        }
                        inst.listaslocales[inst.playlistidx].elementos = listaelementos;
                        mainmenu.gettearinstancia()
                            .clientelalistas
                            .Client
                            .Send(Encoding.UTF8.GetBytes("Receive__==__==__" + JsonConvert.SerializeObject(inst.listaslocales[inst.playlistidx])));
                        RunOnUiThread(() => Toast.MakeText(this, "Lista enviada exitosamente", ToastLength.Long).Show());

                  

                        RunOnUiThread(() => this.Finish());
                    }
                }).Start();
            };

            botoninfo.Click += delegate
            {
                new Thread(() =>
                {
                if (query == "Fromremote")
                {
                  
                     
                        if (inst.listaslocales.Count > 0)
                        {


                            var listilla = new List<playlistelements>();
                            var texto = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + inst.listaslocales[inst.playlistidx].nombre);
                            var nombreses = texto.Split('$')[0].Split(';').ToList();
                            var links = texto.Split('$')[1].Split(';').ToList();

                            var listaelementos = new List<playlistelements>();
                            for (int i = 0; i < nombreses.Count; i++)
                            {

                                if (nombreses[i].Trim() != "" || links[i].Trim() != "")
                                {
                                    var elemento = new playlistelements()
                                    {
                                        nombre = nombreses[i],
                                        link = links[i]
                                    };
                                    listaelementos.Add(elemento);
                                }
                            }
                            RunOnUiThread(() =>
                            {
                                ListView lista = new ListView(this);
                                adapterlistaremoto adapt = new adapterlistaremoto(this, listaelementos.Select(ax => ax.nombre).ToList(), listaelementos.Select(ax => ax.link).ToList());
                                lista.Adapter = adapt;
                                new AlertDialog.Builder(this)
                                .SetTitle("Elementos de esta lista de reproducción")
                                .SetView(lista).SetPositiveButton("Entendido!", (dd, fgf) => { })
                                .Create()
                                .Show();
                            });

                        }



                  

                }
                else {
                     
                         
                            var elementos = inst.listasremotas[inst.playlistidx];
                           RunOnUiThread(() =>
                            {
                                ListView lista = new ListView(this);
                                adapterlistaremoto adapt = new adapterlistaremoto(this, elementos.elementos.Select(ax => ax.nombre).ToList(), elementos.elementos.Select(ax => ax.link).ToList());
                                lista.Adapter = adapt;
                                new AlertDialog.Builder(this)
                                .SetTitle("Elementos de esta lista de reproducción")
                                .SetView(lista).SetPositiveButton("Entendido!", (dd, fgf) => { })
                                .Create()
                                .Show();
                            });
                      




                    }


                }).Start();
            };

            botonreproducir.Click += delegate {








                if (query == "Fromremote")
                {

                    if (inst.listaslocales.Count > 0)
                    {


                        var listilla = new List<playlistelements>();
                        var texto = File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/gr3playerplaylist/" + inst.listaslocales[inst.playlistidx].nombre);
                        var nombreses = texto.Split('$')[0].Split(';').ToList();
                        var links = texto.Split('$')[1].Split(';').ToList();

                        var listaelementos = new List<playlistelements>();
                        for (int i = 0; i < nombreses.Count; i++)
                        {

                            if (nombreses[i].Trim() != "" && links[i].Trim() != "")
                            {
                                var elemento = new playlistelements()
                                {
                                    nombre = nombreses[i],
                                    link = links[i]
                                };
                                listaelementos.Add(elemento);
                            }
                        }
                        inst.listaslocales[inst.playlistidx].elementos = listaelementos;
                        mainmenu.gettearinstancia()
                             .clientelalistas
                             .Client
                             .Send(Encoding.UTF8.GetBytes(query + "__==__==__" + JsonConvert.SerializeObject(inst.listaslocales[inst.playlistidx])));
                    }
                    else
                    {
                        Toast.MakeText(this, "No hay listas de reproduccion", ToastLength.Long).Show();
                    }
                }
                else
                {

                    if (inst.listasremotas.Count > 0)
                    {
                        mainmenu.gettearinstancia()
                            .clientelalistas
                            .Client
                            .Send(Encoding.UTF8.GetBytes(query + "__==__==__" + JsonConvert.SerializeObject(inst.listasremotas[inst.playlistidx])));
                    }
                    else
                    {
                        Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                    }

                }





                this.Finish();

            };




            // Create your application here
        }
    }
}