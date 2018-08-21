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

namespace App1
{
    [Activity(Label = "intentoplayer", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
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
            if (clasesettings.gettearvalor("playerstatus") != "reproduciendo")
            {
             


                clasesettings.guardarsetting("cquerry", "play()");
            }
            else

            {
             

                clasesettings.guardarsetting("cquerry", "pause()");

            }
        }
    }
}