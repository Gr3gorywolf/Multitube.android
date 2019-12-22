using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Utils
{
  static  class StringsHelper
    {
        public static string StringBetween(string prefix, string suffix, string parent)
        {
            int start = parent.IndexOf(prefix) + prefix.Length;

            if (start < prefix.Length)
                return string.Empty;

            int end = parent.IndexOf(suffix, start);

            if (end == -1)
                end = parent.Length;

            return parent.Substring(start, end - start);
        }

        public static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
        public static string GenerateSerial()
        {

            Random rValue = new Random();
            char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm' };
            return rValue.Next(1, 9).ToString() + letters[rValue.Next(0, 12)].ToString()
                +
                rValue.Next(1, 9).ToString() + letters[rValue.Next(0, 12)].ToString()
                 +
                rValue.Next(1, 9).ToString() + letters[rValue.Next(0, 12)].ToString()
                 +
                rValue.Next(1, 9).ToString() + letters[rValue.Next(0, 12)].ToString()
                 +
                rValue.Next(1, 9).ToString() + letters[rValue.Next(0, 12)].ToString();
        }
    }
}