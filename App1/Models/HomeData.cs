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
    public class HomeData
    {

        public List<PlaylistElement> LastVideos { get; set; }
        public List<PlaylistElement> Suggestions { get; set; }
        public List<PlaylistElement> Favorites { get; set; }
    }

}