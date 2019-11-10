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

namespace App1.Utils
{
   public static class Constants
    {
        public static string FirebaseSuggestionsUrl = "";
        public static string FirebaseServerSelectionUrl = "";
        public static string FirebaseServerSelectionApiKey = "";
        public static string FirebaseServerSelectionUsername = "";
        public static string FirebaseServerSelectionPassword = "";
        public static string CachePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/.gr3cache";

    }
}