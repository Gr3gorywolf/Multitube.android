using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Android.App;
using Android.Content;
using Android.OS;

using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace App1
{
   public class Videos
    {
    
            public string url { get; set; }
            public string nombre { get; set; }
            public Android.Graphics.Bitmap imagen { get; set; }
            public string tiempo { get; set; }
        public string imgurl { get; set; }

    }
    public class Videosimage
    {

      
        public string nombre { get; set; }
        public string imagen { get; set; }
   

    }
}