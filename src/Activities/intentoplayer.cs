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
using App1.Utils;

namespace App1
{
    [Activity(Label = "intentoplayer", Theme = "@style/Theme.DesignDemo")]
    public class intentoplayer : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layoutvacio);
            if (Intent.GetStringExtra("command") == "playpause")
            {
                playpause();
            }
            this.Finish();
            // Create your application here
        }
        public void playpause()
        {
            if (SettingsHelper.GetSetting("playerstatus") != "reproduciendo")
            {



                SettingsHelper.SaveSetting("cquerry", "play()");
            }
            else

            {


                SettingsHelper.SaveSetting("cquerry", "pause()");

            }
        }
    }
}