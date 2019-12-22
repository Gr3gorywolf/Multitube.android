using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Utils
{
    class HtmlHelper
    {


        public static string GetNode(string name, string source)
        {
            return WebUtility.HtmlDecode(
                  StringsHelper.StringBetween(
                      '<' + name + '>', "</" + name + '>', source));
        }
        public static IEnumerable<string> GetUrisFromManifest(string source)
        {
            string opening = "<BaseURL>";
            string closing = "</BaseURL>";
            int start = source.IndexOf(opening);
            if (start != -1)
            {
                string temp = source.Substring(start);
                var uris = temp.Split(new string[] { opening }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => v.Substring(0, v.IndexOf(closing)));
                return uris;
            }
            throw new NotSupportedException();
        }
    }
}