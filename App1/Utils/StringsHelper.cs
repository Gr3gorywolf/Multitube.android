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
  static  class StringsHelper
    {

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