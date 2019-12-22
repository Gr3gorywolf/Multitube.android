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
    public class Video
    {

        public string Url { get; set; }
        public string Title { get; set; }
        public Android.Graphics.Bitmap Portrait { get; set; }
        public string Duration { get; set; }
        public string ImageUrl { get; set; }

    }
}