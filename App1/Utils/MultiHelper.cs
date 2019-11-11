using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using VideoLibrary;
using Android.Renderscripts;
using System.Net;
using Android.Net.Wifi;
using Android.Net;
using System.IO;
using Android.Glide;
using Android.Glide.Request;
using Android.Support.V7.Palette;
using System.Drawing;
using Android.Support.V7.Graphics;
using System.IO.Compression;
using Newtonsoft.Json;
using HtmlAgilityPack;
using Bitmap = Android.Graphics.Bitmap;
using App1.Models;
using App1.Utils;

namespace App1
{



   
   
 


   

    
  
  
   
    class MultiHelper
    {


        public static Activity context = null;
        public static bool updateschecked = false; 
         
      
        public static bool HasInternetConnection()
        {
            string CheckUrl = "https://www.youtube.com/";

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);

                iNetRequest.Timeout = 10000;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());
                iNetResponse.Close();

                return true;

            }
            catch (WebException)
            {

                // Console.WriteLine (".....no connection..." + ex.ToString ());

                return false;
            }
        }
        public static string DeviceId
        {
            get
            {
                if (File.Exists(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/uid"))
                {
                    return File.ReadAllText(Android.OS.Environment.ExternalStorageDirectory + "/.gr3cache/uid").Trim();
                }
                else
                {


                    return null;
                }
            }
        }

        public static void ExecuteGarbageCollection()
        {
            try
            {


                if (true)
                {


                    GC.Collect();
                }
            }
            catch (Exception)
            {

            }

        }



       
       
       
       


    }
}