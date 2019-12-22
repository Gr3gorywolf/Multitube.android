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

namespace App1.Models
{
    public class PlayList
    {
        public string Name { get; set; }
        public string Portrait
        {
            get; set;
        }
        public List<PlaylistElement> MediaElements { get; set; }
    }
}