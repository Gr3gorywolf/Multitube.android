using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Models
{
    public class History
    {
        
        public List<PlaylistElement> Videos { get; set; }
        public Dictionary<string, int> Links { get; set; }
    }
}