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
   public static class SettingsHelper
    {
        public static bool HasKey(string key)
        {
            try
            {
                string value = GetSetting(key);
                if (value != null && value.Length >= 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetSetting(string key)
        {
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            return prefs.GetString(key, null);
        }
        public static void SaveSetting(string key, string value)
        {

            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Settings", FileCreationMode.Private);
            ISharedPreferencesEditor prefEditor = prefs.Edit();
            prefEditor.PutString(key, value);
            prefEditor.Commit();

        }
    }
}