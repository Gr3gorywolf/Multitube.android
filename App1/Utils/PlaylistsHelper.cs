using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Models;
using Newtonsoft.Json;

namespace App1.Utils
{
    public class PlaylistsHelper
    {

        public static bool HasMediaElements
        {
            get
            {
                var elementlist = GetMedia(Constants.CachePath + "/downloaded.gr3d")
               .Concat(GetMedia(Constants.CachePath + "/downloaded.gr3d2")).ToList();
                var counter = 0;
                foreach (var elem in elementlist)
                {
                    if (File.Exists(elem.Path))
                        counter++;
                }
                if (counter > 0)
                    return true;
                else
                    return false;
            }
        }


        public static Dictionary<string, PlaylistElement> AddToFavouriteList(Activity context, Dictionary<string, PlaylistElement> list, PlaylistElement elements)
        {
            if (list.ContainsKey(elements.Link))
            {
                list.Remove(elements.Link);
                context.RunOnUiThread(() => { Toast.MakeText(context, "Elemento eliminado de favoritos", ToastLength.Long).Show(); });
            }
            else
            {
                list.Add(elements.Link, elements);
                context.RunOnUiThread(() => { Toast.MakeText(context, "Elemento agregado a favoritos. Ahora aparecera en su pantalla de inicio", ToastLength.Long).Show(); });
            }
            var arch = File.CreateText(Constants.CachePath + "/favourites.json");
            arch.Write(JsonConvert.SerializeObject(list));
            arch.Close();
            return list;
        }


        public static List<MediaElement> GetMedia(string path)
        {

            List<MediaElement> intmedia = new List<MediaElement>();
            if (File.Exists(path))
            {
                foreach (var parts in File.ReadAllText(path).Split('¤'))
                {
                    if (parts.Trim().Length > 0)
                    {
                        intmedia.Add(new MediaElement()
                        {
                            Name = parts.Split('²')[0],
                            Link = parts.Split('²')[1],
                            Path = parts.Split('²')[2]
                        });

                    }


                }
                return intmedia;
            }
            else
                return new List<MediaElement>();
        }

        public static string SerializeMedia(List<MediaElement> elementos)
        {
            string serialized = "";
            foreach (var elem in elementos)
                serialized += elem.Name + "²" + elem.Link + "²" + elem.Path + "¤";

            return serialized;
        }
    }
}