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
using Newtonsoft.Json;

namespace App1.Models
{

    public class PlaylistElement
    {
        [JsonProperty(PropertyName = "nombre")]
        public string Name { get; set; }
        public string Link { get; set; }
    }
}