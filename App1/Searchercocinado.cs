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
using Android.Webkit;
using System.Net;
using System.Text.RegularExpressions;

namespace App1
{
    class Searchercocinado
    {







        public string devolverurl(string searchquerry) { 
      WebClient  webcliente = new WebClient();
         
        string html = webcliente.DownloadString("https://www.youtube.com/results?search_query=" + searchquerry );
            string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
            MatchCollection result = Regex.Matches(html, pattern, RegexOptions.Singleline);
          string  url = string.Concat("http://www.youtube.com/watch?v=", VideoItemHelper.cull(result[0].Value, "watch?v=", "\""));





            return url;
        }











        public class VideoItemHelper
        {
            public static string cull(string strSource, string strStart, string strEnd)
            {
                int Start, End;

                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);

                    return strSource.Substring(Start, End - Start);
                }
                else
                {
                    return "";
                }
            }
        }
    }
}