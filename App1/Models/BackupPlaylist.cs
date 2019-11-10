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
    public class BackupPlaylist
    {
        public List<string> Titles { get; set; }
        public List<string> Links { get; set; }
        public List<string> Portraits { get; set; }
        public int Index { get; set; }
    }
}